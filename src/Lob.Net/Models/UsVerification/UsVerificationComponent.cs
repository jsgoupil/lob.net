using Newtonsoft.Json;

namespace Lob.Net.Models
{
    public class UsVerificationComponent
    {
        public string PrimaryNumber { get; set; }
        public StreetDirection? StreetPredirection { get; set; }
        public string StreetName { get; set; }
        public string StreetSuffix { get; set; }
        public StreetDirection? StreetPostdirection { get; set; }
        public string SecondaryDesignator { get; set; }
        public string SecondaryNumber { get; set; }
        public string PmbDesignator { get; set; }
        public string PmbNumber { get; set; }
        public string ExtraSecondaryDesignator { get; set; }
        public string ExtraSecondaryNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        [JsonProperty("zip_code_plus_4")]
        public string ZipCodePlus4 { get; set; }
        public ZipCodeType ZipCodeType { get; set; }
        public string DeliveryPointBarcode { get; set; }
        public AddressType? AddressType { get; set; }
        public RecordType? RecordType { get; set; }
        public bool DefaultBuildingAddress { get; set; }
        public string County { get; set; }
        public string CountyFips { get; set; }
        public string CarrierRoute { get; set; }
        public CarrierRouteType? CarrierRouteType { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
