using Castle.Windsor;
using Castle.Windsor.Installer;
using Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SportsMGMTBLL.IOC
{
    public static  class Reslover
    {
        public static WindsorContainer Resolve()
        {
            // application starts...
            var container = new WindsorContainer();

            // adds and configures all components using WindsorInstallers from executing assembly
            container.Install(FromAssembly.This());
            //resolve registered service in the i winsor container install method 
            container.Resolve<IAttendanceDataAccess>();
            container.Resolve<IContractsDataAccess>();
            container.Resolve<IExceptions>();
            container.Resolve<IGameDataAccess>();
            container.Resolve<IPlayerStatsDA>();
            container.Resolve<IPracticeDataAccess>();
 
            return container;
        }
        //instantiate interfaces here

    }
}
