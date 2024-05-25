using ConsoleApplication1.AirService;
using ConsoleApplication1.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class AirTicketTest
    {
        internal void GenerateTicket(string airLocatorCode,string[] pricingInfoRefs)
        {
           
            AirTicketingRsp airTicketRsp;
            var airPricingInfo = new List<AirTicketingReqAirPricingInfoRef>();
            for (int i = 0; i < pricingInfoRefs.Length; i++)
            {
                var pricingInfoRef = new AirTicketingReqAirPricingInfoRef
                {
                    Key = pricingInfoRefs[i]
                };
                airPricingInfo.Add(pricingInfoRef);
            }
            var airTicketReq = new AirTicketingReq()
            {
                BillingPointOfSaleInfo = new BillingPointOfSaleInfo { OriginApplication = "uAPI" },
                AirReservationLocatorCode = new AirReservationLocatorCode { Value = airLocatorCode },
                AirPricingInfoRef = airPricingInfo.ToArray(),
                AuthorizedBy = "ZarenTravel",
                TargetBranch = CommonUtility.GetConfigValue(ProjectConstants.G_TARGET_BRANCH),
            };

            AirTicketingPortTypeClient client = new AirTicketingPortTypeClient("AirTicketingPort", WsdlService.AIR_ENDPOINT);
            client.ClientCredentials.UserName.UserName = Helper.RetrunUsername();
            client.ClientCredentials.UserName.Password = Helper.ReturnPassword();
            try
            {
                var httpHeaders = Helper.ReturnHttpHeader();
                client.Endpoint.EndpointBehaviors.Add(new HttpHeadersEndpointBehavior(httpHeaders));

                airTicketRsp = client.service(airTicketReq);
                var result = string.Join(" ", airTicketRsp.Items.Cast<TicketFailureInfo>().FirstOrDefault().Message);
               
                Console.WriteLine(result);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in Tickting : " + e.Message);
            }

        }
    }
}
