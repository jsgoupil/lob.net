using System;
using System.Collections.Generic;

namespace Lob.Net.Models
{
    public class CheckRequest
    {
        public string Description { get; set; }
        public AddressReference To { get; set; }
        public AddressReference From { get; set; }
        public string BankAccount { get; set; }
        public decimal Amount { get; set; }
        public string Memo { get; set; }
        public int? CheckNumber { get; set; }
        public string Logo { get; set; }
        public string Message { get; set; }
        public string CheckBottom { get; set; }
        public string Attachment { get; set; }
        public IDictionary<string, string> MergeVariables { get; set; }
        public MailType MailType { get; set; } = MailType.UspsFirstClass;
        public DateTime? SendDate { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
    }
}
