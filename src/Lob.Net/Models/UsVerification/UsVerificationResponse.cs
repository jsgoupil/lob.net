namespace Lob.Net.Models
{
    public class UsVerificationResponse
    {
        public string Id { get; set; }
        public string Recipient { get; set; }
        public string PrimaryLine { get; set; }
        public string SecondaryLine { get; set; }
        public string Urbanization { get; set; }
        public string LastLine { get; set; }
        public UsDeliverability Deliverability { get; set; }
        public UsVerificationComponent Components { get; set; }
        public UsDeliverabilityAnalysis DeliverabilityAnalysis { get; set; }
        public string Object { get; set; }
    }
}
