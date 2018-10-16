namespace Lob.Net.Models
{
    public class UsAutocompletionResponse
    {
        public string Id { get; set; }
        public UsAutocompletionSuggestion[] Suggestions { get; set; }
        public string Object { get; set; }
    }
}
