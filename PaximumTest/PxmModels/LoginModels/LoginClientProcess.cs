using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace PaximumTest.PxmModels.LoginModels
{
    public class LoginClientProcess
    {
        public async Task<string> GetToken()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage();
            var bodyString = "{\"Agency\": \"PXM25520\",  \"User\": \"USR1\",  \"Password\": \"test!23\"}";
            var content = new StringContent(bodyString, Encoding.UTF8, "application/json");
            request.RequestUri = new Uri("http://service.stage.paximum.com/v2/api/authenticationservice/login");
            request.Method = HttpMethod.Post;
            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "TestUser Paximum");
            request.Content = content;
            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            var token= JsonConvert.DeserializeObject<Main>(result).Body;
            return token.Token;
        }
    }
}
