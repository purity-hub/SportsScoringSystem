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
    public partial class Clock : Form
    {
        public Clock()
        {
            InitializeComponent();
        }

        //点击的是确认还是取消
        public int isSure = 0;

        //位置
        public string location = "";

        //宽(高度)
        public int size = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            //确认
            //修改Form1实例
            location = this.comboBox1.Text;
            if (location == "不显示")
            {
                size = 0;
            }
            else
            {
                size = int.Parse(this.textBox1.Text);
            }
            isSure = 1;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //取消
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //限制只能输入数字
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
            //限制不能为空
            if (e.KeyChar == 13)
            {
                if (this.textBox1.Text == "")
                {
                    MessageBox.Show("不能为空");
                    e.Handled = true;
                }
            }
        }
    }
}
