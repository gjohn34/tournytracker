using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Model;

namespace TrackerLibrary
{
    public static class TournamentLogic
    {
        public static void CreateRounds(TournamentModel tournament)
        {
            int teamCount = tournament.EnteredTeams.Count;
            List<TeamModel> randomizedTeams = RandomizeTeamOrder(tournament.EnteredTeams);
            int rounds = CalculateRounds(teamCount);
            int byes = CalculateByes(rounds, teamCount);

            tournament.Rounds.Add(CreateFirstRound(byes, randomizedTeams));
            CreateOtherRounds(tournament, rounds);
        }
        private static List<MatchupModel> CreateFirstRound(int byes, List<TeamModel> teams) //randomized teams
        {
            List<MatchupModel> result = new List<MatchupModel>();
            MatchupModel match = new MatchupModel();
            foreach (TeamModel team in teams)
            {
                match.Entries.Add(new MatchupEntryModel { TeamCompeting = team });
                if (byes > 0)
                {
                    byes -= 1;
                    match.Entries.Add(new MatchupEntryModel { TeamCompeting = new TeamModel() });
                }
                if (match.Entries.Count == 2)
                {
                    match.MatchupRound = 1;
                    result.Add(match);
                    match = new MatchupModel();
                    // Save complete match
                }
            }
            return result;
        }

        private static void CreateOtherRounds(TournamentModel tournament, int rounds)
        {
            int round = 2;
            List<MatchupModel> previousRound = tournament.Rounds[0];
            List<MatchupModel> currentRound = new List<MatchupModel>();
            MatchupModel currentMatchup = new MatchupModel();
            
            while (round <= rounds)
            {
                foreach (MatchupModel match in previousRound)
                {
                    currentMatchup.Entries.Add(new MatchupEntryModel { ParentMatchup = match });

                    if (currentMatchup.Entries.Count > 1)
                    {
                        currentMatchup.MatchupRound = round;
                        currentRound.Add(currentMatchup);
                        currentMatchup = new MatchupModel();

                    }
                }
                tournament.Rounds.Add(currentRound);
                previousRound = currentRound;
                currentRound = new List<MatchupModel>();
                round += 1;
            }
        }

        private static int CalculateRounds(int teamCount)
        { 
            int output = 1;
            int val = 2;

            while (val < teamCount)
            {
                output += 1;
                val *= 2;
            }
            return output;
        }

        private static List<TeamModel> RandomizeTeamOrder(List<TeamModel> teams)
        {
            return teams.OrderBy(x => Guid.NewGuid()).ToList();
        }

        private static int CalculateByes(int rounds, int teamCount)
        {
            int totalTeams = 1;
            for (int i = 1; i <= rounds; i++)
            {
                totalTeams *= 2;
            }
            return totalTeams - teamCount;
        }
        // Order list randomly of teams
        // Check listis big enough, add in bye matches
        // 2*2*2*2
        // Creaet first round of matchups /w byes
        // Create every round set after that
    }
}
