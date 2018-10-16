using System;
using System.Collections.Generic;

namespace Lob.Net.Models
{
    public class LetterResponse
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public AddressResponse To { get; set; }
        public AddressResponse From { get; set; }
        public bool Color { get; set; }
        public bool DoubleSided { get; set; }
        public AddressPlacement AddressPlacement { get; set; }
        public bool ReturnEnvelope { get; set; }
        public int? PerforatedPage { get; set; }
        public ExtraService? ExtraService { get; set; }
        public MailType MailType { get; set; }
        public string Url { get; set; }
        public string TemplateId { get; set; }
        public string TemplateVersionId { get; set; }
        public Carrier Carrier { get; set; }
        public string TrackingNumber { get; set; }
        public TrackingEvent[] TrackingEvents { get; set; }
        public Thumbnail[] Thumbnails { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime SendDate { get; set; }
        public bool Deleted { get; set; }
        public string Object { get; set; }
    }
}
