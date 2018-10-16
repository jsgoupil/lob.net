using Lob.Net.Helpers;
using Newtonsoft.Json;

namespace Lob.Net.Models
{
    [JsonConverter(typeof(EnumConverter<AddressPlacement>))]
    public enum AddressPlacement
    {
        TopFirstPage,
        InsertBlankPage
    }

    [JsonConverter(typeof(EnumConverter<ExtraService>))]
    public enum ExtraService
    {
        Certified,
        Registered
    }

    [JsonConverter(typeof(EnumConverter<MailType>))]
    public enum MailType
    {
        UspsFirstClass,
        UspsStandard,
        UpsNextDayAir
    }

    [JsonConverter(typeof(EnumConverter<Carrier>))]
    public enum Carrier
    {
        [Value("USPS")]
        USPS,
        [Value("UPS")]
        UPS
    }

    [JsonConverter(typeof(EnumConverter<AccountType>))]
    public enum AccountType
    {
        Company,
        Individual
    }

    public enum SortBy
    {
        DateCreated,
        SendDate
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }

    public enum EventTypeResource
    {
        Postcards,
        Letters,
        Checks,
        Addresses,
        BankAccounts
    }
}
