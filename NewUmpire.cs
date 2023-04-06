using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SportsScoringSystem
{
    public partial class NewUmpire : Form
    {
        public Model.Umpire umpire = new Model.Umpire();
        public NewUmpire()
        {
            InitializeComponent();
        }
        public NewUmpire(Model.Umpire umpire)
        {
            InitializeComponent();
            comboBox1.Text = umpire.Type;
            textBox1.Text = umpire.Uname;
            textBox2.Text = umpire.Country;
            umpire.Id = umpire.Id;
            umpire.Type = umpire.Type;
            umpire.Uname = umpire.Uname;
            umpire.Country = umpire.Country;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //确定
            umpire.Type = comboBox1.Text;
            umpire.Uname = textBox1.Text;
            umpire.Country = textBox2.Text;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //取消
            umpire = null;
            this.Close();
        }
    }
}
