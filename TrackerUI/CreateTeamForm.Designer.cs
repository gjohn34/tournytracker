
namespace TrackerUI
{
    partial class CreateTeamForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.teamNameValue = new System.Windows.Forms.TextBox();
            this.teamNameLabel = new System.Windows.Forms.Label();
            this.headerLabel = new System.Windows.Forms.Label();
            this.selectTeamMemberDropDown = new System.Windows.Forms.ComboBox();
            this.selectTeamMemberLabel = new System.Windows.Forms.Label();
            this.addMemberButton = new System.Windows.Forms.Button();
            this.newMemberGroupBox = new System.Windows.Forms.GroupBox();
            this.cellphoneValue = new System.Windows.Forms.TextBox();
            this.cellphoneLabel = new System.Windows.Forms.Label();
            this.createMemberButton = new System.Windows.Forms.Button();
            this.emailValue = new System.Windows.Forms.TextBox();
            this.emailLabel = new System.Windows.Forms.Label();
            this.lastNameValue = new System.Windows.Forms.TextBox();
            this.lastNameLabel = new System.Windows.Forms.Label();
            this.firstNameValue = new System.Windows.Forms.TextBox();
            this.firstNameLabel = new System.Windows.Forms.Label();
            this.teamMembersListBox = new System.Windows.Forms.ListBox();
            this.deleteSelectedMemberButton = new System.Windows.Forms.Button();
            this.createTeamButton = new System.Windows.Forms.Button();
            this.newMemberGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // teamNameValue
            // 
            this.teamNameValue.Location = new System.Drawing.Point(22, 114);
            this.teamNameValue.Name = "teamNameValue";
            this.teamNameValue.Size = new System.Drawing.Size(313, 29);
            this.teamNameValue.TabIndex = 7;
            // 
            // teamNameLabel
            // 
            this.teamNameLabel.AutoSize = true;
            this.teamNameLabel.Font = new System.Drawing.Font("Arial", 14.2F);
            this.teamNameLabel.Location = new System.Drawing.Point(22, 84);
            this.teamNameLabel.Name = "teamNameLabel";
            this.teamNameLabel.Size = new System.Drawing.Size(112, 22);
            this.teamNameLabel.TabIndex = 6;
            this.teamNameLabel.Text = "Team Name";
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Arial", 16.2F);
            this.headerLabel.Location = new System.Drawing.Point(22, 40);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(137, 25);
            this.headerLabel.TabIndex = 5;
            this.headerLabel.Text = "Create Team";
            // 
            // selectTeamMemberDropDown
            // 
            this.selectTeamMemberDropDown.Font = new System.Drawing.Font("Arial", 14.2F);
            this.selectTeamMemberDropDown.FormattingEnabled = true;
            this.selectTeamMemberDropDown.Location = new System.Drawing.Point(22, 216);
            this.selectTeamMemberDropDown.Name = "selectTeamMemberDropDown";
            this.selectTeamMemberDropDown.Size = new System.Drawing.Size(318, 30);
            this.selectTeamMemberDropDown.TabIndex = 10;
            // 
            // selectTeamMemberLabel
            // 
            this.selectTeamMemberLabel.AutoSize = true;
            this.selectTeamMemberLabel.Font = new System.Drawing.Font("Arial", 14.2F);
            this.selectTeamMemberLabel.Location = new System.Drawing.Point(22, 186);
            this.selectTeamMemberLabel.Name = "selectTeamMemberLabel";
            this.selectTeamMemberLabel.Size = new System.Drawing.Size(191, 22);
            this.selectTeamMemberLabel.TabIndex = 9;
            this.selectTeamMemberLabel.Text = "Select Team Member";
            // 
            // addMemberButton
            // 
            this.addMemberButton.Font = new System.Drawing.Font("Arial", 12.2F);
            this.addMemberButton.Location = new System.Drawing.Point(84, 269);
            this.addMemberButton.Name = "addMemberButton";
            this.addMemberButton.Size = new System.Drawing.Size(154, 35);
            this.addMemberButton.TabIndex = 11;
            this.addMemberButton.Text = "Add Member";
            this.addMemberButton.UseVisualStyleBackColor = true;
            this.addMemberButton.Click += new System.EventHandler(this.addMemberButton_Click);
            // 
            // newMemberGroupBox
            // 
            this.newMemberGroupBox.Controls.Add(this.cellphoneValue);
            this.newMemberGroupBox.Controls.Add(this.cellphoneLabel);
            this.newMemberGroupBox.Controls.Add(this.createMemberButton);
            this.newMemberGroupBox.Controls.Add(this.emailValue);
            this.newMemberGroupBox.Controls.Add(this.emailLabel);
            this.newMemberGroupBox.Controls.Add(this.lastNameValue);
            this.newMemberGroupBox.Controls.Add(this.lastNameLabel);
            this.newMemberGroupBox.Controls.Add(this.firstNameValue);
            this.newMemberGroupBox.Controls.Add(this.firstNameLabel);
            this.newMemberGroupBox.Font = new System.Drawing.Font("Arial", 12F);
            this.newMemberGroupBox.Location = new System.Drawing.Point(22, 324);
            this.newMemberGroupBox.Name = "newMemberGroupBox";
            this.newMemberGroupBox.Size = new System.Drawing.Size(313, 292);
            this.newMemberGroupBox.TabIndex = 12;
            this.newMemberGroupBox.TabStop = false;
            this.newMemberGroupBox.Text = "Add New Member";
            this.newMemberGroupBox.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // cellphoneValue
            // 
            this.cellphoneValue.Location = new System.Drawing.Point(128, 185);
            this.cellphoneValue.Name = "cellphoneValue";
            this.cellphoneValue.Size = new System.Drawing.Size(170, 26);
            this.cellphoneValue.TabIndex = 4;
            // 
            // cellphoneLabel
            // 
            this.cellphoneLabel.AutoSize = true;
            this.cellphoneLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.cellphoneLabel.Location = new System.Drawing.Point(10, 188);
            this.cellphoneLabel.Name = "cellphoneLabel";
            this.cellphoneLabel.Size = new System.Drawing.Size(87, 19);
            this.cellphoneLabel.TabIndex = 2;
            this.cellphoneLabel.Text = "Cellphone";
            // 
            // createMemberButton
            // 
            this.createMemberButton.Font = new System.Drawing.Font("Arial", 12.2F);
            this.createMemberButton.Location = new System.Drawing.Point(62, 236);
            this.createMemberButton.Name = "createMemberButton";
            this.createMemberButton.Size = new System.Drawing.Size(175, 35);
            this.createMemberButton.TabIndex = 11;
            this.createMemberButton.Text = "Create Member";
            this.createMemberButton.UseVisualStyleBackColor = true;
            this.createMemberButton.Click += new System.EventHandler(this.createMemberButton_Click);
            // 
            // emailValue
            // 
            this.emailValue.Location = new System.Drawing.Point(128, 140);
            this.emailValue.Name = "emailValue";
            this.emailValue.Size = new System.Drawing.Size(170, 26);
            this.emailValue.TabIndex = 3;
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.emailLabel.Location = new System.Drawing.Point(10, 143);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(51, 19);
            this.emailLabel.TabIndex = 2;
            this.emailLabel.Text = "Email";
            // 
            // lastNameValue
            // 
            this.lastNameValue.Location = new System.Drawing.Point(128, 96);
            this.lastNameValue.Name = "lastNameValue";
            this.lastNameValue.Size = new System.Drawing.Size(170, 26);
            this.lastNameValue.TabIndex = 2;
            // 
            // lastNameLabel
            // 
            this.lastNameLabel.AutoSize = true;
            this.lastNameLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lastNameLabel.Location = new System.Drawing.Point(10, 99);
            this.lastNameLabel.Name = "lastNameLabel";
            this.lastNameLabel.Size = new System.Drawing.Size(90, 19);
            this.lastNameLabel.TabIndex = 2;
            this.lastNameLabel.Text = "Last Name";
            // 
            // firstNameValue
            // 
            this.firstNameValue.Location = new System.Drawing.Point(128, 51);
            this.firstNameValue.Name = "firstNameValue";
            this.firstNameValue.Size = new System.Drawing.Size(170, 26);
            this.firstNameValue.TabIndex = 1;
            // 
            // firstNameLabel
            // 
            this.firstNameLabel.AutoSize = true;
            this.firstNameLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.firstNameLabel.Location = new System.Drawing.Point(10, 54);
            this.firstNameLabel.Name = "firstNameLabel";
            this.firstNameLabel.Size = new System.Drawing.Size(91, 19);
            this.firstNameLabel.TabIndex = 0;
            this.firstNameLabel.Text = "First Name";
            // 
            // teamMembersListBox
            // 
            this.teamMembersListBox.FormattingEnabled = true;
            this.teamMembersListBox.ItemHeight = 21;
            this.teamMembersListBox.Location = new System.Drawing.Point(370, 149);
            this.teamMembersListBox.Name = "teamMembersListBox";
            this.teamMembersListBox.Size = new System.Drawing.Size(352, 445);
            this.teamMembersListBox.TabIndex = 13;
            this.teamMembersListBox.SelectedIndexChanged += new System.EventHandler(this.teamMembersListBox_SelectedIndexChanged);
            // 
            // deleteSelectedMemberButton
            // 
            this.deleteSelectedMemberButton.Font = new System.Drawing.Font("Arial", 12.2F);
            this.deleteSelectedMemberButton.Location = new System.Drawing.Point(547, 108);
            this.deleteSelectedMemberButton.Name = "deleteSelectedMemberButton";
            this.deleteSelectedMemberButton.Size = new System.Drawing.Size(175, 35);
            this.deleteSelectedMemberButton.TabIndex = 11;
            this.deleteSelectedMemberButton.Text = "Delete Selected";
            this.deleteSelectedMemberButton.UseVisualStyleBackColor = true;
            this.deleteSelectedMemberButton.Click += new System.EventHandler(this.deleteSelectedMemberButton_Click);
            // 
            // createTeamButton
            // 
            this.createTeamButton.Font = new System.Drawing.Font("Arial", 12.2F);
            this.createTeamButton.Location = new System.Drawing.Point(254, 635);
            this.createTeamButton.Name = "createTeamButton";
            this.createTeamButton.Size = new System.Drawing.Size(220, 35);
            this.createTeamButton.TabIndex = 19;
            this.createTeamButton.Text = "Create Team";
            this.createTeamButton.UseVisualStyleBackColor = true;
            // 
            // CreateTeamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(774, 695);
            this.Controls.Add(this.createTeamButton);
            this.Controls.Add(this.teamMembersListBox);
            this.Controls.Add(this.newMemberGroupBox);
            this.Controls.Add(this.deleteSelectedMemberButton);
            this.Controls.Add(this.addMemberButton);
            this.Controls.Add(this.selectTeamMemberDropDown);
            this.Controls.Add(this.selectTeamMemberLabel);
            this.Controls.Add(this.teamNameValue);
            this.Controls.Add(this.teamNameLabel);
            this.Controls.Add(this.headerLabel);
            this.Font = new System.Drawing.Font("Arial", 13.8F);
            this.Name = "CreateTeamForm";
            this.Text = "Create Team";
            this.Load += new System.EventHandler(this.CreateTeamForm_Load_1);
            this.newMemberGroupBox.ResumeLayout(false);
            this.newMemberGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox teamNameValue;
        private System.Windows.Forms.Label teamNameLabel;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.ComboBox selectTeamMemberDropDown;
        private System.Windows.Forms.Label selectTeamMemberLabel;
        private System.Windows.Forms.Button addMemberButton;
        private System.Windows.Forms.GroupBox newMemberGroupBox;
        private System.Windows.Forms.Label firstNameLabel;
        private System.Windows.Forms.TextBox cellphoneValue;
        private System.Windows.Forms.Label cellphoneLabel;
        private System.Windows.Forms.Button createMemberButton;
        private System.Windows.Forms.TextBox emailValue;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.TextBox lastNameValue;
        private System.Windows.Forms.Label lastNameLabel;
        private System.Windows.Forms.TextBox firstNameValue;
        private System.Windows.Forms.ListBox teamMembersListBox;
        private System.Windows.Forms.Button deleteSelectedMemberButton;
        private System.Windows.Forms.Button createTeamButton;
    }
}