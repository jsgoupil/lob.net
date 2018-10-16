using System;
using System.Collections.Generic;

namespace Lob.Net.Models
{
    public class CheckResponse
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public int CheckNumber { get; set; }
        public string Memo { get; set; }
        public decimal Amount { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public string CheckBottomTemplateId { get; set; }
        public string AttachmentTemplateId { get; set; }
        public string CheckBottomTemplateVersionId { get; set; }
        public string AttachmentTemplateVersionId { get; set; }
        public AddressResponse To { get; set; }
        public AddressResponse From { get; set; }
        public BankAccountResponse BankAccount { get; set; }
        public Carrier Carrier { get; set; }
        public string TrackingNumber { get; set; }
        public TrackingEvent[] TrackingEvents { get; set; }
        public Thumbnail[] Thumbnails { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public MailType MailType { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime SendDate { get; set; }
        public bool Deleted { get; set; }
        public string Object { get; set; }
    }
}
