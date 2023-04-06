using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace SportsScoringSystem
{
    public partial class Form1 : Form
    {
        private System.Timers.Timer timer1 = new System.Timers.Timer();

        public int x=0, y=0;//默认为左侧
        public int size = 200;

        public Form1()
        {
            InitializeComponent();
        }
        //画动态时钟
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.Black);
            //屏幕闪烁，所以采用双缓冲技术
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            //画时钟
            DrawClock(g,x,y,size);
            //画秒针
            DrawSecond(g, x, y, size);
            //画分针
            DrawMinute(g, x, y, size);
            //画时针
            DrawHour(g, x, y, size);
            //画数字时间
            DrawTime(g, x, y, size);
        }
        //画时钟
        private void DrawClock(Graphics g,int x,int y,int size)
        {
            //画外圆
            g.FillEllipse(new SolidBrush(Color.Black), x, y, size, size);
            //画内圆
            g.FillEllipse(new SolidBrush(Color.Black), x+10, y+10, size-20, size-20);
            //画刻度
            for (int i = 0; i < 60; i++)
            {
                if (i % 5 == 0)
                {
                    //大段
                    g.DrawLine(new Pen(Color.White, 2), x+size/2 + (size*0.9f/2) * (float)Math.Sin(i * 6 * Math.PI / 180), y+size/2 - (size*0.9f/2) * (float)Math.Cos(i * 6 * Math.PI / 180), x+size/2 + (size*0.8f/2) * (float)Math.Sin(i * 6 * Math.PI / 180), y+size/2 - (size*0.8f/2) * (float)Math.Cos(i * 6 * Math.PI / 180));
                }
                else
                {

                    g.DrawLine(new Pen(Color.White, 1), x+size/2 + (size*0.9f/2) * (float)Math.Sin(i * 6 * Math.PI / 180), y+size/2 - (size*0.9f/2) * (float)Math.Cos(i * 6 * Math.PI / 180), x+size/2 + (size*0.85f/2) * (float)Math.Sin(i * 6 * Math.PI / 180), y+size/2 - (size*0.85f/2) * (float)Math.Cos(i * 6 * Math.PI / 180));
                }
            }
        }
        //画秒针
        private void DrawSecond(Graphics g,int x,int y,int size)
        {
            g.DrawLine(new Pen(Color.Red, 1), x+size/2, y + size / 2, x+size / 2 + size*0.8f / 2 * (float)Math.Sin(DateTime.Now.Second * 6 * Math.PI / 180), y+size / 2 - size*0.8f / 2 * (float)Math.Cos(DateTime.Now.Second * 6 * Math.PI / 180));
        }
        //画分针
        private void DrawMinute(Graphics g,int x,int y, int size)
        {
            g.DrawLine(new Pen(Color.Blue, 2), x+size / 2, y + size / 2, x+size / 2 + size*0.7f / 2 * (float)Math.Sin(DateTime.Now.Minute * 6 * Math.PI / 180), y+size / 2 - size*0.7f / 2 * (float)Math.Cos(DateTime.Now.Minute * 6 * Math.PI / 180));
        }
        //画时针
        private void DrawHour(Graphics g,int x,int y, int size)
        {
            g.DrawLine(new Pen(Color.Green, 4), x+size / 2, y+size / 2, x+size / 2 + size*0.6f / 2 * (float)Math.Sin((DateTime.Now.Hour * 30 + DateTime.Now.Minute * 0.5) * Math.PI / 180), y+size / 2 - size*0.6f / 2 * (float)Math.Cos((DateTime.Now.Hour * 30 + DateTime.Now.Minute * 0.5) * Math.PI / 180));
        }
        //画数字时间
        private void DrawTime(Graphics g, int x,int y,int size)
        {
            //在表盘下方显示时间
            g.DrawString(DateTime.Now.ToString(), new Font("宋体", size*0.06f), new SolidBrush(Color.White), x+size*0.2f, y+size+size*0.06f);
        }

        //画倒计时
        public void DrawCountDown(Graphics g, int x, int y, int size)
        {
            //直接显示数字时间,后根据需求增加表盘之类的需求

        }

        //定时器
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Elapsed += new ElapsedEventHandler(timer1_Elapsed);
            timer1.Enabled = true;
        }
        //定时器事件
        private void timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Invalidate();
        }


        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            //显示鼠标
            Cursor.Show();
        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            //隐藏鼠标
            Cursor.Hide();
        }

        //修改所画时钟的大小
        public void Clock_Resize(string location,int size)
        {
            //先清除原来的时钟
            timer1.Enabled = false;
            //清除屏幕
            //获取背景颜色
            Color color = this.BackColor;
            //清除屏幕
            Graphics g = this.CreateGraphics();
            g.Clear(color);
            if (location == "不显示")
            {
                this.size = 0;
            }
            else if (location == "左侧")
            {
                //0,0
                this.DrawClock(g, 0, 0, size);
                //画秒针
                DrawSecond(g,0,0,size);
                //画分针
                DrawMinute(g, 0,0,size);
                //画时针
                DrawHour(g, 0,0,size);
                //画数字时间
                DrawTime(g, 0,0,size);
                //动态时钟
                this.size = size;
                this.x = 0; this.y = 0;
                timer1.Enabled = true;
            }
            else if (location == "右侧")
            {
                //width-size,0
                this.DrawClock(g, this.Width - size, 0, size);
                //画秒针
                DrawSecond(g, this.Width - size, 0, size);
                //画分针
                DrawMinute(g, this.Width - size, 0, size);
                //画时针
                DrawHour(g, this.Width - size, 0, size);
                //画数字时间
                DrawTime(g, this.Width - size, 0, size);
                //动态时钟
                this.size = size;
                this.x = this.Width - size;
                this.y = 0;
                timer1.Enabled = true;
            } 
        }
    }
}
