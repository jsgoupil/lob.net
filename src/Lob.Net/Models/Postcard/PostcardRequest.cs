using System;
using System.Collections.Generic;

namespace Lob.Net.Models
{
    public class PostcardRequest
    {
        public string Description { get; set; }
        public AddressReference To { get; set; }
        public AddressReference From { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
        public IDictionary<string, string> MergeVariables { get; set; }
        public string Size { get; set; } = "4x6";
        public MailType MailType { get; set; }
        public DateTime? SendDate { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
    }
}
