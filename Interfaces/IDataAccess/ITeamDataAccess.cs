using SportsMGMTCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.IDataAccess
{
    public interface ITeamDataAccess
    {
        List<Team> GetTeams();
        void CreateTeam(Team team);
        void UpdateTeam(Team team);
        void DeleteTeam(Team team);
    }
}
