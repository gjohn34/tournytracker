﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Model;


namespace TrackerLibrary.DataAccess
{
    public interface IDataConnection
    {
        PrizeModel CreatePrize(PrizeModel model);
        PersonModel CreatePerson(PersonModel model);
        List<PersonModel> GetPerson_All();
        List<TournamentModel> GetTournament_All();
        List<TeamModel> GetTeam_All();
        TeamModel CreateTeam(TeamModel model);
        TournamentModel CreateTournament(TournamentModel model);
        void UpdateMatchup(MatchupModel model);
    }
}
