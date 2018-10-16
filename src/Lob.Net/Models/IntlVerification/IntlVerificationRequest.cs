namespace Lob.Net.Models
{
    public class IntlVerificationRequest
    {
        public string Recipient { get; set; }
        public string PrimaryLine { get; set; }
        public string SecondaryLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
