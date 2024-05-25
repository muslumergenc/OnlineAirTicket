namespace PaximumTest.PxmModels.HotelListModels
{
    public partial class Main
    {
        public Body Body { get; set; }
        public Header Header { get; set; }
    }

    public partial class Body
    {
        public Guid SearchId { get; set; }
        public DateTimeOffset ExpiresOn { get; set; }
        public Hotel[] Hotels { get; set; }
        public Details Details { get; set; }
    }

    public partial class Details
    {
        public bool EnablePaging { get; set; }
    }

    public partial class Hotel
    {
        public Geolocation Geolocation { get; set; }
        public double Stars { get; set; }
        public double Rating { get; set; }
        public BoardGroup[] Themes { get; set; }
        public Facility[] Facilities { get; set; }
        public City Location { get; set; }
        public Country Country { get; set; }
        public City City { get; set; }
        public GiataInfo GiataInfo { get; set; }
        public Offer[] Offers { get; set; }
        public string Address { get; set; }
        public BoardGroup[] BoardGroups { get; set; }
        public Board[] Boards { get; set; }
        public HotelCategory HotelCategory { get; set; }
        public bool HasThirdPartyOwnOffer { get; set; }
        public HotelThirdPartyInformation ThirdPartyInformation { get; set; }
        public long Provider { get; set; }
        public string Thumbnail { get; set; }
        public Uri ThumbnailFull { get; set; }
        public Description Description { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public partial class BoardGroup
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public partial class Board
    {
        public long? Id { get; set; }
    }

    public partial class City
    {
        public string Name { get; set; }
        public string CountryId { get; set; }
        public long Provider { get; set; }
        public bool IsTopRegion { get; set; }
        public long Id { get; set; }
    }

    public partial class Country
    {
        public string InternationalCode { get; set; }
        public string Name { get; set; }
        public long Provider { get; set; }
        public bool IsTopRegion { get; set; }
    }

    public partial class Description
    {
        public string Text { get; set; }
    }

    public partial class Facility
    {
        public bool IsPriced { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public partial class Geolocation
    {
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }

    public partial class GiataInfo
    {
        public long HotelId { get; set; }
        public long DestinationId { get; set; }
    }

    public partial class HotelCategory
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Code { get; set; }
    }

    public partial class Offer
    {
        public long Night { get; set; }
        public bool IsAvailable { get; set; }
        public long Availability { get; set; }
        public bool AvailabilityChecked { get; set; }
        public Room[] Rooms { get; set; }
        public bool IsRefundable { get; set; }
        public CancellationPolicy[] CancellationPolicies { get; set; }
        public bool ThirdPartyOwnOffer { get; set; }
        public object[] Restrictions { get; set; }
        public object[] Warnings { get; set; }
        public DateTimeOffset ExpiresOn { get; set; }
        public string OfferId { get; set; }
        public DateTimeOffset CheckIn { get; set; }
        public Price Price { get; set; }
    }

    public partial class CancellationPolicy
    {
        public long RoomNumber { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public Price Price { get; set; }
        public long Provider { get; set; }
    }

    public partial class Price
    {
        public double Amount { get; set; }
        public string Currency { get; set; }
    }

    public partial class Room
    {
        public long RoomId { get; set; }
        public string RoomName { get; set; }
        public string BoardName { get; set; }
        public BoardGroup[] BoardGroups { get; set; }
        public long StopSaleGuaranteed { get; set; }
        public long StopSaleStandart { get; set; }
        public Traveller[] Travellers { get; set; }
        public RoomThirdPartyInformation ThirdPartyInformation { get; set; }
        public bool VisiblePl { get; set; }
        public long? BoardId { get; set; }
    }

    public partial class RoomThirdPartyInformation
    {
    }

    public partial class Traveller
    {
        public long Type { get; set; }
        public long Age { get; set; }
        public string Nationality { get; set; }
    }

    public partial class HotelThirdPartyInformation
    {
        public object[] Infos { get; set; }
    }

    public partial class Header
    {
        public Guid RequestId { get; set; }
        public bool Success { get; set; }
        public DateTimeOffset ResponseTime { get; set; }
        public Message[] Messages { get; set; }
    }

    public partial class Message
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public long MessageType { get; set; }
        public string MessageMessage { get; set; }
    }
}
