using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
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
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IAttendanceDataAccess>().ImplementedBy<AttendanceDataAccess>(),
                Component.For<IContractsDataAccess>().ImplementedBy<ContractsDataAccess>(),
                Component.For<IExceptions>().ImplementedBy<ExeceptionDataAccess>(),
                Component.For<IGameDataAccess>().ImplementedBy<GameDataAccess>(),
                Component.For<IPlayerStatsDA>().ImplementedBy<PlayerStatsDA>(),
                Component.For<IPracticeDataAccess>().ImplementedBy<PracticeDataAccess>(),
                Component.For<IRolesDataAccess>().ImplementedBy<RolesDataAccess>(),
                Component.For<ITeamDataAccess>().ImplementedBy<TeamDataAccess>(),
                Component.For<IUsersDataAcesss>().ImplementedBy<UsersDataAccess>(),
                Component.For<IContracts>().ImplementedBy<ContractsBLL>(),
                Component.For<IAttendanceBLL>().ImplementedBy<AttendanceBLL>(),
                Component.For<IGame>().ImplementedBy<GameBLL>(),
                Component.For<IPractice>().ImplementedBy<PracticeBLL>(),
                Component.For<IPlayerStats>().ImplementedBy<PlayerStatsBLL>(),
                Component.For<IRole>().ImplementedBy<RolesBLL>(),
                Component.For<IUser>().ImplementedBy<UsersBLL>(),
                Component.For<ITeam>().ImplementedBy<TeamBLL>(),
                Component.For<IExceptionsBLL>().ImplementedBy<ExceptionLogBLL>()
                );
        }
    }
    
}
