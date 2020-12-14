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

namespace TrackerUI
{
    public partial class CreatePrizeForm : Form
    {
        public CreatePrizeForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void prizeAmountValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void placeNameValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void placeNumberValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void CreatePrizeForm_Load(object sender, EventArgs e)
        {

        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm().Valid)
            {
                PrizeModel model = new PrizeModel(
                    placeNameValue.Text, 
                    placeNumberValue.Text, 
                    prizeAmountValue.Text, 
                    prizePercentageValue.Text
                );

                GlobalConfig.Connection.CreatePrize(model);
                placeNameValue.Text = "";
                placeNumberValue.Text = "";
                prizeAmountValue.Text = "0";
                prizePercentageValue.Text = "0";
            } else
            {
                // TODO - Loop over errors
                MessageBox.Show("This is bad m8");
            }
        }
        private Validate ValidateForm()
        {
            Validate result = new Validate();
            int placeNumber = 0;
            if (!int.TryParse(placeNumberValue.Text, out placeNumber))
            {
                result.New("Place Number must be a number");
            }
            if (placeNumber < 1)
            {
                result.New("Place Number cannot be less than 1");
            }
            if (placeNameValue.Text.Length == 0)
            {
                result.New("Place Name must be present");
            }
            decimal prizeAmount = 0;
            double prizePercentage = 0;

            if (!decimal.TryParse(prizeAmountValue.Text, out prizeAmount) || !double.TryParse(prizePercentageValue.Text, out prizePercentage))
            {
                result.New("Prize must be a number");
            }
            if (prizeAmount <= 0 && prizePercentage <= 0)
            {
                result.New("Prize must be greater than 0");
            }
            if (prizeAmount < 0 || prizePercentage > 100)
            {
                result.New("Prize must be below 100%");
            }

            return result;
        }
    }
}

public class Validate
{
    public bool Valid { get; set; }
    public List<string> Message { get; set; }
    public Validate()
    {
        Valid = true;
        Message = new List<string>();
    }

    public void New(string message)
    {
        Valid = false;
        Message.Add(message);
    }
}
