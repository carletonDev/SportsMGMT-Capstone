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


namespace SportsMGMTBLL.IOC
{
    public static  class Resolve
    {
        public static void Resolver()
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
            container.Resolve<IRolesDataAccess>();
            container.Resolve<ITeamDataAccess>();
            container.Resolve<IUsersDataAcesss>();
            //resolve business logic layer
            container.Resolve<IAttendanceBLL>();
            container.Resolve<IContracts>();
            container.Resolve<IGame>();
            container.Resolve<IPlayerStats>();
            container.Resolve<IPractice>();
            container.Resolve<IUser>();
            container.Resolve<ITeam>();
            container.Resolve<IRole>();
            container.Resolve<IExceptionsBLL>();
        }
        public static IAttendanceDataAccess Attendance()
        {
            return new AttendanceDataAccess();
        }
        public static IContractsDataAccess Contracts()
        {
            return new ContractsDataAccess();
        }
        public static IExceptions Exceptions()
        {
            return new ExeceptionDataAccess();
        }
        public static IGameDataAccess Game()
        {
            return new GameDataAccess();
        }
        public static IPlayerStatsDA PlayerStats()
        {
            return new PlayerStatsDA();
        }
        public static IPracticeDataAccess Practice()
        {
            return new PracticeDataAccess();
        }
        public static IRolesDataAccess Roles()
        {
            return new RolesDataAccess();
        }
        public static ITeamDataAccess Team()
        {
            return new TeamDataAccess();
        }
        public static IUsersDataAcesss Users()
        {
            return new UsersDataAccess();
        }
        //instantiate interfaces here

    }
}
