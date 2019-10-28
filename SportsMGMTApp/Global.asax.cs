using SportsMGMTApp.App_Start;
using SportsMGMTBLL.IOC;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SportsMGMTApp
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //resolve the interfaces you made
            //install castle.windsor.mvc to auto gen the controller factory to instantiate them at app start in assembly
            //it also needs to be here
            ContainerBootstrapper.Bootstrap();
        }
        protected void Application_AcquireRequestState(object sender, EventArgs e)
       {
            string UserName = Session["UserName"] as string;
            string Sessroles = Session["Roles"] as string;
            if (string.IsNullOrEmpty(UserName))
            {
                return;
            }
            //the i is the stored session of the username and thier custom type which my custom type 
            GenericIdentity i = new GenericIdentity(UserName, "MyCustomType");
            if (Sessroles == null)  {Sessroles = "";}
            string[] roles = Sessroles.Split(',');
            GenericPrincipal p = new GenericPrincipal(i, roles);
            HttpContext.Current.User = p;
        }
    }
}
