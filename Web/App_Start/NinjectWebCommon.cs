using Core.Interfaces;
using Core.Interfaces.Repositories.BaseData;
using Core.Interfaces.Repositories.Business;
using Infrastructure;
using Infrastructure.Identity;
using Infrastructure.Repositories.BaseData;
using Infrastructure.Repositories.Business;
using Infrastructure.Services;
using Microsoft.AspNet.Identity;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Web.App_Start.NinjectWebCommon), "Stop")]

namespace Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Core.Domain.Identity;
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
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<IContactRepository>().To<ContactRepository>().InRequestScope();
            kernel.Bind<ICurrencyRepository>().To<CurrencyRepository>().InRequestScope();
            kernel.Bind<ICountryRepository>().To<CountryRepository>().InRequestScope();
            kernel.Bind<IPartnerRepository>().To<PartnerRepository>().InRequestScope();
            kernel.Bind<IInstitutionRepository>().To<InstitutionRepository>().InRequestScope();
            kernel.Bind<IAuthenticationService>().To<AuthenticationService>().InRequestScope();
            kernel.Bind(typeof(UserManager<User>)).ToSelf().InRequestScope();
            kernel.Bind<IUserStore<User>>().To<ApplicationUserStore>().InRequestScope();
        }
    }
}
