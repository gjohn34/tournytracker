using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Model;

namespace TrackerUI
{
    public partial class TournamentViewerForm : Form
    {
        private TournamentModel tournament;
        List<int> rounds = new List<int>();
        List<MatchupModel> selectedMatchups = new List<MatchupModel>();
        //List<MatchupModel> matchups = new List<MatchupModel>();

        public TournamentViewerForm(TournamentModel tournamentModel)
        {
            InitializeComponent();

            tournament = tournamentModel;

            LoadFormData();
            LoadRounds();

        }
        private void LoadFormData()
        {
            tournamentName.Text = tournament.TournamentName;
            //roundDropDown.DataSource = tournament.Rounds;
            //roundDropDown.DisplayMember = "Id";
        }
        private void LoadMatchupsListBox()
        {
            matchupListBox.DataSource = null;
            matchupListBox.DataSource = selectedMatchups;
            if (selectedMatchups.Count == 0)
            {
                RenderForm(1, false);
                scoreButton.Visible = false;
            }
            matchupListBox.DisplayMember = "DisplayName";
        }
        private void LoadRoundDropDown()
        {
            roundDropDown.DataSource = null;
            roundDropDown.DataSource = rounds;
        }
        private void LoadRounds()
        {
            rounds = new List<int>();

            rounds.Add(1);
            int currentRound = 1;
            foreach (List<MatchupModel> matchups in tournament.Rounds)
            {
                if (matchups.First().MatchupRound > currentRound)
                {
                    currentRound = matchups.First().MatchupRound;
                    rounds.Add(currentRound);
                }
            }
            LoadRoundDropDown();
        }

