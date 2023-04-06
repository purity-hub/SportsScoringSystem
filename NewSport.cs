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
    public partial class NewSport : Form
    {
        private int projectId;//项目ID
        private List<Model.TeamInfoShow> TeamInfoShow = new List<Model.TeamInfoShow> { };
        public NewSport()
        {
            InitializeComponent();
            //一开始禁用下拉框
            comboBox2.Enabled = false;
            //默认为自选项目
            listBox1.SelectedIndex = 0;
            
        }
        public NewSport(Model.ProjectShow show)
        {
            InitializeComponent();
            this.Text = "编辑信息";
            textBox1.Text = show.PName;
            dateTimePicker1.Value = show.DateTime;
            listBox1.SelectedItem = show.PType;
            comboBox3.Text = show.Boxing;
            comboBox4.Text = show.Apparatus;
            comboBox1.Text = show.Type;
            comboBox2.Text = show.TypeValue;
            TeamInfoShow = show.TeamInfoShow;
            dataGridView1.Rows.Clear();
            projectId = show.ID;
            foreach (Model.TeamInfoShow item in show.TeamInfoShow)
            {
                string[] strings = new string[]
                {
                    item.Rname,
                    item.Region,
                    item.Leader,
                    item.Instructor
                };
                dataGridView1.Rows.Add(strings);
            }
            //删除button1_Click按钮事件
            button1.Click -= button1_Click;
            
            //添加button1_Click按钮事件
            button1.Click += button1_Click1;
            

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //联动改变下面的下拉框
            if (comboBox1.SelectedIndex == -1)
            {
                //未选择时禁用下拉框
                comboBox2.Enabled = false;
                
            } 
            else if (comboBox1.SelectedIndex == 0)
            {
                //形式
                comboBox2.Enabled = true;
                comboBox2.Items.Clear();
                string[] items = new string[] 
                {
                    "个人赛",
                    "团体赛",
                    "个人及团体赛"
                };
                comboBox2.Items.AddRange(items);
            }
            else
            {
                //年龄
                comboBox2.Enabled = true;
                comboBox2.Items.Clear();
                string[] items = new string[]
                {
                    "成年赛",
                    "青少年赛",
                    "儿童赛"
                };
                comboBox2.Items.AddRange(items);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                //未选择
                //默认为自选
                
            }
            else if(listBox1.SelectedIndex == 0)
            {
                //自选项目
                radioButton1.Text = "自选拳术";
                radioButton2.Text = "自选器械";
                comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
                string[] items1 = new string[]
                {
                    "长拳",
                    "南拳",
                    "太极拳"
                };
                string[] items2 = new string[]
                {
                    "剑术",
                    "刀术",
                    "南刀",
                    "太极剑",
                    "枪术",
                    "棍术",
                    "南棍"
                };
                comboBox3.Items.Clear();
                comboBox3.Items.AddRange(items1);
                comboBox4.Items.Clear();
                comboBox4.Items.AddRange(items2);
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                comboBox3.Visible = true;
                comboBox4.Visible = true;
            }
            else if(listBox1.SelectedIndex == 1)
            {
                //规定项目
                radioButton1.Text = "规定拳术";
                radioButton2.Text = "规定器械";
                comboBox3.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox4.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox3.Items.Clear();
                comboBox4.Items.Clear();
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                comboBox3.Visible = true;
                comboBox4.Visible = true;
            }
            else if(listBox1.SelectedIndex == 2)
            {
                //对练
                radioButton1.Text = "项目";
                comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox3.Items.Clear();
                string[] items = new string[] 
                { 
                    "徒手对练",
                    "器械对练",
                    "徒手与器械对练"
                };
                comboBox3.Items.AddRange(items);
                radioButton1.Visible = true;
                radioButton2.Visible = false;
                comboBox3.Visible = true;
                comboBox4.Visible = false;
            }
            else if(listBox1.SelectedIndex == 3)
            {
                //集体项目
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                comboBox3.Visible = false;
                comboBox4.Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //新建赛队信息
            NewGameInfo newGameInfo = new NewGameInfo();
            newGameInfo.ShowDialog();
            if (newGameInfo.isSure==1)
            {
                string[] strings = new string[]
                {
                    newGameInfo.TeamInfo.Rname,
                    newGameInfo.TeamInfo.Region,
                    newGameInfo.TeamInfo.Leader,
                    newGameInfo.TeamInfo.Instructor
                };
                dataGridView1.Rows.Add(strings);
                //将信息保存在TeamInfoShow
                Model.TeamInfoShow teamInfoShow = new Model.TeamInfoShow();
                teamInfoShow.Rname = newGameInfo.TeamInfo.Rname;
                teamInfoShow.Rlogo = newGameInfo.TeamInfo.Rlogo;
                teamInfoShow.Region = newGameInfo.TeamInfo.Region;
                teamInfoShow.Leader = newGameInfo.TeamInfo.Leader;
                teamInfoShow.Instructor = newGameInfo.TeamInfo.Instructor;
                teamInfoShow.Players = newGameInfo.PlayerList;
                TeamInfoShow.Add(teamInfoShow);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //编辑
            if (dataGridView1.CurrentRow != null)
            {
                int index = dataGridView1.CurrentRow.Index;//选择的行索引
                Model.TeamInfo teamInfo = new Model.TeamInfo();
                List<Model.Player> players = new List<Model.Player>();
                teamInfo.Id = TeamInfoShow[index].Id;
                teamInfo.Rname = TeamInfoShow[index].Rname;
                teamInfo.Rlogo = TeamInfoShow[index].Rlogo;
                teamInfo.Region = TeamInfoShow[index].Region;
                teamInfo.Leader = TeamInfoShow[index].Leader;
                teamInfo.Instructor = TeamInfoShow[index].Instructor;
                players = TeamInfoShow[index].Players;
                NewGameInfo newGameInfo = new NewGameInfo(teamInfo, players);
                newGameInfo.ShowDialog();
                if (newGameInfo.isSure == 1)
                {
                    string[] strings = new string[]
                    {
                        newGameInfo.TeamInfo.Rname,
                        newGameInfo.TeamInfo.Region,
                        newGameInfo.TeamInfo.Leader,
                        newGameInfo.TeamInfo.Instructor
                    };
                    dataGridView1.Rows[index].SetValues(strings);
                    //将信息保存在TeamInfoShow[index]
                    Model.TeamInfoShow teamInfoShow = new Model.TeamInfoShow();
                    teamInfoShow.Id = teamInfo.Id;
                    teamInfoShow.Rname = newGameInfo.TeamInfo.Rname;
                    teamInfoShow.Rlogo = newGameInfo.TeamInfo.Rlogo;
                    teamInfoShow.Region = newGameInfo.TeamInfo.Region;
                    teamInfoShow.Leader = newGameInfo.TeamInfo.Leader;
                    teamInfoShow.Instructor = newGameInfo.TeamInfo.Instructor;
                    teamInfoShow.Players = newGameInfo.PlayerList;
                    TeamInfoShow[index] = teamInfoShow;
                }
                
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //删除
            if (dataGridView1.CurrentRow != null)
            {
                int index = dataGridView1.CurrentRow.Index;//选择的行索引
                dataGridView1.Rows.RemoveAt(index);
                TeamInfoShow.RemoveAt(index);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //导入
            //导入
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "文本文件|*.txt";
            openFileDialog.Title = "导入";
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "")
            {
                StreamReader sr = new StreamReader(openFileDialog.FileName, Encoding.Default);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] strings = line.Split(',');
                    dataGridView1.Rows.Add(strings);
                    //将信息保存在TeamInfoShow
                    Model.TeamInfoShow teamInfoShow = new Model.TeamInfoShow();
                    teamInfoShow.Rname = strings[0];
                    teamInfoShow.Rlogo = strings[1];
                    teamInfoShow.Region = strings[2];
                    teamInfoShow.Leader = strings[3];
                    teamInfoShow.Instructor = strings[4];
                    teamInfoShow.Players = new List<Model.Player>();
                    TeamInfoShow.Add(teamInfoShow);
                }
                sr.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //导出
            //导出
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "文本文件|*.txt";
            saveFileDialog.Title = "导出";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.Default);
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string line = "";
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        line += dataGridView1.Rows[i].Cells[j].Value.ToString() + ",";
                    }
                    sw.WriteLine(line);
                }
                sw.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //确定
            //将数据插入数据库
            //1、插入[project]表
            string pname = textBox1.Text;
            //pname开头不能为空
            pname = pname.Trim();
            if(pname != "")
            {
                DateTime dt = dateTimePicker1.Value;
                string ptype = listBox1.SelectedItem.ToString();
                string boxing = "";//拳术
                string apparatus = "";//器械
                //没有选中的为""
                if (ptype == "自选项目")
                {
                    if (radioButton1.Checked)
                    {
                        if (comboBox3.SelectedIndex != -1)
                        {
                            boxing = comboBox3.Items[comboBox3.SelectedIndex].ToString();
                        }
                    }
                    else
                    {
                        if (comboBox4.SelectedIndex != -1)
                        {
                            apparatus = comboBox4.Items[comboBox4.SelectedIndex].ToString();
                        }
                    }                   
                }
                else if (ptype == "规定项目")
                {
                    if (radioButton1.Checked)
                    {
                        boxing = comboBox3.Text;
                    }
                    else
                    {
                        apparatus = comboBox4.Text;
                    }  
                }
                else if (ptype == "对练")
                {
                    if (radioButton1.Checked)
                    {
                        boxing = comboBox3.Text;
                    }
                }
                else if (ptype == "集体项目")
                {
                    //什么也不做
                }
                string type = "";
                if (comboBox1.SelectedIndex != -1)
                {
                    type = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                }
                string typevalue = "";
                if (comboBox2.SelectedIndex != -1)
                {
                    typevalue = comboBox2.Items[comboBox2.SelectedIndex].ToString();
                }
                Model.Project model = new Model.Project();
                model.PName = pname;
                model.PType = ptype;
                model.DateTime = dt;
                model.Type = type;
                model.TypeValue = typevalue;
                model.Boxing = boxing;
                model.Apparatus = apparatus;
                DAL.Project project = new DAL.Project();
                //得到自增ID
                int result = project.Add(model);
                //2、插入[teaminfo]表
                if(TeamInfoShow.Count> 0)
                {
                    //写入了teamInfo
                    int[] result1 = new int[TeamInfoShow.Count];
                    DAL.TeamInfo teamInfo1 = new DAL.TeamInfo();
                    for (int i = 0; i < TeamInfoShow.Count; i++)
                    {
                        Model.TeamInfo teamInfo = new Model.TeamInfo();
                        teamInfo.Rname = TeamInfoShow[i].Rname;
                        teamInfo.Rlogo = TeamInfoShow[i].Rlogo;
                        teamInfo.Region = TeamInfoShow[i].Region;
                        teamInfo.Leader = TeamInfoShow[i].Leader;
                        teamInfo.Instructor = TeamInfoShow[i].Instructor;
                        int resultTemp = teamInfo1.Add(teamInfo);
                        teamInfo.Id = resultTemp;
                        result1.SetValue(resultTemp, i);
                    }
                    //3、插入[players]表
                    if (TeamInfoShow[0].Players.Count > 0)
                    {
                        //写入了players
                        int[] result2 = new int[TeamInfoShow[0].Players.Count];
                        DAL.Player players1 = new DAL.Player();
                        for (int i = 0; i < TeamInfoShow.Count; i++)
                        {
                            for (int j = 0; j < TeamInfoShow[i].Players.Count; j++)
                            {
                                Model.Player players = new Model.Player();
                                players.Pname = TeamInfoShow[i].Players[j].Pname;
                                players.Member = TeamInfoShow[i].Players[j].Member;
                                players.First = TeamInfoShow[i].Players[j].First;
                                int resultTemp = players1.Add(players);
                                players.Id = resultTemp;
                                result2.SetValue(resultTemp, j);
                            }
                        }
                        //4、插入[team_player]表
                        DAL.TeamPlayer team_Player1 = new DAL.TeamPlayer();
                        for (int i = 0; i < TeamInfoShow.Count; i++)
                        {
                            for (int j = 0; j < TeamInfoShow[i].Players.Count; j++)
                            {
                                Model.TeamPlayer team_Player = new Model.TeamPlayer();
                                team_Player.Tid = result1[i];
                                team_Player.Pid = result2[j];
                                team_Player.Id = team_Player1.Add(team_Player);
                            }
                        }
                    }
                    //5、插入[project_team]表
                    DAL.ProjectTeam projectTeam = new DAL.ProjectTeam();
                    for (int i = 0; i < result1.Length; i++)
                    {
                        Model.ProjectTeam projectTeam1 = new Model.ProjectTeam();
                        projectTeam1.Tid = result1[i];
                        projectTeam1.Pid = result;//新建项目只有一个
                        projectTeam.Add(projectTeam1);
                    }
                }                                           
                this.Close();
            }
            else
            {
                MessageBox.Show("赛队名为必填项!","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }
        private void button1_Click1(object sender, EventArgs e)
        {
            //更新
            //1、更新[project]表
            string pname = textBox1.Text;
            DateTime dt = dateTimePicker1.Value;
            string ptype = listBox1.SelectedItem.ToString();
            string boxing = "";
            string apparatus = "";
            //没有选中的为""
            if (ptype == "自选项目")
            {
                if (radioButton1.Checked)
                {
                    if (comboBox3.SelectedIndex != -1)
                    {
                        boxing = comboBox3.Items[comboBox3.SelectedIndex].ToString();
                    }
                }
                else
                {
                    if (comboBox4.SelectedIndex != -1)
                    {
                        apparatus = comboBox4.Items[comboBox4.SelectedIndex].ToString();
                    }
                }
            }
            else if (ptype == "规定项目")
            {
                if (radioButton1.Checked)
                {
                    boxing = comboBox3.Text;
                }
                else
                {
                    apparatus = comboBox4.Text;
                }
            }
            else if (ptype == "对练")
            {
                if (radioButton1.Checked)
                {
                    boxing = comboBox3.Text;
                }
            }
            else if (ptype == "集体项目")
            {
                //什么也不做
            }
            string type = "";
            if (comboBox1.SelectedIndex != -1)
            {
                type = comboBox1.Items[comboBox1.SelectedIndex].ToString();
            }
            string typevalue = "";
            if (comboBox2.SelectedIndex != -1)
            {
                typevalue = comboBox2.Items[comboBox2.SelectedIndex].ToString();
            }
            Model.Project model = new Model.Project();
            model.ID = projectId;
            model.PName = pname;
            model.PType = ptype;
            model.DateTime = dt;
            model.Type = type;
            model.TypeValue = typevalue;
            model.Boxing = boxing;
            model.Apparatus = apparatus;
            DAL.Project project = new DAL.Project();
            project.Update(model);
            //2、更新[teaminfo]表
            DAL.TeamInfo teamInfo1 = new DAL.TeamInfo();
            
            //无法确定发生了数量的变化是否是否编辑还是删除加添加
            //可能也发生了其它数据的变化
            //则直接删除项目下的全部数据,再加入数据
            DAL.TeamInfo teamInfo = new DAL.TeamInfo();
            DAL.ProjectTeam projectTeam = new DAL.ProjectTeam();
            DAL.Player players1 = new DAL.Player();
            DAL.TeamPlayer teamPlayer = new DAL.TeamPlayer();
            //删除[players]表
            DataSet dataset = teamInfo.GetTeamInfoById(projectId);
            //删除[teaminfo]表
            teamInfo.DeleteByPid(projectId);
            for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
            {
                players1.DeleteByTid(Convert.ToInt32(dataset.Tables[0].Rows[i]["id"]));
                //删除team_player表
                teamPlayer.DeleteByTid(Convert.ToInt32(dataset.Tables[0].Rows[i]["id"]));
            }
            //删除project_team表数据
            projectTeam.DeleteByPid(projectId);

            //再加入数据
            //2、插入[teaminfo]表
            int[] result1 = new int[TeamInfoShow.Count];//存储团队信息ID
            for (int i = 0; i < TeamInfoShow.Count; i++)
            {
                Model.TeamInfo model1 = new Model.TeamInfo();
                model1.Rname = TeamInfoShow[i].Rname;
                model1.Rlogo = TeamInfoShow[i].Rlogo;
                model1.Region = TeamInfoShow[i].Region;
                model1.Leader = TeamInfoShow[i].Leader;
                model1.Instructor = TeamInfoShow[i].Instructor;
                int resultTemp = teamInfo.Add(model1);
                model1.Id = resultTemp;
                result1.SetValue(resultTemp, i);
            }
            
            if (TeamInfoShow.Count > 0)
            {
                int[][] result2 = new int[TeamInfoShow.Count][];
                //3、插入[players]表
                for (int i = 0; i < TeamInfoShow.Count; i++)
                {
                    result2[i] = new int[TeamInfoShow.Count];
                    for (int j = 0; j < TeamInfoShow[i].Players.Count; j++)
                    {
                        Model.Player players = new Model.Player();
                        players.Pname = TeamInfoShow[i].Players[j].Pname;
                        players.Member = TeamInfoShow[i].Players[j].Member;
                        players.First = TeamInfoShow[i].Players[j].First;
                        int resultTemp = players1.Add(players);
                        players.Id = resultTemp;
                        result2[i][j] = resultTemp;
                    }
                }
                //4、插入[team_player]表
                DAL.TeamPlayer team_Player1 = new DAL.TeamPlayer();
                for (int i = 0; i < TeamInfoShow.Count; i++)
                {
                    for (int j = 0; j < TeamInfoShow[i].Players.Count; j++)
                    {
                        Model.TeamPlayer team_Player = new Model.TeamPlayer();
                        team_Player.Tid = result1[i];
                        team_Player.Pid = result2[i][j];
                        team_Player.Id = team_Player1.Add(team_Player);
                    }
                }
            }
                
                
            
            
            //5、插入[project_team]表
            for (int i = 0; i < result1.Length; i++)
            {
                Model.ProjectTeam projectTeam1 = new Model.ProjectTeam();
                projectTeam1.Tid = result1[i];
                projectTeam1.Pid = projectId;//新建项目只有一个
                projectTeam.Add(projectTeam1);
            }

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //取消
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //选中radioButton1
            if (radioButton1.Checked)
            {
                
                comboBox3.Enabled = true;
                comboBox4.Enabled = false;
            }else{
                comboBox3.Enabled = false;
                comboBox4.Enabled = true;
            }
        }
    }
}
