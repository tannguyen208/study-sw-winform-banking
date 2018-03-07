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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        async private void btnLogin_Click(object sender, EventArgs e)
        {
            account acc = await functions.check(txtAccNo.Text);
            if(acc != null)
            {

                new main(acc).Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("login faild");
            }
        }
    }
}
