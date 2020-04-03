using Lob.Net.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lob.Net
{
    public class LobUsVerifications : ILobUsVerifications
    {
        private const string URL_VERIFICATIONS = "/v1/us_verifications";
        private const string URL_AUTOCOMPLETIONS = "/v1/us_autocompletions";
        private const string URL_ZIP_LOOKUPS = "/v1/us_zip_lookups";
        protected ILobCommunicator lobCommunicator;

        public LobUsVerifications(
            ILobCommunicator lobCommunicator
        )
        {
            this.lobCommunicator = lobCommunicator;
        }

        public Task<UsVerificationResponse> VerifyAsync(UsVerificationRequest request, UsVerificationCase @case = UsVerificationCase.Upper, CancellationToken cancellationToken = default)
        {
            var url = $"{URL_VERIFICATIONS}?case={@case.ToString().ToLower()}";
            return lobCommunicator.PostAsync<UsVerificationResponse>(url, request, cancellationToken);
        }

        public Task<UsAutocompletionResponse> AutocompleteAsync(UsAutocompletionRequest request, string ipAddress = default, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(ipAddress))
            {
                request.GeoIpSort = false;
                return lobCommunicator.PostAsync<UsAutocompletionResponse>(URL_AUTOCOMPLETIONS, request, cancellationToken);
            }

            request.GeoIpSort = true;
            var extraHeader = new Dictionary<string, string>
            {
                { "X-Forwarded-For", ipAddress }
            };
            return lobCommunicator.PostAsync<UsAutocompletionResponse>(URL_AUTOCOMPLETIONS, request, extraHeader);
        }

        public Task<UsZipLookupResponse> ZipLookupAsync(string zipCode, CancellationToken cancellationToken = default)
        {
            return ZipLookupAsync(new UsZipLookupRequest
            {
                ZipCode = zipCode
            }, cancellationToken);
        }

        public Task<UsZipLookupResponse> ZipLookupAsync(UsZipLookupRequest request, CancellationToken cancellationToken = default)
        {
            return lobCommunicator.PostAsync<UsZipLookupResponse>(URL_ZIP_LOOKUPS, request, cancellationToken);
        }
    }
}
