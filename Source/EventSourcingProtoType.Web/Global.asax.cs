using System.Web.Mvc;
using System.Web.Routing;
using EventSourcingProtoType.Messages;
using EventSourcingProtoType.Messages.Commands;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Routing.TypeBased;

namespace EventSourcingProtoType.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static BuiltinHandlerActivator Activator;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Activator = new BuiltinHandlerActivator();
            Configure.With(Activator)
                .Transport(t => t.UseMsmqAsOneWayClient())
                .Routing(r => r.TypeBased().MapAssemblyOf<CreateSportCommand>("Rebus"))
                .Start();
        }

        protected void Application_End()
        {
            Activator.Dispose();
        }
    }
}
