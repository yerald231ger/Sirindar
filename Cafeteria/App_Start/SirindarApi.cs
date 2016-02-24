using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Configuration;
using Cafeteria.AppModels;
using Newtonsoft.Json;
using CNSirindar.Models;

namespace Cafeteria.App_Start
{
    public class SirindarApi
    {
        private static readonly Lazy<SirindarApi> instance =
            new Lazy<SirindarApi>(() => new SirindarApi(new HttpClient()));

        private HttpClient httpClient;

        public HttpClient Client { get { return httpClient; } }

        private SirindarApi(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = UriManager.Get("SirindarApi");
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
        }

        public async Task<bool> LogIn(LoginModel model)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", model.grant_type),
                new KeyValuePair<string, string>("username", model.username),
                new KeyValuePair<string, string>("password", model.password)
            });
           
                var result = await Client.PostAsync("token", content);
            if (result.IsSuccessStatusCode)
            {
                SetBearerToken((await result.Content.ReadAsAsync<TokenModel>()).access_token);
                return true;
            }
            return false;          

        }

        public void SetBearerToken(string token)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public static SirindarApi Instance { get { return instance.Value; } }
    }
}
