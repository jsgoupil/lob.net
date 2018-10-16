using Lob.Net.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using Xunit;

namespace Lob.Net.Formatters.Tests
{
    public class LobJsonConverterTests : BaseTests
    {
        [Fact]
        public void LobEvent_Generic()
        {
            var eventPostcardCreated = "{\n  \"id\": \"evt_015ad20e7d049d75\",\n  \"body\": {\n    \"id\": \"psc_b452d4712c0d8c1d\",\n    \"description\": \"Test Postcard\",\n    \"metadata\": {},\n    \"to\": {\n      \"id\": \"adr_6d05c676a4b4dfd4\",\n      \"description\": \"Test Address\",\n      \"name\": \"Larry Lobster\",\n      \"address_line1\": \"123 Test St\",\n      \"address_line2\": \"Unit 1\",\n      \"address_city\": \"San Francisco\",\n      \"address_state\": \"CA\",\n      \"address_zip\": \"94107\",\n      \"address_country\": \"UNITED STATES\",\n      \"metadata\": {},\n      \"date_created\": \"2018-10-16T00:24:28.415Z\",\n      \"date_modified\": \"2018-10-16T00:24:28.415Z\",\n      \"object\": \"address\"\n    },\n    \"from\": {\n      \"id\": \"adr_18f12a54018d3ff8\",\n      \"description\": \"Test Address\",\n      \"name\": \"Larry Lobster\",\n      \"address_line1\": \"123 Test St\",\n      \"address_line2\": \"Unit 1\",\n      \"address_city\": \"San Francisco\",\n      \"address_state\": \"CA\",\n      \"address_zip\": \"94107\",\n      \"address_country\": \"UNITED STATES\",\n      \"metadata\": {},\n      \"date_created\": \"2018-10-16T00:24:28.415Z\",\n      \"date_modified\": \"2018-10-16T00:24:28.415Z\",\n      \"object\": \"address\"\n    },\n    \"url\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_b452d4712c0d8c1d.pdf?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1542253225&Signature=P%2FlAod1qVa7hz1C77brdLAQeDWM%3D\",\n    \"carrier\": \"USPS\",\n    \"tracking_events\": [],\n    \"thumbnails\": [\n      {\n        \"small\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_b452d4712c0d8c1d_thumb_small_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1542253225&Signature=O5n2r3XcsjEMTnPZ3EliPeRZVhY%3D\",\n        \"medium\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_b452d4712c0d8c1d_thumb_medium_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1542253225&Signature=UVI4VB82sePQYIcLEBZ5w%2BqgM5I%3D\",\n        \"large\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_b452d4712c0d8c1d_thumb_large_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1542253225&Signature=rZURcSbBx0S2TJ97EBNhpPCUcBM%3D\"\n      },\n      {\n        \"small\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_b452d4712c0d8c1d_thumb_small_2.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1542253225&Signature=Zsa86DiDPOf8tAXzZqWvVFWMpHM%3D\",\n        \"medium\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_b452d4712c0d8c1d_thumb_medium_2.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1542253225&Signature=84bWFvKq8GTFY6QTncM3aLTB%2BCY%3D\",\n        \"large\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_b452d4712c0d8c1d_thumb_large_2.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1542253225&Signature=e67NqdnzmTrPamtgQx82DM1Gtis%3D\"\n      }\n    ],\n    \"size\": \"4x6\",\n    \"mail_type\": \"usps_first_class\",\n    \"expected_delivery_date\": \"2018-10-23\",\n    \"date_created\": \"2018-10-16T00:24:28.415Z\",\n    \"date_modified\": \"2018-10-16T00:24:28.415Z\",\n    \"send_date\": \"2018-10-16T00:24:28.415Z\",\n    \"object\": \"postcard\"\n  },\n  \"reference_id\": \"psc_b452d4712c0d8c1d\",\n  \"event_type\": {\n    \"id\": \"postcard.created\",\n    \"enabled_for_test\": true,\n    \"resource\": \"postcards\",\n    \"object\": \"event_type\"\n  },\n  \"date_created\": \"2018-10-16T03:40:25.050Z\",\n  \"object\": \"event\"\n}";

            var serviceCollection = GetServiceProvider();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var serializerSettings = serviceProvider.GetService<LobSerializerSettings>();
            var mvcSerializerSettings = new JsonSerializerSettings();
            mvcSerializerSettings.Converters.Add(new LobJsonConverter(serializerSettings));

            var serializer = JsonSerializer.Create(mvcSerializerSettings);

            var jObject = JObject.Parse(eventPostcardCreated);
            var result = jObject.ToObject<LobEvent<PostcardResponse>>(serializer);

            Assert.Equal(new DateTime(2018, 10, 16, 3, 40, 25, 50, DateTimeKind.Utc), result.DateCreated);
            Assert.True(result.EventType.EnabledForTest);
            Assert.Equal("postcard.created", result.EventType.Id);
            Assert.Equal("event_type", result.EventType.Object);
            Assert.Equal(EventTypeResource.Postcards, result.EventType.Resource);
            Assert.Equal("evt_015ad20e7d049d75", result.Id);
            Assert.Equal("event", result.Object);
            Assert.Equal("psc_b452d4712c0d8c1d", result.ReferenceId);

            Assert.IsType<PostcardResponse>(result.Body);
            var postcardResponse = result.Body as PostcardResponse;
            Assert.Equal("psc_b452d4712c0d8c1d", postcardResponse.Id);
        }

