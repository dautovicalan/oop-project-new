﻿using DataAccessLayer;
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
    public partial class PlayersRangListForm : Form
    {
        public PlayersRangListForm()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, EventArgs e) => FormUtils.PrintDataGridView(this.dgvPlayerStats, "Players Statistic");

        private async void PlayersRangListForm_Load(object sender, EventArgs e) => this.dgvPlayerStats.DataSource = await DataProvider.GetPlayers();
    }
}
