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
    public partial class MediaOptions : Form
    {

        public int isSure = 0;
        public int Tool = 0;//工具栏
        public int circulate = 0;//循环播放
        public int menu = 0;//禁用右键菜单

        public MediaOptions()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //确认
            isSure = 1;
            if (checkBox1.Checked)
            {
                Tool= 1;
            }
            if (checkBox2.Checked)
            {
                circulate= 1;
            }
            if (checkBox3.Checked)
            {
                menu= 1;
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //取消
            this.Close();
        }
    }
}
