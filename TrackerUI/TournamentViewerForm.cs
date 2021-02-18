using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        private void roundDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            int round = (int)roundDropDown.SelectedItem;
            foreach (List<MatchupModel> matchups in tournament.Rounds)
            {
                if (matchups.First().MatchupRound == round)
                {
                    selectedMatchups = matchups;
                }
            }
            LoadMatchupsListBox();
            //matchups = new List<MatchupModel>();
            //List<MatchupModel> allMatchups = tournament.Rounds[roundDropDown.SelectedIndex];
            //foreach (MatchupModel matchup in allMatchups)
            //{
            //    if (matchup.Entries.Count > 1)
            //    {
            //        matchups.Add(matchup);
            //    }
            //}
            //LoadListBox();


        }
        //private void LoadListBox()
        //{
        //    matchupListBox.DataSource = null;
        //    matchupListBox.DataSource = matchups;
        //    matchupListBox.DisplayMember = "Id";
        //}
        private void RenderForm(int group, bool value)
        {
            // Team1 and Team2
            if (group == 1)
            {
                if (value)
                {
                    teamOneName.Visible = true;
                    teamOneScoreValue.Visible = true;
                    teamOneScoreLabel.Visible = true;

                    teamTwoName.Visible = false;
                    teamTwoScoreValue.Visible = false;
                    teamTwoScoreLabel.Visible = false;

                    vsLabel.Visible = false;
                    scoreButton.Visible = false;
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
                    scoreButton.Visible = false;
                }
            }
            if (group == 2)
            {
                if (value)
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
    }
}
