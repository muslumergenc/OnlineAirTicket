namespace PaximumTest.PxmModels.LoginModels
{
   
    

        public partial class Main
        {
            public Body Body { get; set; }
            public Header Header { get; set; }
        }

        public partial class Body
        {
            public string Token { get; set; }
            public DateTimeOffset ExpiresOn { get; set; }
            public long TokenId { get; set; }
            public UserInfo UserInfo { get; set; }
            public bool LoggedInWithMasterKey { get; set; }
        }

        public partial class UserInfo
        {
            public MainAgency MainAgency { get; set; }
            public Agency Agency { get; set; }
            public Office Office { get; set; }
            public Operator Operator { get; set; }
            public Market Market { get; set; }
            public long WebSiteId { get; set; }
            public long MarketWebSiteId { get; set; }
            public bool AllowChangePassword { get; set; }
            public bool AllowCreateNewUser { get; set; }
            public bool AllowCreateAgency { get; set; }
            public bool AllowMakeReservation { get; set; }
            public bool AllowEditReservation { get; set; }
            public bool AllowCancelReservation { get; set; }
            public bool AllowB2BUpdate { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public bool AllowApiAccess { get; set; }
            public DateTimeOffset LastAccessDate { get; set; }
            public string Name { get; set; }
            public long Id { get; set; }
            public string Code { get; set; }
        }

        public partial class Agency
        {
            public long Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public string RegisterCode { get; set; }
            public string FirmName { get; set; }
            public string LicenseNo { get; set; }
            public bool OwnAgency { get; set; }
            public long PaymentFrom { get; set; }
            public string QcPcc { get; set; }
            public string QIdentNo { get; set; }
            public bool UseOwnWebSettings { get; set; }
            public bool SendFlightInfoSms { get; set; }
            public long AllowUnpaidRes { get; set; }
            public bool AceExport { get; set; }
            public string Nationality { get; set; }
            public bool AllowNon3DPayments { get; set; }
            public string WebSetGrp { get; set; }
            public bool BonusUseSysParam { get; set; }
            public bool BonusUserSeeAgencyW { get; set; }
            public bool BonusUserSeeOwnW { get; set; }
            public bool AllowAddCredit { get; set; }
            public bool ShowAgencyLogoOnB2B { get; set; }
            public bool HideInstallmentTable { get; set; }
            public bool HideAgencyCreditOnWeb { get; set; }
            public bool Force2FactorAuth { get; set; }
            public long Location { get; set; }
        }

        public partial class MainAgency
        {
            public bool OwnAgency { get; set; }
            public bool UseOwnWebSettings { get; set; }
            public bool SendFlightInfoSms { get; set; }
            public long AllowUnpaidRes { get; set; }
            public bool AceExport { get; set; }
            public bool AllowNon3DPayments { get; set; }
            public bool BonusUseSysParam { get; set; }
            public bool BonusUserSeeAgencyW { get; set; }
            public bool BonusUserSeeOwnW { get; set; }
            public bool AllowAddCredit { get; set; }
            public bool ShowAgencyLogoOnB2B { get; set; }
            public bool HideInstallmentTable { get; set; }
            public bool HideAgencyCreditOnWeb { get; set; }
            public bool Force2FactorAuth { get; set; }
            public long Location { get; set; }
        }

        public partial class Market
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Favicon { get; set; }
            public string FaviconPng { get; set; }
        }

        public partial class Office
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }

        public partial class Operator
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Thumbnail { get; set; }
            public bool AgencyCanDiscountCommission { get; set; }
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
