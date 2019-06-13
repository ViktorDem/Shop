using System.Web.Http;

namespace Shop.WebAPI.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);
        }
    }
}