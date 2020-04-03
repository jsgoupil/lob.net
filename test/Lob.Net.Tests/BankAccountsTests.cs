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
    public class BankAccountsTests : BaseRequestTests
    {
        public BankAccountsTests()
            : base()
        {

        }

        [Fact]
        public async Task CreateRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Post, "https://api.lob.com/v1/bank_accounts")
                    .WithHeaders("Accept", "application/json")
                    .WithHeaders("Lob-Version", "2020-02-11")
                    .WithHeaders("Authorization", "Basic S2V5Og==")
                    .WithContent("{\"description\":\"My Personal Account\",\"routing_number\":\"322271627\",\"account_number\":\"123456789\",\"account_type\":\"individual\",\"signatory\":\"Jean-Sébastien Goupil\",\"metadata\":{\"met1\":\"v1\",\"met2\":\"v2\"}}")
                    .Respond("application/json", "{\n    \"id\": \"bank_da4daf54431d39d\",\n    \"description\": \"My Personal Account\",\n    \"metadata\": {\n        \"met1\": \"v1\",\n        \"met2\": \"v2\"\n    },\n    \"routing_number\": \"322271627\",\n    \"account_number\": \"123456789\",\n    \"account_type\": \"individual\",\n    \"signatory\": \"Jean-Sébastien Goupil\",\n    \"signature_url\": null,\n    \"bank_name\": \"J.P. MORGAN CHASE BANK, N.A.\",\n    \"verified\": false,\n    \"date_created\": \"2018-10-11T19:39:32.184Z\",\n    \"date_modified\": \"2018-10-11T19:39:32.184Z\",\n    \"object\": \"bank_account\"\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var bankAccounts = serviceProvider.GetService<ILobBankAccounts>();
            var result = await bankAccounts.CreateAsync(new BankAccountRequest
            {
                AccountNumber = "123456789",
                RoutingNumber = "322271627",
                AccountType = AccountType.Individual,
                Description = "My Personal Account",
                Signatory = "Jean-Sébastien Goupil",
                Metadata = new Dictionary<string, string>
                {
                    { "met1", "v1" },
                    { "met2", "v2" }
                }
            });

            Assert.Equal("123456789", result.AccountNumber);
            Assert.Equal(AccountType.Individual, result.AccountType);
            Assert.Equal("J.P. MORGAN CHASE BANK, N.A.", result.BankName);
            Assert.Equal(new DateTime(2018, 10, 11, 19, 39, 32, 184, DateTimeKind.Utc), result.DateCreated);
            Assert.Equal(new DateTime(2018, 10, 11, 19, 39, 32, 184, DateTimeKind.Utc), result.DateModified);
            Assert.False(result.Deleted);
            Assert.Equal("My Personal Account", result.Description);
            Assert.Equal("bank_da4daf54431d39d", result.Id);
            Assert.Equal(2, result.Metadata.Count);
            Assert.Equal("v1", result.Metadata["met1"]);
            Assert.Equal("v2", result.Metadata["met2"]);
            Assert.Equal("bank_account", result.Object);
            Assert.Equal("322271627", result.RoutingNumber);
            Assert.Equal("Jean-Sébastien Goupil", result.Signatory);
            Assert.Null(result.SignatureUrl);
            Assert.False(result.Verified);
        }

        [Fact]
        public async Task CreateRequestWithIdempotency()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Post, "https://api.lob.com/v1/bank_accounts")
                    .WithHeaders("Idempotency-Key", "GUID")
                    .Respond("application/json", "{}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var letters = serviceProvider.GetService<ILobBankAccounts>();
            var result = await letters.CreateAsync(new BankAccountRequest(), "GUID");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task VerifyRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Post, "https://api.lob.com/v1/bank_accounts/bank_da4daf54431d39d/verify")
                    .WithContent("{\"amounts\":[14,31]}")
                    .Respond("application/json", "{\n    \"id\": \"bank_da4daf54431d39d\",\n    \"description\": \"My Personal Account\",\n    \"metadata\": {\n        \"met1\": \"v1\",\n        \"met2\": \"v2\"\n    },\n    \"routing_number\": \"322271627\",\n    \"account_number\": \"123456789\",\n    \"account_type\": \"individual\",\n    \"signatory\": \"Jean-Sébastien Goupil\",\n    \"signature_url\": null,\n    \"bank_name\": \"J.P. MORGAN CHASE BANK, N.A.\",\n    \"verified\": true,\n    \"date_created\": \"2018-10-11T19:39:32.184Z\",\n    \"date_modified\": \"2018-10-11T19:39:32.184Z\",\n    \"object\": \"bank_account\"\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var bankAccounts = serviceProvider.GetService<ILobBankAccounts>();
            var result = await bankAccounts.VerifyAsync("bank_da4daf54431d39d", 14, 31);

            Assert.True(result.Verified);
        }

        [Fact]
        public async Task DeleteRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Delete, "https://api.lob.com/v1/bank_accounts/bank_da4daf54431d39d")
                    .Respond("application/json", "{\n  \"id\": \"bank_da4daf54431d39d\",\n  \"deleted\": true\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var bankAccounts = serviceProvider.GetService<ILobBankAccounts>();
            var result = await bankAccounts.DeleteAsync("bank_da4daf54431d39d");

            Assert.Equal("bank_da4daf54431d39d", result.Id);
            Assert.True(result.Deleted);
        }

        [Fact]
        public async Task RetrieveRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Get, "https://api.lob.com/v1/bank_accounts/bank_da4daf54431d39d")
                    .Respond("application/json", "{\n    \"id\": \"bank_da4daf54431d39d\",\n    \"description\": \"My Personal Account\",\n    \"metadata\": {\n        \"met1\": \"v1\",\n        \"met2\": \"v2\"\n    },\n    \"routing_number\": \"322271627\",\n    \"account_number\": \"123456789\",\n    \"account_type\": \"individual\",\n    \"signatory\": \"Jean-Sébastien Goupil\",\n    \"signature_url\": null,\n    \"bank_name\": \"J.P. MORGAN CHASE BANK, N.A.\",\n    \"verified\": false,\n    \"date_created\": \"2018-10-11T19:39:32.184Z\",\n    \"date_modified\": \"2018-10-11T19:39:32.184Z\",\n    \"object\": \"bank_account\"\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var bankAccounts = serviceProvider.GetService<ILobBankAccounts>();
            var result = await bankAccounts.RetrieveAsync("bank_da4daf54431d39d");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ListRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Get, "https://api.lob.com/v1/bank_accounts")
                    .WithExactQueryString("limit=100&include%5B%5D=total_count&metadata%5Bm1%5D=v1&metadata%5Bm2%5D=v2&date_created=%7B%22gt%22%3A%222015-12-12T00%3A00%3A00.0000000%22%2C%22lt%22%3A%222017-01-01T00%3A00%3A00.0000000%22%7D")
                    .Respond("application/json", "{\n    \"count\": 1,\n    \"data\": [\n        {\n            \"id\": \"bank_da4daf54431d39d\",\n            \"description\": \"My Personal Account\",\n            \"metadata\": {\n                \"met1\": \"v1\",\n                \"met2\": \"v2\"\n            },\n            \"routing_number\": \"322271627\",\n            \"account_number\": \"123456789\",\n            \"account_type\": \"individual\",\n            \"signatory\": \"Jean-Sébastien Goupil\",\n            \"signature_url\": null,\n            \"bank_name\": \"J.P. MORGAN CHASE BANK, N.A.\",\n            \"verified\": false,\n            \"date_created\": \"2018-10-11T19:39:32.184Z\",\n            \"date_modified\": \"2018-10-11T19:39:32.184Z\",\n            \"object\": \"bank_account\"\n        }\n    ],\n    \"next_url\": null,\n    \"object\": \"list\",\n    \"previous_url\": null,\n    \"total_count\": 1\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var bankAccounts = serviceProvider.GetService<ILobBankAccounts>();
            var result = await bankAccounts.ListAsync(new BankAccountFilter
            {
                CreatedAfter = new DateTime(2015, 12, 12),
                CreatedBefore = new DateTime(2017, 1, 1),
                IncludeTotalCount = true,
                Limit = 100,
                Metadata = new Dictionary<string, string>
                {
                    { "m1", "v1" },
                    { "m2", "v2" }
                }
            });

            Assert.Equal(1, result.Count);
            Assert.Equal(1, result.TotalCount);
            Assert.Equal("list", result.Object);
            Assert.Single(result.Data);
            Assert.NotNull(result.Data[0]);
        }

        [Fact]
        public async Task ListObjectsRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Get, "https://api.lob.com/v1/bank_accounts")
                    .WithExactQueryString("limit=100&include%5B%5D=total_count&metadata%5Bm1%5D=v1&metadata%5Bm2%5D=v2&date_created=%7B%22gt%22%3A%222015-12-12T00%3A00%3A00.0000000%22%2C%22lt%22%3A%222017-01-01T00%3A00%3A00.0000000%22%7D")
                    .Respond("application/json", "{\n    \"count\": 1,\n    \"data\": [\n        {\n            \"id\": \"bank_da4daf54431d39d\",\n            \"description\": \"My Personal Account\",\n            \"metadata\": {\n                \"met1\": \"v1\",\n                \"met2\": \"v2\"\n            },\n            \"routing_number\": \"322271627\",\n            \"account_number\": \"123456789\",\n            \"account_type\": \"individual\",\n            \"signatory\": \"Jean-Sébastien Goupil\",\n            \"signature_url\": null,\n            \"bank_name\": \"J.P. MORGAN CHASE BANK, N.A.\",\n            \"verified\": false,\n            \"date_created\": \"2018-10-11T19:39:32.184Z\",\n            \"date_modified\": \"2018-10-11T19:39:32.184Z\",\n            \"object\": \"bank_account\"\n        }\n    ],\n    \"next_url\": null,\n    \"object\": \"list\",\n    \"previous_url\": null,\n    \"total_count\": 1\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var bankAccounts = serviceProvider.GetService<ILobBankAccounts>();
            var enumerable = bankAccounts.ListObjectsAsync(new BankAccountFilter
            {
                CreatedAfter = new DateTime(2015, 12, 12),
                CreatedBefore = new DateTime(2017, 1, 1),
                IncludeTotalCount = true,
                Limit = 100,
                Metadata = new Dictionary<string, string>
                {
                    { "m1", "v1" },
                    { "m2", "v2" }
                }
            });
            var list = new List<BankAccountResponse>();
            await foreach (var data in enumerable)
            {
                list.Add(data);
            }

            Assert.Single(list);
            Assert.NotNull(list[0]);
        }
    }
}
