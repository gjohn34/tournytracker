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
                tournament.Rounds = new List<List<MatchupModel>>();

                //string[] roundList = cols[5].Split('^');
                //foreach (string round in roundList)
                //{
                //}

                //string[] rounds = cols[5].Split('^');
                //foreach (string round in rounds)
                //{
                //    string[] roundId = round.Split('|');
                //}

            }

            return tournaments;
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
                string teamMembers = "";
                foreach (PersonModel teamMember in model.TeamMembers)
                {
                    teamMembers += $"{teamMember.Id}|";
                }
                teamMembers = teamMembers.TrimEnd('|');
                lines.Add($"{model.Id},{model.TeamName},{teamMembers}");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
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
            foreach (MatchupEntryModel entry in matchup.Entries)
            {
                entry.SaveEntryToFile(matchupEntryFile);
            }

        }

        public static void SaveEntryToFile(this MatchupEntryModel entry, string matchupEntryFile)
        {

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
                entry.TeamCompeting = GenerateTeamById(int.Parse(cols[1]));
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

        public static MatchupModel GenerateMatchupById(int id)
        {
            List<MatchupModel> matchups = GlobalConfig.MatchupFile.FullFilePath().LoadFile().ConvertToMatchupModels();
            return matchups.Find(x => x.Id == id);
        }

        private static List<MatchupModel> ConvertToMatchupModels(this List<string>)
        {
            throw new NotImplementedException();
        }


        private static TeamModel GenerateTeamById(int id)
        {
            List<TeamModel> teams = GlobalConfig.TeamsFile.FullFilePath().LoadFile().ConvertToTeamModels(GlobalConfig.PeopleFile);
            return teams.Find(x => x.Id == id);
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
                model.Entries = ConvertToMatchupEntryModels(cols[1]);
                model.Winner = GenerateTeamById(int.Parse(cols[2]));
                model.MatchupRound = int.Parse(cols[3]);
                foreach (string id in cols[1].Split('|'))
                {

                }
                // Populate Entries
            }
        }



    }
}
