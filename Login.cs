using SportsScoringSystem.DBUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SportsScoringSystem
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DbHelperOleDb.connectionString = DbHelperOleDb.oldconnectionString;
            DAL.User user = new DAL.User();
            Model.User model = new Model.User();
            if (user.Exists(textBox1.Text))
            {
                //存在用户
                model = user.GetModel(textBox1.Text);
                if (model.password == textBox2.Text)
                {
                    //密码正确
                    MessageBox.Show("登录成功", "欢迎", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    Main main = new Main();
                    main.Show();
                }
                else
                {
                    MessageBox.Show("登录失败,请重试", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("登录失败,请重试", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
