using CheapListBackEnd.Reposiroty;
using CheapListBackEnd.Repository;
using CheapListBackEnd.RepositoryInterfaces;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace CheapListBackEnd
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IAppUsersRepository,SQLAppUsersRepository>();
            container.RegisterType<IAppGroupRepository, SQLAppGroupRepository>();
            container.RegisterType<IAppListRepository, SQLAppListRepository>();
            container.RegisterType<IAppProductRepository, SQLAppProductRepository>();
            container.RegisterType<ICitiesRepository, SQLCitiesRepository>();
            container.RegisterType<IWebCrawlerRepository, WebCrawlerRepository>();
            

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}