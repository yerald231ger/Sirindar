using Sirindar.Core.Repositories;
using Sirindar.Entity.Repositories;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Sirindar.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Sirindar.App_Start.NinjectWebCommon), "Stop")]

namespace Sirindar.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDeporteRepository>().To<DeporteRepository>();
            kernel.Bind<IDeportistaRepository>().To<DeportistaRepository>();
            kernel.Bind<IDeporteDeportistaRepository>().To<DeporteDeportistaRepository>();
            kernel.Bind<IDependenciaRepository>().To<DependenciaRepository>();
            kernel.Bind<IAsignacionBloqueRepository>().To<AsignacionBloqueRepository>();
            kernel.Bind<IAsistenciaRepository>().To<AsistenciaRepository>();
            kernel.Bind<IGrupoAlimenticioRepository>().To<GrupoAlimenticioRepository>();
            kernel.Bind<IHorarioRepository>().To<HorarioRepository>();
            kernel.Bind<IGrupoRepository>().To<GrupoRepository>();
            kernel.Bind<IBloqueRepository>().To<BloqueRepository>();
            kernel.Bind<IClasificacionDeporteRepository>().To<ClasificacionRepository>();
        }        
    }
}
