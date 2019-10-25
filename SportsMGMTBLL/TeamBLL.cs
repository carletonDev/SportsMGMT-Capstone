

namespace SportsMGMTBLL
{
    using Interfaces.IBusinessLogic;
    using Interfaces.IDataAccess;
    using SportsMGMTCommon;
    using SportsMGMTDataAccess;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class TeamBLL:ITeam
    {
        //CRUD FOR TEAMS
        //Method to call the Team DA layer and get the current teams
        ITeamDataAccess teamData;
        public TeamBLL(ITeamDataAccess team)
        {
            teamData = team;
        }
        public List<Team> GetTeams()
        {

            List<Team> getTeams = teamData.GetTeams();

            return getTeams;
        }
        //BLL method to Create a team into the database
        public void CreateTeam(Team team)
        {

            teamData.CreateTeam(team);
        }
        public void UpdateTeam(Team team)
        {

            teamData.UpdateTeam(team);
        }
        public void DeleteTeam(Team team)
        {

            teamData.DeleteTeam(team);
        }


    }
}
