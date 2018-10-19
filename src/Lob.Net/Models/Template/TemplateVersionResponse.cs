using System;

namespace Lob.Net.Models
{
    public class TemplateVersionResponse
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Html { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool Deleted { get; set; }
        public string Object { get; set; }
    }
}
