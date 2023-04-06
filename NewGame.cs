using SportsScoringSystem.DAL;
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
    public partial class NewGame : Form
    {
        public Model.GameShow gameShow = new Model.GameShow();
        private int projectId;
        public NewGame(int index,string[] teamName)
        {
            InitializeComponent();
            projectId = index;
            gameShow.Umpires = new List<Model.Umpire> { };
            comboBox1.Items.AddRange(teamName);
            comboBox2.Items.AddRange(teamName);
        }
        public NewGame(Model.GameShow show)
        {
            InitializeComponent();
            gameShow.Umpires = new List<Model.Umpire> { };
            gameShow = show;
            //将string 转化为decimal
            numericUpDown1.Value = Convert.ToDecimal(show.Time);
            comboBox1.Text = show.Team1;
            comboBox2.Text = show.Team2;
            comboBox3.Text = show.Color1;
            comboBox4.Text = show.Color2;
            textBox1.Text = show.Clothes1;
            textBox2.Text = show.Clothes2;
            foreach (Model.Umpire umpire in show.Umpires)
            {
                string[] strings = new string[]
                {
                    umpire.Type,
                    umpire.Uname,
                    umpire.Country,
                };
                dataGridView1.Rows.Add(strings);
            }
            button6.Click -= button6_Click;
            button6.Click += button6_Click1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //确认
            string time = numericUpDown1.Value.ToString();//场次
            //参赛队
            string team1 = comboBox1.Text;//赛队1
            string team2 = comboBox2.Text;//赛队2
            string color1 = comboBox3.Text;//颜色1
            string color2 = comboBox4.Text;//颜色2
            string clother1 = textBox1.Text;//服装1
            string clother2 = textBox2.Text;//服装2
            gameShow.Time = time;
            gameShow.Team1 = team1;
            gameShow.Team2 = team2;
            gameShow.Color1 = color1;
            gameShow.Color2 = color2;
            gameShow.Clothes1 = clother1;
            gameShow.Clothes2 = clother2;
            //下面代码在新增裁判时已经生效了
            //裁判datagridview
            //List<Model.Umpire> umpires = new List<Model.Umpire> { };
            //for (int i = 0; i < gameShow.Umpires.Count; i++)
            //{
            //    Model.Umpire umpire = new Model.Umpire();
            //    umpire.Type = dataGridView1.Rows[i].Cells[0].Value.ToString();
            //    umpire.Uname = dataGridView1.Rows[i].Cells[1].Value.ToString();
            //    umpire.Country = dataGridView1.Rows[i].Cells[2].Value.ToString();
            //    umpires.Add(umpire);
            //}
            //gameShow.Umpires = umpires;
            //更新数据库
            //1、插入game表
            DAL.Game game = new DAL.Game();
            Model.Game model1 = new Model.Game();
            model1.Time = time;
            model1.Team1 = team1;
            model1.Team2 = team2;
            model1.Color1 = color1;
            model1.Color2 = color2;
            model1.Clothes1 = clother1;
            model1.Clothes2 = clother2;
            int gameId = game.Add(model1);
            model1.Id = gameId;
            //2、插入umpire表
            DAL.Umpire umpire1 = new DAL.Umpire();
            for (int i = 0; i < gameShow.Umpires.Count; i++)
            {
                Model.Umpire umpire2 = gameShow.Umpires[i];
                int umpireId = umpire1.Add(umpire2);
                gameShow.Umpires[i].Id = umpireId;
                //3、插入game_umpire表
                DAL.GameUmpire gameUmpire = new DAL.GameUmpire();
                Model.GameUmpire modelUmpire = new Model.GameUmpire();
                modelUmpire.Gid = gameId;
                modelUmpire.Uid = umpireId;
                gameUmpire.Add(modelUmpire);
            }
            //4、插入project_game表
            DAL.ProjectGame projectGame = new DAL.ProjectGame();
            Model.ProjectGame projectGame1 = new Model.ProjectGame();
            projectGame1.Pid = projectId;
            projectGame1.Gid = gameId;
            projectGame.Add(projectGame1);
            this.Close();
        }
        private void button6_Click1(object sender, EventArgs e)
        {
            //更新
            //1、更新[game]表
            DAL.Game game = new DAL.Game();
            Model.Game game1 = new Model.Game();
            string time = numericUpDown1.Value.ToString();//场次
            //参赛队
            string team1 = comboBox1.Text;//赛队1
            string team2 = comboBox2.Text;//赛队2
            string color1 = comboBox3.Text;//颜色1
            string color2 = comboBox4.Text;//颜色2
            string clother1 = textBox1.Text;//服装1
            string clother2 = textBox2.Text;//服装2
            game1.Id = gameShow.Id;
            game1.Time = time;
            game1.Team1 = team1;
            game1.Team2 = team2;
            game1.Color1 = color1;
            game1.Color2 = color2;
            game1.Clothes1 = clother1;
            game1.Clothes2 = clother2;
            game.Update(game1);
            //2、删除[umpire]表
            DAL.Umpire umpire = new DAL.Umpire();
            umpire.DeleteByGid(gameShow.Id);
            //3、删除[game_umpire]表
            DAL.GameUmpire gameUmpire = new DAL.GameUmpire();
            gameUmpire.DeleteByGid(gameShow.Id);
            //4、插入数据
            for (int i = 0; i < gameShow.Umpires.Count; i++)
            {
                Model.Umpire umpire2 = gameShow.Umpires[i];
                int umpireId = umpire.Add(umpire2);
                gameShow.Umpires[i].Id = umpireId;
                //3、插入game_umpire表
                Model.GameUmpire modelUmpire = new Model.GameUmpire();
                modelUmpire.Gid = gameShow.Id;
                modelUmpire.Uid = umpireId;
                gameUmpire.Add(modelUmpire);
            }
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //新建裁判
            NewUmpire newUmpire = new NewUmpire();
            newUmpire.ShowDialog();
            if (newUmpire.umpire.Uname != null)
            {
                gameShow.Umpires.Add(newUmpire.umpire);
                string type = newUmpire.umpire.Type;
                string uname = newUmpire.umpire.Uname;
                string country = newUmpire.umpire.Country;
                dataGridView1.Rows.Add(type, uname, country);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //编辑裁判
            if(dataGridView1.CurrentRow != null)
            {
                int index = dataGridView1.CurrentRow.Index;
                NewUmpire newUmpire = new NewUmpire(gameShow.Umpires[index]);
                newUmpire.ShowDialog();
                if (newUmpire.umpire.Uname != null)
                {
                    gameShow.Umpires[index] = newUmpire.umpire;
                    string type = newUmpire.umpire.Type;
                    string uname = newUmpire.umpire.Uname;
                    string country = newUmpire.umpire.Country;
                    dataGridView1.Rows[index].SetValues(type, uname, country);
                }
            }
            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //删除
            int index = dataGridView1.CurrentRow.Index;
            dataGridView1.Rows.RemoveAt(index);
            gameShow.Umpires.RemoveAt(index);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //导入
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //导出
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //取消
            gameShow = null;
            this.Close();
        }
    }
}
