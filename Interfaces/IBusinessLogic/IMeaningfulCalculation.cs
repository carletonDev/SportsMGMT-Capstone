using System;
using System.Collections.Generic;
using SportsMGMTCommon;

namespace SportsMGMTBLL
{
    public interface IMeaningfulCalculation
    {
        List<Game> AllGames(List<Game> homeGames, List<Game> awayGame);
        void AverageSalary(DashBoard dashboard, List<Users> getUsers);
        void ContractExpires(Users users, DashBoard dashboard);
        string FormatGameName(Game game, Users user);
        void GameTime(Users users, DashBoard dashboard, List<Game> weeklyGame);
        string HexidecimalRandom(Random r);
        Team MyTeam(Users users);
        void MyTeamName(Users users, DashBoard dashboard, Team freeAgent);
        List<Practice> PracticeByUser(Users users);
        void PracticeTime(Users users, DashBoard dashboard, List<Practice> WeeklyPractice);
        string PracticeTypeFormat(Practice practice);
        string[] RandomColor(int count);
        DashBoard ReturnDashBoard(Users users);
    }
}