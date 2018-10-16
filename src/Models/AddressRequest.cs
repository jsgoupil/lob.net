using System;
using System.Collections.Generic;
using System.Text;

namespace Lob.Net.Models
{
    public class AddressRequest
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string AddressZip { get; set; }
        public string AddressCountry { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
    }
}
