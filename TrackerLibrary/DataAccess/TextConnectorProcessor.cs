using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Model;

namespace TrackerLibrary.DataAccess.TextHelpers
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string filename)
        {
            return $"{ConfigurationManager.AppSettings["filePath"]}\\{filename}";
        }
        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        public static List<PrizeModel> ConvertToPrizeModels(this List<string> lines)
        {
            List<PrizeModel> output = new List<PrizeModel>();

            foreach(string line in lines)
            {
                string[] cols = line.Split(',');

                PrizeModel prize = new PrizeModel
                {
                    Id = int.Parse(cols[0]),
                    PlaceNumber = int.Parse(cols[1]),
                    PlaceName = cols[2],
                    PrizeAmount = decimal.Parse(cols[3]),
                    PrizePercentage = double.Parse(cols[4])
                };
                output.Add(prize);
            }
            return output;
        }

        public static List<PersonModel> ConvertToPersonModels(this List<string> lines)
        {
            List<PersonModel> output = new List<PersonModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                PersonModel prize = new PersonModel
                {
                    Id = int.Parse(cols[0]),
                    FirstName = cols[1],
                    LastName = cols[2],
                    EmailAddress = cols[3],
                    CellPhoneNumber = cols[4]
                };
                output.Add(prize);
            }
            return output;
        }

        public static List<TeamModel> ConvertToTeamModels(this List<string> lines, string peopleFileName)
        {
            List<TeamModel> output = new List<TeamModel>();
            List<PersonModel> people = peopleFileName.FullFilePath().LoadFile().ConvertToPersonModels();
            PersonModel[] peopleArray = people.ToArray();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                TeamModel team = new TeamModel
                {
                    Id = int.Parse(cols[0]),
                    TeamName = cols[1]
                };

                string[] ids = cols[2].Split('|');
                foreach (string id in ids)
                {
                    PersonModel person = Array.Find(peopleArray, x => x.Id == int.Parse(id));
                    team.TeamMembers.Add(person);
                    //team.TeamMembers.Add((PersonModel)people.Where(x => x.Id == int.Parse(id)));
                    //team.TeamMembers.Add(people.Where(x => x.Id == int.Parse(id)).First());
                }
                output.Add(team);
            }
            return output;
        }

        public static List<TournamentModel> ConvertToTournamentModels(
            this List<string> lines,
            string teamFileName, 
            string peopleFileName, 
            string prizeFileName
            )
        {
            List<TournamentModel> tournaments = new List<TournamentModel>();
            List<TeamModel> teams = teamFileName.FullFilePath().LoadFile().ConvertToTeamModels(peopleFileName);
            TeamModel[] teamArray = teams.ToArray();
            List<PrizeModel> prizes = prizeFileName.FullFilePath().LoadFile().ConvertToPrizeModels();
            PrizeModel[] prizeArray = prizes.ToArray();
            List<MatchupModel> matchups = GlobalConfig.MatchupFile.FullFilePath().LoadFile().ConvertToMatchupModels();
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                TournamentModel tournament = new TournamentModel
                {
                    Id = int.Parse(cols[0]),
                    TournamentName = cols[1],
                    EntryFee = decimal.Parse(cols[2]),
                };

                // Adding Team
                string[] teamIds = cols[3].Split('|');
                foreach (string id in teamIds)
                {
                    TeamModel team = Array.Find(teamArray, x => x.Id == int.Parse(id));
                    tournament.EnteredTeams.Add(team);
                }

                // Adding Prizes

                string[] prizesId = cols[4].Split('|');
                foreach (string id in prizesId)
                {
                    PrizeModel prize = Array.Find(prizeArray, x => x.Id == int.Parse(id));
                    tournament.Prizes.Add(prize);
                }

                // TODO - Adding Rounds
                string[] rounds = cols[5].Split('|');
                foreach (string round in rounds)
                {
                    List<MatchupModel> tournamentRounds = new List<MatchupModel>();
                    string[] matchupIds = round.Split('^');
                    foreach (string id in matchupIds)
                    {
                        tournamentRounds.Add(matchups.Find(x => x.Id == int.Parse(id)));
                    }
                    tournament.Rounds.Add(tournamentRounds);
                }
                tournaments.Add(tournament);
            }

            return tournaments;
        }
        public static List<MatchupModel> ConvertStringToMatchupModels(this List<string> lines)
        {
            // cols
            // id=0, entires=1 (pipe delimited by id), winner=2, matchupRound=3
            List<MatchupModel> output = new List<MatchupModel>();


            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                MatchupModel model = new MatchupModel();

                model.Id = int.Parse(cols[0]);
                model.Entries = ConvertStringToMatchupEntryModels(cols[1]);
                if (cols[2] == "")
                {
                    model.Winner = null;
                } else
                {
                    model.Winner = GenerateTeamById(int.Parse(cols[2]));
                }
                model.MatchupRound = int.Parse(cols[3]);
                output.Add(model);
            }
            return output;
        }
        private static string ConvertTeamMembersToString(List<PersonModel> teamMembers)
        {
            string teamMemberIds = "";
            if (teamMembers.Count == 0)
            {
                return "";
            }
            foreach (PersonModel teamMember in teamMembers)
            {
                teamMemberIds += $"{teamMember.Id}|";
            }
            teamMemberIds = teamMemberIds.TrimEnd('|');

            return teamMemberIds;
        }
        private static string ConvertMatchupEntriesToString(List<MatchupEntryModel> entries)
        {
            string entryIds = "";
            if (entries.Count == 0)
            {
                return "";
            }
            foreach (MatchupEntryModel entry in entries)
            {
                entryIds += $"{entry.Id}|";
            }
            entryIds = entryIds.TrimEnd('|');

            return entryIds;
        }
        private static List<MatchupEntryModel> ConvertStringToMatchupEntryModels(string input)
        {
            List<MatchupEntryModel> output = new List<MatchupEntryModel>();
            List<MatchupEntryModel> entries = GlobalConfig.MatchupEntryFile.FullFilePath().LoadFile().ConvertToMatchupEntryModels();
            string[] ids = input.Split('|');
            foreach (string id in ids)
            {
                output.Add(entries.Find(x => x.Id == int.Parse(id)));
            }
            return output;
        }
        public static List<MatchupEntryModel> ConvertToMatchupEntryModels(this List<string> lines)
        {
            List<MatchupEntryModel> output = new List<MatchupEntryModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                MatchupEntryModel entry = new MatchupEntryModel();
                entry.Id = int.Parse(cols[0]);
                if (cols[1] != "")
                {
                    entry.TeamCompeting = GenerateTeamById(int.Parse(cols[1]));

                } else
                {
                    entry.TeamCompeting = null;
                }
                entry.Score = double.Parse(cols[2]);

                int parentId = 0;
                if (int.TryParse(cols[3], out parentId))
                {
                    entry.ParentMatchup = GenerateMatchupById(parentId);
                } else
                {
                    entry.ParentMatchup = null;
                }
                output.Add(entry);
            }
            return output;
        }
        private static List<MatchupModel> ConvertToMatchupModels(this List<string> lines)
        {
            //id=0, entries =1 (pipe delimted by id), winner=2, matchupRound=3
            List<MatchupModel> output = new List<MatchupModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                MatchupModel model = new MatchupModel();
                model.Id = int.Parse(cols[0]);
                model.Entries = ConvertStringToMatchupEntryModels(cols[1]);
                if (cols[2].Length == 0)
                {
                    model.Winner = null;
                }
                else
                {
                    model.Winner = GenerateTeamById(int.Parse(cols[2]));
                }
                model.MatchupRound = int.Parse(cols[3]);
                output.Add(model);
            }
            return output;
            throw new NotImplementedException();
        }

        private static TeamModel GenerateTeamById(int id)
        {
            List<TeamModel> teams = GlobalConfig.TeamsFile.FullFilePath().LoadFile().ConvertToTeamModels(GlobalConfig.PeopleFile);
            return teams.Find(x => x.Id == id);
        }
        public static MatchupModel GenerateMatchupById(int id)
        {
            List<MatchupModel> matchups = GlobalConfig.MatchupFile.FullFilePath().LoadFile().ConvertToMatchupModels();
            return matchups.Find(x => x.Id == id);
        }
        public static void SaveToTournamentsFile(this List<TournamentModel> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach (TournamentModel model in models)
            {

                // Teams
                string teamIds = "";
                foreach (TeamModel team in model.EnteredTeams)
                {
                    teamIds += $"{team.Id}|";
                }
                teamIds = teamIds.TrimEnd('|');

                // Prizes
                string prizeIds = "";
                foreach (PrizeModel prize in model.Prizes)
                {
                    prizeIds += $"{prize.Id}|";
                }
                prizeIds = prizeIds.TrimEnd('|');

                // Rounds
                string roundList = "";
                foreach (List<MatchupModel> list in model.Rounds)
                {
                    string roundIds = "";
                    foreach (MatchupModel round in list)
                    {
                        roundIds += $"{round.Id}|";
                    }
                    roundIds = roundIds.TrimEnd('|');
                    roundList += $"{roundIds}^";
                }
                roundList = roundList.TrimEnd('^');

                lines.Add($"{model.Id},{model.TournamentName},{model.EntryFee},{teamIds},{prizeIds},{roundList}");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }
        public static void SaveToRoundsFile(this TournamentModel model, string matchupFile, string matchupEntryFile)
        {
            // Loop through each round
            // Loop through each Matchup
            // Get the ID for new matchup
            // Save the record
            // Loop through each entry
            // Get the ID
            // Save it
            foreach (List<MatchupModel> round in model.Rounds)
            {
                foreach (MatchupModel matchup in round)
                {
                    // Load all the matchups from file
                    // Get the Id + 1
                    // Save the matchup record
                    matchup.SaveMatchupToFile(matchupFile, matchupEntryFile);
                }
            }
        }
        public static void SaveMatchupToFile(this MatchupModel matchup, string matchupFile, string matchupEntryFile)
        {
            // Problem is that we are loading too fast too furious
            List<MatchupModel> matchups = GlobalConfig.MatchupFile.FullFilePath().LoadFile().ConvertToMatchupModels();

            int currentId = 1;

            if (matchups.Count > 0)
            {
                currentId = matchups.OrderByDescending(x => x.Id).First().Id + 1;
            }

            matchup.Id = currentId;

            matchups.Add(matchup);

            List<string> lines = new List<string>();

            foreach (MatchupModel model in matchups)
            {

                string winner = "";
                if (model.Winner != null)
                {
                    winner = model.Winner.Id.ToString();
                }
                lines.Add($"{model.Id},,{winner},{model.MatchupRound}");
            }
            File.WriteAllLines(GlobalConfig.MatchupFile.FullFilePath(), lines);


            foreach (MatchupEntryModel entry in matchup.Entries)
            {
                entry.SaveEntryToFile(matchupEntryFile);
            }

            lines = new List<string>();

            foreach (MatchupModel model in matchups)
            {

                string winner = "";
                if (model.Winner != null)
                {
                    winner = model.Winner.Id.ToString();
                }
                lines.Add($"{model.Id},{ConvertMatchupEntriesToString(model.Entries)},{winner},{model.MatchupRound}");
            }
            File.WriteAllLines(GlobalConfig.MatchupFile.FullFilePath(), lines);

        }
        public static void SaveEntryToFile(this MatchupEntryModel entry, string matchupEntryFile)
        {
            // Move loadFile out of function and pass entries down
            List<MatchupEntryModel> entries = GlobalConfig.MatchupEntryFile.FullFilePath().LoadFile().ConvertToMatchupEntryModels();

            int currentId = 1;

            if (entries.Count > 0)
            {
                currentId = entries.OrderByDescending(x => x.Id).First().Id + 1;
            }

            entry.Id = currentId;
            entries.Add(entry);

            List<string> lines = new List<string>();

            foreach (MatchupEntryModel model in entries)
            {
                string teamCompetingId = "";
                if (model.TeamCompeting != null)
                {
                    teamCompetingId = model.TeamCompeting.Id.ToString();
                }
                string parent = "";
                if (model.ParentMatchup != null)
                {
                    parent = model.ParentMatchup.Id.ToString();
                }
                lines.Add($"{model.Id},{teamCompetingId},{model.Score},{parent}");
            }

            File.WriteAllLines(GlobalConfig.MatchupEntryFile.FullFilePath(), lines);
        }
        public static void SaveToPrizesFile(this List<PrizeModel> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach (PrizeModel model in models)
            {
                lines.Add($"{model.Id},{model.PlaceNumber},{model.PlaceName},{model.PrizeAmount},{model.PrizePercentage}");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);

        }
        public static void SaveToPeopleFile(this List<PersonModel> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach (PersonModel model in models)
            {
                lines.Add($"{model.Id},{model.FirstName},{model.LastName},{model.EmailAddress},{model.CellPhoneNumber}");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }
        public static void SaveToTeamsFile(this List<TeamModel> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach (TeamModel model in models)
            {
                lines.Add($"{model.Id},{model.TeamName},{ConvertTeamMembersToString(model.TeamMembers)}");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }
    }
}
