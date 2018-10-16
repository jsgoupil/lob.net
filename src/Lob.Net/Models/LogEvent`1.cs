using System;

namespace Lob.Net.Models
{
    public class LobEvent<T>
    {
        public string Id { get; set; }
        public T Body { get; set; }
        public string ReferenceId { get; set; }
        public EventType EventType { get; set; }
        public DateTime DateCreated { get; set; }
        public string Object { get; set; }
    }
}
