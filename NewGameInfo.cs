using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SportsScoringSystem
{
    public partial class NewGameInfo : Form
    {
        public Model.TeamInfo TeamInfo = new Model.TeamInfo();//赛队信息
        public List<Model.Player> PlayerList = new List<Model.Player>();//队员集合
        public int isSure = 0;
        public NewGameInfo(Model.TeamInfo teamInfo,List<Model.Player> players)
        {
            //编辑
            InitializeComponent();
            //将值写入对象中,避免取消导致的空异常
            TeamInfo = teamInfo;
            PlayerList = players;
            textBox1.Text = teamInfo.Rname;
            textBox2.Text = teamInfo.Rlogo;
            textBox3.Text = teamInfo.Region;
            textBox4.Text = teamInfo.Leader;
            textBox5.Text = teamInfo.Instructor;
            dataGridView1.Rows.Clear();
            if(players.Count > 0)
            {
                foreach (var item in players)
                {
                    dataGridView1.Rows.Add(item.Pname, item.Member, item.First);
                }
            }
            
        }
        public NewGameInfo()
        {
            InitializeComponent();

        }
        //这个要改一下
        public void showList(string strWhere)
        {
            Model.Player model = new Model.Player();
            DAL.Player player = new DAL.Player();
            DataSet dataSet = player.GetList(strWhere);
            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //新增
            Players Players = new Players();
            Players.ShowDialog();
            if (Players.isSure==1)
            {
                string[] strings = new string[] { Players.player.Member, Players.player.Pname, Players.player.First };
                dataGridView1.Rows.Add(strings);
            }
            
        }

        private void NewGameInfo_Load(object sender, EventArgs e)
        {
            //showList(string.Empty);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //编辑
            //获取选中行
            if (dataGridView1.CurrentRow != null)
            {
                int row = dataGridView1.CurrentRow.Index;
                string[] str = new string[dataGridView1.ColumnCount];
                for (int i = 0; i < str.Length; i++)
                {
                    str[i] = dataGridView1.Rows[row].Cells[i].Value.ToString();
                }
                //将值赋予新窗口
                Players Players = new Players(str[0], str[1], str[2]);
                Players.ShowDialog();
                //改变后的值
                if (Players.isSure == 1)
                {
                    string[] strings = new string[] { Players.player.Member, Players.player.Pname, Players.player.First };
                    for (int i = 0; i < strings.Length; i++)
                    {
                        dataGridView1.Rows[row].Cells[i].Value = strings[i];
                    }
                }
                
                //showList(string.Empty);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //删除
            if (dataGridView1.CurrentRow != null)
            {
                int row = dataGridView1.CurrentRow.Index;
                dataGridView1.Rows.RemoveAt(row);
            }   
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //导入
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;   //是否允许多选
            dialog.Title = "请选择要处理的文件";  //窗口title
            dialog.Filter = "文本文件(*.txt)|*.*";   //可选择的文件类型
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = dialog.FileName;  //获取文件路径
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //导出为.txt文件
            string localFilePath = "";
            SaveFileDialog sfd = new SaveFileDialog();
            //设置文件类型 
            sfd.Filter = "文本文件(*.txt)|*.*";
            //设置默认文件类型显示顺序 
            sfd.FilterIndex = 1;

            //保存对话框是否记忆上次打开的目录 
            sfd.RestoreDirectory = true;

            //点了保存按钮进入 
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                localFilePath = sfd.FileName.ToString()+".txt"; //获得文件路径 
                FileStream aFile = new FileStream(localFilePath, FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(aFile);
                //写入内容
                string text = "";
                int rows = dataGridView1.RowCount;
                int cols = dataGridView1.ColumnCount;
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        text += "\"";
                        text += dataGridView1.Rows[i].Cells[j].Value.ToString();
                        text += "\" ";
                    }
                    text += "\n";
                }
                sw.Write(text);
                sw.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //确定
            string rname = textBox1.Text;
            string rlogo = textBox2.Text;
            string region = textBox3.Text;
            string leader = textBox4.Text;
            string instructor = textBox5.Text;
            //需要两个公共model
            TeamInfo.Rname = rname;
            TeamInfo.Rlogo = rlogo;
            TeamInfo.Region = region;
            TeamInfo.Leader = leader;
            TeamInfo.Instructor = instructor;
            //此时已经确定了队员的信息
            if(PlayerList.Count != dataGridView1.RowCount)
            {
                //发生了数量的变化
                PlayerList.Clear();
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    Model.Player player = new Model.Player();
                    player.Member = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    player.Pname = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    player.First = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    PlayerList.Add(player);
                }
            }
            else
            {
                //只是编辑
                PlayerList.Clear();
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    Model.Player player = new Model.Player();
                    //player.Id = PlayerList[i].Id;
                    player.Member = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    player.Pname = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    player.First = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    PlayerList.Add(player);
                }
            }
            isSure = 1;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //取消
            TeamInfo = null;
            PlayerList = null;
            this.Close();
        }

    }
}
