using System;
using System.Collections.Generic;

namespace Lob.Net.Models
{
    public class BankAccountResponse
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public string RoutingNumber { get; set; }
        public string AccountNumber { get; set; }
        public AccountType AccountType { get; set; }
        public string Signatory { get; set; }
        public string SignatureUrl { get; set; }
        public string BankName { get; set; }
        public bool Verified { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool Deleted { get; set; }
        public string Object { get; set; }
    }
}
