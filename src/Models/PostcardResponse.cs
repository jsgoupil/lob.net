using System;
using System.Collections.Generic;

namespace Lob.Net.Models
{
    public class PostcardResponse
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public Address To { get; set; }
        public Address From { get; set; }
        public string Url { get; set; }
        public string FrontTemplateId { get; set; }
        public string BackTemplateId { get; set; }
        public string FrontTemplateVersionId { get; set; }
        public string BackTemplateVersionId { get; set; }
        public Carrier Carrier { get; set; }
        public TrackingEvent[] TrackingEvents { get; set; }
        public Thumbnail[] Thumbnails { get; set; }
        public string Size { get; set; }
        public MailType MailType { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime SendDate { get; set; }
        public bool Deleted { get; set; }
        public string Object { get; set; }
    }
}
