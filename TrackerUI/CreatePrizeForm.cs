using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.DataAccess;
using TrackerLibrary.Model;
using TrackerUI.FormHelpers.Validate;

namespace TrackerUI
{
    public partial class CreatePrizeForm : Form
    {
        IPrizeRequester callingForm;
        public CreatePrizeForm(IPrizeRequester caller)
        {
            InitializeComponent();
            callingForm = caller;
        }

        private void CreatePrizeForm_Load(object sender, EventArgs e)
        {

        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            Validate validation = ValidateForm();
            if (validation.Valid)
            {
                PrizeModel model = new PrizeModel(
                    placeNameValue.Text, 
                    placeNumberValue.Text, 
                    prizeAmountValue.Text, 
                    prizePercentageValue.Text
                );

                GlobalConfig.Connection.CreatePrize(model);
                callingForm.PrizeComplete(model);
                this.Close();
            } else
            {
                // TODO - Loop over errors
                validation.DisplayErrors();
                //MessageBox.Show("This is bad m8");
            }
        }
        private Validate ValidateForm()
        {
            Validate errors = new Validate();
            int placeNumber = 0;
            if (!int.TryParse(placeNumberValue.Text, out placeNumber))
            {
                errors.New("Place Number must be a number");
            }
            if (placeNumber < 1)
            {
                errors.New("Place Number cannot be less than 1");
            }
            if (placeNameValue.Text.Length == 0)
            {
                errors.New("Place Name must be present");
            }
            decimal prizeAmount = 0;
            double prizePercentage = 0;

            if (!decimal.TryParse(prizeAmountValue.Text, out prizeAmount) || !double.TryParse(prizePercentageValue.Text, out prizePercentage))
            {
                errors.New("Prize must be a number");
            }
            if (prizeAmount <= 0 && prizePercentage <= 0)
            {
                errors.New("Prize must be greater than 0");
            }
            if (prizeAmount < 0 || prizePercentage > 100)
            {
                errors.New("Prize must be below 100%");
            }

            return errors;
        }
    }
}