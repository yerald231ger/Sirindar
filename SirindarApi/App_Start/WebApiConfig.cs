using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Sirindar.Core.Repositories;
using Sirindar.Core.UnitOfWork;
using Sirindar.Entity;
using Sirindar.Entity.Repositories;
using SirindarApi.App_Start;

namespace SirindarApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<IHorarioRepository, HorarioRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDeportistaRepository, DeportistaRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IAsistenciaRepository, AsistenciaRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
            // Configuración y servicios de Web API
            // Configure Web API para usar solo la autenticación de token de portador.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Rutas de Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
