using SportsScoringSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SportsScoringSystem.Properties;
using Shell32;
using AxWMPLib;
using System.Threading;

namespace SportsScoringSystem
{
    public partial class Main : Form
    {
        private List<ProjectShow> projectShows = new List<ProjectShow>();
        private List<Model.GameShow> gameShows = new List<Model.GameShow>();

        //开始比赛标志
        private int isGaming = 0;

        //现在选择的项目
        private int NowProject = -1;
        //现在选择的比赛
        private int NowGame = -1;

        //全局视频控件
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();


        Form1 form1 = new Form1();
        public Main()
        {
            InitializeComponent();
            if (listBox1.Items.Count > 0)
            {
                listBox1.SelectedIndex = 0;
            }
            //下面这个可以直接放在设计里面，但是会导致vs无法识别设计
            //获取本机IP地址
            string HostName = Dns.GetHostName(); //得到主机名
            IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
            for (int i = 0; i < IpEntry.AddressList.Length; i++)
            {
                //从IP地址列表中筛选出IPv4类型的IP地址
                //AddressFamily.InterNetwork表示此IP为IPv4,
                //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    string ip = "";
                    ip = IpEntry.AddressList[i].ToString();
                    this.label1.Text = IpEntry.AddressList[i].ToString();
                }
            }
            //播放器选项禁用
            button43.Enabled = false;
            //播放之后才会有继续和停止
            button41.Enabled= false;
            button42.Enabled = false;
            //倒计时
            button37.Enabled = false;
            button38.Enabled = false;
            //关闭比赛按钮
            button8.Enabled = false;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //新建赛事
            NewSport newSport = new NewSport();
            newSport.ShowDialog();
            showList();
            listBox1.SelectedIndex = listBox1.Items.Count - 1;

        }

        private void Main_Load(object sender, EventArgs e)
        {
            //每当用户加载窗体时发生
            showList();
            showList2();
            //第二个屏幕
            form1 = new Form1();
            FormStartScreen(1,form1);
            
        }
        public void FormStartScreen(int screen, Form form)
        {
            if (Screen.AllScreens.Length < screen)
            {
                MessageBox.Show("当前主机连接最多的屏幕是" + Screen.AllScreens.Length + " 个，不能投屏到第" + screen + "个 屏幕！");
                return;
            }
            screen = screen - 1;
            if (form == null)
            {
                form = new Form();
            }
            if(Screen.AllScreens.Length > 1)
            {
                //有显示器
                form.StartPosition = FormStartPosition.CenterScreen;
                Screen s = Screen.AllScreens[1];
                form.Location = new System.Drawing.Point(s.Bounds.X, s.Bounds.Y);
                form.Size = new Size(s.WorkingArea.Width, s.WorkingArea.Height);
                form.BackColor = Color.Black;
                form.StartPosition = FormStartPosition.Manual;
                form.Show();
                form.BringToFront();
            }
            else
            {
                //没有显示器,本地显示
                form.StartPosition = FormStartPosition.CenterScreen;
                Screen s = Screen.AllScreens[screen];
                form.Location = new System.Drawing.Point(s.Bounds.X, s.Bounds.Y);
                form.Size = new Size(s.WorkingArea.Width/2, (int)(s.WorkingArea.Height/2.5));
                form.BackColor = Color.Black;
                form.StartPosition = FormStartPosition.Manual;
                form.Show();
                form.BringToFront();
            }
            
        }

