using System.Collections.Generic;
using System.Windows.Forms;

namespace TrackerUI.FormHelpers.Validate
{
    public class Validate
    {
        public bool Valid { get; set; }
        public List<string> Messages { get; set; }
        public Validate()
        {
            Valid = true;
            Messages = new List<string>();
        }

        public void New(string message)
        {
            Valid = false;
            Messages.Add(message);
        }
        public void DisplayErrors()
        {
            foreach (string message in Messages)
            {
                MessageBox.Show($"Error: {message}");
            }
        }
    }
}