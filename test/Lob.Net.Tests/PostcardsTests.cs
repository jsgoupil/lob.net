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
    public class PostcardsTests : BaseRequestTests
    {
        public PostcardsTests()
            : base()
        {

        }

        [Fact]
        public async Task CreateRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Post, "https://api.lob.com/v1/postcards")
                    .WithHeaders("Accept", "application/json")
                    .WithHeaders("Lob-Version", "2018-06-05")
                    .WithHeaders("Authorization", "Basic S2V5Og==")
                    .WithContent("{\"description\":\"My Description\",\"to\":{\"description\":\"MyDescription\",\"name\":\"MyName\",\"company\":\"MyCompany\",\"phone\":\"2065551234\",\"email\":\"contact@jsgoupil.com\",\"address_line1\":\"addr1\",\"address_line2\":\"addr2\",\"address_city\":\"city1\",\"address_state\":\"WA\",\"address_zip\":\"98103\",\"address_country\":\"US\",\"metadata\":{\"m1\":\"v1\",\"m2\":\"v2\"}},\"from\":\"adr_738379e5622a9f04\",\"front\":\"front\",\"back\":\"back\",\"merge_variables\":{\"var1\":\"value1\",\"var2\":\"value2\"},\"size\":\"4x6\",\"mail_type\":\"usps_first_class\",\"send_date\":\"2019-10-10T23:00:26.998\",\"metadata\":{\"met1\":\"v1\",\"met2\":\"v2\"}}")
                    .Respond("application/json", "{\n    \"id\": \"psc_9abb3c02f56b331b\",\n    \"description\": \"My Description\",\n    \"metadata\": {\n        \"met1\": \"v1\",\n        \"met2\": \"v2\"\n    },\n    \"to\": {\n        \"id\": \"adr_520c6fa71011fc52\",\n        \"description\": \"MyDescription\",\n        \"name\": \"MYNAME\",\n        \"company\": \"MYCOMPANY\",\n        \"phone\": \"2065551234\",\n        \"email\": \"contact@jsgoupil.com\",\n        \"address_line1\": \"ADDR1\",\n        \"address_line2\": \"ADDR2\",\n        \"address_city\": \"CITY1\",\n        \"address_state\": \"WA\",\n        \"address_zip\": \"98103\",\n        \"address_country\": \"UNITED STATES\",\n        \"metadata\": {\n            \"m1\": \"v1\",\n            \"m2\": \"v2\"\n        },\n        \"date_created\": \"2018-10-11T00:51:32.477Z\",\n        \"date_modified\": \"2018-10-11T00:51:32.477Z\",\n        \"deleted\": true,\n        \"object\": \"address\"\n    },\n    \"from\": {\n        \"id\": \"adr_738379e5622a9f04\",\n        \"description\": \"OtherDescription\",\n        \"name\": \"JS GOUPIL\",\n        \"company\": null,\n        \"phone\": \"5555555555\",\n        \"email\": null,\n        \"address_line1\": \"456 59TH ST CT W\",\n        \"address_line2\": null,\n        \"address_city\": \"SEATTLE\",\n        \"address_state\": \"WA\",\n        \"address_zip\": \"98388\",\n        \"address_country\": \"UNITED STATES\",\n        \"metadata\": {},\n        \"date_created\": \"2018-10-09T22:14:50.639Z\",\n        \"date_modified\": \"2018-10-09T22:14:50.639Z\",\n        \"object\": \"address\"\n    },\n    \"url\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_9abb3c02f56b331b.pdf?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541811092&Signature=8XRYx%2Fff3VhUdLGTaLcRPkypDWg%3D\",\n    \"front_template_id\": \"tmpl_7e7fdb7d1cb261d\",\n    \"back_template_id\": \"tmpl_7e7fdb7d1cb261d\",\n    \"front_template_version_id\": \"vrsn_861e42dba614478\",\n    \"back_template_version_id\": \"vrsn_861e42dba614478\",\n    \"carrier\": \"USPS\",\n    \"tracking_events\": [],\n    \"thumbnails\": [\n        {\n            \"small\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_9abb3c02f56b331b_thumb_small_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541811092&Signature=BBhsLKcetmzrPI6RA3dGZjd05XE%3D\",\n            \"medium\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_9abb3c02f56b331b_thumb_medium_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541811092&Signature=rRnNPC2H8E%2Bp3tHHv03TyHLNUsQ%3D\",\n            \"large\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_9abb3c02f56b331b_thumb_large_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541811092&Signature=CfoqIoxiTQEiOwwQv1pjcj%2FFi3s%3D\"\n        },\n        {\n            \"small\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_9abb3c02f56b331b_thumb_small_2.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541811092&Signature=zv22biijyQ9yb5hzKAi%2BGCHK7KA%3D\",\n            \"medium\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_9abb3c02f56b331b_thumb_medium_2.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541811092&Signature=RBYNcIs4WH0nHHqM3YpcWQ5ikNo%3D\",\n            \"large\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_9abb3c02f56b331b_thumb_large_2.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541811092&Signature=62GmCojnmDMagVpuBlmfsdt%2F9%2Fw%3D\"\n        }\n    ],\n    \"size\": \"4x6\",\n    \"mail_type\": \"usps_first_class\",\n    \"expected_delivery_date\": \"2018-10-18\",\n    \"date_created\": \"2018-10-11T00:51:32.495Z\",\n    \"date_modified\": \"2018-10-11T00:51:32.495Z\",\n    \"send_date\": \"2018-10-11T00:56:32.494Z\",\n    \"object\": \"postcard\"\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var postcards = serviceProvider.GetService<ILobPostcards>();
            var result = await postcards.CreateAsync(new PostcardRequest
            {
                Back = "back",
                Description = "My Description",
                From = new AddressReference("adr_738379e5622a9f04"),
                Front = "front",
                MailType = MailType.UspsFirstClass,
                MergeVariables = new Dictionary<string, string>
                {
                    { "var1", "value1" },
                    { "var2", "value2" }
                },
                Metadata = new Dictionary<string, string>
                {
                    { "met1", "v1" },
                    { "met2", "v2" }
                },
                SendDate = new DateTime(2019, 10, 10, 23, 0, 26, 998),
                Size = "4x6",
                To = new AddressReference(new AddressRequest
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
                })
            });

            Assert.Equal("tmpl_7e7fdb7d1cb261d", result.BackTemplateId);
            Assert.Equal("vrsn_861e42dba614478", result.BackTemplateVersionId);
            Assert.Equal(Carrier.USPS, result.Carrier);
            Assert.Equal(new DateTime(2018, 10, 11, 0, 51, 32, 495, DateTimeKind.Utc), result.DateCreated);
            Assert.Equal(new DateTime(2018, 10, 11, 0, 51, 32, 495, DateTimeKind.Utc), result.DateModified);
            Assert.False(result.Deleted);
            Assert.Equal("My Description", result.Description);
            Assert.Equal(new DateTime(2018, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), result.ExpectedDeliveryDate);
            Assert.Equal("adr_738379e5622a9f04", result.From.Id);
            Assert.Equal("OtherDescription", result.From.Description);
            Assert.Equal("JS GOUPIL", result.From.Name);
            Assert.Null(result.From.Company);
            Assert.Equal("5555555555", result.From.Phone);
            Assert.Null(result.From.Email);
            Assert.Equal("456 59TH ST CT W", result.From.AddressLine1);
            Assert.Null(result.From.AddressLine2);
            Assert.Equal("SEATTLE", result.From.AddressCity);
            Assert.Equal("WA", result.From.AddressState);
            Assert.Equal("98388", result.From.AddressZip);
            Assert.Equal("UNITED STATES", result.From.AddressCountry);
            Assert.Equal(0, result.From.Metadata.Count);
            Assert.Equal(new DateTime(2018, 10, 9, 22, 14, 50, 639, DateTimeKind.Utc), result.From.DateCreated.Value);
            Assert.Equal(new DateTime(2018, 10, 9, 22, 14, 50, 639, DateTimeKind.Utc), result.From.DateModified.Value);
            Assert.False(result.From.Deleted);
            Assert.Equal("address", result.From.Object);
            Assert.Equal("tmpl_7e7fdb7d1cb261d", result.FrontTemplateId);
            Assert.Equal("vrsn_861e42dba614478", result.FrontTemplateVersionId);
            Assert.Equal("psc_9abb3c02f56b331b", result.Id);
            Assert.Equal(MailType.UspsFirstClass, result.MailType);
            Assert.Equal(2, result.Metadata.Count);
            Assert.Equal("v1", result.Metadata["met1"]);
            Assert.Equal("v2", result.Metadata["met2"]);
            Assert.Equal("postcard", result.Object);
            Assert.Equal(new DateTime(2018, 10, 11, 0, 56, 32, 494, DateTimeKind.Utc), result.SendDate);
            Assert.Equal("4x6", result.Size);
            Assert.Equal("https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_9abb3c02f56b331b_thumb_small_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541811092&Signature=BBhsLKcetmzrPI6RA3dGZjd05XE%3D", result.Thumbnails[0].Small);
            Assert.Equal("https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_9abb3c02f56b331b_thumb_medium_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541811092&Signature=rRnNPC2H8E%2Bp3tHHv03TyHLNUsQ%3D", result.Thumbnails[0].Medium);
            Assert.Equal("https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_9abb3c02f56b331b_thumb_large_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541811092&Signature=CfoqIoxiTQEiOwwQv1pjcj%2FFi3s%3D", result.Thumbnails[0].Large);
            Assert.Equal("adr_520c6fa71011fc52", result.To.Id);
            Assert.Equal("MyDescription", result.To.Description);
            Assert.Equal("MYNAME", result.To.Name);
            Assert.Equal("MYCOMPANY", result.To.Company);
            Assert.Equal("2065551234", result.To.Phone);
            Assert.Equal("contact@jsgoupil.com", result.To.Email);
            Assert.Equal("ADDR1", result.To.AddressLine1);
            Assert.Equal("ADDR2", result.To.AddressLine2);
            Assert.Equal("CITY1", result.To.AddressCity);
            Assert.Equal("WA", result.To.AddressState);
            Assert.Equal("98103", result.To.AddressZip);
            Assert.Equal("UNITED STATES", result.To.AddressCountry);
            Assert.Equal(2, result.To.Metadata.Count);
            Assert.Equal("v1", result.To.Metadata["m1"]);
            Assert.Equal("v2", result.To.Metadata["m2"]);
            Assert.Equal(new DateTime(2018, 10, 11, 0, 51, 32, 477, DateTimeKind.Utc), result.To.DateCreated.Value);
            Assert.Equal(new DateTime(2018, 10, 11, 0, 51, 32, 477, DateTimeKind.Utc), result.To.DateModified.Value);
            Assert.True(result.To.Deleted);
            Assert.Equal("address", result.To.Object);
            Assert.Empty(result.TrackingEvents);
            Assert.Equal("https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_9abb3c02f56b331b.pdf?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541811092&Signature=8XRYx%2Fff3VhUdLGTaLcRPkypDWg%3D", result.Url);
        }

        [Fact]
        public async Task CreateRequestWithIdempotency()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Post, "https://api.lob.com/v1/postcards")
                    .WithHeaders("Idempotency-Key", "GUID")
                    .Respond("application/json", "{}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var letters = serviceProvider.GetService<ILobPostcards>();
            var result = await letters.CreateAsync(new PostcardRequest(), "GUID");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Delete, "https://api.lob.com/v1/postcards/psc_9abb3c02f56b331b")
                    .Respond("application/json", "{\n  \"id\": \"psc_9abb3c02f56b331b\",\n  \"deleted\": true\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var postcards = serviceProvider.GetService<ILobPostcards>();
            var result = await postcards.DeleteAsync("psc_9abb3c02f56b331b");

            Assert.Equal("psc_9abb3c02f56b331b", result.Id);
            Assert.True(result.Deleted);
        }

        [Fact]
        public async Task RetrieveRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Get, "https://api.lob.com/v1/postcards/psc_9abb3c02f56b331b")
                    .Respond("application/json", "{\n    \"id\": \"psc_9abb3c02f56b331b\",\n    \"description\": \"My Description\",\n    \"metadata\": {\n        \"met1\": \"v1\",\n        \"met2\": \"v2\"\n    },\n    \"to\": {\n        \"id\": \"adr_520c6fa71011fc52\",\n        \"description\": \"MyDescription\",\n        \"name\": \"MYNAME\",\n        \"company\": \"MYCOMPANY\",\n        \"phone\": \"2065551234\",\n        \"email\": \"contact@jsgoupil.com\",\n        \"address_line1\": \"ADDR1\",\n        \"address_line2\": \"ADDR2\",\n        \"address_city\": \"CITY1\",\n        \"address_state\": \"WA\",\n        \"address_zip\": \"98103\",\n        \"address_country\": \"UNITED STATES\",\n        \"metadata\": {\n            \"m1\": \"v1\",\n            \"m2\": \"v2\"\n        },\n        \"date_created\": \"2018-10-11T00:51:32.477Z\",\n        \"date_modified\": \"2018-10-11T00:51:32.477Z\",\n        \"deleted\": true,\n        \"object\": \"address\"\n    },\n    \"from\": {\n        \"id\": \"adr_738379e5622a9f04\",\n        \"description\": \"OtherDescription\",\n        \"name\": \"JS GOUPIL\",\n        \"company\": null,\n        \"phone\": \"5555555555\",\n        \"email\": null,\n        \"address_line1\": \"456 59TH ST CT W\",\n        \"address_line2\": null,\n        \"address_city\": \"SEATTLE\",\n        \"address_state\": \"WA\",\n        \"address_zip\": \"98388\",\n        \"address_country\": \"UNITED STATES\",\n        \"metadata\": {},\n        \"date_created\": \"2018-10-09T22:14:50.639Z\",\n        \"date_modified\": \"2018-10-09T22:14:50.639Z\",\n        \"object\": \"address\"\n    },\n    \"url\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_9abb3c02f56b331b.pdf?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541811092&Signature=8XRYx%2Fff3VhUdLGTaLcRPkypDWg%3D\",\n    \"front_template_id\": \"tmpl_7e7fdb7d1cb261d\",\n    \"back_template_id\": \"tmpl_7e7fdb7d1cb261d\",\n    \"front_template_version_id\": \"vrsn_861e42dba614478\",\n    \"back_template_version_id\": \"vrsn_861e42dba614478\",\n    \"carrier\": \"USPS\",\n    \"tracking_events\": [],\n    \"thumbnails\": [\n        {\n            \"small\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_9abb3c02f56b331b_thumb_small_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541811092&Signature=BBhsLKcetmzrPI6RA3dGZjd05XE%3D\",\n            \"medium\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_9abb3c02f56b331b_thumb_medium_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541811092&Signature=rRnNPC2H8E%2Bp3tHHv03TyHLNUsQ%3D\",\n            \"large\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_9abb3c02f56b331b_thumb_large_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541811092&Signature=CfoqIoxiTQEiOwwQv1pjcj%2FFi3s%3D\"\n        },\n        {\n            \"small\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_9abb3c02f56b331b_thumb_small_2.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541811092&Signature=zv22biijyQ9yb5hzKAi%2BGCHK7KA%3D\",\n            \"medium\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_9abb3c02f56b331b_thumb_medium_2.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541811092&Signature=RBYNcIs4WH0nHHqM3YpcWQ5ikNo%3D\",\n            \"large\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_9abb3c02f56b331b_thumb_large_2.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541811092&Signature=62GmCojnmDMagVpuBlmfsdt%2F9%2Fw%3D\"\n        }\n    ],\n    \"size\": \"4x6\",\n    \"mail_type\": \"usps_first_class\",\n    \"expected_delivery_date\": \"2018-10-18\",\n    \"date_created\": \"2018-10-11T00:51:32.495Z\",\n    \"date_modified\": \"2018-10-11T00:51:32.495Z\",\n    \"send_date\": \"2018-10-11T00:56:32.494Z\",\n    \"object\": \"postcard\"\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var postcards = serviceProvider.GetService<ILobPostcards>();
            var result = await postcards.RetrieveAsync("psc_9abb3c02f56b331b");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ListRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Get, "https://api.lob.com/v1/postcards")
                    .WithExactQueryString("offset=0&limit=100&include%5B%5D=total_count&metadata%5Bm1%5D=v1&metadata%5Bm2%5D=v2&date_created=%7B%22gt%22%3A%222015-12-12T00%3A00%3A00.0000000%22%2C%22lt%22%3A%222017-01-01T00%3A00%3A00.0000000%22%7D&scheduled=true&send_date=%7B%22gt%22%3A%222016-12-12T00%3A00%3A00.0000000%22%2C%22lt%22%3A%222018-01-01T00%3A00%3A00.0000000%22%7D&mail_type=usps_first_class&sort_by%5Bdate_created%5D=asc&size=4x6")
                    .Respond("application/json", "{\n    \"data\": [\n        {\n            \"id\": \"ltr_e69f2ee9de166ba7\",\n            \"description\": \"My Description\",\n            \"metadata\": {\n                \"met1\": \"v1\",\n                \"met2\": \"v2\"\n            },\n            \"to\": {\n                \"id\": \"adr_c66916085e130482\",\n                \"description\": \"MyDescription\",\n                \"name\": \"MYNAME\",\n                \"company\": \"MYCOMPANY\",\n                \"phone\": \"2065551234\",\n                \"email\": \"contact@jsgoupil.com\",\n                \"address_line1\": \"ADDR1\",\n                \"address_line2\": \"ADDR2\",\n                \"address_city\": \"CITY1\",\n                \"address_state\": \"WA\",\n                \"address_zip\": \"98103\",\n                \"address_country\": \"UNITED STATES\",\n                \"metadata\": {\n                    \"m1\": \"v1\",\n                    \"m2\": \"v2\"\n                },\n                \"date_created\": \"2018-10-10T22:55:26.983Z\",\n                \"date_modified\": \"2018-10-10T22:55:26.983Z\",\n                \"deleted\": true,\n                \"object\": \"address\"\n            },\n            \"from\": {\n                \"id\": \"adr_738379e5622a9f04\",\n                \"description\": \"OtherDescription\",\n                \"name\": \"JS GOUPIL\",\n                \"company\": null,\n                \"phone\": \"5555555555\",\n                \"email\": null,\n                \"address_line1\": \"456 59TH ST CT W\",\n                \"address_line2\": null,\n                \"address_city\": \"SEATTLE\",\n                \"address_state\": \"WA\",\n                \"address_zip\": \"98388\",\n                \"address_country\": \"UNITED STATES\",\n                \"metadata\": {},\n                \"date_created\": \"2018-10-09T22:14:50.639Z\",\n                \"date_modified\": \"2018-10-09T22:14:50.639Z\",\n                \"object\": \"address\"\n            },\n            \"color\": true,\n            \"double_sided\": true,\n            \"address_placement\": \"insert_blank_page\",\n            \"return_envelope\": true,\n            \"perforated_page\": 4,\n            \"extra_service\": \"certified\",\n            \"mail_type\": \"usps_first_class\",\n            \"url\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/ltr_e69f2ee9de166ba7.pdf?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541810458&Signature=quD1WfFx0Uxuq2NX2IZkMU42DBM%3D\",\n            \"template_id\": \"tmpl_7e7fdb7d1cb261d\",\n            \"template_version_id\": \"vrsn_861e42dba614478\",\n            \"carrier\": \"USPS\",\n            \"tracking_number\": null,\n            \"tracking_events\": [],\n            \"thumbnails\": [\n                {\n                    \"small\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/ltr_e69f2ee9de166ba7_thumb_small_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541810458&Signature=U2Dbo8%2F4Ajh2QxIGh4E0r9juRpY%3D\",\n                    \"medium\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/ltr_e69f2ee9de166ba7_thumb_medium_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541810458&Signature=zL3396FKJYWjeUXGQxzdO7P%2BrUc%3D\",\n                    \"large\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/ltr_e69f2ee9de166ba7_thumb_large_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541810458&Signature=PYptnLX7vNrrItltIsXXl8MWths%3D\"\n                },\n                {\n                    \"small\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/ltr_e69f2ee9de166ba7_thumb_small_2.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541810458&Signature=P%2B0vAINrOme2CWwJVQlJs3obXU4%3D\",\n                    \"medium\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/ltr_e69f2ee9de166ba7_thumb_medium_2.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541810458&Signature=zuxEk0misysX4i5sadjWvNDuD5o%3D\",\n                    \"large\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/ltr_e69f2ee9de166ba7_thumb_large_2.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541810458&Signature=4wEYZ%2B7zf2cug%2Fk5nDPmTlgDqAk%3D\"\n                },\n                {\n                    \"small\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/ltr_e69f2ee9de166ba7_thumb_small_3.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541810458&Signature=9tjhHOXNNSMyRsw2Rgdvw52LCBs%3D\",\n                    \"medium\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/ltr_e69f2ee9de166ba7_thumb_medium_3.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541810458&Signature=R%2BLLPkAKTGAxI%2BURalg8nylswWU%3D\",\n                    \"large\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/ltr_e69f2ee9de166ba7_thumb_large_3.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541810458&Signature=K8moRqhdua2A7E%2BVMIjR6AvtVXg%3D\"\n                }\n            ],\n            \"expected_delivery_date\": \"2018-10-18\",\n            \"date_created\": \"2018-10-10T22:55:26.999Z\",\n            \"date_modified\": \"2018-10-10T22:55:29.382Z\",\n            \"send_date\": \"2018-10-10T23:00:26.998Z\",\n            \"object\": \"letter\"\n        }\n    ],\n    \"count\": 1,\n    \"object\": \"list\"\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var postcards = serviceProvider.GetService<ILobPostcards>();
            var result = await postcards.ListAsync(new PostcardFilter
            {
                CreatedAfter = new DateTime(2015, 12, 12),
                CreatedBefore = new DateTime(2017, 1, 1),
                IncludeTotalCount = true,
                Limit = 100,
                Offset = 0,
                MailType = MailType.UspsFirstClass,
                Metadata = new Dictionary<string, string>
                {
                    { "m1", "v1" },
                    { "m2", "v2" }
                },
                Scheduled = true,
                SendAfter = new DateTime(2016, 12, 12),
                SendBefore = new DateTime(2018, 1, 1),
                Size = "4x6",
                SortBy = new ListSortBy(SortBy.DateCreated, SortDirection.Ascending)
            });

            Assert.Equal(1, result.Count);
            Assert.Equal("list", result.Object);
            Assert.Single(result.Data);
            Assert.NotNull(result.Data[0]);
        }
    }
}
