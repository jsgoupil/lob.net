using System.Collections.Generic;

namespace Lob.Net.Models
{
    public class TemplateRequest
    {
        public string Description { get; set; }
        public string Html { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
    }
}
