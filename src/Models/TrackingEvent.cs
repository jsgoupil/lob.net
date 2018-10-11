using System;

namespace Lob.Net.Models
{
    public class TrackingEvent
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime Time { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string Object { get; set; }
    }
}
