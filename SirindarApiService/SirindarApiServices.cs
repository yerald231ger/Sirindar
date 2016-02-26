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
                var horarios = JsonConvert.DeserializeObject<IEnumerable<Horario>>(result);
                return horarios;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Deportista> GetDeportista(int matriculaId)
        {
            var result = await httpClient.GetStringAsync("api/deportista/" + matriculaId);
            try
            {
                var deportista = JsonConvert.DeserializeObject<Deportista>(result);
                return deportista;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<AsistenciaResultado> RegistrarAsistencia(Asistencia asistencia)
        {
            var result = await httpClient.PostAsJsonAsync("api/asistencia/", asistencia);
            try
            {
                var response = await result.Content.ReadAsAsync<AsistenciaResultado>();
                return response;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

}
