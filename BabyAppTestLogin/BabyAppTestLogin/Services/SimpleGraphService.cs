
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BabyAppTestLogin.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Essentials;
namespace BabyAppTestLogin.Services
{
   
         public class SimpleGraphService
        {
            public async Task<UserContext> GetUserAsync()
            {
            UserContext currentUser = new UserContext();
                using (var client = new HttpClient())
                {
                    var token = await SecureStorage.GetAsync("AccessToken");

                    if (!string.IsNullOrEmpty(token))
                    {
                        var message = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/me");
                        message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                        var response = await client.SendAsync(message);

                        if (response.IsSuccessStatusCode)
                        {
                       
                            var json = await response.Content.ReadAsStringAsync();
                            var data = (JObject)JsonConvert.DeserializeObject(json);

                            currentUser.GivenName = data["displayName"].Value<string>();
                            currentUser.JobTitle = data["jobTitle"].Value<string>();
                            currentUser.EmailAddress = data["mail"].Value<string>();

                            return currentUser;
                       
                        }
                    }
                    else
                    {
                        return null;
                    }
                }

                return null;
            }

    }

}