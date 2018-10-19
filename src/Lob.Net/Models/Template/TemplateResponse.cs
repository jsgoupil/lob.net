using System;
using System.Collections.Generic;

namespace Lob.Net.Models
{
    public class TemplateResponse
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public TemplateVersionResponse[] Versions { get; set; }
        public TemplateVersionResponse PublishedVersion { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool Deleted { get; set; }
        public string Object { get; set; }
    }
}
