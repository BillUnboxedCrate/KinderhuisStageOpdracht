using KinderhuisStageOpdracht.Models.DAL;
using KinderhuisStageOpdracht.Models.DAL.Mappers;
using KinderhuisStageOpdracht.Models.Domain;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(KinderhuisStageOpdracht.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(KinderhuisStageOpdracht.App_Start.NinjectWebCommon), "Stop")]

namespace KinderhuisStageOpdracht.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
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
            //Add repos here
            kernel.Bind<IGebruikerRepository>().To<GebruikerRepository>().InRequestScope();
            kernel.Bind<ITaakRepository>().To<TaakRepository>().InRequestScope();
            kernel.Bind<IMenuRepository>().To<MenuRepository>().InRequestScope();
            kernel.Bind<IOpvangtehuisRepository>().To<OpvangtehuisRepository>().InRequestScope();

            kernel.Bind<ProjectContext>().ToSelf().InRequestScope();
        }        
    }
}