        private void showList()
        {
            DAL.Project project = new DAL.Project();
            DataSet dataSet = project.GetList(string.Empty);
            listBox1.Items.Clear();
            projectShows.Clear();
            int[] resultId = new int[dataSet.Tables[0].Rows.Count];
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                Model.ProjectShow show = new Model.ProjectShow();
                show.TeamInfoShow = new List<TeamInfoShow>();
                show.ID = Convert.ToInt32(row["ID"]);
                show.PName = row["PName"].ToString();
                show.DateTime = Convert.ToDateTime(row["DateTime"]);
                show.PType = row["PType"].ToString();
                show.Type = row["Type"].ToString();
                show.TypeValue = row["TypeValue"].ToString();
                show.Boxing = row["Boxing"].ToString();
                show.Apparatus = row["Apparatus"].ToString();
                //查询[teaminfo]表
                //连表查询[project_team]表
                DAL.TeamInfo teamInfo = new DAL.TeamInfo();
                DataSet teamInfoSet = teamInfo.GetTeamInfoById(show.ID);
                List<Model.TeamInfoShow> teamInfoShows = new List<Model.TeamInfoShow>();
                for (int i = 0;i < teamInfoSet.Tables[0].Rows.Count;i++)
                {
                    Model.TeamInfoShow teamInfoShow = new Model.TeamInfoShow();
                    teamInfoShow.Id = Convert.ToInt32(teamInfoSet.Tables[0].Rows[i]["id"]);
                    teamInfoShow.Rname = teamInfoSet.Tables[0].Rows[i]["rname"].ToString();
                    teamInfoShow.Rlogo = teamInfoSet.Tables[0].Rows[i]["rlogo"].ToString();
                    teamInfoShow.Region = teamInfoSet.Tables[0].Rows[i]["region"].ToString();
                    teamInfoShow.Leader = teamInfoSet.Tables[0].Rows[i]["leader"].ToString();
                    teamInfoShow.Instructor = teamInfoSet.Tables[0].Rows[i]["instructor"].ToString();
                    show.TeamInfoShow.Add(teamInfoShow);
                    //每个队伍的队员集合
                    //连表查询[team_player]表
                    //将teamInfoSet.Tables[0].Rows[0].
                    DataSet playersSet = teamInfo.GetPlayersById(Convert.ToInt32(teamInfoSet.Tables[0].Rows[i]["id"].ToString()));
                    show.TeamInfoShow[i].Players = new List<Player>();
                    for (int j = 0; j < playersSet.Tables[0].Rows.Count;j++)
                    {
                        Model.Player player = new Model.Player();
                        player.Id = Convert.ToInt32(playersSet.Tables[0].Rows[j]["id"].ToString());
                        player.Member = playersSet.Tables[0].Rows[j]["member"].ToString();
                        player.Pname = playersSet.Tables[0].Rows[j]["pname"].ToString();
                        player.First = playersSet.Tables[0].Rows[j]["first"].ToString();
                        show.TeamInfoShow[i].Players.Add(player);
                    }
                }
                projectShows.Add(show);
                resultId.SetValue(Convert.ToInt32(row["ID"]), row.Table.Rows.IndexOf(row));
                listBox1.Items.Add(row["pname"]);
            }
        }
        private void showList2(){
            //显示赛事列表
            if (listBox1.SelectedIndex != -1)
            {
                //选中项目
                int index = listBox1.SelectedIndex;
                DAL.Game game = new DAL.Game();
                DataSet dataSet = game.GetListByPid(projectShows[index].ID);
                listBox2.Items.Clear();
                gameShows.Clear();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Model.GameShow show = new Model.GameShow();
                    show.Id = Convert.ToInt32(row["id"]);
                    show.Time = row["time"].ToString();
                    show.Team1 = row["team1"].ToString();
                    show.Team2 = row["team2"].ToString();
                    show.Color1 = row["color1"].ToString();
                    show.Color2 = row["color2"].ToString();
                    show.Clothes1 = row["clothes1"].ToString();
                    show.Clothes2 = row["clothes2"].ToString();
                    gameShows.Add(show);
                    //查询umpire表
                    DAL.Umpire umpire = new DAL.Umpire();
                    DataSet umpireSet = umpire.GetListByGid(Convert.ToInt32(row["id"]));
                    show.Umpires = new List<Umpire>();
                    foreach (DataRow umpireRow in umpireSet.Tables[0].Rows)
                    {
                        Model.Umpire umpire1 = new Model.Umpire();
                        umpire1.Id = Convert.ToInt32(umpireRow["id"]);
                        umpire1.Type = umpireRow["type"].ToString();
                        umpire1.Uname = umpireRow["uname"].ToString();
                        umpire1.Country = umpireRow["country"].ToString();
                        show.Umpires.Add(umpire1);
                    }
                    gameShows.Add(show);
                    if (projectShows[listBox1.SelectedIndex].PType == "对练")
                    {
                        listBox2.Items.Add("[" + row["time"] + "]  " + row["team1"] + " : " + row["team2"]);
                    }
                    else
                    {
                        listBox2.Items.Add("[" + row["time"] + "]  " + row["team1"]);
                    }
                    
                }
                
            }
            else
            {
                listBox2.Items.Clear();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //编辑
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择赛事");
                return;
            }
            else
            {
                int index = listBox1.SelectedIndex;
                NewSport newSport = new NewSport(projectShows[index]);
                newSport.ShowDialog();
                showList();
                listBox1.SelectedIndex = index;
            }
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //删除
            //连带删除全部相关信息
            MessageBoxButtons boxButtons = MessageBoxButtons.OKCancel;
            DialogResult dialogResult = MessageBox.Show("删除的比赛将无法恢复，确认要删除吗？","删除",boxButtons);
            if(dialogResult == DialogResult.OK)
            {
                //确认
                //删除全部数据
                int index = listBox1.SelectedIndex;
                Model.ProjectShow model = projectShows[index];
                //1、project表
                DAL.Project project = new DAL.Project();
                project.Delete(projectShows[index].ID);
                //2、teaminfo表
                DAL.TeamInfo teamInfo = new DAL.TeamInfo();
                teamInfo.DeleteByPid(projectShows[index].ID);
                //3、players表
                DAL.Player player = new DAL.Player();
                DAL.TeamPlayer teamPlayer = new DAL.TeamPlayer();
                for (int i = 0; i < projectShows[index].TeamInfoShow.Count; i++)
                {
                    player.DeleteByTid(projectShows[index].TeamInfoShow[i].Id);
                    //5、team_player表
                    teamPlayer.DeleteByTid(projectShows[index].TeamInfoShow[i].Id);
                }
                //4、project_team表
                DAL.ProjectTeam projectTeam = new DAL.ProjectTeam();
                projectTeam.DeleteByPid(projectShows[index].ID);
                //新增project_game表删除
                DAL.ProjectGame projectGame = new DAL.ProjectGame();
                projectGame.Delete(projectShows[index].ID);
                showList();
                //获取listbox2的长度 
                int length = listBox2.Items.Count;
                if (length > 0)
                {
                    for (int i = 0; i < length; i++)
                    {
                        //删除
                        int Gid = gameShows[i].Id;
                        //1、删除game表
                        DAL.Game game = new DAL.Game();
                        game.DeleteById(Gid);
                        //2、删除[umpire]表
                        DAL.Umpire umpire = new DAL.Umpire();
                        umpire.DeleteByGid(Gid);
                        //3、删除[game_umpire]表
                        DAL.GameUmpire gameUmpire = new DAL.GameUmpire();
                        gameUmpire.DeleteByGid(Gid);
                    }
                }
                this.showList2();
            }
            else
            {
                //取消
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //新建比赛
            if (listBox1.SelectedIndex != -1)
            {
                int index = listBox1.SelectedIndex;
                string[] teamName = new string[projectShows[index].TeamInfoShow.Count];
                //将团队名称传入给下拉框以便选择
                for(int i = 0; i < projectShows[index].TeamInfoShow.Count; i++)
                {
                    teamName[i] = projectShows[index].TeamInfoShow[i].Rname;
                }
                if (projectShows[index].PType == "对练")
                {
                    NewGame newGame = new NewGame(projectShows[index].ID,teamName);
                    newGame.ShowDialog();
                    gameShows.Add(newGame.gameShow);
                    showList2();
                }
                else
                {
                    NewGame2 newGame = new NewGame2(projectShows[index].ID,teamName);
                    newGame.ShowDialog();
                    gameShows.Add(newGame.gameShow);
                    showList2();
                }

            }
            else
            {
                MessageBox.Show("请先选择赛会");
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //编辑
            if (NowProject != -1)//这里采用NowProject来取代选择，因为可能用户会取消掉赛会的选择，然而比赛列表未发生变化
            {
                if (listBox2.SelectedIndex != -1)
                {
                    if (projectShows[NowProject].PType == "对练")
                    {
                        int index = listBox2.SelectedIndex;
                        NewGame newGame = new NewGame(gameShows[index]);
                        newGame.ShowDialog();
                        showList2();
                    }
                    else
                    {
                        int index = listBox2.SelectedIndex;
                        NewGame2 newGame = new NewGame2(gameShows[index]);
                        newGame.ShowDialog();
                        showList2();
                    }

                }
                else
                {
                    MessageBox.Show("请先选择比赛");
                }

            }
            else
            {
                MessageBox.Show("请先选择赛会");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //删除
            int index = listBox2.SelectedIndex;
            int Gid = gameShows[index].Id;
            //1、删除game表
            DAL.Game game = new DAL.Game();
            game.DeleteById(Gid);
            //2、删除[umpire]表
            DAL.Umpire umpire = new DAL.Umpire();
            umpire.DeleteByGid(Gid);
            //3、删除[game_umpire]表
            DAL.GameUmpire gameUmpire = new DAL.GameUmpire();
            gameUmpire.DeleteByGid(Gid);
            this.showList2();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //单击事件------>切换listBox2
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                NowProject = index;
                showList2();
            }
            else
            {
                listBox1.SelectedIndex = -1;//不做任何操作，将ListBox的选中项取消
            }
        }

        private void listBox2_MouseClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox2.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                NowGame = index;
            }
            else
            {
                listBox2.SelectedIndex = -1;//不做任何操作，将ListBox的选中项取消
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //双击事件------>进入编辑页面
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                NewSport newSport = new NewSport(projectShows[index]);
                newSport.ShowDialog();
                showList();
                listBox1.SelectedIndex = index;
            }
            else
            {
                listBox1.SelectedIndex = -1;//不做任何操作，将ListBox的选中项取消
            }
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        //textBox4固定输入格式__:__:__
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //输入框中固定输入格式__:__:__
            //输入时加入:
            if (textBox4.Text.Length == 2 || textBox4.Text.Length == 5)
            {
                if (e.KeyChar != 8)
                {
                    textBox4.Text += ":";
                    textBox4.SelectionStart = textBox4.Text.Length;
                }
            }
            //非法输入
            if (textBox4.Text.Length == 8)
            {
                if (e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            }
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button21_Click_1(object sender, EventArgs e)
        {
            //关闭系统
            this.Close();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            //使用手册
            //打开网址
            System.Diagnostics.Process.Start("https://mp.weixin.qq.com/s/mhEr5T4IxZEkc_aKQGa6Kg");
        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //右边比赛的双击事件------>编辑
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                if (projectShows[listBox1.SelectedIndex].PType == "对练")
                {
                    NewGame newGame = new NewGame(gameShows[index]);
                    newGame.ShowDialog();
                    showList2();
                }
                else
                {
                    
                    NewGame2 newGame = new NewGame2(gameShows[index]);
                    newGame.ShowDialog();
                    showList2();
                }
            }
            else
            {
                listBox2.SelectedIndex = -1;//不做任何操作，将ListBox的选中项取消
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //打开比赛---->屏幕上显示参赛的信息
            if(NowProject != -1)
            {
                if(NowGame != -1)
                {
                    if(isGaming == 0)
                    {
                        //未打开过比赛
                        //跳转选项卡
                        tabControl1.SelectedIndex = 5;
                        //屏幕上显示参赛的信息
                        isGaming = 1;//打开了比赛
                        button8.Enabled = true;
                        if (form1.size == 0)
                        {
                            //不显示时钟
                            DrawProject("不显示", form1.size);
                        }
                        else
                        {
                            if (form1.x != 0)
                            {
                                //右侧
                                DrawProject("右侧", form1.size);
                            }
                            else
                            {
                                //左侧
                                DrawProject("左侧", form1.size);
                            }
                        }
                        
                    }
                    else
                    {
                        //已经打开过了比赛，后再没有关闭比赛时又再次打开比赛
                        DialogResult AF = MessageBox.Show("您还有比赛没有关闭，确认重新开始比赛吗？", "确认框", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (AF == DialogResult.OK)
                        {
                            //用户点击确认后执行的代码
                            //清空之前的比赛
                            form1.Controls.Clear();
                            //跳转选项卡
                            tabControl1.SelectedIndex = 5;
                            //屏幕上显示参赛的信息
                            isGaming = 1;//打开了比赛
                            button8.Enabled = true;
                            if (form1.size == 0)
                            {
                                //不显示时钟
                                DrawProject("不显示", form1.size);
                            }
                            else
                            {
                                if (form1.x != 0)
                                {
                                    //右侧
                                    DrawProject("右侧", form1.size);
                                }
                                else
                                {
                                    //左侧
                                    DrawProject("左侧", form1.size);
                                }
                            }
                        }
                    }
                    
                }
                else{
                    MessageBox.Show("请选择比赛");
                }
            }else{
                MessageBox.Show("请选择项目");
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //关闭比赛
            button8.Enabled=false;
            form1.Controls.Clear();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //联系我
            System.Diagnostics.Process.Start(@"mailto:1580575248@qq.com?subject=武术套路打分系统");
        }

        private void DrawProject(string location,int size)
        {
            int index1 = NowProject;
            int index2 = NowGame;
            string time = gameShows[index2].Time;//场次
            string projectName = projectShows[index1].PName;//赛会名称
            string gameName1 = gameShows[index2].Team1;//比赛团队名或者是个人的名称
            string gameName2 = gameShows[index2].Team2;//对练时的两个队伍或个人
            string boxing = projectShows[index1].Boxing;//拳术
            string apparatus = projectShows[index1].Apparatus;//器械
            //先确定比赛信息的展示位置(屏幕大小减去时钟的size)
            int width = form1.Width - size;
            int height = form1.Height;
            //新建panel的原因是每次刷新form1会导致把信息覆盖掉,故直接在form1上面直接加一个panel
            //新建一个panel用于展示比赛信息
            //移除form1的所有控件
            form1.Controls.Clear();
            Panel panel = new Panel();
            panel.Size = new Size(width, height);
            if (location == "左侧")
            {
                panel.Location = new Point(size, 0);
            }
            else if (location == "右侧")
            {
                panel.Location = new Point(0, 0);
            }
            else if(location=="不显示")
            {
                //不显示
                panel.Location = new Point(0, 0);
            }
            //panel.Location = new Point(form1.size, 0);
            panel.BackColor = form1.BackColor;//背景颜色相同
            form1.Controls.Add(panel);
            //画出矩形区域
            Graphics g = panel.CreateGraphics();
            Pen pen = new Pen(Color.Blue, 2);
            //填充颜色
            SolidBrush brush = new SolidBrush(Color.Blue);
            g.FillRectangle(brush, 0, 0, width, height);
            //对练
            if (projectShows[NowProject].PType == "对练")
            {
                //赛会名称在正上方
                Font font = new Font("宋体", 30, FontStyle.Bold);
                g.DrawString(projectName, font, Brushes.White, new Point(width / 2 - 100, 10));
                //场次数显示
                Font font2 = new Font("宋体", 20, FontStyle.Bold);
                g.DrawString("第" + time + "场", font2, Brushes.White, new Point(0, height / 3 - 100));
                //团队名称团队1vs团队2
                Font font1 = new Font("宋体", 20, FontStyle.Bold);
                g.DrawString(gameName1 + "vs" + gameName2, font1, Brushes.White, new Point(width - 150, height / 3 - 100));
                //比赛项目
                Font font3 = new Font("宋体", 20, FontStyle.Bold);
                g.DrawString(boxing + apparatus, font3, Brushes.White, new Point(width - 150, height / 3 - 100));
                //(A组)动作质量
                Font font4 = new Font("宋体", 15, FontStyle.Bold);
                g.DrawString("(A组)动作质量", font4, Brushes.White, new Point(0, height / 3 - 40));
                //(B组)演练水平
                Font font5 = new Font("宋体", 15, FontStyle.Bold);
                g.DrawString("(B组)演练水平", font5, Brushes.White, new Point(0, height / 3));
                //(C组)难度
                Font font6 = new Font("宋体", 15, FontStyle.Bold);
                g.DrawString("(C组)难度", font6, Brushes.White, new Point(0, height / 3 + 40));
                //创新
                Font font7 = new Font("宋体", 15, FontStyle.Bold);
                g.DrawString("创新:", font7, Brushes.White, new Point(0, height / 3 + 80));
                //扣分
                Font font8 = new Font("宋体", 15, FontStyle.Bold);
                g.DrawString("扣分:", font8, Brushes.White, new Point(width / 2, height / 3 + 80));
                //得分
                Font font9 = new Font("宋体", 20, FontStyle.Bold);
                g.DrawString("得分:", font9, Brushes.White, new Point(0, height / 3 + 120));
            }
            else
            {
                //赛会名称在正上方
                Font font = new Font("宋体", 30, FontStyle.Bold);
                g.DrawString(projectName, font, Brushes.White, new Point(width / 2 - 100, 10));
                //场次数显示
                Font font2 = new Font("宋体", 20, FontStyle.Bold);
                g.DrawString("第" + time + "场", font2, Brushes.White, new Point(0, height / 3 - 100));
                //团队名称
                Font font1 = new Font("宋体", 20, FontStyle.Bold);
                g.DrawString(gameName1, font1, Brushes.White, new Point(width / 2 - 100, height / 3 - 100));
                //比赛项目
                Font font3 = new Font("宋体", 20, FontStyle.Bold);
                g.DrawString(boxing + apparatus, font3, Brushes.White, new Point(width - 150, height / 3 - 100));
                //(A组)动作质量
                Font font4 = new Font("宋体", 15, FontStyle.Bold);
                g.DrawString("(A组)动作质量", font4, Brushes.White, new Point(0, height / 3 - 40));
                //(B组)演练水平
                Font font5 = new Font("宋体", 15, FontStyle.Bold);
                g.DrawString("(B组)演练水平", font5, Brushes.White, new Point(0, height / 3));
                //(C组)难度
                Font font6 = new Font("宋体", 15, FontStyle.Bold);
                g.DrawString("(C组)难度", font6, Brushes.White, new Point(0, height / 3 + 40));
                //创新
                Font font7 = new Font("宋体", 15, FontStyle.Bold);
                g.DrawString("创新:", font7, Brushes.White, new Point(0, height / 3 + 80));
                //扣分
                Font font8 = new Font("宋体", 15, FontStyle.Bold);
                g.DrawString("扣分:", font8, Brushes.White, new Point(width / 2, height / 3 + 80));
                //得分
                Font font9 = new Font("宋体", 20, FontStyle.Bold);
                g.DrawString("得分:", font9, Brushes.White, new Point(0, height / 3 + 120));
            }
        }

        private void DrawGameInfo(string location, int size)
        {
            //绘制比赛信息
            if (isGaming == 1)
            {
                int index1 = NowProject;
                int index2 = NowGame;
                string time = gameShows[index2].Time;//场次
                string projectName = projectShows[index1].PName;//赛会名称
                string typevalue = projectShows[index1].TypeValue;
                string gameName1 = gameShows[index2].Team1;//比赛团队名或者是个人的名称
                string gameName2 = gameShows[index2].Team2;//对练时的两个队伍或个人
                string boxing = projectShows[index1].Boxing;//拳术
                string apparatus = projectShows[index1].Apparatus;//器械
                form1.Controls.Clear();
                Panel panel = new Panel();
                int width = form1.Width - size;
                int height = form1.Height;
                panel.Size = new Size(width, height);
                if (location == "左侧")
                {
                    panel.Location = new Point(size, 0);
                }
                else if (location == "右侧")
                {
                    panel.Location = new Point(0, 0);
                }
                else if (location == "不显示")
                {
                    //不显示
                    panel.Location = new Point(0, 0);
                }
                //panel.Location = new Point(form1.size, 0);
                panel.BackColor = form1.BackColor;//背景颜色相同
                form1.Controls.Add(panel);
                //画出矩形区域
                Graphics g = panel.CreateGraphics();
                Pen pen = new Pen(Color.Blue, 2);
                //文字滚动效果
                //赛会名称在正上方
                Font font = new Font("宋体", 30, FontStyle.Bold);
                g.DrawString(projectName, font, Brushes.White, new Point(width / 2 - 100, 10));
                //场次数显示
                Font font2 = new Font("宋体", 20, FontStyle.Bold);
                g.DrawString(typevalue, font2, Brushes.White, new Point(0, height / 3 - 100));
                //比赛项目
                Font font3 = new Font("宋体", 20, FontStyle.Bold);
                g.DrawString(boxing + apparatus, font3, Brushes.White, new Point(width - 150, height / 3 - 100));
                
            }else{
                MessageBox.Show("请先开启比赛");
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            //打开Clock
            Clock clock = new Clock();
            clock.ShowDialog();
            //改变form1
            if (clock.isSure == 1)
            {
                //确认
                //改变form1时钟的位置
                form1.Clock_Resize(clock.location,clock.size);
                if (isGaming == 1)
                {
                    //开启了比赛
                    DrawProject(clock.location, clock.size);
                }
            }
        }


        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//判断点击的是否为右键
            {
                //选中节点非根节点
                if (e.Node.Parent != null)
                {
                    新建ToolStripMenuItem.Visible = true;
                    contextMenuStrip1.Show(treeView1, e.Location);//弹出操作菜单 
                }
                else
                {
                    //选择根节点
                    //删除"新建平级"选项
                    新建ToolStripMenuItem.Visible = false;
                    //删除"删除"选项
                    contextMenuStrip1.Show(treeView1, e.Location);//弹出操作菜单 
                }
                
            }
            else if(e.Button == MouseButtons.Left)
            {
                //单击左键
                //选中节点时，加载相应的资源
                string path = "D:\\武术套路计分系统\\SportsScoringSystem\\media";
                if (e.Node!=null)
                {
                    //选中节点,包括自己
                    if (e.Node.Level > 0)
                    {
                        //不是根节点
                        string path1 = e.Node.Text;
                        //string path1 = "";
                        //获取父节点
                        TreeNode node1 = e.Node.Parent;
                        //i从1开始则不包括media
                        for (int i = 1; i < e.Node.Level; i++)
                        {
                            //获取父节点的文本
                            string text = node1.Text;
                            //获取父节点的父节点
                            node1 = node1.Parent;
                            path1 = text + "\\" + path1;
                        }
                        path = path + "\\" + path1;
                        //扫描目录下的视频或者图片
                        DirectoryInfo info = new DirectoryInfo(path);
                        List<FileInfo> list = new List<FileInfo>();
                        foreach (FileInfo file in info.GetFiles("*.*"))
                        {
                            list.Add(file);
                        }
                        //list加入右边的表格中
                        dataGridView1.Rows.Clear();
                        //标题、类型、文件名、时长
                        foreach (FileInfo file in list)
                        {
                            //引用Shell32.dll文件
                            ShellClass sc = new ShellClass();
                            //获取时长
                            Folder folder = sc.NameSpace(path);
                            FolderItem folderItem = folder.ParseName(file.Name);
                            //获取文件时长
                            string time = folder.GetDetailsOf(folderItem, 27);
                            //获取文件名
                            string name = file.Name;
                            //获取文件类型
                            string type = folder.GetDetailsOf(folderItem, 2);
                            //获取文件路径
                            string path2 = file.FullName;
                            //添加到表格中
                            string[] strings = new string[] { name, type, path2, time };
                            dataGridView1.Rows.Add(strings);
                        }
                    }
                    

                } 
            }
        }



        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //新建平级节点
            TreeNode node = new TreeNode();
            node.Text = "新节点";
            if(treeView1.SelectedNode.Parent==null)
            {
                //根节点
                treeView1.Nodes.Add(node);
            }
            else
            {
                //非根节点
                treeView1.SelectedNode.Parent.Nodes.Add(node);
            }
            //文件夹中新建文件夹
            //获取完整路径
            string path = "D:\\武术套路计分系统\\SportsScoringSystem\\media";
            //平级节点不包括自己
            //string path1 = treeView1.SelectedNode.Text;
            string path1 = "";
            //获取父节点
            TreeNode node1 = treeView1.SelectedNode.Parent;
            //i从1开始则不包括media
            for (int i=1;i<treeView1.SelectedNode.Level;i++)
            {
                //获取父节点的文本
                string text = node1.Text;
                //获取父节点的父节点
                node1 = node1.Parent;
                path1 = text +"\\"+ path1;
            }
            path = path + "\\" + path1 + "\\新节点";
            DirectoryInfo info = new DirectoryInfo(path);
            if (!info.Exists)
            {
                //节点可能文件夹重名,因为都是'新节点'
                info.Create();
            }
            else
            {
                treeView1.SelectedNode.Parent.Nodes.Remove(node);
            }
        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            //编辑后
            //获取编辑后的节点的文本
            string NewName = e.Label;
            if (NewName != null)
            {
                //获取完整路径
                string path = "D:\\武术套路计分系统\\SportsScoringSystem\\media";
                //子级节点包括自己(编辑前的名称)
                string path1 = e.Node.Text;
                //string path1 = "";
                //获取父节点
                TreeNode node1 = treeView1.SelectedNode.Parent;
                //i从1开始则不包括media
                for (int i = 1; i < treeView1.SelectedNode.Level; i++)
                {
                    //获取父节点的文本
                    string text = node1.Text;
                    //获取父节点的父节点
                    node1 = node1.Parent;
                    path1 = text + "\\" + path1;
                    NewName = text + "\\" + NewName;
                }
                string OldPath = path + "\\" + path1;
                string NewPath = path + "\\" + NewName;
                DirectoryInfo info = new DirectoryInfo(OldPath);

                //节点可能文件夹重名,因为都是'新节点'
                info.MoveTo(NewPath);
            }
            
        }

        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //编辑节点
            treeView1.SelectedNode.BeginEdit();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //删除文件夹
            string path = "D:\\武术套路计分系统\\SportsScoringSystem\\media";
            //子级节点包括自己
            string path1 = treeView1.SelectedNode.Text;
            //string path1 = "";
            //获取父节点
            TreeNode node1 = treeView1.SelectedNode.Parent;
            //i从1开始则不包括media
            for (int i = 1; i < treeView1.SelectedNode.Level; i++)
            {
                //获取父节点的文本
                string text = node1.Text;
                //获取父节点的父节点
                node1 = node1.Parent;
                path1 = text + "\\" + path1;
            }
            path = path + "\\" + path1;
            DirectoryInfo info = new DirectoryInfo(path);
            //删除文件夹及其子文件夹
            info.Delete(true);
            //删除节点
            treeView1.Nodes.Remove(treeView1.SelectedNode);
        }

        private void 新建下级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //新建下级节点
            TreeNode node = new TreeNode();
            node.Text = "新子节点";
            treeView1.SelectedNode.Nodes.Add(node);
            //文件夹中新建文件夹
            //获取完整路径
            string path = "D:\\武术套路计分系统\\SportsScoringSystem\\media";
            //子级节点包括自己
            string path1 = "";
            if(treeView1.SelectedNode.Level > 0)
            {
                //不是根节点(media)
                path1 = treeView1.SelectedNode.Text;
            }
            
            //string path1 = "";
            //获取父节点
            TreeNode node1 = treeView1.SelectedNode.Parent;
            //i从1开始则不包括media
            for (int i = 1; i < treeView1.SelectedNode.Level; i++)
            {
                //获取父节点的文本
                string text = node1.Text;
                //获取父节点的父节点
                node1 = node1.Parent;
                path1 = text + "\\" + path1;
            }
            path = path + "\\" + path1 + "\\新子节点";
            DirectoryInfo info = new DirectoryInfo(path);
            if (!info.Exists)
            {
                //节点可能文件夹重名,因为都是'新子节点'
                info.Create();
            }
            else
            {
                treeView1.SelectedNode.Nodes.Remove(node);
            }
        }

        private void PopulateTreeView(){
            treeView1.Nodes.Clear();
            TreeNode rootNode;
            string path = "D:\\武术套路计分系统\\SportsScoringSystem\\media";
            DirectoryInfo info = new DirectoryInfo(path);
            if (info.Exists)
            {
                rootNode = new TreeNode(info.Name);
                rootNode.Tag = info;
                GetDirectories(info.GetDirectories(), rootNode);
                treeView1.Nodes.Add(rootNode);
            }
        }

        private void GetDirectories(DirectoryInfo[] subDirs, TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            DirectoryInfo[] subsubDirs;
            foreach (DirectoryInfo subDir in subDirs)
            {
                aNode = new TreeNode(subDir.Name, 0, 0);
                aNode.Tag = subDir;
                subsubDirs = subDir.GetDirectories();
                // DirectoryInfo.Length表示子目录的个数
                if (subsubDirs.Length != 0)
                    GetDirectories(subsubDirs, aNode);
                nodeToAddTo.Nodes.Add(aNode);
            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            //选项卡切换事件
            if (e.TabPageIndex == 1)
            {
                //选中播放列表
                //刷新树形菜单
                PopulateTreeView();
            }
        }

        private void button40_Click(object sender, EventArgs e)
        {
            //播放视频
            button43.Enabled = true;
            button41.Enabled = true;
            button42.Enabled = true;
            button41.Text = "暂停";
            button41.Click -= button41_Click1;
            button41.Click += button41_Click;
            ((System.ComponentModel.ISupportInitialize)(axWindowsMediaPlayer1)).BeginInit();
            axWindowsMediaPlayer1.Enabled = true;
            axWindowsMediaPlayer1.Location = new System.Drawing.Point(0, 0);
            axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            axWindowsMediaPlayer1.Size = new System.Drawing.Size(form1.Width, form1.Height);
            axWindowsMediaPlayer1.TabIndex = 0;
            
            //清除屏幕比赛信息
            form1.Controls.Clear();
            //加入视频控件
            form1.Controls.Add(axWindowsMediaPlayer1);
            //播放视频
            axWindowsMediaPlayer1.URL = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            //全屏
            //axWindowsMediaPlayer1.fullScreen = true;
            //隐藏功能栏
            axWindowsMediaPlayer1.uiMode = "none";
            
            ((System.ComponentModel.ISupportInitialize)(axWindowsMediaPlayer1)).EndInit();
            //播放完毕后返回主界面
            axWindowsMediaPlayer1.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(axWindowsMediaPlayer1_PlayStateChange);
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == 1)
            {
                button43.Enabled = false;
                button41.Enabled = false;
                button42.Enabled = false;
                form1.Controls.Clear();
                //播放完毕后检查原先有没有开始比赛
                //打开比赛---->屏幕上显示参赛的信息
                if (NowProject != -1)
                {
                    if (NowGame != -1)
                    {
                        //跳转选项卡
                        tabControl1.SelectedIndex = 5;
                        //屏幕上显示参赛的信息
                        isGaming = 1;//打开了比赛
                        button8.Enabled = true;
                        if (form1.size == 0)
                        {
                            //不显示时钟
                            DrawProject("不显示", form1.size);
                        }
                        else
                        {
                            if (form1.x != 0)
                            {
                                //右侧
                                DrawProject("右侧", form1.size);
                            }
                            else
                            {
                                //左侧
                                DrawProject("左侧", form1.size);
                            }
                        }
                    }
                }
            }
        }

        private void button41_Click(object sender, EventArgs e)
        {
            //暂停视频
            axWindowsMediaPlayer1.Ctlcontrols.pause();
            button41.Text = "继续";
            button41.Click -= button41_Click;
            button41.Click += button41_Click1;
        }

        private void button41_Click1(object sender, EventArgs e)
        {
            //继续播放
            axWindowsMediaPlayer1.Ctlcontrols.play();
            button41.Text = "暂停";
            button41.Click -= button41_Click1;
            button41.Click += button41_Click;
        }

        private void button42_Click(object sender, EventArgs e)
        {
            //停止视频
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void button43_Click(object sender, EventArgs e)
        {
            //播放器选项
            //打开选项窗口
            MediaOptions mediaOptions = new MediaOptions();
            mediaOptions.ShowDialog();
            if (mediaOptions.isSure == 1)
            {
                //确认
                if (mediaOptions.Tool == 1)
                {
                    //显示工具栏
                    axWindowsMediaPlayer1.uiMode = "full";

                }
                else
                {
                    //不显示工具栏
                    axWindowsMediaPlayer1.uiMode = "none";                        
                }
                if (mediaOptions.circulate == 1)
                {
                    //循环播放
                    axWindowsMediaPlayer1.settings.setMode("loop", true);
                }
                else{
                    //不循环播放
                    axWindowsMediaPlayer1.settings.setMode("loop", false);
                }
                if(mediaOptions.menu == 1)
                {
                    //显示右键菜单
                    axWindowsMediaPlayer1.enableContextMenu = false;
                }
                else
                {
                    //不显示右键菜单
                    axWindowsMediaPlayer1.enableContextMenu = true;
                }
            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            //开始
            button37.Enabled = true;
            //Graphics g = panel.CreateGraphics();
            //form1.DrawCountDown(g);
        }

        private void button37_Click(object sender, EventArgs e)
        {
            //暂停
            button37.Enabled = false;
            button38.Enabled = true;
        }

        private void button38_Click(object sender, EventArgs e)
        {
            //继续
            button38.Enabled = false;
            button37.Enabled = true;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //显示端
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //播放端
        }

        //***********************************************************************************
        Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private void button18_Click(object sender, EventArgs e)
        {
            //打分器
            Scorer scorer= new Scorer(socketWatch);
            scorer.ShowDialog(this);
            socketWatch = scorer.socketWatch;
            //设置监听队列
            //最多10个打分器
            socketWatch.Listen(10);//某个时间段内 能够连入服务器的 最大 客户数量
            Thread th = new Thread(Listen);
            th.IsBackground = true;
            //开始监听
            th.Start(socketWatch);
        }

        /// <summary>
        /// 等待客户端的连接 并创建与之通信的Socket
        /// </summary>
        Socket socketSend;//创建负责通讯的Socket
        private void Listen(object o)
        {
            Socket socketWatch = o as Socket;//o转为Socket,否则返回null
            try
            {
                while (true)
                {
                    //等待客户端连接 并创建一个负责通信的Socket
                    socketSend = socketWatch.Accept();
                    ShowMsg(socketSend.RemoteEndPoint.ToString() + "连接成功");
                    //开启一个线程 不停接收客户端发送过来的消息

                    //客户端连接成功之后，服务器接受客户端发来的消息

                    /*只能接受一次 字符*/
                    //byte[] buffer = new byte[1024 * 1024 * 2];
                    //int r = socketSend.Receive(buffer);
                    //string str = Encoding.UTF8.GetString(buffer, 0, r);
                    //ShowMsg(socketSend.RemoteEndPoint + ":" + str);

                    Thread th = new Thread(Recive);
                    th.IsBackground = true;
                    th.Start(socketSend);
                }
            }
            catch
            {

            }

        }

        /// <summary>
        ///服务器端 不停接收 客户端 发送过来的消息 
        /// </summary>
        private void Recive(object o)
        {
            Socket socketSend = o as Socket;
            while (true)
            {
                try
                {
                    //客户端连接成功后，服务器应该接收客户发来的消息
                    byte[] buffer = new byte[1024 * 1024 * 2];
                    //实际接收的有效字节数
                    int r = socketSend.Receive(buffer);
                    if (r == 0)
                    {
                        break;
                    }
                    string str = Encoding.UTF8.GetString(buffer, 0, r);
                    ShowMsg(socketSend.RemoteEndPoint + ":" + str);

                }
                catch
                {

                }
            }
        }

        private void ShowMsg(string str)
        {
            MessageBox.Show(str);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string str = "测试";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
            socketSend.Send(buffer);
        }
        //**********************************************************************

        private void button17_Click(object sender, EventArgs e)
        {
            //快捷键
        }

        private void button19_Click(object sender, EventArgs e)
        {
            //辅助功能
        }

        private void button22_Click(object sender, EventArgs e)
        {
            //欢迎信息
            //清空之前的比赛
            form1.Controls.Clear();
            //跳转选项卡
            tabControl1.SelectedIndex = 5;
            if (form1.size == 0)
            {
                //不显示时钟
                DrawWelcomeInfo("不显示", form1.size);
            }
            else
            {
                if (form1.x != 0)
                {
                    //右侧
                    DrawWelcomeInfo("右侧", form1.size);
                }
                else
                {
                    //左侧
                    DrawWelcomeInfo("左侧", form1.size);
                }
            }
        }

        private void DrawWelcomeInfo(string location,int size)
        {
            //绘制欢迎信息
            //绘制背景
            Panel panel = new Panel();
            panel.Location = new Point(0, 0);
            panel.Size = new Size(1920, 1080);
            panel.BackColor = form1.BackColor;
            //绘制文字
            if (location == "左侧")
            {
                panel.Location = new Point(size, 0);
            }
            else if (location == "右侧")
            {
                panel.Location = new Point(0, 0);
            }
            else if(location=="不显示")
            {
                //不显示
                panel.Location = new Point(0, 0);
            }
            //panel实现滚动效果
            panel.AutoScroll = true;
            //添加到窗体
            form1.Controls.Add(panel);
            Graphics g = panel.CreateGraphics();
            Pen pen = new Pen(Color.Blue, 2);
            //绘制文字
            Font font = new Font("宋体", 50);
            SolidBrush brush = new SolidBrush(Color.Red);
            g.DrawString("欢迎来到比赛", font, brush, 100, 100);
            g.DrawString("欢迎来到比赛", font, brush, 100, 200);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            //比赛信息
            //清空之前的比赛
            form1.Controls.Clear();
            //跳转选项卡
            tabControl1.SelectedIndex = 5;
            if (form1.size == 0)
            {
                //不显示时钟
                DrawGameInfo("不显示", form1.size);
            }
            else
            {
                if (form1.x != 0)
                {
                    //右侧
                    DrawGameInfo("右侧", form1.size);
                }
                else
                {
                    //左侧
                    DrawGameInfo("左侧", form1.size);
                }
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            //成绩信息
            if (isGaming == 1)
            {
                //清空之前的比赛
                form1.Controls.Clear();
                //跳转选项卡
                tabControl1.SelectedIndex = 5;
                //屏幕上显示参赛的信息
                isGaming = 1;//打开了比赛
                button8.Enabled = true;
                if (form1.size == 0)
                {
                    //不显示时钟
                    DrawProject("不显示", form1.size);
                }
                else
                {
                    if (form1.x != 0)
                    {
                        //右侧
                        DrawProject("右侧", form1.size);
                    }
                    else
                    {
                        //左侧
                        DrawProject("左侧", form1.size);
                    }
                }
            }
            else
            {
                MessageBox.Show("请先开始比赛");
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //裁判台里面B组演练水平数据变化
            int num1 = (int)numericUpDown1.Value;
            int num2 = (int)numericUpDown2.Value;
            int num3 = (int)numericUpDown3.Value;
            int num4 = (int)numericUpDown4.Value;
            int num5 = (int)numericUpDown5.Value;
            //计算平均值
            double avg = (num1 + num2 + num3 + num4 + num5) / 5.0;
            //显示平均值
            label17.Text = avg.ToString();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            //裁判台里面B组演练水平数据变化
            int num1 = (int)numericUpDown1.Value;
            int num2 = (int)numericUpDown2.Value;
            int num3 = (int)numericUpDown3.Value;
            int num4 = (int)numericUpDown4.Value;
            int num5 = (int)numericUpDown5.Value;
            //计算平均值
            double avg = (num1 + num2 + num3 + num4 + num5) / 5.0;
            //显示平均值
            label17.Text = avg.ToString();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            //裁判台里面B组演练水平数据变化
            int num1 = (int)numericUpDown1.Value;
            int num2 = (int)numericUpDown2.Value;
            int num3 = (int)numericUpDown3.Value;
            int num4 = (int)numericUpDown4.Value;
            int num5 = (int)numericUpDown5.Value;
            //计算平均值
            double avg = (num1 + num2 + num3 + num4 + num5) / 5.0;
            //显示平均值
            label17.Text = avg.ToString();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            //裁判台里面B组演练水平数据变化
            int num1 = (int)numericUpDown1.Value;
            int num2 = (int)numericUpDown2.Value;
            int num3 = (int)numericUpDown3.Value;
            int num4 = (int)numericUpDown4.Value;
            int num5 = (int)numericUpDown5.Value;
            //计算平均值
            double avg = (num1 + num2 + num3 + num4 + num5) / 5.0;
            //显示平均值
            label17.Text = avg.ToString();
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            //裁判台里面B组演练水平数据变化
            int num1 = (int)numericUpDown1.Value;
            int num2 = (int)numericUpDown2.Value;
            int num3 = (int)numericUpDown3.Value;
            int num4 = (int)numericUpDown4.Value;
            int num5 = (int)numericUpDown5.Value;
            //计算平均值
            double avg = (num1 + num2 + num3 + num4 + num5) / 5.0;
            //显示平均值
            label17.Text = avg.ToString();
        }

        private void label17_TextChanged(object sender, EventArgs e)
        {
            //同步更新屏幕上的分数
            //成绩信息
            if (isGaming == 1)
            {
                //清空之前的比赛
                form1.Controls.Clear();
                //跳转选项卡
                tabControl1.SelectedIndex = 5;
                //屏幕上显示参赛的信息
                isGaming = 1;//打开了比赛
                button8.Enabled = true;
                if (form1.size == 0)
                {
                    //不显示时钟
                    DrawProject("不显示", form1.size);
                }
                else
                {
                    if (form1.x != 0)
                    {
                        //右侧
                        DrawProject("右侧", form1.size);
                    }
                    else
                    {
                        //左侧
                        DrawProject("左侧", form1.size);
                    }
                }
                //获取panel
                Panel panel = form1.Controls[0] as Panel;
                //获取画笔
                Graphics g = panel.CreateGraphics();
                //获取画笔
                Pen pen = new Pen(Color.Red, 2);
                //获取字体
                Font font = new Font("宋体", 20);
                //获取字符串
                string str = label17.Text;
                //加入到屏幕上 (B组)演练水平右边
                int width = form1.Width - form1.size;
                int height = form1.Height;
                g.DrawString(str, font, Brushes.Red, width/3, height / 3);
            }
            else
            {
                
            }
        }
    }
}
