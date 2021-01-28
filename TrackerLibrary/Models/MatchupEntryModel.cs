using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Model
{
    public class MatchupEntryModel
    {
        public int Id { get; set; }
        /// <summary>
        ///     Represents one team in the matchup.
        /// </summary>
        public TeamModel TeamCompeting { get; set; }

        public int TeamCompetingId { get; set; }
        /// <summary>
        ///     Represents the score for this team.
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        ///     Represents the previous match that this team won.
        /// </summary>
        public MatchupModel ParentMatchup { get; set; }
        public int ParentMatchupId { get; set; }

    }
}
