using System;
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
    public partial class transfer : Form
    {
        account acc, acc_to = null;
        public transfer(account acc)
        {
            InitializeComponent();
            this.acc = acc;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if(acc_to != null)
            {
                var rs = await functions.transfer(acc, acc_to.Id, (int)numericUpDown1.Value, textBox1.Text);
                if(rs!=null)
                {
                    MessageBox.Show("Thành công");
                    this.DialogResult = DialogResult.OK;

                }
                else
                {
                    MessageBox.Show("Thất bại");
                }
            }
            else
            {
                MessageBox.Show("Người nhận???");
            }
        }

        async private void button2_Click(object sender, EventArgs e)
        {
            if(acc.AccNo.Equals(textBox2.Text))
            {
                MessageBox.Show("Thung roi!");
                return;
            }
            // btn chẹck
            acc_to = await functions.checkAcount(textBox2.Text, acc);
            if (acc_to != null)
            {
                MessageBox.Show(acc_to.AccName);
            }
            else
            {
                MessageBox.Show("not found!");
            }
        }

        private void transfer_Load(object sender, EventArgs e)
        {

        }
    }
}
