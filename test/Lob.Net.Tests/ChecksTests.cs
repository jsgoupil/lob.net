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
    public class ChecksTests : BaseRequestTests
    {
        public ChecksTests()
            : base()
        {

        }

        [Fact]
        public async Task CreateRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Post, "https://api.lob.com/v1/checks")
                    .WithHeaders("Accept", "application/json")
                    .WithHeaders("Lob-Version", "2018-06-05")
                    .WithHeaders("Authorization", "Basic S2V5Og==")
                    .WithContent("{\"description\":\"Paying Employee\",\"to\":{\"description\":\"MyDescription\",\"name\":\"MyName\",\"company\":\"MyCompany\",\"phone\":\"2065551234\",\"email\":\"contact@jsgoupil.com\",\"address_line1\":\"addr1\",\"address_line2\":\"addr2\",\"address_city\":\"city1\",\"address_state\":\"WA\",\"address_zip\":\"98103\",\"address_country\":\"US\",\"metadata\":{\"m1\":\"v1\",\"m2\":\"v2\"}},\"from\":\"adr_738379e5622a9f04\",\"bank_account\":\"bank_da4daf54431d39d\",\"amount\":100.0,\"memo\":\"My Memo\",\"check_number\":1234,\"logo\":\"https://s3-us-west-2.amazonaws.com/lob-assets/lob_check_logo.png\",\"message\":\"My Message\",\"attachment\":\"tmpl_7e7fdb7d1cb261d\",\"merge_variables\":{\"var1\":\"value1\",\"var2\":\"value2\"},\"mail_type\":\"ups_next_day_air\",\"send_date\":\"2019-10-10T23:00:26.998\",\"metadata\":{\"met1\":\"v1\",\"met2\":\"v2\"}}")
                    .Respond("application/json", "{\n    \"id\": \"chk_d7613d3be349237b\",\n    \"description\": \"Paying Employee\",\n    \"metadata\": {\n        \"met1\": \"v1\",\n        \"met2\": \"v2\"\n    },\n    \"check_number\": 1234,\n    \"memo\": \"My Memo\",\n    \"amount\": 100,\n    \"message\": \"My Message\",\n    \"url\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b.pdf?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541879773&Signature=T2Fk%2Fg9JxxZ2TW9qACnBrneD2LE%3D\",\n    \"check_bottom_template_id\": null,\n    \"attachment_template_id\": \"tmpl_7e7fdb7d1cb261d\",\n    \"check_bottom_template_version_id\": null,\n    \"attachment_template_version_id\": \"vrsn_861e42dba614478\",\n    \"to\": {\n        \"id\": \"adr_9570bceaa6968c3f\",\n        \"description\": \"MyDescription\",\n        \"name\": \"MYNAME\",\n        \"company\": \"MYCOMPANY\",\n        \"phone\": \"2065551234\",\n        \"email\": \"contact@jsgoupil.com\",\n        \"address_line1\": \"ADDR1\",\n        \"address_line2\": \"ADDR2\",\n       \"address_city\": \"CITY1\",\n        \"address_state\": \"WA\",\n        \"address_zip\": \"98103\",\n        \"address_country\": \"UNITED STATES\",\n        \"metadata\": {\n            \"m1\": \"v1\",\n            \"m2\": \"v2\"\n        },\n        \"date_created\": \"2018-10-11T19:56:12.802Z\",\n        \"date_modified\": \"2018-10-11T19:56:12.802Z\",\n        \"deleted\": true,\n        \"object\": \"address\"\n    },\n    \"from\": {\n        \"id\": \"adr_738379e5622a9f04\",\n        \"description\": \"OtherDescription\",\n        \"name\": \"JS GOUPIL\",\n        \"company\": null,\n        \"phone\": \"5555555555\",\n        \"email\": null,\n        \"address_line1\": \"456 59TH ST CT W\",\n        \"address_line2\": null,\n        \"address_city\": \"SEATTLE\",\n        \"address_state\": \"WA\",\n        \"address_zip\": \"98388\",\n        \"address_country\": \"UNITED STATES\",\n        \"metadata\": {},\n        \"date_created\": \"2018-10-09T22:14:50.639Z\",\n        \"date_modified\": \"2018-10-09T22:14:50.639Z\",\n        \"object\": \"address\"\n    },\n    \"bank_account\": {\n        \"id\": \"bank_da4daf54431d39d\",\n        \"description\": \"My Personal Account\",\n        \"metadata\": {\n            \"met1\": \"v1\",\n            \"met2\": \"v2\"\n        },\n        \"routing_number\": \"322271627\",\n        \"account_number\": \"123456789\",\n        \"account_type\": \"individual\",\n        \"signatory\": \"Jean-Sébastien Goupil\",\n        \"signature_url\": null,\n        \"bank_name\": \"J.P. MORGAN CHASE BANK, N.A.\",\n        \"verified\": true,\n        \"date_created\": \"2018-10-11T19:39:32.184Z\",\n        \"date_modified\": \"2018-10-11T19:52:56.304Z\",\n        \"object\": \"bank_account\"\n    },\n    \"carrier\": \"UPS\",\n    \"tracking_number\": null,\n    \"tracking_events\": [],\n    \"thumbnails\": [\n        {\n            \"small\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_small_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541879773&Signature=DoQBY%2BDM9%2FHQfiAO9bsXGLdO2Jw%3D\",\n            \"medium\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_medium_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541879773&Signature=lR2Xd382XGurIVR%2Fx%2BzNT4wKb8g%3D\",\n            \"large\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_large_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541879773&Signature=8uNysF03%2BB%2FdI%2ByecteiZhwHySg%3D\"\n        },\n        {\n            \"small\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_small_2.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541879773&Signature=jmy%2FWgBEoSyG71IOm%2FYqMouLvqU%3D\",\n            \"medium\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_medium_2.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541879773&Signature=5ssYjO8uNLnmWS7W3ghPn8XJsKE%3D\",\n            \"large\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_large_2.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541879773&Signature=imnxr%2FHCbhGh0Py6rO7DgAYopXY%3D\"\n        }\n    ],\n    \"expected_delivery_date\": \"2018-10-15\",\n    \"mail_type\": \"ups_next_day_air\",\n    \"date_created\": \"2018-10-11T19:56:12.970Z\",\n    \"date_modified\": \"2018-10-11T19:56:12.970Z\",\n    \"send_date\": \"2018-10-11T20:01:12.970Z\",\n    \"object\": \"check\"\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var checks = serviceProvider.GetService<ILobChecks>();
            var result = await checks.CreateAsync(new CheckRequest
            {
                Amount = 100,
                BankAccount = "bank_da4daf54431d39d",
                From = new AddressRequest("adr_738379e5622a9f04"),
                To = new AddressRequest(new Address
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
                }),
                Description = "Paying Employee",
                MailType = MailType.UpsNextDayAir,
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
                Attachment = "tmpl_7e7fdb7d1cb261d",
                CheckNumber = 1234,
                Logo = "https://s3-us-west-2.amazonaws.com/lob-assets/lob_check_logo.png",
                Memo = "My Memo",
                Message = "My Message"
            });

            Assert.Equal(100, result.Amount);
            Assert.Equal("tmpl_7e7fdb7d1cb261d", result.AttachmentTemplateId);
            Assert.Equal("vrsn_861e42dba614478", result.AttachmentTemplateVersionId);
            Assert.Equal("123456789", result.BankAccount.AccountNumber);
            Assert.Equal(AccountType.Individual, result.BankAccount.AccountType);
            Assert.Equal("J.P. MORGAN CHASE BANK, N.A.", result.BankAccount.BankName);
            Assert.Equal(new DateTime(2018, 10, 11, 19, 39, 32, 184, DateTimeKind.Utc), result.BankAccount.DateCreated);
            Assert.Equal(new DateTime(2018, 10, 11, 19, 52, 56, 304, DateTimeKind.Utc), result.BankAccount.DateModified);
            Assert.False(result.BankAccount.Deleted);
            Assert.Equal("My Personal Account", result.BankAccount.Description);
            Assert.Equal("bank_da4daf54431d39d", result.BankAccount.Id);
            Assert.Equal(2, result.BankAccount.Metadata.Count);
            Assert.Equal("v1", result.BankAccount.Metadata["met1"]);
            Assert.Equal("v2", result.BankAccount.Metadata["met2"]);
            Assert.Equal("bank_account", result.BankAccount.Object);
            Assert.Equal("322271627", result.BankAccount.RoutingNumber);
            Assert.Equal("Jean-Sébastien Goupil", result.BankAccount.Signatory);
            Assert.Null(result.BankAccount.SignatureUrl);
            Assert.True(result.BankAccount.Verified);
            Assert.Equal(Carrier.UPS, result.Carrier);
            Assert.Null(result.CheckBottomTemplateId);
            Assert.Null(result.CheckBottomTemplateVersionId);
            Assert.Equal(new DateTime(2018, 10, 11, 19, 56, 12, 970, DateTimeKind.Utc), result.DateCreated);
            Assert.Equal(new DateTime(2018, 10, 11, 19, 56, 12, 970, DateTimeKind.Utc), result.DateModified);
            Assert.False(result.Deleted);
            Assert.Equal("Paying Employee", result.Description);
            Assert.Equal(new DateTime(2018, 10, 15, 0, 0, 0, DateTimeKind.Utc), result.ExpectedDeliveryDate);
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
            Assert.Null(result.From.Deleted);
            Assert.Equal("address", result.From.Object);
            Assert.Equal("adr_9570bceaa6968c3f", result.To.Id);
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
            Assert.Equal(new DateTime(2018, 10, 11, 19, 56, 12, 802, DateTimeKind.Utc), result.To.DateCreated.Value);
            Assert.Equal(new DateTime(2018, 10, 11, 19, 56, 12, 802, DateTimeKind.Utc), result.To.DateModified.Value);
            Assert.True(result.To.Deleted);
            Assert.Equal("address", result.To.Object);
            Assert.Equal("chk_d7613d3be349237b", result.Id);
            Assert.Equal(MailType.UpsNextDayAir, result.MailType);
            Assert.Equal("My Memo", result.Memo);
            Assert.Equal("My Message", result.Message);
            Assert.Equal(2, result.To.Metadata.Count);
            Assert.Equal("v1", result.To.Metadata["m1"]);
            Assert.Equal("v2", result.To.Metadata["m2"]);
            Assert.Equal("check", result.Object);
            Assert.Equal(new DateTime(2018, 10, 11, 20, 1, 12, 970, DateTimeKind.Utc), result.SendDate);
            Assert.Equal("https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_small_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541879773&Signature=DoQBY%2BDM9%2FHQfiAO9bsXGLdO2Jw%3D", result.Thumbnails[0].Small);
            Assert.Equal("https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_medium_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541879773&Signature=lR2Xd382XGurIVR%2Fx%2BzNT4wKb8g%3D", result.Thumbnails[0].Medium);
            Assert.Equal("https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_large_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541879773&Signature=8uNysF03%2BB%2FdI%2ByecteiZhwHySg%3D", result.Thumbnails[0].Large);
            Assert.Empty(result.TrackingEvents);
            Assert.Null(result.TrackingNumber);
            Assert.Equal("https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b.pdf?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541879773&Signature=T2Fk%2Fg9JxxZ2TW9qACnBrneD2LE%3D", result.Url);
        }

        [Fact]
        public async Task CreateRequestWithIdempotency()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Post, "https://api.lob.com/v1/checks")
                    .WithHeaders("Idempotency-Key", "GUID")
                    .Respond("application/json", "{}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var letters = serviceProvider.GetService<ILobChecks>();
            var result = await letters.CreateAsync(new CheckRequest(), "GUID");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task CancelRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Delete, "https://api.lob.com/v1/checks/chk_d7613d3be349237b")
                    .Respond("application/json", "{\n  \"id\": \"chk_d7613d3be349237b\",\n  \"deleted\": true\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var checks = serviceProvider.GetService<ILobChecks>();
            var result = await checks.CancelAsync("chk_d7613d3be349237b");

            Assert.Equal("chk_d7613d3be349237b", result.Id);
            Assert.True(result.Deleted);
        }

        [Fact]
        public async Task RetrieveRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Get, "https://api.lob.com/v1/checks/chk_d7613d3be349237b")
                    .Respond("application/json", "{\n    \"id\": \"chk_d7613d3be349237b\",\n    \"description\": \"Paying Employee\",\n    \"metadata\": {\n        \"met1\": \"v1\",\n        \"met2\": \"v2\"\n    },\n    \"check_number\": 1234,\n    \"memo\": \"My Memo\",\n    \"amount\": 100,\n    \"message\": \"My Message\",\n    \"url\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b.pdf?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541879773&Signature=T2Fk%2Fg9JxxZ2TW9qACnBrneD2LE%3D\",\n    \"check_bottom_template_id\": null,\n    \"attachment_template_id\": \"tmpl_7e7fdb7d1cb261d\",\n    \"check_bottom_template_version_id\": null,\n    \"attachment_template_version_id\": \"vrsn_861e42dba614478\",\n    \"to\": {\n        \"id\": \"adr_9570bceaa6968c3f\",\n        \"description\": \"MyDescription\",\n        \"name\": \"MYNAME\",\n        \"company\": \"MYCOMPANY\",\n        \"phone\": \"2065551234\",\n        \"email\": \"contact@jsgoupil.com\",\n        \"address_line1\": \"ADDR1\",\n        \"address_line2\": \"ADDR2\",\n       \"address_city\": \"CITY1\",\n        \"address_state\": \"WA\",\n        \"address_zip\": \"98103\",\n        \"address_country\": \"UNITED STATES\",\n        \"metadata\": {\n            \"m1\": \"v1\",\n            \"m2\": \"v2\"\n        },\n        \"date_created\": \"2018-10-11T19:56:12.802Z\",\n        \"date_modified\": \"2018-10-11T19:56:12.802Z\",\n        \"deleted\": true,\n        \"object\": \"address\"\n    },\n    \"from\": {\n        \"id\": \"adr_738379e5622a9f04\",\n        \"description\": \"OtherDescription\",\n        \"name\": \"JS GOUPIL\",\n        \"company\": null,\n        \"phone\": \"5555555555\",\n        \"email\": null,\n        \"address_line1\": \"456 59TH ST CT W\",\n        \"address_line2\": null,\n        \"address_city\": \"SEATTLE\",\n        \"address_state\": \"WA\",\n        \"address_zip\": \"98388\",\n        \"address_country\": \"UNITED STATES\",\n        \"metadata\": {},\n        \"date_created\": \"2018-10-09T22:14:50.639Z\",\n        \"date_modified\": \"2018-10-09T22:14:50.639Z\",\n        \"object\": \"address\"\n    },\n    \"bank_account\": {\n        \"id\": \"bank_da4daf54431d39d\",\n        \"description\": \"My Personal Account\",\n        \"metadata\": {\n            \"met1\": \"v1\",\n            \"met2\": \"v2\"\n        },\n        \"routing_number\": \"322271627\",\n        \"account_number\": \"123456789\",\n        \"account_type\": \"individual\",\n        \"signatory\": \"Jean-Sébastien Goupil\",\n        \"signature_url\": null,\n        \"bank_name\": \"J.P. MORGAN CHASE BANK, N.A.\",\n        \"verified\": true,\n        \"date_created\": \"2018-10-11T19:39:32.184Z\",\n        \"date_modified\": \"2018-10-11T19:52:56.304Z\",\n        \"object\": \"bank_account\"\n    },\n    \"carrier\": \"UPS\",\n    \"tracking_number\": null,\n    \"tracking_events\": [],\n    \"thumbnails\": [\n        {\n            \"small\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_small_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541879773&Signature=DoQBY%2BDM9%2FHQfiAO9bsXGLdO2Jw%3D\",\n            \"medium\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_medium_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541879773&Signature=lR2Xd382XGurIVR%2Fx%2BzNT4wKb8g%3D\",\n            \"large\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_large_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541879773&Signature=8uNysF03%2BB%2FdI%2ByecteiZhwHySg%3D\"\n        },\n        {\n            \"small\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_small_2.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541879773&Signature=jmy%2FWgBEoSyG71IOm%2FYqMouLvqU%3D\",\n            \"medium\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_medium_2.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541879773&Signature=5ssYjO8uNLnmWS7W3ghPn8XJsKE%3D\",\n            \"large\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_large_2.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541879773&Signature=imnxr%2FHCbhGh0Py6rO7DgAYopXY%3D\"\n        }\n    ],\n    \"expected_delivery_date\": \"2018-10-15\",\n    \"mail_type\": \"ups_next_day_air\",\n    \"date_created\": \"2018-10-11T19:56:12.970Z\",\n    \"date_modified\": \"2018-10-11T19:56:12.970Z\",\n    \"send_date\": \"2018-10-11T20:01:12.970Z\",\n    \"object\": \"check\"\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var checks = serviceProvider.GetService<ILobChecks>();
            var result = await checks.RetrieveAsync("chk_d7613d3be349237b");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ListRequest()
        {
            var serviceCollection = GetServiceProvider(mock =>
            {
                mock.When(HttpMethod.Get, "https://api.lob.com/v1/checks")
                    .WithExactQueryString("offset=0&limit=100&include%5B%5D=total_count&metadata%5Bm1%5D=v1&metadata%5Bm2%5D=v2&date_created=%7B%22gt%22%3A%222015-12-12T00%3A00%3A00.0000000%22%2C%22lt%22%3A%222017-01-01T00%3A00%3A00.0000000%22%7D&scheduled=true&send_date=%7B%22gt%22%3A%222016-12-12T00%3A00%3A00.0000000%22%2C%22lt%22%3A%222018-01-01T00%3A00%3A00.0000000%22%7D&mail_type=usps_first_class&sort_by%5Bdate_created%5D=asc")
                    .Respond("application/json", "{\n    \"data\": [\n        {\n            \"id\": \"chk_d7613d3be349237b\",\n            \"description\": \"Paying Employee\",\n            \"metadata\": {\n                \"met1\": \"v1\",\n                \"met2\": \"v2\"\n            },\n            \"check_number\": 1234,\n            \"memo\": \"My Memo\",\n            \"amount\": 100,\n            \"message\": \"My Message\",\n            \"url\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b.pdf?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541882641&Signature=pAW54r2Gyv6pRdjk8rxnQ3jX1Uk%3D\",\n            \"check_bottom_template_id\": null,\n            \"attachment_template_id\": \"tmpl_7e7fdb7d1cb261d\",\n            \"check_bottom_template_version_id\": null,\n            \"attachment_template_version_id\": \"vrsn_861e42dba614478\",\n            \"to\": {\n                \"id\": \"adr_9570bceaa6968c3f\",\n                \"description\": \"MyDescription\",\n                \"name\": \"MYNAME\",\n                \"company\": \"MYCOMPANY\",\n                \"phone\": \"2065551234\",\n                \"email\": \"contact@jsgoupil.com\",\n                \"address_line1\": \"ADDR1\",\n                \"address_line2\": \"ADDR2\",\n                \"address_city\": \"CITY1\",\n                \"address_state\": \"WA\",\n                \"address_zip\": \"98103\",\n                \"address_country\": \"UNITED STATES\",\n                \"metadata\": {\n                    \"m1\": \"v1\",\n                    \"m2\": \"v2\"\n                },\n                \"date_created\": \"2018-10-11T19:56:12.802Z\",\n                \"date_modified\": \"2018-10-11T19:56:12.802Z\",\n                \"deleted\": true,\n                \"object\": \"address\"\n            },\n            \"from\": {\n                \"id\": \"adr_738379e5622a9f04\",\n                \"description\": \"OtherDescription\",\n                \"name\": \"JS GOUPIL\",\n                \"company\": null,\n                \"phone\": \"5555555555\",\n                \"email\": null,\n                \"address_line1\": \"456 59TH ST CT W\",\n                \"address_line2\": null,\n                \"address_city\": \"SEATTLE\",\n                \"address_state\": \"WA\",\n                \"address_zip\": \"98388\",\n                \"address_country\": \"UNITED STATES\",\n                \"metadata\": {},\n                \"date_created\": \"2018-10-09T22:14:50.639Z\",\n                \"date_modified\": \"2018-10-09T22:14:50.639Z\",\n                \"object\": \"address\"\n            },\n            \"bank_account\": {\n                \"id\": \"bank_da4daf54431d39d\",\n                \"description\": \"My Personal Account\",\n                \"metadata\": {\n                    \"met1\": \"v1\",\n                    \"met2\": \"v2\"\n                },\n                \"routing_number\": \"322271627\",\n                \"account_number\": \"123456789\",\n                \"account_type\": \"individual\",\n                \"signatory\": \"Jean-Sébastien Goupil\",\n               \"signature_url\": null,\n                \"bank_name\": \"J.P. MORGAN CHASE BANK, N.A.\",\n                \"verified\": true,\n                \"date_created\": \"2018-10-11T19:39:32.184Z\",\n                \"date_modified\": \"2018-10-11T19:52:56.304Z\",\n                \"object\": \"bank_account\"\n            },\n            \"carrier\": \"UPS\",\n            \"tracking_number\": null,\n            \"tracking_events\": [],\n            \"thumbnails\": [\n                {\n                    \"small\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_small_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541882641&Signature=ithIlG55bgth8T%2B%2B2%2B%2BMK5U5iQQ%3D\",\n                    \"medium\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_medium_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541882641&Signature=YMitFAdNXyY7JHT7MOC%2BXiScfBI%3D\",\n                    \"large\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_large_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541882641&Signature=bZDb2frq8Awo%2F%2BkNwndrDbX3xTQ%3D\"\n                },\n                {\n                    \"small\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_small_2.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541882641&Signature=zD%2FhAqy7gwqIJA2rtOUDYzVo4z8%3D\",\n                    \"medium\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_medium_2.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541882641&Signature=FkFLUQqIVaMYMI92jsi6%2BrHs9k4%3D\",\n                    \"large\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_large_2.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541882641&Signature=uoUhZ3tae7syPAHnohFvMRC9b98%3D\"\n                },\n                {\n                    \"small\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_small_3.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541882641&Signature=3noGDxu7AO%2BOxckxCiD%2FbTmF1uQ%3D\",\n                    \"medium\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_medium_3.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541882641&Signature=Kg0Wmpqr592p2pqM2%2FWtRQYeiPE%3D\",\n                    \"large\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_large_3.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541882641&Signature=5sVoZMY6NfZrMiT9W9PQcJBB43Q%3D\"\n                },\n                {\n                    \"small\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_small_4.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541882641&Signature=MESsSzH52HqzuNc0Qx3gcBP4BZM%3D\",\n                    \"medium\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_medium_4.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541882641&Signature=H69N5V1vTgM1a9X8Tqp2MDywwjE%3D\",\n                    \"large\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/chk_d7613d3be349237b_thumb_large_4.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1541882641&Signature=F3aTjkBLaUR5E0Sbh3WseRx7aYc%3D\"\n                }\n            ],\n            \"expected_delivery_date\": \"2018-10-15\",\n            \"mail_type\": \"ups_next_day_air\",\n            \"date_created\": \"2018-10-11T19:56:12.970Z\",\n            \"date_modified\": \"2018-10-11T19:56:16.054Z\",\n            \"send_date\": \"2018-10-11T20:01:12.970Z\",\n            \"object\": \"check\"\n        }\n    ],\n    \"count\": 1,\n    \"object\": \"list\"\n}");
                mock.Fallback.Throw(new Exception("Fallback"));
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var checks = serviceProvider.GetService<ILobChecks>();
            var result = await checks.ListAsync(new CheckFilter
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
                SortBy = new ListSortBy(SortBy.DateCreated, SortDirection.Ascending)
            });

            Assert.Equal(1, result.Count);
            Assert.Equal("list", result.Object);
            Assert.Single(result.Data);
            Assert.NotNull(result.Data[0]);
        }
    }
}
