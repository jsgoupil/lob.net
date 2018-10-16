namespace Lob.Net.Models
{
    public class UsVerificationRequest
    {
        public UsVerificationRequest()
        {
        }

        public UsVerificationRequest(string primaryLine, string city, string state, string recipient = null, string secondaryLine = null, string urbanization = null, string zipCode = null)
            : base()
        {
            PrimaryLine = primaryLine;
            City = city;
            State = state;
            Recipient = recipient;
            SecondaryLine = secondaryLine;
            Urbanization = urbanization;
            ZipCode = zipCode;
        }

        public UsVerificationRequest(string primaryLine, string zipCode, string recipient = null, string secondaryLine = null, string urbanization = null)
            : base()
        {
            PrimaryLine = primaryLine;
            ZipCode = zipCode;
            Recipient = recipient;
            SecondaryLine = secondaryLine;
            Urbanization = urbanization;
        }

        public UsVerificationRequest(string address)
        {
            Address = address;
        }

        public string Recipient { get; set; }
        public string PrimaryLine { get; set; }
        public string SecondaryLine { get; set; }
        public string Urbanization { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
    }
}
