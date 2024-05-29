﻿using ConsoleApplication1.AirService;
using ConsoleApplication1.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class AirLFSTest
    {
        public static string MY_APP_NAME = "UAPI";
        private string origin = "IST";
        private string destination = "PAR";

        public LowFareSearchRsp LowFareShop(bool solutionResult)
        {
            LowFareSearchReq lowFareSearchReq = new LowFareSearchReq();
            LowFareSearchRsp lowFareSearchRsp;
            lowFareSearchReq = SetUpLFSSearch(lowFareSearchReq, solutionResult);
            AirLowFareSearchPortTypeClient client = new AirLowFareSearchPortTypeClient("AirLowFareSearchPort", WsdlService.AIR_ENDPOINT);
            client.ClientCredentials.UserName.UserName = "Universal API/uAPI3086477596-c8c010ec";
            client.ClientCredentials.UserName.Password = "cQ_4K7n&5q";
            try
            {
                var httpHeaders = Helper.ReturnHttpHeader();
                client.Endpoint.EndpointBehaviors.Add(new HttpHeadersEndpointBehavior(httpHeaders));

                lowFareSearchRsp = client.service(null, lowFareSearchReq);                
                Console.WriteLine(lowFareSearchRsp.AirSegmentList.Count());

                var numbers = lowFareSearchRsp.AirSegmentList.Select(x => x.FlightNumber).ToArray();

                Console.WriteLine("Flight Numbers: "+string.Join(", ",numbers));
                Console.WriteLine("Search Response Success");
                return lowFareSearchRsp;
            }
            catch (Exception se)
            {
                Console.WriteLine("Error : " + se.Message);
                client.Abort();
                return null;
            }
        }

        private LowFareSearchReq SetUpLFSSearch(LowFareSearchReq lowFareSearchReq, bool solutionResult)
        {
            lowFareSearchReq.TargetBranch = CommonUtility.GetConfigValue(ProjectConstants.G_TARGET_BRANCH);
            lowFareSearchReq.SolutionResult = solutionResult;  //Change it to true if you want AirPricingSolution, by default it is false
                                                      //and will send AirPricePoint in the result

            //set the GDS via a search modifier
            String[] gds = new String[] { "1G" };
            AirSearchModifiers modifiers = AirReq.CreateModifiersWithProviders(gds);

            AirReq.AddPointOfSale(lowFareSearchReq, MY_APP_NAME);

            //try to limit the size of the return... not supported by 1G!
            modifiers.MaxSolutions = string.Format("1500");
            lowFareSearchReq.AirSearchModifiers = modifiers;


            //travel is for denver to san fransisco 2 months from now, one week trip
            SearchAirLeg outbound = AirReq.CreateSearchLeg(origin, destination);
            AirReq.AddSearchDepartureDate(outbound, Helper.daysInFuture(20));
            AirReq.AddSearchEconomyPreferred(outbound);

            //coming back
            /*SearchAirLeg ret = AirReq.CreateSearchLeg(destination, origin);
            AirReq.AddSearchDepartureDate(ret, Helper.daysInFuture(65));
            //put traveller in econ
            AirReq.AddSearchEconomyPreferred(ret);*/

            lowFareSearchReq.Items = new SearchAirLeg[1];
            lowFareSearchReq.Items.SetValue(outbound, 0);
            //lowFareSearchReq.Items.SetValue(ret, 1);

            //AirPricingModifiers priceModifiers = AirReq.AddAirPriceModifiers(typeAdjustmentType.Amount, +40);

            //lowFareSearchReq.AirPricingModifiers = priceModifiers;

            lowFareSearchReq.SearchPassenger = AirReq.AddSearchPassenger();

            return lowFareSearchReq;
        }
    }
}
