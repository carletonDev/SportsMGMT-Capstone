using Castle.Windsor;
using Castle.Windsor.Installer;
using Interfaces.IBusinessLogic;
using Interfaces.IDataAccess;

using SportsMGMTDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Unity;

namespace SportsMGMTBLL.IOC
{
    public static  class Resolver
    {
        public static UnityContainer Resolve()
        {
            // application starts...
            var container = new UnityContainer();
            container.RegisterType<IAttendanceDataAccess, AttendanceDataAccess>();
            container.RegisterType<IContractsDataAccess, ContractsDataAccess>();
            container.RegisterType<IExceptions, ExeceptionDataAccess>();
            container.RegisterType<IGameDataAccess, GameDataAccess>();
            container.RegisterType<IPlayerStatsDA, PlayerStatsDA>();
            container.RegisterType<IPracticeDataAccess, PracticeDataAccess>();
            container.RegisterType<IRolesDataAccess, RolesDataAccess>();
            container.RegisterType<ITeamDataAccess, TeamDataAccess>();
            container.RegisterType<IUsersDataAcesss, UsersDataAccess>();
            container.RegisterType<IAttendanceBLL, AttendanceBLL>();
            container.RegisterType<IContracts,ContractsBLL>();
            container.RegisterType<IExceptionsBLL, ExceptionLogBLL>();
            container.RegisterType<IGame, GameBLL>();
            container.RegisterType<IPlayerStats, PlayerStatsBLL>();
            container.RegisterType<IPractice, PracticeBLL>();
            container.RegisterType<IRole, RolesBLL>();
            container.RegisterType<ITeam, TeamBLL>();
            container.RegisterType<IUser, UsersBLL>();
            container.RegisterType<IMeaningfulCalculation, MeaningfulCalculation>();

            return container;
            
        }


    }
}
