﻿using Castle.Windsor;
using Castle.Windsor.Installer;
using Interfaces.IBusinessLogic;
using Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SportsMGMTBLL.IOC
{
    public static  class Resolver
    {
        public static WindsorContainer Resolve()
        {
            // application starts...
            var container = new WindsorContainer();

            // adds and configures all components using WindsorInstallers from executing assembly
            container.Install(FromAssembly.This());
            //resolve registered service in the i winsor container install method 

            //resolve data access layer
            container.Resolve<IAttendanceDataAccess>();
            container.Resolve<IContractsDataAccess>();
            container.Resolve<IExceptions>();
            container.Resolve<IGameDataAccess>();
            container.Resolve<IPlayerStatsDA>();
            container.Resolve<IPracticeDataAccess>();
            //resolve business logic layer
            container.Resolve<IAttendanceBLL>();
            container.Resolve<IContracts>();
            container.Resolve<IGame>();
            container.Resolve<IPlayerStats>();
            container.Resolve<IPractice>();
            container.Resolve<IUser>();
            container.Resolve<ITeam>();
            container.Resolve<IRole>();
            return container;
        }
        //instantiate interfaces here

    }
}
