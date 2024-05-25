using Newtonsoft.Json;
using System.Text;

namespace PaximumTest.PxmModels.HotelListModels
{
    public class HotelListProcess
    {
        public string[] GetHotelList(string token)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://service.stage.paximum.com/v2/api/productservice/pricesearch");
            request.Method = HttpMethod.Post;

            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "TestUser Paximum");
            request.Headers.Add("Authorization", "Bearer "+token);

            var bodyString = "{  \"checkAllotment\": true,  \"checkStopSale\": true,  \"getOnlyDiscountedPrice\": false,  \"getOnlyBestOffers\": true,  \"productType\": 2,  \"nationality\": \"DE\",   \"checkIn\": \"2024-06-16\",   \"night\":\"2\",  \"currency\": \"TRY\",  \"culture\": \"tr-TR\",  \"arrivalLocations\": [    {      \"id\": \"23472\",      \"type\": \"City\"    }     ],  \"roomCriteria\": [    {      \"adult\": 2,      \"childAges\": []    }  ]}";
            var content = new StringContent(bodyString, Encoding.UTF8, "application/json");
            request.Content = content;

            var response = client.SendAsync(request);
            var result = response.Result.Content.ReadAsStringAsync();
            var main = JsonConvert.DeserializeObject<Main>(result.Result);

           var hotelNameList= main.Body.Hotels.Select(x => x.Name).Distinct();

            return hotelNameList.ToArray();
        }
    }
}
