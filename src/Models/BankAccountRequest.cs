using System.Collections.Generic;

namespace Lob.Net.Models
{
    public class BankAccountRequest
    {
        public string Description { get; set; }
        public string RoutingNumber { get; set; }
        public string AccountNumber { get; set; }
        public AccountType AccountType { get; set; }
        public string Signatory { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
    }
}
