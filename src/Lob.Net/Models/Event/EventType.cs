namespace Lob.Net.Models
{
    public class EventType
    {
        public string Id { get; set; }
        public bool EnabledForTest { get; set; }
        public EventTypeResource Resource { get; set; }
        public string Object { get; set; }
    }
}
