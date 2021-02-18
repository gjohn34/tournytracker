using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Model
{
    public class MatchupModel
    {
        public int Id { get; set; }

        /// <summary>
        /// List of 2 MatchupEntries for the two teams competing
        /// </summary>
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();
        /// <summary>
        /// initially empty, populated when a winner is decided
        /// </summary>
        public TeamModel Winner { get; set; }

        public int WinnerId { get; set; }

        /// <summary>
        /// RoundId
        /// </summary>
        public int MatchupRound { get; set; }

        public string DisplayName
        {
            get
            {
                string output = "";

                foreach (MatchupEntryModel entry in Entries)
                {
                    if (entry.TeamCompeting != null)
                    {
                        if (output.Length == 0)
                        {
                            output = entry.TeamCompeting.TeamName;
                        } else
                        {
                            output += $" vs {entry.TeamCompeting.TeamName}";
                        }
                    } else
                    {
                        return "Previous Winners not decided";
                    }
                }
                return output;
                //return $"{Entries[0].TeamCompeting.TeamName} vs {Entries[1].TeamCompeting.TeamName}";
            }
        }
    }
}