        [Fact]
        public void LobEvent_Non_Generic()
        {
            var eventPostcardCreated = "{\n  \"id\": \"evt_015ad20e7d049d75\",\n  \"body\": {\n    \"id\": \"psc_b452d4712c0d8c1d\",\n    \"description\": \"Test Postcard\",\n    \"metadata\": {},\n    \"to\": {\n      \"id\": \"adr_6d05c676a4b4dfd4\",\n      \"description\": \"Test Address\",\n      \"name\": \"Larry Lobster\",\n      \"address_line1\": \"123 Test St\",\n      \"address_line2\": \"Unit 1\",\n      \"address_city\": \"San Francisco\",\n      \"address_state\": \"CA\",\n      \"address_zip\": \"94107\",\n      \"address_country\": \"UNITED STATES\",\n      \"metadata\": {},\n      \"date_created\": \"2018-10-16T00:24:28.415Z\",\n      \"date_modified\": \"2018-10-16T00:24:28.415Z\",\n      \"object\": \"address\"\n    },\n    \"from\": {\n      \"id\": \"adr_18f12a54018d3ff8\",\n      \"description\": \"Test Address\",\n      \"name\": \"Larry Lobster\",\n      \"address_line1\": \"123 Test St\",\n      \"address_line2\": \"Unit 1\",\n      \"address_city\": \"San Francisco\",\n      \"address_state\": \"CA\",\n      \"address_zip\": \"94107\",\n      \"address_country\": \"UNITED STATES\",\n      \"metadata\": {},\n      \"date_created\": \"2018-10-16T00:24:28.415Z\",\n      \"date_modified\": \"2018-10-16T00:24:28.415Z\",\n      \"object\": \"address\"\n    },\n    \"url\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_b452d4712c0d8c1d.pdf?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1542253225&Signature=P%2FlAod1qVa7hz1C77brdLAQeDWM%3D\",\n    \"carrier\": \"USPS\",\n    \"tracking_events\": [],\n    \"thumbnails\": [\n      {\n        \"small\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_b452d4712c0d8c1d_thumb_small_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1542253225&Signature=O5n2r3XcsjEMTnPZ3EliPeRZVhY%3D\",\n        \"medium\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_b452d4712c0d8c1d_thumb_medium_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1542253225&Signature=UVI4VB82sePQYIcLEBZ5w%2BqgM5I%3D\",\n        \"large\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_b452d4712c0d8c1d_thumb_large_1.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1542253225&Signature=rZURcSbBx0S2TJ97EBNhpPCUcBM%3D\"\n      },\n      {\n        \"small\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_b452d4712c0d8c1d_thumb_small_2.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1542253225&Signature=Zsa86DiDPOf8tAXzZqWvVFWMpHM%3D\",\n        \"medium\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_b452d4712c0d8c1d_thumb_medium_2.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1542253225&Signature=84bWFvKq8GTFY6QTncM3aLTB%2BCY%3D\",\n        \"large\": \"https://s3.us-west-2.amazonaws.com/assets.lob.com/psc_b452d4712c0d8c1d_thumb_large_2.png?AWSAccessKeyId=AKIAIILJUBJGGIBQDPQQ&Expires=1542253225&Signature=e67NqdnzmTrPamtgQx82DM1Gtis%3D\"\n      }\n    ],\n    \"size\": \"4x6\",\n    \"mail_type\": \"usps_first_class\",\n    \"expected_delivery_date\": \"2018-10-23\",\n    \"date_created\": \"2018-10-16T00:24:28.415Z\",\n    \"date_modified\": \"2018-10-16T00:24:28.415Z\",\n    \"send_date\": \"2018-10-16T00:24:28.415Z\",\n    \"object\": \"postcard\"\n  },\n  \"reference_id\": \"psc_b452d4712c0d8c1d\",\n  \"event_type\": {\n    \"id\": \"postcard.created\",\n    \"enabled_for_test\": true,\n    \"resource\": \"postcards\",\n    \"object\": \"event_type\"\n  },\n  \"date_created\": \"2018-10-16T03:40:25.050Z\",\n  \"object\": \"event\"\n}";

            var serviceCollection = GetServiceProvider();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var serializerSettings = serviceProvider.GetService<LobSerializerSettings>();
            var mvcSerializerSettings = new JsonSerializerSettings();
            mvcSerializerSettings.Converters.Add(new LobJsonConverter(serializerSettings));

            var serializer = JsonSerializer.Create(mvcSerializerSettings);

            var jObject = JObject.Parse(eventPostcardCreated);
            var result = jObject.ToObject<LobEvent>(serializer);

            Assert.Equal(new DateTime(2018, 10, 16, 3, 40, 25, 50, DateTimeKind.Utc), result.DateCreated);
            Assert.True(result.EventType.EnabledForTest);
            Assert.Equal("postcard.created", result.EventType.Id);
            Assert.Equal("event_type", result.EventType.Object);
            Assert.Equal(EventTypeResource.Postcards, result.EventType.Resource);
            Assert.Equal("evt_015ad20e7d049d75", result.Id);
            Assert.Equal("event", result.Object);
            Assert.Equal("psc_b452d4712c0d8c1d", result.ReferenceId);

            Assert.IsType<JObject>(result.Body);
            var resultJObject = result.Body as JObject;
            Assert.Equal("psc_b452d4712c0d8c1d", resultJObject["id"]);

            var postcardResponse = result.ToPostcard();
            Assert.NotNull(postcardResponse);
            Assert.IsType<PostcardResponse>(postcardResponse.Body);
            Assert.Equal("psc_b452d4712c0d8c1d", postcardResponse.Body.Id);
            Assert.Equal("123 Test St", postcardResponse.Body.From.AddressLine1);

            Assert.NotNull(result.SerializerSettings);
        }
    }
}
