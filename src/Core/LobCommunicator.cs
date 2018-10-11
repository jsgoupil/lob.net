using Lob.Net.Exceptions;
using Lob.Net.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Lob.Net
{
    internal class LobCommunicator : ILobCommunicator
    {
        protected readonly DefaultContractResolver contractResolver;
        protected readonly JsonSerializerSettings serializerSettings;
        protected readonly HttpClient client;

        public LobCommunicator(
            IOptions<LobOptions> lobOptions,
            IHttpClientFactory httpClientFactory
        )
        {
            var apiKey = lobOptions.Value.ApiKey;
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("The API Key must be provided.");
            }

            client = httpClientFactory.CreateClient(CoreValues.HTTP_CLIENT_NAME);
            client.BaseAddress = new Uri(CoreValues.SERVER_URL, UriKind.Absolute);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Lob.Net", CoreValues.VERSION));
            client.DefaultRequestHeaders.Add("Lob-Version", CoreValues.VERSION);

            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{apiKey}:"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        async Task<T> ILobCommunicator.GetAsync<T>(string url)
        {
            var response = await client.GetAsync(url);
            return await ProcessResponseAsync<T>(response);
        }

        async Task<T> ILobCommunicator.DeleteAsync<T>(string url)
        {
            var response = await client.DeleteAsync(url);
            return await ProcessResponseAsync<T>(response);
        }

        async Task<T> ILobCommunicator.PostAsync<T>(string url, object body, string idempotencyKey)
        {
            var obj = JsonConvert.SerializeObject(body, Formatting.None, serializerSettings);
            var content = new StringContent(obj, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(idempotencyKey))
            {
                content.Headers.TryAddWithoutValidation("Idempotency-Key", idempotencyKey);
            }

            var response = await client.PostAsync(url, content);
            return await ProcessResponseAsync<T>(response);
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
    }
}
