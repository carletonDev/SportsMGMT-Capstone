﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsMGMTCommon;
using SportsMGMTBLL;
using System.Collections.Generic;

namespace SportsMGMTUnitTest
{
    [TestClass]
    public class TeamUnitTest
    {
        [TestMethod]
        public void GetTeam()
        {
            //Arrange -- create BLL object and populate list with database teams
            TeamBLL teamBLL = new TeamBLL();
            List<Team> getTeams = teamBLL.GetTeams();
            //Act- check if Data is in the database
            bool check = getTeams.Exists(m => m.TeamName == "Macon SmallTowners");
            //Assert- their exists a team named Macon SmallTowners which will be true always
            Assert.IsTrue(check);
        }
        [TestMethod]
        public void CreateTeam()
        {
            //Arrange - Create a Test object for insert and create a team BLL object
            TeamBLL teamBLL = new TeamBLL();
            Team team = new Team();
            //Act --Add Data to database get list of teams check if team was added
            team.SalaryCap = 1113.000M;
            team.TeamName = "Virginia Testers";
            team.TeamType = "Test";
            teamBLL.CreateTeam(team);
            List<Team> checkTeam = teamBLL.GetTeams();
            bool check = checkTeam.Exists(m => m.TeamType == "Test");
            //Assert that the insert added was true
            Assert.IsTrue(check);
        }
        [TestMethod]
        public void UpdateTeam()
        {
            //Arrange -create a new BLL object of the TeamBLL class and change Macon SmallTowners wins to 1 and losses to 1
            TeamBLL teamBLL = new TeamBLL();
            List<Team> teams = teamBLL.GetTeams();

            //Find the team from the list that you want to update and set the wins and losses the other values will be the same
            Team team = teams.Find(m => m.TeamName == "Macon SmallTowners");
            team.Wins = 1;
            team.Losses = 1;
            teamBLL.UpdateTeam(team);
            List<Team> teamscheck = teamBLL.GetTeams();
            //Assert -that the update has been made all teams are null in wins losses as of 6/27
            Assert.IsTrue(teamscheck.Exists(m => m.Wins == 1));

        }
        [TestMethod]
        public void DeleteTeam()
        {
            //Arrange
            Team team = new Team();
            TeamBLL teamBLL = new TeamBLL();

            //Act--Delete the Test Team Virginia Testers check to see if it still exists in the list of teams from the database obj
            team.TeamName = "Virginia Testers";
            teamBLL.DeleteTeam(team);
            List<Team> getTeams = teamBLL.GetTeams();
            bool check = getTeams.Exists(m => m.TeamName == "Virginia Testers");
            //Assert the check is false since it has been deleted
            Assert.IsFalse(check);
        }
        [TestMethod]
        public void CapSpace()
        {
            //Arrange - Set up team object set it to a team in db 
            Team team = new Team();
            team.TeamName = "Macon SmallTowners";
            //call BLL
            TeamBLL teamBLL = new TeamBLL();
            //Act set capSpace field to the cap space remaining in team
            decimal capSpace = teamBLL.CapSpace(team);
            //check if it pulled information in database
            //Assert its false that it pulled the initializer 0.0M
            Assert.IsFalse(0.0M == capSpace);
        }
    }
}
