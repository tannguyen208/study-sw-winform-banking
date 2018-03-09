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
    public partial class history : Form
    {
        account acc;
        public history(account acc)
        {
            InitializeComponent();
            this.acc = acc;
        }

        async private void history_Load(object sender, EventArgs e)
        {
            dgHistory.DataSource = await functions.history(acc);
        }
    }
}
