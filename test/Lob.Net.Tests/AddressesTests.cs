using Lob.Net.Models;
using Microsoft.Extensions.DependencyInjection;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Lob.Net.Tests
{
    public class AddressesTests : BaseRequestTests
    {
        public AddressesTests()
            : base()
        {

        }

        [Fact]
        public async Task CreateRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Post, "https://api.lob.com/v1/addresses")
                    .WithHeaders("Accept", "application/json")
                    .WithHeaders("Lob-Version", "2018-06-05")
                    .WithHeaders("Authorization", "Basic S2V5Og==")
                    .WithContent("{\"description\":\"MyDescription\",\"name\":\"MyName\",\"company\":\"MyCompany\",\"phone\":\"2065551234\",\"email\":\"contact@jsgoupil.com\",\"address_line1\":\"addr1\",\"address_line2\":\"addr2\",\"address_city\":\"city1\",\"address_state\":\"WA\",\"address_zip\":\"98103\",\"address_country\":\"US\",\"metadata\":{\"m1\":\"v1\",\"m2\":\"v2\"}}")
                    .Respond("application/json", "{\n    \"id\": \"adr_b8cf174eda20c810\",\n    \"description\": \"MyDescription\",\n    \"name\": \"MYNAME\",\n    \"company\": \"MYCOMPANY\",\n    \"phone\": \"2065551234\",\n    \"email\": \"contact@jsgoupil.com\",\n    \"address_line1\": \"ADDR1\",\n    \"address_line2\": \"ADDR2\",\n    \"address_city\": \"CITY1\",\n    \"address_state\": \"WA\",\n    \"address_zip\": \"98103\",\n    \"address_country\": \"UNITED STATES\",\n    \"metadata\": {\n        \"m1\": \"v1\",\n        \"m2\": \"v2\"\n    },\n    \"date_created\": \"2018-10-16T01:26:50.323Z\",\n    \"date_modified\": \"2018-10-16T01:26:50.323Z\",\n    \"object\": \"address\"\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var addresses = serviceProvider.GetService<ILobAddresses>();
            var result = await addresses.CreateAsync(new AddressRequest
            {
                AddressLine1 = "addr1",
                AddressLine2 = "addr2",
                AddressCity = "city1",
                AddressState = "WA",
                AddressCountry = "US",
                AddressZip = "98103",
                Company = "MyCompany",
                Email = "contact@jsgoupil.com",
                Description = "MyDescription",
                Metadata = new Dictionary<string, string>
                    {
                        { "m1", "v1" },
                        { "m2", "v2" }
                    },
                Name = "MyName",
                Phone = "2065551234"
            });

            Assert.Equal("adr_b8cf174eda20c810", result.Id);
            Assert.Equal("MyDescription", result.Description);
            Assert.Equal("MYNAME", result.Name);
            Assert.Equal("MYCOMPANY", result.Company);
            Assert.Equal("2065551234", result.Phone);
            Assert.Equal("contact@jsgoupil.com", result.Email);
            Assert.Equal("ADDR1", result.AddressLine1);
            Assert.Equal("ADDR2", result.AddressLine2);
            Assert.Equal("CITY1", result.AddressCity);
            Assert.Equal("WA", result.AddressState);
            Assert.Equal("98103", result.AddressZip);
            Assert.Equal("UNITED STATES", result.AddressCountry);
            Assert.Equal(2, result.Metadata.Count);
            Assert.Equal("v1", result.Metadata["m1"]);
            Assert.Equal("v2", result.Metadata["m2"]);
            Assert.Equal(new DateTime(2018, 10, 16, 1, 26, 50, 323, DateTimeKind.Utc), result.DateCreated.Value);
            Assert.Equal(new DateTime(2018, 10, 16, 1, 26, 50, 323, DateTimeKind.Utc), result.DateModified.Value);
            Assert.False(result.Deleted);
            Assert.Equal("address", result.Object);
        }

        [Fact]
        public async Task CreateRequestWithIdempotency()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Post, "https://api.lob.com/v1/addresses")
                    .WithHeaders("Idempotency-Key", "GUID")
                    .Respond("application/json", "{}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var addresses = serviceProvider.GetService<ILobAddresses>();
            var result = await addresses.CreateAsync(new AddressRequest(), "GUID");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Delete, "https://api.lob.com/v1/addresses/adr_b8cf174eda20c810")
                    .Respond("application/json", "{\n  \"id\": \"adr_b8cf174eda20c810\",\n  \"deleted\": true\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var addresses = serviceProvider.GetService<ILobAddresses>();
            var result = await addresses.DeleteAsync("adr_b8cf174eda20c810");

            Assert.Equal("adr_b8cf174eda20c810", result.Id);
            Assert.True(result.Deleted);
        }

        [Fact]
        public async Task RetrieveRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Get, "https://api.lob.com/v1/addresses/adr_b8cf174eda20c810")
                    .Respond("application/json", "{\n    \"id\": \"adr_b8cf174eda20c810\",\n    \"description\": \"MyDescription\",\n    \"name\": \"MYNAME\",\n    \"company\": \"MYCOMPANY\",\n    \"phone\": \"2065551234\",\n    \"email\": \"contact@jsgoupil.com\",\n    \"address_line1\": \"ADDR1\",\n    \"address_line2\": \"ADDR2\",\n    \"address_city\": \"CITY1\",\n    \"address_state\": \"WA\",\n    \"address_zip\": \"98103\",\n    \"address_country\": \"UNITED STATES\",\n    \"metadata\": {\n        \"m1\": \"v1\",\n        \"m2\": \"v2\"\n    },\n    \"date_created\": \"2018-10-16T01:26:50.323Z\",\n    \"date_modified\": \"2018-10-16T01:26:50.323Z\",\n    \"object\": \"address\"\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var addresses = serviceProvider.GetService<ILobAddresses>();
            var result = await addresses.RetrieveAsync("adr_b8cf174eda20c810");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ListRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Get, "https://api.lob.com/v1/addresses")
                    .WithExactQueryString("offset=0&limit=100&include%5B%5D=total_count&metadata%5Bm1%5D=v1&metadata%5Bm2%5D=v2&date_created=%7B%22gt%22%3A%222015-12-12T00%3A00%3A00.0000000%22%2C%22lt%22%3A%222017-01-01T00%3A00%3A00.0000000%22%7D")
                    .Respond("application/json", "{\n    \"data\": [\n        {\n            \"id\": \"adr_b8cf174eda20c810\",\n            \"description\": \"MyDescription\",\n            \"name\": \"MYNAME\",\n            \"company\": \"MYCOMPANY\",\n            \"phone\": \"2065551234\",\n            \"email\": \"contact@jsgoupil.com\",\n            \"address_line1\": \"ADDR1\",\n            \"address_line2\": \"ADDR2\",\n            \"address_city\": \"CITY1\",\n            \"address_state\": \"WA\",\n            \"address_zip\": \"98103\",\n            \"address_country\": \"UNITED STATES\",\n            \"metadata\": {\n                \"m1\": \"v1\",\n                \"m2\": \"v2\"\n            },\n            \"date_created\": \"2018-10-16T01:26:50.323Z\",\n            \"date_modified\": \"2018-10-16T01:26:50.323Z\",\n            \"object\": \"address\"\n        }\n    ],\n    \"count\": 1,\n    \"object\": \"list\"\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var addresses = serviceProvider.GetService<ILobAddresses>();
            var result = await addresses.ListAsync(new AddressFilter
            {
                CreatedAfter = new DateTime(2015, 12, 12),
                CreatedBefore = new DateTime(2017, 1, 1),
                IncludeTotalCount = true,
                Limit = 100,
                Offset = 0,
                Metadata = new Dictionary<string, string>
                {
                    { "m1", "v1" },
                    { "m2", "v2" }
                },
            });

            Assert.Equal(1, result.Count);
            Assert.Equal("list", result.Object);
            Assert.Single(result.Data);
            Assert.NotNull(result.Data[0]);
        }
    }
}
