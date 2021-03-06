﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class main : Form
    {
        account acc_cur;
        public main(account acc)
        {
            InitializeComponent();
            this.acc_cur = acc;
        }

        private void main_Shown(object sender, EventArgs e)
        {
            txtAcc.Text = acc_cur.AccName;
            txtBalance.Text = acc_cur.Balance.ToString("N0") + " VNĐ";
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            transfer tr = new transfer(acc_cur);
            if(tr.ShowDialog() == DialogResult.OK )
            {
                // load balance
                acc_cur.Balance -= (int)tr.numericUpDown1.Value;
                txtBalance.Text = acc_cur.Balance.ToString();

            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            new history(acc_cur).Show();
        }
    }
}
