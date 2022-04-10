﻿using DataAccessLayer;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsForms.Utils;

namespace WindowsForms.Forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {            
            this.cbTeamType.Items.Add(TeamType.Men);
            this.cbTeamType.Items.Add(TeamType.Women);
            this.cbLanguage.Items.Add(Language.Croatian);
            this.cbLanguage.Items.Add(Language.English);
            this.lblCurrentSettings.Text = DataProvider.GetConfigInfo();

            this.cbTeamType.SelectedItem = DataProvider.GetTeamType();
            this.cbLanguage.SelectedItem = DataProvider.GetLanguage();
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.cbLanguage.Text) || String.IsNullOrEmpty(this.cbLanguage.Text))
            {
                FormUtils.DisplayMessageBox("Please choose with options", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
                return;
            }
            if (FormUtils.DisplayMessageBox("Do you want to change settings?", "Settings change", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)                            
                DataProvider.UpdateConfig((TeamType)Enum.Parse(typeof(TeamType), this.cbTeamType.Text), (Language)Enum.Parse(typeof(Language), this.cbLanguage.Text));                            
        }
    }
}
