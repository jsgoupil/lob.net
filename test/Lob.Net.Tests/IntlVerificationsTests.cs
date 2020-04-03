using Lob.Net.Models;
using Microsoft.Extensions.DependencyInjection;
using RichardSzalay.MockHttp;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Lob.Net.Tests
{
    public class IntlVerificationsTests : BaseRequestTests
    {
        public IntlVerificationsTests()
            : base()
        {
        }

        [Fact]
        public async Task IntlVerifications()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Post, "https://api.lob.com/v1/intl_verifications")
                    .WithHeaders("Accept", "application/json")
                    .WithHeaders("Lob-Version", "2020-02-11")
                    .WithHeaders("Authorization", "Basic S2V5Og==")
                    .WithContent("{\"primary_line\":\"370 Water St\",\"city\":\"Summerside\",\"postal_code\":\"C1N 1C4\",\"country\":\"CA\"}")
                    .Respond("application/json", "{\n  \"id\": \"intl_ver_c7cb63d68f8d6\",\n  \"recipient\": null,\n  \"primary_line\": \"370 WATER ST\",\n  \"secondary_line\": \"\",\n  \"last_line\": \"SUMMERSIDE PE C1N 1C4\",\n  \"country\": \"CA\",\n  \"deliverability\": \"deliverable\",\n  \"components\": {\n    \"primary_number\": \"370\",\n    \"street_name\": \"WATER ST\",\n    \"city\": \"SUMMERSIDE\",\n    \"state\": \"PE\",\n    \"postal_code\": \"C1N 1C4\"\n  },\n  \"object\": \"intl_verification\"\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var intlVerifications = serviceProvider.GetService<ILobIntlVerifications>();
            var result = await intlVerifications.VerifyAsync(new IntlVerificationRequest
            {
                City = "Summerside",
                Country = "CA",
                PrimaryLine = "370 Water St",
                PostalCode = "C1N 1C4"
            });

            Assert.Equal("SUMMERSIDE", result.Components.City);
            Assert.Equal("C1N 1C4", result.Components.PostalCode);
            Assert.Equal("370", result.Components.PrimaryNumber);
            Assert.Equal("PE", result.Components.State);
            Assert.Equal("WATER ST", result.Components.StreetName);
            Assert.Equal("CA", result.Country);
            Assert.Equal(IntlDeliverability.Deliverable, result.Deliverability);
            Assert.Equal("intl_ver_c7cb63d68f8d6", result.Id);
            Assert.Equal("SUMMERSIDE PE C1N 1C4", result.LastLine);
            Assert.Equal("intl_verification", result.Object);
            Assert.Equal("370 WATER ST", result.PrimaryLine);
            Assert.Null(result.Recipient);
            Assert.Empty(result.SecondaryLine);
        }
    }
}
