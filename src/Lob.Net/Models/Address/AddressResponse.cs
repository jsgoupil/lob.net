using System;
using System.Collections.Generic;

namespace Lob.Net.Models
{
    public class AddressResponse
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        // Max 20 chars
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string AddressZip { get; set; }

        // ISO-3166
        public string AddressCountry { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool Deleted { get; set; }
        public string Object { get; set; }
    }
}
