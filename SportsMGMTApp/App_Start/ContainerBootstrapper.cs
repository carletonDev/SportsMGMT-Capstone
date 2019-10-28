using System;
using Castle.Windsor;
using Castle.Windsor.Installer;
using SportsMGMTBLL.IOC;

namespace SportsMGMTApp.App_Start
{
    //singleton class that installs the controller configuration for dependency injection using install method to find the register then returns itself and disposes itself at app shutdown
    public class ContainerBootstrapper : IContainerAccessor, IDisposable
    {
        readonly IWindsorContainer container;

        ContainerBootstrapper(IWindsorContainer container)
        {
            this.container = container;
        }

        public IWindsorContainer Container
        {
            get { return container; }
        }
        //need to modify this method can't run them both on app start because it configures a new container, and you need the old one to register types will run prestart though so no need for globalasax unless
        //you made a custom controller factory
        public static ContainerBootstrapper Bootstrap()
        {

            var container = Resolve.Resolver().
                Install(FromAssembly.This());
            return new ContainerBootstrapper(container);
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}