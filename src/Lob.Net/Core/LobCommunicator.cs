using Lob.Net.Exceptions;
using Lob.Net.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lob.Net
{
    public class LobCommunicator : ILobCommunicator
    {
        protected readonly LobSerializerSettings serializerSettings;
        protected readonly HttpClient client;

        public LobCommunicator(
            IOptions<LobOptions> lobOptions,
            IHttpClientFactory httpClientFactory,
            LobSerializerSettings serializerSettings
        )
        {
            var apiKey = lobOptions.Value.ApiKey;
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("The API Key must be provided.");
            }

            client = httpClientFactory.CreateClient(CoreValues.HTTP_CLIENT_NAME);
            client.BaseAddress = new Uri(lobOptions.Value.ServerUrl, UriKind.Absolute);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Lob.Net", lobOptions.Value.Version));
            client.DefaultRequestHeaders.Add("Lob-Version", lobOptions.Value.Version);

            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{apiKey}:"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            this.serializerSettings = serializerSettings;
        }

        public async Task<T> GetAsync<T>(string url, CancellationToken cancellationToken = default)
        {
            var response = await client.GetAsync(url, cancellationToken);
            return await ProcessResponseAsync<T>(response);
        }

        public async Task<T> DeleteAsync<T>(string url, CancellationToken cancellationToken = default)
        {
            var response = await client.DeleteAsync(url, cancellationToken);
            return await ProcessResponseAsync<T>(response);
        }

        public Task<T> PostAsync<T>(string url, object body, CancellationToken cancellationToken = default)
        {
            return PostAsync<T>(url, body, (IDictionary<string, string>)null, cancellationToken);
        }

        public Task<T> PostAsync<T>(string url, object body, string idempotencyKey, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(idempotencyKey))
            {
                return PostAsync<T>(url, body, (IDictionary<string, string>)null, cancellationToken);
            }

            return PostAsync<T>(url, body, new Dictionary<string, string> { { "Idempotency-Key", idempotencyKey } }, cancellationToken);
        }

        public Task<T> PostAsync<T>(string url, object body, IDictionary<string, string> extraHeaders, CancellationToken cancellationToken = default)
        {
            return InternalPostAsync<T>(url, body, extraHeaders, cancellationToken);
        }

        private async Task<T> ProcessResponseAsync<T>(HttpResponseMessage response)
        {
            var textResult = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(textResult, serializerSettings);
                switch ((int)response.StatusCode)
                {
                    case (int)System.Net.HttpStatusCode.Unauthorized:
                        throw new UnauthorizedException(error);
                    case (int)System.Net.HttpStatusCode.Forbidden:
                        throw new ForbiddenException(error);
                    case (int)System.Net.HttpStatusCode.NotFound:
                        throw new NotFoundException(error);
                    case (int)System.Net.HttpStatusCode.BadRequest:
                    case 422:
                        throw new BadRequestException(error);
                    case 429:
                        throw new TooManyRequestsException(error);
                    case (int)System.Net.HttpStatusCode.InternalServerError:
                        throw new ServerErrorException(error);
                }

                throw new Exception("An unexpected error occurred.");
            }

            return JsonConvert.DeserializeObject<T>(textResult, serializerSettings);
        }

        private async Task<T> InternalPostAsync<T>(string url, object body, IDictionary<string, string> extraHeaders = null, CancellationToken cancellationToken = default)
        {
            var obj = JsonConvert.SerializeObject(body, Formatting.None, serializerSettings);
            var content = new StringContent(obj, Encoding.UTF8, "application/json");

            if (extraHeaders != null)
            {
                foreach (var kvp in extraHeaders)
                {
                    content.Headers.TryAddWithoutValidation(kvp.Key, kvp.Value);
                }
            }

            var response = await client.PostAsync(url, content);
            return await ProcessResponseAsync<T>(response);
        }
    }
}
