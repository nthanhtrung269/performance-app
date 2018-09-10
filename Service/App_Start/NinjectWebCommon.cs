using Core.Interfaces;
using Core.Interfaces.Repositories.BaseData;
using Core.Interfaces.Repositories.Business;
using Core.Interfaces.Repositories.System;
using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Repositories.BaseData;
using Infrastructure.Repositories.Business;
using Infrastructure.Repositories.System;
using Infrastructure.Identity;
using Microsoft.AspNet.Identity;
using Core.Domain.Identity;
using Core.Interfaces.Services;
using Core.Services;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Service.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Service.App_Start.NinjectWebCommon), "Stop")]

namespace Service.App_Start
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
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<IAccountRepository>().To<AccountRepository>().InRequestScope();
            kernel.Bind<IContactRepository>().To<ContactRepository>().InRequestScope();
            kernel.Bind<ICurrencyRepository>().To<CurrencyRepository>().InRequestScope();
            kernel.Bind<ICountryRepository>().To<CountryRepository>().InRequestScope();
            kernel.Bind<IPartnerRepository>().To<PartnerRepository>().InRequestScope();
            kernel.Bind<IInstitutionRepository>().To<InstitutionRepository>().InRequestScope();
            kernel.Bind<IInstitutionRepository>().To<InstitutionRepository>().InRequestScope();
            kernel.Bind<IPortfolioRepository>().To<PortfolioRepository>().InRequestScope();
            kernel.Bind<ITaskRepository>().To<TaskRepository>().InRequestScope();
            kernel.Bind<IAssetService>().To<AssetService>().InRequestScope();
            kernel.Bind<ISessionService>().To<SessionService>().InRequestScope();
            kernel.Bind(typeof(UserManager<User>)).ToSelf().InRequestScope();
            kernel.Bind<IUserStore<User>>().To<ApplicationUserStore>().InRequestScope();
        }        
    }
}
