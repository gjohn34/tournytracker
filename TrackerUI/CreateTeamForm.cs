using System;
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
        public CreateTeamForm()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void CreateTeamForm_Load_1(object sender, EventArgs e)
        {

        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {
            Validate validation = ValidateForm();
            if (validation.Valid)
            {
                PersonModel model = new PersonModel(
                    firstNameValue.Text,
                    lastNameValue.Text,
                    emailValue.Text,
                    cellphoneValue.Text
                );
                GlobalConfig.Connection.CreatePerson(model);
                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                cellphoneValue.Text = "";
            } else
            {
                validation.DisplayErrors();
            }
        }
        private Validate ValidateForm()
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
    }
}
