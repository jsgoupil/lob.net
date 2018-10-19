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
    public class TemplatesTests : BaseRequestTests
    {
        public TemplatesTests()
            : base()
        {

        }

        #region Templates
        [Fact]
        public async Task CreateRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Post, "https://api.lob.com/v1/templates")
                    .WithHeaders("Accept", "application/json")
                    .WithHeaders("Lob-Version", "2018-06-05")
                    .WithHeaders("Authorization", "Basic S2V5Og==")
                    .WithContent("{\"description\":\"My Description\",\"html\":\"<html>hello\",\"metadata\":{\"met1\":\"v1\",\"met2\":\"v2\"}}")
                    .Respond("application/json", "{\n    \"id\": \"tmpl_ffa2eff0666dd83\",\n    \"description\": \"My Description\",\n    \"versions\": [\n        {\n            \"id\": \"vrsn_de41850f9642eb1\",\n            \"description\": \"My Description\",\n            \"html\": \"<html>hello\",\n            \"date_created\": \"2018-10-19T18:43:32.396Z\",\n            \"date_modified\": \"2018-10-19T18:43:32.396Z\",\n            \"object\": \"version\"\n        }\n    ],\n    \"published_version\": {\n        \"id\": \"vrsn_de41850f9642eb1\",\n        \"description\": \"My Description\",\n        \"html\": \"<html>hello\",\n        \"date_created\": \"2018-10-19T18:43:32.396Z\",\n        \"date_modified\": \"2018-10-19T18:43:32.396Z\",\n        \"object\": \"version\"\n    },\n    \"metadata\": {\n        \"met1\": \"v1\",\n        \"met2\": \"v2\"\n    },\n    \"date_created\": \"2018-10-19T18:43:32.396Z\",\n    \"date_modified\": \"2018-10-19T18:43:32.396Z\",\n    \"object\": \"template\"\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var templates = serviceProvider.GetService<ILobTemplates>();
            var result = await templates.CreateAsync(new TemplateRequest
            {
                Description = "My Description",
                Html = "<html>hello",
                Metadata = new Dictionary<string, string>
                {
                    { "met1", "v1" },
                    { "met2", "v2" }
                }
            });
            
            Assert.Equal(new DateTime(2018, 10, 19, 18, 43, 32, 396, DateTimeKind.Utc), result.DateCreated);
            Assert.Equal(new DateTime(2018, 10, 19, 18, 43, 32, 396, DateTimeKind.Utc), result.DateModified);
            Assert.False(result.Deleted);
            Assert.Equal("My Description", result.Description);
            Assert.Equal("tmpl_ffa2eff0666dd83", result.Id);
            Assert.Equal(2, result.Metadata.Count);
            Assert.Equal("v1", result.Metadata["met1"]);
            Assert.Equal("v2", result.Metadata["met2"]);
            Assert.Equal("template", result.Object);
            Assert.Equal(new DateTime(2018, 10, 19, 18, 43, 32, 396, DateTimeKind.Utc), result.PublishedVersion.DateCreated);
            Assert.Equal(new DateTime(2018, 10, 19, 18, 43, 32, 396, DateTimeKind.Utc), result.PublishedVersion.DateModified);
            Assert.False(result.PublishedVersion.Deleted);
            Assert.Equal("My Description", result.PublishedVersion.Description);
            Assert.Equal("<html>hello", result.PublishedVersion.Html);
            Assert.Equal("vrsn_de41850f9642eb1", result.PublishedVersion.Id);
            Assert.Equal("version", result.PublishedVersion.Object);
            Assert.Single(result.Versions);
            Assert.Equal(new DateTime(2018, 10, 19, 18, 43, 32, 396, DateTimeKind.Utc), result.Versions[0].DateCreated);
            Assert.Equal(new DateTime(2018, 10, 19, 18, 43, 32, 396, DateTimeKind.Utc), result.Versions[0].DateModified);
            Assert.False(result.Versions[0].Deleted);
            Assert.Equal("My Description", result.Versions[0].Description);
            Assert.Equal("<html>hello", result.Versions[0].Html);
            Assert.Equal("vrsn_de41850f9642eb1", result.Versions[0].Id);
            Assert.Equal("version", result.Versions[0].Object);
        }

        [Fact]
        public async Task CreateRequestWithIdempotency()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Post, "https://api.lob.com/v1/templates")
                    .WithHeaders("Idempotency-Key", "GUID")
                    .Respond("application/json", "{}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var letters = serviceProvider.GetService<ILobTemplates>();
            var result = await letters.CreateAsync(new TemplateRequest(), "GUID");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Delete, "https://api.lob.com/v1/templates/tmpl_ffa2eff0666dd83")
                    .Respond("application/json", "{\n  \"id\": \"tmpl_ffa2eff0666dd83\",\n  \"deleted\": true\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var templates = serviceProvider.GetService<ILobTemplates>();
            var result = await templates.DeleteAsync("tmpl_ffa2eff0666dd83");

            Assert.Equal("tmpl_ffa2eff0666dd83", result.Id);
            Assert.True(result.Deleted);
        }

        [Fact]
        public async Task RetrieveRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Get, "https://api.lob.com/v1/templates/tmpl_ffa2eff0666dd83")
                    .Respond("application/json", "{\n    \"id\": \"tmpl_ffa2eff0666dd83\",\n    \"description\": \"My Description\",\n    \"versions\": [\n        {\n            \"id\": \"vrsn_de41850f9642eb1\",\n            \"description\": \"My Description\",\n            \"html\": \"<html>hello\",\n            \"date_created\": \"2018-10-19T18:43:32.396Z\",\n            \"date_modified\": \"2018-10-19T18:43:32.396Z\",\n            \"object\": \"version\"\n        }\n    ],\n    \"published_version\": {\n        \"id\": \"vrsn_de41850f9642eb1\",\n        \"description\": \"My Description\",\n        \"html\": \"<html>hello\",\n        \"date_created\": \"2018-10-19T18:43:32.396Z\",\n        \"date_modified\": \"2018-10-19T18:43:32.396Z\",\n        \"object\": \"version\"\n    },\n    \"metadata\": {\n        \"met1\": \"v1\",\n        \"met2\": \"v2\"\n    },\n    \"date_created\": \"2018-10-19T18:43:32.396Z\",\n    \"date_modified\": \"2018-10-19T18:43:32.396Z\",\n    \"object\": \"template\"\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var templates = serviceProvider.GetService<ILobTemplates>();
            var result = await templates.RetrieveAsync("tmpl_ffa2eff0666dd83");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Post, "https://api.lob.com/v1/templates/tmpl_ffa2eff0666dd83")
                    .WithContent("{\"description\":\"New Description\",\"published_version\":\"vrsn_de41850f9642eb1\"}")
                    .Respond("application/json", "{\n    \"id\": \"tmpl_ffa2eff0666dd83\",\n    \"description\": \"My Description\",\n    \"versions\": [\n        {\n            \"id\": \"vrsn_de41850f9642eb1\",\n            \"description\": \"My Description\",\n            \"html\": \"<html>hello\",\n            \"date_created\": \"2018-10-19T18:43:32.396Z\",\n            \"date_modified\": \"2018-10-19T18:43:32.396Z\",\n            \"object\": \"version\"\n        }\n    ],\n    \"published_version\": {\n        \"id\": \"vrsn_de41850f9642eb1\",\n        \"description\": \"My Description\",\n        \"html\": \"<html>hello\",\n        \"date_created\": \"2018-10-19T18:43:32.396Z\",\n        \"date_modified\": \"2018-10-19T18:43:32.396Z\",\n        \"object\": \"version\"\n    },\n    \"metadata\": {\n        \"met1\": \"v1\",\n        \"met2\": \"v2\"\n    },\n    \"date_created\": \"2018-10-19T18:43:32.396Z\",\n    \"date_modified\": \"2018-10-19T18:43:32.396Z\",\n    \"object\": \"template\"\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var templates = serviceProvider.GetService<ILobTemplates>();
            var result = await templates.UpdateAsync("tmpl_ffa2eff0666dd83", new TemplateUpdate
            {
                Description = "New Description",
                PublishedVersion = "vrsn_de41850f9642eb1"
            });

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ListRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Get, "https://api.lob.com/v1/templates")
                    .WithExactQueryString("offset=0&limit=100&include%5B%5D=total_count&date_created=%7B%22gt%22%3A%222015-12-12T00%3A00%3A00.0000000%22%2C%22lt%22%3A%222017-01-01T00%3A00%3A00.0000000%22%7D&metadata%5Bm1%5D=v1&metadata%5Bm2%5D=v2")
                    .Respond("application/json", "{\n    \"data\": [\n        {\n            \"id\": \"tmpl_ffa2eff0666dd83\",\n            \"description\": \"My Description\",\n            \"versions\": [\n                {\n                    \"id\": \"vrsn_de41850f9642eb1\",\n                    \"description\": \"My Description\",\n                    \"html\": \"<html>hello\",\n                    \"date_created\": \"2018-10-19T18:43:32.396Z\",\n                    \"date_modified\": \"2018-10-19T18:43:32.396Z\",\n                    \"object\": \"version\"\n                }\n            ],\n            \"published_version\": {\n                \"id\": \"vrsn_de41850f9642eb1\",\n                \"description\": \"My Description\",\n                \"html\": \"<html>hello\",\n                \"date_created\": \"2018-10-19T18:43:32.396Z\",\n                \"date_modified\": \"2018-10-19T18:43:32.396Z\",\n                \"object\": \"version\"\n            },\n            \"metadata\": {\n                \"met1\": \"v1\",\n                \"met2\": \"v2\"\n            },\n            \"date_created\": \"2018-10-19T18:43:32.396Z\",\n            \"date_modified\": \"2018-10-19T18:43:32.396Z\",\n            \"object\": \"template\"\n        }\n    ],\n    \"count\": 1,\n    \"object\": \"list\",\n    \"total_count\": 1\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var templates = serviceProvider.GetService<ILobTemplates>();
            var result = await templates.ListAsync(new TemplateFilter
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
                }
            });

            Assert.Equal(1, result.Count);
            Assert.Equal(1, result.TotalCount);
            Assert.Equal("list", result.Object);
            Assert.Single(result.Data);
            Assert.NotNull(result.Data[0]);
        }
        #endregion


        #region TemplateVersions
        [Fact]
        public async Task CreateVersionRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Post, "https://api.lob.com/v1/templates/tmpl_ffa2eff0666dd83/versions")
                    .WithHeaders("Accept", "application/json")
                    .WithHeaders("Lob-Version", "2018-06-05")
                    .WithHeaders("Authorization", "Basic S2V5Og==")
                    .WithContent("{\"description\":\"My Description\",\"html\":\"<html>hello\"}")
                    .Respond("application/json", "{\n    \"id\": \"vrsn_815de35fef403f8\",\n    \"description\": \"My Description\",\n    \"html\": \"<html>hello\",\n    \"date_created\": \"2018-10-19T19:06:30.847Z\",\n    \"date_modified\": \"2018-10-19T19:06:30.847Z\",\n    \"object\": \"version\"\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var templates = serviceProvider.GetService<ILobTemplates>();
            var result = await templates.CreateVersionAsync("tmpl_ffa2eff0666dd83", new TemplateVersionRequest
            {
                Description = "My Description",
                Html = "<html>hello"
            });

            Assert.Equal(new DateTime(2018, 10, 19, 19, 06, 30, 847, DateTimeKind.Utc), result.DateCreated);
            Assert.Equal(new DateTime(2018, 10, 19, 19, 06, 30, 847, DateTimeKind.Utc), result.DateModified);
            Assert.False(result.Deleted);
            Assert.Equal("My Description", result.Description);
            Assert.Equal("<html>hello", result.Html);
            Assert.Equal("vrsn_815de35fef403f8", result.Id);
            Assert.Equal("version", result.Object);
        }

        [Fact]
        public async Task CreateVersionRequestWithIdempotency()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Post, "https://api.lob.com/v1/templates/tmpl_ffa2eff0666dd83/versions")
                    .WithHeaders("Idempotency-Key", "GUID")
                    .Respond("application/json", "{}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var letters = serviceProvider.GetService<ILobTemplates>();
            var result = await letters.CreateVersionAsync("tmpl_ffa2eff0666dd83", new TemplateVersionRequest(), "GUID");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteVersionRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Delete, "https://api.lob.com/v1/templates/tmpl_ffa2eff0666dd83/versions/vrsn_815de35fef403f8")
                    .Respond("application/json", "{\n  \"id\": \"vrsn_815de35fef403f8\",\n  \"deleted\": true\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var templates = serviceProvider.GetService<ILobTemplates>();
            var result = await templates.DeleteVersionAsync("tmpl_ffa2eff0666dd83", "vrsn_815de35fef403f8");

            Assert.Equal("vrsn_815de35fef403f8", result.Id);
            Assert.True(result.Deleted);
        }

        [Fact]
        public async Task RetrieveVersionRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Get, "https://api.lob.com/v1/templates/tmpl_ffa2eff0666dd83/versions/vrsn_815de35fef403f8")
                    .Respond("application/json", "{\n    \"id\": \"vrsn_815de35fef403f8\",\n    \"description\": \"My Version\",\n    \"html\": \"<html>hello2\",\n    \"date_created\": \"2018-10-19T19:06:30.847Z\",\n    \"date_modified\": \"2018-10-19T19:06:30.847Z\",\n    \"object\": \"version\"\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var templates = serviceProvider.GetService<ILobTemplates>();
            var result = await templates.RetrieveVersionAsync("tmpl_ffa2eff0666dd83", "vrsn_815de35fef403f8");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateVersionRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Post, "https://api.lob.com/v1/templates/tmpl_ffa2eff0666dd83/versions/vrsn_815de35fef403f8")
                    .WithContent("{\"description\":\"New Description\"}")
                    .Respond("application/json", "{\n    \"id\": \"vrsn_815de35fef403f8\",\n    \"description\": \"New Description\",\n    \"html\": \"<html>hello2\",\n    \"date_created\": \"2018-10-19T19:06:30.847Z\",\n    \"date_modified\": \"2018-10-19T19:06:30.847Z\",\n    \"object\": \"version\"\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var templates = serviceProvider.GetService<ILobTemplates>();
            var result = await templates.UpdateVersionAsync("tmpl_ffa2eff0666dd83", "vrsn_815de35fef403f8", "New Description");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ListVersionRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Get, "https://api.lob.com/v1/templates/tmpl_ffa2eff0666dd83/versions")
                    .WithExactQueryString("offset=0&limit=100&include%5B%5D=total_count&date_created=%7B%22gt%22%3A%222015-12-12T00%3A00%3A00.0000000%22%2C%22lt%22%3A%222017-01-01T00%3A00%3A00.0000000%22%7D")
                    .Respond("application/json", "{\n    \"data\": [\n        {\n            \"id\": \"vrsn_de41850f9642eb1\",\n            \"description\": \"My Description\",\n            \"html\": \"<html>hello\",\n            \"date_created\": \"2018-10-19T18:43:32.396Z\",\n            \"date_modified\": \"2018-10-19T18:43:32.396Z\",\n            \"object\": \"version\"\n        }\n    ],\n    \"count\": 1,\n    \"object\": \"list\",\n    \"total_count\": 1\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var templates = serviceProvider.GetService<ILobTemplates>();
            var result = await templates.ListVersionAsync("tmpl_ffa2eff0666dd83", new TemplateVersionFilter
            {
                CreatedAfter = new DateTime(2015, 12, 12),
                CreatedBefore = new DateTime(2017, 1, 1),
                IncludeTotalCount = true,
                Limit = 100,
                Offset = 0
            });

            Assert.Equal(1, result.Count);
            Assert.Equal(1, result.TotalCount);
            Assert.Equal("list", result.Object);
            Assert.Single(result.Data);
            Assert.NotNull(result.Data[0]);
        }
        #endregion
    }
}
