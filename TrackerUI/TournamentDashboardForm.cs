using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Model;

namespace TrackerUI
{
    public partial class TournamentDashboardForm : Form, ITournamentRequester
    {
        List<TournamentModel> tournaments = GlobalConfig.Connection.GetTournament_All();
        public TournamentDashboardForm()
        {
            InitializeComponent();

            LoadLists();
        }

        private void LoadLists()
        {
            tournamentDropdown.DataSource = null;

            tournaments.Sort(delegate (TournamentModel x, TournamentModel y)
            {
                return x.TournamentName.CompareTo(y.TournamentName);
            });

            tournamentDropdown.DataSource = tournaments;
            tournamentDropdown.DisplayMember = "TournamentName";
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            CreateTournamentForm frm = new CreateTournamentForm(this);

            frm.Show();
        }

        public void TournamentComplete(TournamentModel model)
        {
            tournaments.Add(model);
            LoadLists();
        }

        private void loadTournamentButton_Click(object sender, EventArgs e)
        {
            TournamentModel selectedTournament = (TournamentModel)tournamentDropdown.SelectedItem;
            TournamentViewerForm frm = new TournamentViewerForm(selectedTournament);
            frm.Show();
        }
    }
}
