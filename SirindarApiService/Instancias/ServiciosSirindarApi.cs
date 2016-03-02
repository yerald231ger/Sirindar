using ServiciosCafeteria.AppModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CNSirindar.Models;
using Newtonsoft.Json;
using ServiciosCafeteria.Interfaces;
using System.Net;


namespace ServiciosCafeteria.Instancias
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

            try
            {
                var response = await httpClient.PostAsync("token", content);

                TokenModel token;

                HttpResult(response, out token);

                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
                return true;
            }
            catch (ServiciosCafeteriaInternalServerErrorException e)
            {
                throw new ServiciosCafeteriaException(e.Message);
            }
            catch (ServiciosCafeteriaException)
            {
                return false;
            }
            catch (HttpRequestException e)
            {
                throw new ServiciosCafeteriaException(e.InnerException.InnerException.Message);
            }
        }

        public async Task<IEnumerable<Horario>> Horarios()
        {
            try
            {
                var response = await httpClient.GetAsync("api/horarios");

                IEnumerable<Horario> horarios;

                HttpResult(response, out horarios);

                return horarios;
            }
            catch (HttpRequestException e)
            {
                throw new ServiciosCafeteriaException(e.InnerException.InnerException.Message);
            }
        }

        public async Task<Deportista> GetDeportista(int matriculaId)
        {
            try
            {
                var result = await httpClient.GetAsync("api/deportista/" + matriculaId);

                Deportista deportista;

                HttpResult(result, out deportista);

                return deportista;
            }
            catch (HttpRequestException e)
            {
                throw new ServiciosCafeteriaException(e.InnerException.InnerException.Message);
            }
        }

        public async Task<AsistenciaResultado> RegistrarAsistencia(Asistencia asistencia)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/asistencia/", asistencia);

                var d = response.Content.ReadAsStringAsync().Result;

                AsistenciaResultado asistenciaResultado;
                HttpResult(response, out asistenciaResultado);

                return asistenciaResultado;
            }
            catch (ServiciosCafeteriaBadRequestException e)
            {
                return new AsistenciaResultado { Aceptado = false, Razon = e.Content.ReadAsStringAsync().Result };                
            }
            catch (HttpRequestException e)
            {
                throw new ServiciosCafeteriaException(e.InnerException.InnerException.Message);
            }
        }


        private static void HttpResult<T>(HttpResponseMessage response, out T objectToReturn) where T : class
        {
            objectToReturn = null;
            switch (response.StatusCode)
            {
                case HttpStatusCode.Accepted:
                    break;
                case HttpStatusCode.BadGateway:
                    break;
                case HttpStatusCode.BadRequest:
                    objectToReturn = response.Content.ReadAsStringAsync().Result as T;
                    throw new ServiciosCafeteriaBadRequestException(response.Content, HttpStatusCode.BadRequest.ToString());
                case HttpStatusCode.Conflict:
                    break;
                case HttpStatusCode.Continue:
                    break;
                case HttpStatusCode.Created:
                    break;
                case HttpStatusCode.ExpectationFailed:
                    break;
                case HttpStatusCode.Forbidden:
                    break;
                case HttpStatusCode.Found:
                    break;
                case HttpStatusCode.GatewayTimeout:
                    break;
                case HttpStatusCode.Gone:
                    break;
                case HttpStatusCode.HttpVersionNotSupported:
                    break;
                case HttpStatusCode.InternalServerError:
                    throw new ServiciosCafeteriaInternalServerErrorException(response.Content, response.Content.ReadAsStringAsync().Result);
                case HttpStatusCode.LengthRequired:
                    break;
                case HttpStatusCode.MethodNotAllowed:
                    break;
                case HttpStatusCode.Moved:
                    break;
                case HttpStatusCode.NoContent:
                    break;
                case HttpStatusCode.NonAuthoritativeInformation:
                    break;
                case HttpStatusCode.NotAcceptable:
                    break;
                case HttpStatusCode.NotFound:
                    objectToReturn = null;
                    break;
                case HttpStatusCode.NotImplemented:
                    break;
                case HttpStatusCode.NotModified:
                    break;
                case HttpStatusCode.OK:
                    objectToReturn = response.Content.ReadAsAsync<T>().Result;
                    break;
                case HttpStatusCode.PartialContent:
                    throw new ApplicationException(HttpStatusCode.PartialContent.ToString());
                case HttpStatusCode.PreconditionFailed:
                    break;
                case HttpStatusCode.ProxyAuthenticationRequired:
                    break;
                case HttpStatusCode.RedirectKeepVerb:
                    break;
                case HttpStatusCode.RedirectMethod:
                    break;
                case HttpStatusCode.RequestEntityTooLarge:
                    break;
                case HttpStatusCode.RequestTimeout:
                    break;
                case HttpStatusCode.RequestUriTooLong:
                    break;
                case HttpStatusCode.RequestedRangeNotSatisfiable:
                    break;
                case HttpStatusCode.ResetContent:
                    break;
                case HttpStatusCode.ServiceUnavailable:
                    break;
                case HttpStatusCode.SwitchingProtocols:
                    break;
                case HttpStatusCode.Unauthorized:
                    break;
                case HttpStatusCode.UnsupportedMediaType:
                    break;
                case HttpStatusCode.Unused:
                    break;
                case HttpStatusCode.UpgradeRequired:
                    break;
                case HttpStatusCode.UseProxy:
                    break;
            }
        }
    }
    public class SirindarApi2 : ISirindarApi
    {
        public Task<bool> LogIn(LoginModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Deportista> GetDeportista(int matricula)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Horario>> Horarios()
        {
            throw new NotImplementedException();
        }

        public Task<AsistenciaResultado> RegistrarAsistencia(Asistencia asistencia)
        {
            throw new NotImplementedException();
        }
    }
}
