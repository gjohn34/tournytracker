using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Model;
using TrackerUI.FormHelpers.Validate;


namespace TrackerUI
{
    public partial class CreateTournamentForm : Form, IPrizeRequester, ITeamRequester
    {
        List<TeamModel> availableTeams = GlobalConfig.Connection.GetTeam_All();
        List<TeamModel> selectedTeams = new List<TeamModel>();
        List<PrizeModel> selectedPrizes = new List<PrizeModel>();
        public CreateTournamentForm()
        {
            InitializeComponent();
            LoadLists();
        }

        private void LoadLists()
        {
            selectTeamDropDown.DataSource = null;

            availableTeams.Sort(delegate (TeamModel x, TeamModel y)
            {
                return x.TeamName.CompareTo(y.TeamName);
            });

            selectTeamDropDown.DataSource = availableTeams;
            selectTeamDropDown.DisplayMember = "TeamName";

            tournamentTeamsListBox.DataSource = null;
            tournamentTeamsListBox.DataSource = selectedTeams;
            tournamentTeamsListBox.DisplayMember = "TeamName";

            prizesListBox.DataSource = null;
            prizesListBox.DataSource = selectedPrizes;
            prizesListBox.DisplayMember = "PlaceName";


        }

        private Validate ValidateForm()
        {
            Validate errors = new Validate();

            if (tournamentNameValue.Text == "")
            {
                errors.New("Need a tournament name");
            }
            if (decimal.TryParse(entryFeeValue.Text, out _) == false)
            {
                errors.New("Bad Entry Fee");
            } else if (entryFeeValue.Text == "0") 
            {
                errors.New("Fee must be >= 1 ");
            }
            return errors;
        }

        private void CreateTournamentForm_Load(object sender, EventArgs e)
        {

        }

        private void addTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel team = (TeamModel)selectTeamDropDown.SelectedItem;
            if (team != null)
            {
                availableTeams.Remove(team);
                selectedTeams.Add(team);
                LoadLists();
            }
        }

        private void deleteSelectedPlayerButton_Click(object sender, EventArgs e)
        {
            TeamModel team = (TeamModel)tournamentTeamsListBox.SelectedItem;
            if (team != null)
            {
                selectedTeams.Remove(team);
                availableTeams.Add(team);
                LoadLists();
            }
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            // Call the CreatePrizeForm
            CreatePrizeForm cpf = new CreatePrizeForm(this);

            cpf.Show();

        }
        public void PrizeComplete(PrizeModel model)
        {
            selectedPrizes.Add(model);
            LoadLists();
        }
        public void TeamComplete(TeamModel model)
        {
            selectedTeams.Add(model);
            LoadLists();
        }
        private void createNewLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeamForm ctf = new CreateTeamForm(this);
            ctf.Show();
        }

        private void deleteSelectedPrizesButton_Click(object sender, EventArgs e)
        {
            PrizeModel prize = (PrizeModel)prizesListBox.SelectedItem;
            if (prize != null)
            {
                selectedPrizes.Remove(prize);
                LoadLists();
            }
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            // Validate name and Entry
            Validate validation = ValidateForm();
            if (validation.Valid)
            {
                // Create Tournament    
                TournamentModel tournament = new TournamentModel();

                tournament.TournamentName = tournamentNameValue.Text;
                tournament.EntryFee = decimal.Parse(entryFeeValue.Text);
                tournament.EnteredTeams = selectedTeams;
                tournament.Prizes = selectedPrizes;

                GlobalConfig.Connection.CreateTournament(tournament);
            } else
            {
                validation.DisplayErrors();
            }


            // Add prizes to tournamentPrizes joining table

            // Add teams to tournamentTeams joining table

            // Create Matchups
        }
    }
}
