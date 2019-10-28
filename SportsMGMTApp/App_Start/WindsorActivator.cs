using System;
using WebActivatorEx;
//before starting the application runs the  prestart method which bootstraps the controller factory, and at shutdown disposes no need to add in global asax just install castle.windsor.mvc along with castle.windsor
[assembly: PreApplicationStartMethod(typeof(SportsMGMTApp.App_Start.WindsorActivator), "PreStart")]
[assembly: ApplicationShutdownMethodAttribute(typeof(SportsMGMTApp.App_Start.WindsorActivator), "Shutdown")]

namespace SportsMGMTApp.App_Start
{
    public static class WindsorActivator
    {
        static ContainerBootstrapper bootstrapper;

        public static void PreStart()
        {
            bootstrapper = ContainerBootstrapper.Bootstrap();
        }
        
        public static void Shutdown()
        {
            if (bootstrapper != null)
                bootstrapper.Dispose();
        }
    }
}