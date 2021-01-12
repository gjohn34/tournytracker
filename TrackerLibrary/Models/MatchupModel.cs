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

        /// <summary>
        /// RoundId
        /// </summary>
        public int MatchupRound { get; set; }

    }
}
