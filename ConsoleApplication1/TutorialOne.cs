using ConsoleApplication1;
using ConsoleApplication1.AirService;
using ConsoleApplication1.SystemService;
using ConsoleApplication1.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UAPIConsumptionSamples
{
    class TutorialOne
    {
        static void Main(string[] args)
        {
            String payload = "this my payload; there are many like it but this one is mine";
            String someTraceId = "doesntmatter-8176";
            String originApp = "UAPI";
            PingReq req = new PingReq();
            req.Payload = payload;
            req.TraceId = someTraceId;
            ConsoleApplication1.SystemService.BillingPointOfSaleInfo billSetInfo = new ConsoleApplication1.SystemService.BillingPointOfSaleInfo();
            billSetInfo.OriginApplication = originApp;
            req.BillingPointOfSaleInfo = billSetInfo;
            req.TargetBranch = CommonUtility.GetConfigValue(ProjectConstants.G_TARGET_BRANCH);
            Console.WriteLine("PinqReq Success");
            try
            {
             
                AirLFSTest lfsTest = new AirLFSTest();
                Boolean solutionResult = false; 
                LowFareSearchRsp lowFareRsp = lfsTest.LowFareShop(solutionResult);

                if (lowFareRsp != null)
                {
                    typeBaseAirSegment[] airSegments = lowFareRsp.AirSegmentList;
                    List<typeBaseAirSegment> pricingSegments = new List<typeBaseAirSegment>();
                    IEnumerator items = lowFareRsp.Items.GetEnumerator();
                    AirPricingSolution lowestFare = null;
                    AirPricePoint lowest = null;
                    while (items.MoveNext())
                    {
                        if (solutionResult)
                        {
                            AirPricingSolution airPricingSolution = (AirPricingSolution)items.Current;
                            if (lowestFare == null)
                            {
                                lowestFare = airPricingSolution;
                            }
                            else
                            {
                                if (Helper.ConvertToDecimal(lowestFare.TotalPrice) > Helper.ConvertToDecimal(airPricingSolution.TotalPrice))
                                {
                                    lowestFare = airPricingSolution;
                                }
                            }
                        }
                        else
                        {
                            AirPricePointList airPricePointList = (AirPricePointList)items.Current;
                            if (airPricePointList != null)
                            {
                                foreach (var airPricePoint in airPricePointList.AirPricePoint)
                                {
                                    if (lowest == null)
                                    {
                                        lowest = airPricePoint;
                                    }
                                    else
                                    {
                                        if (Helper.ConvertToDecimal(lowest.TotalPrice) > Helper.ConvertToDecimal(airPricePoint.TotalPrice))
                                        {
                                            lowest = airPricePoint;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (lowestFare != null)
                    {
                        IEnumerator journeys = lowestFare.Journey.GetEnumerator();
                        while (journeys.MoveNext())
                        {
                            Journey journeyDetails = (Journey)journeys.Current;
                            if (journeyDetails != null)
                            {
                                AirSegmentRef[] segmentRef = journeyDetails.AirSegmentRef;
                                string refKey = segmentRef[0].Key;
                                IEnumerator airSegmentList = airSegments.GetEnumerator();
                                while (airSegmentList.MoveNext())
                                {
                                    typeBaseAirSegment airSeg = (typeBaseAirSegment)airSegmentList.Current;
                                    if (airSeg.Key.CompareTo(refKey) == 0)
                                    {
                                        pricingSegments.Add(airSeg);
                                        break;
                                    }

                                }
                            }
                        }
                    }
                    if (lowest != null)
                    {
                        IEnumerator pricingInfos = lowest.AirPricingInfo.GetEnumerator();

                        while (pricingInfos.MoveNext())
                        {
                            AirPricingInfo priceInfo = (AirPricingInfo)pricingInfos.Current;
                            if (priceInfo != null)
                            {
                                foreach (var flightOption in priceInfo.FlightOptionsList)
                                {
                                    FlightOption option = flightOption;
                                    IEnumerator options = option.Option.GetEnumerator();
                                    if (options.MoveNext())
                                    {
                                        Option opt = (Option)options.Current;
                                        if (opt != null)
                                        {
                                            IEnumerator bookingInfoList = opt.BookingInfo.GetEnumerator();
                                            if (bookingInfoList.MoveNext())
                                            {
                                                BookingInfo bookingInfo = (BookingInfo)bookingInfoList.Current;
                                                if (bookingInfo != null)
                                                {
                                                    String key = bookingInfo.SegmentRef;
                                                    IEnumerator airSegmentList = airSegments.GetEnumerator();
                                                    while (airSegmentList.MoveNext())
                                                    {
                                                        typeBaseAirSegment airSeg = (typeBaseAirSegment)airSegmentList.Current;
                                                        if (airSeg.Key.CompareTo(key) == 0)
                                                        {
                                                            pricingSegments.Add(airSeg);
                                                            break;
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }

                                    break;
                                }
                            }
                        }
                    }
                    AirFareDisplay fareDisplay = new AirFareDisplay();
                    AirFareDisplayRsp fareDisplayRsp = fareDisplay.GetAirFareDisplayDetails(pricingSegments);
                    Console.WriteLine(fareDisplayRsp.ResponseMessage.SelectMany(x => x.Value).ToArray());
                    fareDisplay.GetAirFareRules(fareDisplayRsp, null);
                    AirPriceRsp priceRsp = AirReq.AirPrice(pricingSegments);
                    AirPricingSolution[] lowestPrice = null;
                    if (priceRsp != null)
                    {
                        if (priceRsp.AirPriceResult != null)
                        {
                            IEnumerator priceResults = priceRsp.AirPriceResult.GetEnumerator();
                            if (priceResults.MoveNext())//We would take  the first Price Result and will Search for the lowest Price
                            {
                                AirPriceResult result = (AirPriceResult)priceResults.Current;
                                if (result.AirPricingSolution != null)
                                {
                                    if (lowestPrice == null)
                                    {
                                        lowestPrice = result.AirPricingSolution;
                                    }
                                    else
                                    {
                                        lowestPrice = result.AirPricingSolution;
                                    }

                                }
                            }
                        }
                        if (lowestPrice != null && priceRsp.AirItinerary != null)
                        {
                            AirBookTest book = new AirBookTest();
                            ConsoleApplication1.UniversalService.AirCreateReservationRsp bookResponse = book.AirBook(lowestPrice.FirstOrDefault(), priceRsp.AirItinerary);
                            if (bookResponse != null)
                            {
                                var urLocatorCode = bookResponse.UniversalRecord.LocatorCode;
                                Console.WriteLine("Universal Record Locator Code :" + urLocatorCode);
                                UniversalRetrieveTest univ = new UniversalRetrieveTest();
                                ConsoleApplication1.UniversalService.UniversalRecordRetrieveRsp univRetRsp = univ.RetrieveRecord(urLocatorCode);
                                var text= string.Join(", ", univRetRsp.UniversalRecord.Items.Select(x => x.CreateDate));
                                Console.WriteLine("Universal Record Create Date: " +text );
                                var result= AirReq.GetPNR(univRetRsp);
                               // Console.WriteLine(result.ETR);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //usually only the error message is useful, not the full stack
                //trace, since the stack trace in is your address space...
                Console.WriteLine("Error : " + e.Message);
            }
        }

    }
}
