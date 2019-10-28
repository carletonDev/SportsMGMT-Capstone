using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;

namespace SportsMGMTApp.Plumbing
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        readonly IWindsorContainer container;

        public WindsorControllerFactory(IWindsorContainer container)
        {
            this.container = container;
        }
        //gets the requests abstract for any controller resolves the controller type and gets or posts the instance of the controller auto gen castle.windsor perfect for mvc5! just need to di the backend
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {            
			if (controllerType != null && container.Kernel.HasComponent(controllerType))
				return (IController)container.Resolve(controllerType);

			return base.GetControllerInstance(requestContext, controllerType);
        }

        public override void ReleaseController(IController controller)
        {
            container.Release(controller);
        }
    }
}