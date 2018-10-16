using System;
using System.Collections.Generic;

namespace Lob.Net.Models
{
    public class LetterRequest
    {
        public string Description { get; set; }
        public AddressReference To { get; set; }
        public AddressReference From { get; set; }
        public bool Color { get; set; }
        public string File { get; set; }
        public IDictionary<string, string> MergeVariables { get; set; }
        public bool DoubleSided { get; set; } = true;
        public AddressPlacement AddressPlacement { get; set; } = AddressPlacement.TopFirstPage;
        public bool ReturnEnvelope { get; set; }
        public int? PerforatedPage { get; set; }
        public MailType MailType { get; set; } = MailType.UspsFirstClass;
        public ExtraService? ExtraService { get; set; }
        public DateTime? SendDate { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
    }
}
