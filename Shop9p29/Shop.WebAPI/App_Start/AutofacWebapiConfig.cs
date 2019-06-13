using Autofac;
using Autofac.Integration.WebApi;
using Shop.BLL.Modules;
using System.Reflection;
using System.Web.Http;

namespace Shop.WebAPI.App_Start
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //!!!!!!!!!!!!!!!!регистрируем ВСЕ контроллеры
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //!!!!!!!!!!!!!!!!регистрируем наш  модуль
            builder.RegisterModule(new ServiceModule());
            Container = builder.Build();

            return Container;
        }
    }
}