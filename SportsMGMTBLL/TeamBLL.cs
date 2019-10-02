

namespace SportsMGMTBLL
{
    using SportsMGMTCommon;
    using SportsMGMTDataAccess;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class TeamBLL
    {
        //CRUD FOR TEAMS
        //Method to call the Team DA layer and get the current teams
        public List<Team> GetTeams()
        {
            TeamDataAccess teamData = new TeamDataAccess();

            List<Team> getTeams = teamData.GetTeams();

            return getTeams;
        }
        //BLL method to Create a team into the database
        public void CreateTeam(Team team)
        {
            TeamDataAccess teamData = new TeamDataAccess();
            teamData.CreateTeam(team);
        }
        public void UpdateTeam(Team team)
        {
            TeamDataAccess teamData = new TeamDataAccess();
            teamData.UpdateTeam(team);
        }
        public void DeleteTeam(Team team)
        {
            TeamDataAccess teamData = new TeamDataAccess();
            teamData.DeleteTeam(team);
        }

        public decimal CapSpace(Team team)
        {
            TeamDataAccess teamData = new TeamDataAccess();
            return teamData.GetTeamSalaryCapRemaining(team);

        }
    }
}
