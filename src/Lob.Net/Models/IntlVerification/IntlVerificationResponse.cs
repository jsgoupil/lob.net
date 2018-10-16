namespace Lob.Net.Models
{
    public class IntlVerificationResponse
    {
        public string Id { get; set; }
        public string Recipient { get; set; }
        public string PrimaryLine { get; set; }
        public string SecondaryLine { get; set; }
        public string LastLine { get; set; }
        public string Country { get; set; }
        public IntlDeliverability Deliverability { get; set; }
        public IntlVerificationComponent Components { get; set; }
        public string Object { get; set; }
    }
}
