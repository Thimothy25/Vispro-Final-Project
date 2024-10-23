using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalPeoject
{
    public partial class BForm2 : Form
    {
        public BForm2()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BCourt frmMain = new BCourt();
            frmMain.Show();
            this.Hide();
        }
    }
}
