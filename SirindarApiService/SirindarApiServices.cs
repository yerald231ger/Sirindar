using SirindarApiService.AppModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CNSirindar.Models;
using Newtonsoft.Json;


namespace SirindarApiService
{
    public class SirindarApi : ISirindarApi
    {
        public static SirindarApi Instance { get { return instance.Value; } }

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
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (await result.Content.ReadAsAsync<TokenModel>()).access_token);
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<Horario>> Horarios()
        {
            var result = await httpClient.GetStringAsync("api/horarios");
            try
            {
                IEnumerable<Horario> horarios = JsonConvert.DeserializeObject<IEnumerable<Horario>>(result);
                return horarios;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Task<Deportista> GetDeportista(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LimiteComidas()
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerificarAsistencia()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegistrarAsistencia()
        {
            throw new NotImplementedException();
        }

        public string InstanceName
        {
            get;
            set;
        }
    }

    public class SirindarApi2 : ISirindarApi
    {

        public Task<bool> LogIn(LoginModel model)
        {
            throw new NotImplementedException();
        }

        public Task<CNSirindar.Models.Deportista> GetDeportista(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CNSirindar.Models.Horario>> Horarios()
        {
            throw new NotImplementedException();
        }

        public Task<bool> LimiteComidas()
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerificarAsistencia()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegistrarAsistencia()
        {
            throw new NotImplementedException();
        }

        public string InstanceName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
