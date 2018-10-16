using Lob.Net.Models;
using Microsoft.Extensions.DependencyInjection;
using RichardSzalay.MockHttp;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Lob.Net.Tests
{
    public class UsVerificationsTests : BaseRequestTests
    {
        public UsVerificationsTests()
            : base()
        {
        }

        [Fact]
        public async Task UsVerifications()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Post, "https://api.lob.com/v1/us_verifications?case=upper")
                    .WithHeaders("Accept", "application/json")
                    .WithHeaders("Lob-Version", "2018-06-05")
                    .WithHeaders("Authorization", "Basic S2V5Og==")
                    .WithContent("{\"primary_line\":\"1709 Broderick St\",\"city\":\"San Francisco\",\"state\":\"CA\"}")
                    .Respond("application/json", "{\n    \"id\": \"us_ver_41f2fa3a2e0644cc9428\",\n    \"recipient\": \"TEST KEYS DO NOT VERIFY ADDRESSES\",\n    \"primary_line\": \"1709 BRODERICK ST\",\n    \"secondary_line\": \"\",\n    \"urbanization\": \"\",\n    \"last_line\": \"SAN FRANCISCO CA 94115-2525\",\n    \"deliverability\": \"deliverable\",\n    \"components\": {\n        \"primary_number\": \"1709\",\n        \"street_predirection\": \"\",\n        \"street_name\": \"BRODERICK\",\n        \"street_suffix\": \"ST\",\n        \"street_postdirection\": \"\",\n        \"secondary_designator\": \"\",\n        \"secondary_number\": \"\",\n        \"pmb_designator\": \"\",\n        \"pmb_number\": \"\",\n        \"extra_secondary_designator\": \"\",\n        \"extra_secondary_number\": \"\",\n        \"city\": \"SAN FRANCISCO\",\n        \"state\": \"CA\",\n        \"zip_code\": \"94115\",\n        \"zip_code_plus_4\": \"2525\",\n        \"zip_code_type\": \"standard\",\n        \"delivery_point_barcode\": \"941152525097\",\n        \"address_type\": \"residential\",\n        \"record_type\": \"street\",\n        \"default_building_address\": false,\n        \"county\": \"SAN FRANCISCO\",\n        \"county_fips\": \"06075\",\n        \"carrier_route\": \"C021\",\n        \"carrier_route_type\": \"city_delivery\",\n        \"latitude\": 37.786029686706215,\n        \"longitude\": -122.4418536183508\n    },\n    \"deliverability_analysis\": {\n        \"dpv_confirmation\": \"Y\",\n        \"dpv_cmra\": \"N\",\n        \"dpv_vacant\": \"N\",\n        \"dpv_footnotes\": [\n            \"AA\",\n            \"BB\"\n        ],\n        \"ews_match\": false,\n        \"lacs_indicator\": \"\",\n        \"lacs_return_code\": \"\",\n        \"suite_return_code\": \"\"\n    },\n    \"object\": \"us_verification\"\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var usVerifications = serviceProvider.GetService<ILobUsVerifications>();
            var result = await usVerifications.Verify(new UsVerificationRequest("1709 Broderick St", "San Francisco", state: "CA"));

            Assert.Equal(AddressType.Residential, result.Components.AddressType);
            Assert.Equal("C021", result.Components.CarrierRoute);
            Assert.Equal(CarrierRouteType.CityDelivery, result.Components.CarrierRouteType);
            Assert.Equal("SAN FRANCISCO", result.Components.City);
            Assert.Equal("SAN FRANCISCO", result.Components.County);
            Assert.Equal("06075", result.Components.CountyFips);
            Assert.False(result.Components.DefaultBuildingAddress);
            Assert.Equal("941152525097", result.Components.DeliveryPointBarcode);
            Assert.Empty(result.Components.ExtraSecondaryDesignator);
            Assert.Empty(result.Components.ExtraSecondaryNumber);
            Assert.Equal(37.786029686706215m, result.Components.Latitude);
            Assert.Equal(-122.4418536183508m, result.Components.Longitude);
            Assert.Empty(result.Components.PmbDesignator);
            Assert.Empty(result.Components.PmbNumber);
            Assert.Equal("1709", result.Components.PrimaryNumber);
            Assert.Equal(RecordType.Street, result.Components.RecordType);
            Assert.Empty(result.Components.SecondaryDesignator);
            Assert.Empty(result.Components.SecondaryNumber);
            Assert.Equal("CA", result.Components.State);
            Assert.Equal("BRODERICK", result.Components.StreetName);
            Assert.Null(result.Components.StreetPostdirection);
            Assert.Null(result.Components.StreetPredirection);
            Assert.Equal("ST", result.Components.StreetSuffix);
            Assert.Equal("94115", result.Components.ZipCode);
            Assert.Equal("2525", result.Components.ZipCodePlus4);
            Assert.Equal(ZipCodeType.Standard, result.Components.ZipCodeType);
            Assert.Equal(UsDeliverability.Deliverable, result.Deliverability);
            Assert.Equal(BooleanState.N, result.DeliverabilityAnalysis.DpvCmra);
            Assert.Equal(DpvConfirmation.Y, result.DeliverabilityAnalysis.DpvConfirmation);
            Assert.Equal(2, result.DeliverabilityAnalysis.DpvFootnotes.Length);
            Assert.Equal(DpvFootNote.AA, result.DeliverabilityAnalysis.DpvFootnotes[0]);
            Assert.Equal(DpvFootNote.BB, result.DeliverabilityAnalysis.DpvFootnotes[1]);
            Assert.Equal(BooleanState.N, result.DeliverabilityAnalysis.DpvVacant);
            Assert.False(result.DeliverabilityAnalysis.EwsMatch);
            Assert.Null(result.DeliverabilityAnalysis.LacsIndicator);
            Assert.Empty(result.DeliverabilityAnalysis.LacsReturnCode);
            Assert.Empty(result.DeliverabilityAnalysis.SuiteReturnCode);
            Assert.Equal("us_ver_41f2fa3a2e0644cc9428", result.Id);
            Assert.Equal("SAN FRANCISCO CA 94115-2525", result.LastLine);
            Assert.Equal("us_verification", result.Object);
            Assert.Equal("1709 BRODERICK ST", result.PrimaryLine);
            Assert.Equal("TEST KEYS DO NOT VERIFY ADDRESSES", result.Recipient);
            Assert.Empty(result.SecondaryLine);
            Assert.Empty(result.Urbanization);
        }

        [Fact]
        public async Task UsAutocompletions()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Post, "https://api.lob.com/v1/us_autocompletions")
                    .WithHeaders("Accept", "application/json")
                    .WithHeaders("Lob-Version", "2018-06-05")
                    .WithHeaders("Authorization", "Basic S2V5Og==")
                    .WithHeaders("X-Forwarded-For", "127.0.0.1")
                    .WithContent("{\"address_prefix\":\"5 su\",\"city\":\"San Francisco\",\"state\":\"CA\",\"geo_ip_sort\":true}")
                    .Respond("application/json", "{\n    \"id\": \"us_auto_f39dbcff4bd24c5891de\",\n    \"suggestions\": [\n        {\n            \"primary_line\": \"5 TELEGRAPH HILL BLVD\",\n            \"city\": \"SAN FRANCISCO\",\n            \"state\": \"CA\",\n            \"zip_code\": \"94133\"\n        }\n    ],\n    \"object\": \"us_autocompletion\"\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var usVerifications = serviceProvider.GetService<ILobUsVerifications>();
            var result = await usVerifications.Autocomplete(new UsAutocompletionRequest
            {
                AddressPrefix = "5 su",
                City = "San Francisco",
                GeoIpSort = true,
                State = "CA"
            }, "127.0.0.1");

            Assert.Equal("us_auto_f39dbcff4bd24c5891de", result.Id);
            Assert.Equal("us_autocompletion", result.Object);
            Assert.Single(result.Suggestions);
            Assert.Equal("SAN FRANCISCO", result.Suggestions[0].City);
            Assert.Equal("5 TELEGRAPH HILL BLVD", result.Suggestions[0].PrimaryLine);
            Assert.Equal("CA", result.Suggestions[0].State);
            Assert.Equal("94133", result.Suggestions[0].ZipCode);
        }

        [Fact]
        public async Task UsZipLookups()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Post, "https://api.lob.com/v1/us_zip_lookups")
                    .WithHeaders("Accept", "application/json")
                    .WithHeaders("Lob-Version", "2018-06-05")
                    .WithHeaders("Authorization", "Basic S2V5Og==")
                    .WithContent("{\"zip_code\":\"94133\"}")
                    .Respond("application/json", "{\n    \"id\": \"us_zip_c7cb63d68f8d6\",\n    \"zip_code\": \"94107\",\n    \"zip_code_type\": \"standard\",\n    \"cities\": [\n        {\n            \"city\": \"SAN FRANCISCO\",\n            \"state\": \"CA\",\n            \"county\": \"SAN FRANCISCO\",\n            \"county_fips\": \"06075\",\n            \"preferred\": true\n        }\n    ],\n    \"object\": \"us_zip_lookup\"\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var usVerifications = serviceProvider.GetService<ILobUsVerifications>();
            var result = await usVerifications.ZipLookup("94133");

            Assert.Single(result.Cities);
            Assert.Equal("SAN FRANCISCO", result.Cities[0].City);
            Assert.Equal("SAN FRANCISCO", result.Cities[0].County);
            Assert.Equal("06075", result.Cities[0].CountyFips);
            Assert.True(result.Cities[0].Preferred);
            Assert.Equal("CA", result.Cities[0].State);
            Assert.Equal("us_zip_c7cb63d68f8d6", result.Id);
            Assert.Equal("us_zip_lookup", result.Object);
            Assert.Equal("94107", result.ZipCode);
            Assert.Equal(ZipCodeType.Standard, result.ZipCodeType);
        }
    }
}
