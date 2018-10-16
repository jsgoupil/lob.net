using Lob.Net.Models;
using System.Threading.Tasks;

namespace Lob.Net
{
    internal class LobUsVerifications : ILobUsVerifications
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

        public Task<UsVerificationResponse> Verify(UsVerificationRequest request, UsVerificationCase @case = UsVerificationCase.Upper)
        {
            var url = $"{URL_VERIFICATIONS}?case={@case.ToString().ToLower()}";
            return lobCommunicator.PostAsync<UsVerificationResponse>(url, request);
        }

        public Task<UsAutocompletionResponse> Autocomplete(UsAutocompletionRequest request, string ipAddress = null)
        {
            if (string.IsNullOrEmpty(ipAddress))
            {
                request.GeoIpSort = false;
                return lobCommunicator.PostAsync<UsAutocompletionResponse>(URL_AUTOCOMPLETIONS, request);
            }

            request.GeoIpSort = true;
            var extraHeader = (Name: "X-Forwarded-For", ipAddress);
            return lobCommunicator.PostAsync<UsAutocompletionResponse>(URL_AUTOCOMPLETIONS, request, extraHeader);
        }

        public Task<UsZipLookupResponse> ZipLookup(string zipCode)
        {
            return ZipLookup(new UsZipLookupRequest
            {
                ZipCode = zipCode
            });
        }

        public Task<UsZipLookupResponse> ZipLookup(UsZipLookupRequest request)
        {
            return lobCommunicator.PostAsync<UsZipLookupResponse>(URL_ZIP_LOOKUPS, request);
        }
    }
}
