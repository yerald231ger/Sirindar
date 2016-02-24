using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Configuration;
using Cafeteria.AppModels;

namespace Cafeteria.App_Start
{
    public class SirindarApi : HttpClient
    {
        private static readonly Lazy<SirindarApi> instance =
            new Lazy<SirindarApi>(() => new SirindarApi());

        private SirindarApi()
        {
        }

        public async Task<bool> LogIn(LoginModel model)
        {
            var result = await Client.PostAsJsonAsync("token", model);
            if (result.IsSuccessStatusCode)
            {
                var token = await result.Content.ReadAsAsync<TokenModel>();
                return true;
            }
            return false;
        }

        public void SetBearerToken(string token)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

       

        public static SirindarApi Client { get { return instance.Value; } }
    }
}