        private void LoadMatchups(int round)
        {
            foreach (List<MatchupModel> matchups in tournament.Rounds)
            {
                if (matchups.First().MatchupRound == round)
                {
                    // Removing byes
                    //List<MatchupModel> noByes = matchups.Where(x => x.Entries.Count > 1).ToList();
                    //selectedMatchups = noByes;
                    if (unplayedOnlyChekBox.Checked)
                    {
                        selectedMatchups = matchups.Where(x => x.Winner == null).ToList();
                    } else
                    {
                        selectedMatchups = matchups;
                    }
                }
            }
            LoadMatchupsListBox();

        }
        private void roundDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchups((int)roundDropDown.SelectedItem);
        }
        //private void LoadListBox()
        //{
        //    matchupListBox.DataSource = null;
        //    matchupListBox.DataSource = matchups;
        //    matchupListBox.DisplayMember = "Id";
        //}
        private void RenderForm(int group, bool render)
        {
            // Team1 and Team2
            if (group == 1)
            {
                if (render)
                {
                    teamOneName.Visible = true;
                    teamOneScoreValue.Visible = true;
                    teamOneScoreLabel.Visible = true;

                    teamTwoName.Visible = false;
                    teamTwoScoreValue.Visible = false;
                    teamTwoScoreLabel.Visible = false;

                    vsLabel.Visible = false;
                    scoreButton.Visible = true;
                } 
                else
                {
                    teamOneName.Visible = false;
                    teamOneScoreValue.Visible = false;
                    teamOneScoreLabel.Visible = false;

                    teamTwoName.Visible = false;
                    teamTwoScoreValue.Visible = false;
                    teamTwoScoreLabel.Visible = false;

                    vsLabel.Visible = false;
                }
            }
            if (group == 2)
            {
                if (render)
                {
                    teamTwoName.Visible = true;
                    teamTwoScoreValue.Visible = true;
                    teamTwoScoreLabel.Visible = true;

                    vsLabel.Visible = true;
                    scoreButton.Visible = true;
                }
                else
                {
                    teamTwoName.Visible = false;
                    teamTwoScoreValue.Visible = false;
                    teamTwoScoreLabel.Visible = false;

                    vsLabel.Visible = false;
                    scoreButton.Visible = false;
                }
            }
        }
        private void LoadMatchup()
        {
            MatchupModel matchup = (MatchupModel)matchupListBox.SelectedItem;
            if (matchup != null)
            {
                for (int i = 0; i < matchup.Entries.Count; i++)
                {
                    if (i == 0)
                    {
                        MatchupEntryModel entry = matchup.Entries[i];
                        if (entry.TeamCompeting != null)
                        {
                            teamOneName.Text = entry.TeamCompeting.TeamName;
                            teamOneScoreValue.Text = entry.Score.ToString();

                            RenderForm(1, true);
                        }
                        else
                        {
                            teamOneName.Text = "Previous Round Unfinished";
                            teamOneScoreValue.Text = "";
                            RenderForm(1, false);
                        }
                    }
                    if (i == 1)
                    {
                        MatchupEntryModel entry = matchup.Entries[i];
                        if (entry.TeamCompeting != null)
                        {
                            teamTwoName.Text = entry.TeamCompeting.TeamName;
                            teamTwoScoreValue.Text = entry.Score.ToString();

                            RenderForm(2, true);
                        }
                        else
                        {
                            teamTwoName.Text = "Previous Round Unfinished";
                            teamTwoScoreValue.Text = "";

                            RenderForm(2, false);
                        }
                    }
                } 
            }

        }
        private void matchupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchup();
            //RenderForm(1, false);
            //MatchupModel selectedMatchup = (MatchupModel)matchupListBox.SelectedItem;
            //if (selectedMatchup != null)
            //{
            //    if (selectedMatchup.Entries[0].TeamCompeting != null)
            //    {
            //        teamOneName.Text = selectedMatchup.Entries[0].TeamCompeting.TeamName;
            //    }
            //    if (selectedMatchup.Entries[1].TeamCompeting != null)
            //    {
            //        teamTwoName.Text = selectedMatchup.Entries[1].TeamCompeting.TeamName;
            //    }
            //}
        }

        private void unplayedOnlyChekBox_CheckedChanged(object sender, EventArgs e)
        {
            LoadMatchups((int)roundDropDown.SelectedItem);
        }

        private void scoreButton_Click(object sender, EventArgs e)
        {
            double teamOneScore = 0;
            double teamTwoScore = 0;
            MatchupModel matchup = (MatchupModel)matchupListBox.SelectedItem;
            List<MatchupEntryModel> entries = matchup.Entries;
            for (int i = 0; i < matchup.Entries.Count; i++)
            {
                if (i == 0)
                {
                    MatchupEntryModel entry = entries[0];
                    if (entry.TeamCompeting != null)
                    {
                        bool scoreValid = double.TryParse(teamOneScoreValue.Text, out teamOneScore);
                        if (scoreValid)
                        {
                            entry.Score = teamOneScore;
                        }
                        else
                        {
                            MessageBox.Show("Invalid Entry: Team One Score");
                            return;
                        }
                    }
                }
                if (i == 1)
                {
                    MatchupEntryModel entry = entries[1];
                    if (entry.TeamCompeting != null)
                    {
                        bool scoreValid = double.TryParse(teamTwoScoreValue.Text, out teamTwoScore);
                        if (scoreValid)
                        {
                            entry.Score = teamTwoScore;
                        }
                        else
                        {
                            MessageBox.Show("Invalid Entry: Team Two Score");
                            return;
                        }
                    }
                }
            }
            if (teamOneScore == teamTwoScore)
            {
                MessageBox.Show("Tie Game. Try Again");
                return;
            }
            TeamModel winner = teamOneScore > teamTwoScore ? entries[0].TeamCompeting : entries[1].TeamCompeting;
            matchup.Winner = winner;
            matchup.WinnerId = winner.Id;

            foreach (List<MatchupModel> round in tournament.Rounds)
            {
                foreach (MatchupModel rm in round)
                {
                    foreach (MatchupEntryModel entry in rm.Entries)
                    {
                        if (entry.ParentMatchup != null)
                        {
                            if (entry.ParentMatchup.Id == matchup.Id)
                            {
                                entry.TeamCompeting = matchup.Winner;
                                GlobalConfig.Connection.UpdateMatchup(rm);
                            }

                        }
                    }
                }
            }

            LoadMatchups((int)roundDropDown.SelectedItem);

            GlobalConfig.Connection.UpdateMatchup(matchup);

            // Update database
        }
    }
}
