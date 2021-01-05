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
    public partial class CreateTeamForm : Form
    {
        private List<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();

        ITeamRequester callingForm;
        public CreateTeamForm(ITeamRequester caller)
        {
            InitializeComponent();

            callingForm = caller;
            //CreateSampleData();

            LoadLists();
        }

        private void CreateSampleData()
        {
            availableTeamMembers.Add(new PersonModel { FirstName = "Foo", LastName = "Bar" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Fizz", LastName = "Bang" });

            selectedTeamMembers.Add(new PersonModel { FirstName = "Jon", LastName = "Snow" });
            selectedTeamMembers.Add(new PersonModel { FirstName = "Ice", LastName = "King" });
        }

        private void LoadLists()
        {
            selectTeamMemberDropDown.DataSource = null;

            availableTeamMembers.Sort(delegate (PersonModel x, PersonModel y)
            {
                return x.FullName.CompareTo(y.FullName);
            });

            selectTeamMemberDropDown.DataSource = availableTeamMembers;
            selectTeamMemberDropDown.DisplayMember = "FullName";

            teamMembersListBox.DataSource = null;

            teamMembersListBox.DataSource = selectedTeamMembers;
            teamMembersListBox.DisplayMember = "FullName";


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void CreateTeamForm_Load_1(object sender, EventArgs e)
        {
            /// Load Team Members

        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {
            Validate validation = ValidateNewMemberForm();
            if (validation.Valid)
            {
                PersonModel model = new PersonModel(
                    firstNameValue.Text,
                    lastNameValue.Text,
                    emailValue.Text,
                    cellphoneValue.Text
                );
                model = GlobalConfig.Connection.CreatePerson(model);
                
                selectedTeamMembers.Add(model);
                
                LoadLists();
                
                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                cellphoneValue.Text = "";
            } else
            {
                validation.DisplayErrors();
            }
        }
        private Validate ValidateNewMemberForm()
        {
            Validate errors = new Validate();
            // TODO - Add Validation
            if (firstNameValue.Text.Length == 0)
            {
                errors.New("First Name must exist");
            }
            if (lastNameValue.Text.Length == 0)
            {
                errors.New("Last Name must exist");
            }
            if (emailValue.Text.Length == 0)
            {
                errors.New("Email must exist");
            }
            if (cellphoneValue.Text.Length == 0)
            {
                errors.New("Mobile must exist");
            }
            return errors;
        }
        private Validate ValidateTeamForm()
        {
            Validate errors = new Validate();
            if (teamNameValue.Text == "")
            {
                errors.New("Team Name must exist");
            }
            if (teamMembersListBox.Items.Count == 0)
            {
                errors.New("No Team Members selected");
            }
            return errors;
        }

        private void teamMembersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel person = (PersonModel)selectTeamMemberDropDown.SelectedItem;
            if (person != null)
            {
                availableTeamMembers.Remove(person);
                selectedTeamMembers.Add(person);
                LoadLists();
            }
        }

        private void deleteSelectedMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel person = (PersonModel)teamMembersListBox.SelectedItem;
            if (person != null)
            {
                selectedTeamMembers.Remove(person);
                availableTeamMembers.Add(person);
                LoadLists();
            }
        }

        private void createTeamButton_Click(object sender, EventArgs e)
        {
            Validate validation = ValidateTeamForm();
            if (validation.Valid)
            {
                TeamModel team = new TeamModel
                {
                    TeamName = teamNameValue.Text,
                    TeamMembers = selectedTeamMembers
                };


                GlobalConfig.Connection.CreateTeam(team);

                callingForm.TeamComplete(team);
                this.Close();
            }
            else
            {
                validation.DisplayErrors();
            }
        }
    }
}
