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

    [JsonConverter(typeof(EnumConverter<EventTypeResource>))]
    public enum EventTypeResource
    {
        Postcards,
        Letters,
        Checks,
        Addresses,
        BankAccounts
    }

    [JsonConverter(typeof(EnumConverter<IntlDeliverability>))]
    public enum IntlDeliverability
    {
        Deliverable,
        DeliverableMissingInfo,
        Undeliverable,
        NoMatch
    }

    [JsonConverter(typeof(EnumConverter<UsDeliverability>))]
    public enum UsDeliverability
    {
        Deliverable,
        DeliverableUnnecessaryUnit,
        DeliverableIncorrectUnit,
        DeliverableMissingUnit,
        Undeliverable
    }

    public enum StreetDirection
    {
        N,
        E,
        S,
        W,
        NE,
        SE,
        NW,
        SW
    }

    [JsonConverter(typeof(EnumConverter<ZipCodeType>))]
    public enum ZipCodeType
    {
        Standard,
        Military,
        Unique,
        PoBox
    }

    public enum AddressType
    {
        Residential,
        Commercial
    }

    [JsonConverter(typeof(EnumConverter<RecordType>))]
    public enum RecordType
    {
        Street,
        Highrise,
        Firm,
        PoBox,
        RuralRoute,
        GeneralDelivery
    }

    [JsonConverter(typeof(EnumConverter<CarrierRouteType>))]
    public enum CarrierRouteType
    {
        CityDelivery,
        RuralRoute,
        HighwayContract,
        PoBox,
        GeneralDelivery
    }

    public enum DpvConfirmation
    {
        Y,
        S,
        D,
        N
    }

    public enum BooleanState
    {
        Y,
        N
    }

    public enum DpvFootNote
    {
        AA,
        A1,
        BB,
        CC,
        N1,
        F1,
        G1,
        U1,
        M1,
        M3,
        P1,
        P3,
        R1,
        RR
    }

    public enum UsVerificationCase
    {
        Upper,
        Proper
    }
}
