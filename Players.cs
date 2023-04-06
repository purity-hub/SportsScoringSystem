using SportsScoringSystem.Model;
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
    public partial class Players : Form
    {
        public Model.Player player = new Model.Player();
        public int isSure = 0;
        public Players()
        {
            InitializeComponent();
        }
        public Players(string member,string pname,string first)
        {
            //编辑
            InitializeComponent();
            //将值写入对象中,避免取消导致的为空异常
            player.Member = member;
            player.Pname = pname;
            player.First = first;
            this.textBox1.Text = member;
            this.textBox2.Text = pname;
            if(first == "是")
            {
                this.checkBox1.Checked = true;
            }
            else
            {
                this.checkBox1.Checked = false;
            }           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //确定
            string member = textBox1.Text;//号码
            string name = textBox2.Text;//姓名
            player.Pname = name;
            player.Member = member;
            if (checkBox1.Checked)
            {
                //选择了首发出场
                player.First = "是";
            }
            else
            {
                //未选中首发出场
                player.First="否";
            }
            //增加成功
            isSure = 1;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //取消
            player = null;
            this.Close();
        }
    }
}
