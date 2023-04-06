using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SportsScoringSystem
{
    public partial class Scorer : Form
    {

        //创建监听的Socket
        public Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public Scorer()
        {
            InitializeComponent();
        }
        public Scorer(Socket socketWatch)
        {
            InitializeComponent();
            this.socketWatch = socketWatch;
        }

        private void Scorer_Load(object sender, EventArgs e)
        {
            //获取局域网下的全部终端
            string[] str = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.Select(p => p.ToString()).ToArray();
            //获取本机IP
            string ip = str[str.Length - 1];
            //获取本机主机名
            string hostName = System.Net.Dns.GetHostEntry(ip).HostName;
            //设置label1为本机IP
            label1.Text = ip + " "+ hostName;
            //截取网段
            ip = ip.Substring(0, ip.LastIndexOf(".") + 1);
            //遍历网段下的全部IP
            for (int i = 1; i < 255; i++)
            {
                //创建Ping对象
                System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
                //异步发送Ping包
                ping.SendAsync(ip + i, 1000, ip + i);
                //Ping包发送完成后触发PingCompleted事件
                ping.PingCompleted += new System.Net.NetworkInformation.PingCompletedEventHandler(ping_PingCompleted);
            }
            label2.Text = "扫描完成。";
        }
        //Ping包发送完成后触发PingCompleted事件
        void ping_PingCompleted(object sender, System.Net.NetworkInformation.PingCompletedEventArgs e)
        {
            //判断是否Ping通
            if (e.Reply.Status == System.Net.NetworkInformation.IPStatus.Success)
            {
                //获取Ping通的IP
                string ip = e.UserState.ToString();
                //获取Ping通的IP的主机名
                //string hostName = System.Net.Dns.GetHostEntry(ip).HostName;
                //向窗体中添加label控件记录IP和对应的按钮
                Label label = new Label();
                label.Text = ip;
                label.Location = new Point(10, 10 + this.Controls.Count * 20);
                this.Controls.Add(label);
                Button button = new Button();
                button.Text = "连接";
                button.Location = new Point(label.Location.X + label.Width + 10, label.Location.Y);
                button.Click += new EventHandler(button_Click);
                button.Tag = ip;
                this.Controls.Add(button);
                //再在按钮后面添加一个label控件显示连接状态
                Label label2 = new Label();
                //唯一标识
                label2.Name = ip;
                label2.Text = "未连接";
                label2.Location = new Point(button.Location.X + button.Width + 10, button.Location.Y);
                this.Controls.Add(label2);
            }
        }
        //button_Click：监听ip端口
        void button_Click(object sender, EventArgs e)
        {
            //获取按钮
            Button button = (Button)sender;
            //获取按钮的Tag属性
            //string ip = button.Tag.ToString();
            //与该ip对应的手机建立连接
            
            //创建监听的Socket
            
            //创建负责监听的Socket
            //创建IP地址和端口号对象
            IPAddress ip = IPAddress.Parse(button.Tag.ToString());//IPAddress.Parse(txtServer.Text);
            IPEndPoint point = new IPEndPoint(ip, 8888);
            //负责监听的Socket绑定IP地址和端口号
            //捕获异常
            try
            {
                socketWatch.Bind(point);
                //获取label中Name属性为ip的label控件
                Label label = (Label)this.Controls.Find(button.Tag.ToString(), true)[0];
                //设置label的Text属性为已连接
                label.Text = "已连接";
                
            }
            catch
            {
                MessageBox.Show("连接失败");
            }
        }
    }
}
