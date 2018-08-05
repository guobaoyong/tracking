using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PreviewDemo;

namespace Trackingcar
{
    public partial class Form1 : Form
    {

        ////声明读写INI文件的API函数
        //[DllImport("kernel32")]
        //private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        //[DllImport("kernel32")]

        //发送状态
        public bool Send_status = true;

        //初始窗体长宽
        private float X, Y;
        //获取系统当前时间
        string year = DateTime.Now.Year.ToString();
        string month = DateTime.Now.Month.ToString();
        string day = DateTime.Now.Day.ToString();
        string hour = DateTime.Now.Hour.ToString();
        string minute = DateTime.Now.Minute.ToString();
        string second = DateTime.Now.Second.ToString();
        public Form1()
        {
            //初始化
            InitializeComponent();

            //防止跨线程访问出错，好多地方会用到
            Control.CheckForIllegalCrossThreadCalls = false;

        }

        //串行端口实例化
        SerialPort Port1 = new SerialPort();

        //波特率
        public int BaudRate;

        //主窗体
        private void Form1_Load(object sender, EventArgs e)
        {
            //初始化
            direction_Init();
            //延时下拉框
            delayTime_Init();
            //时间初始化
            timePresent_Init();
            status_Init();

            //自定义放大
            this.Resize += new EventHandler(Form1_Resize);//执行Form1_Resize方法
            X = this.Width;
            Y = this.Height;
            setTag(this);

        }

        #region 初始化

        //方向初始化，对应窗体右侧小车方向，正序和倒序
        private void direction_Init()
        {
            List<string> dire = new List<string>();
            dire.Add("正序");
            dire.Add("倒序");
            cmb_Direction.Items.Add(dire[0]);
            cmb_Direction.Items.Add(dire[1]);
            //ComboBox 控件的属性selectedindex是控制默认值的，如果selectedindex为-1，则默认值为空，若selectedindex为n，则默认值为选项列表中的第n个选项。
            this.cmb_Direction.SelectedIndex = 0;

        }

        //时间初始化，对应窗体最下边获取系统当前时间
        private void timePresent_Init()
        {
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();
            string time = DateTime.UtcNow.ToString();

            tsl_Date.Text = "      " + year + "/" + month + "/" + day + "                                                           ";
        }

        //状态初始化，对应窗体最左边小车状态
        private void status_Init()
        {

            txt_CarNum.Text = "";
            txt_CardNum.Text = "";
            txt_CarSpeed.Text = "";
            txt_Person.Text = "";
            txt_ScrnSpeed.Text = "";
            txt_Fan.Text = "";


            txt_CarNum.ReadOnly = true;
            txt_CardNum.ReadOnly = true;
            txt_CarSpeed.ReadOnly = true;
            txt_Person.ReadOnly = true;
            txt_ScrnSpeed.ReadOnly = true;
            txt_Fan.ReadOnly = true;

        }

        #endregion

        #region 延时下拉框

        //延时方法，对应窗体右边延时区
        private void delayTime_Init()
        {

            for (int i = 0; i < 100; i++)
            {
                one_Delay.Items.Add(i + " 分钟");
                two_Delay.Items.Add(i + " 分钟");
                three_Delay.Items.Add(i + " 分钟");
                four_Delay.Items.Add(i + " 分钟");
                five_Delay.Items.Add(i + " 分钟");
                six_Delay.Items.Add(i + " 分钟");
                seven_Delay.Items.Add(i + " 分钟");
                eight_Delay.Items.Add(i + " 分钟");
            }
            this.one_Delay.SelectedIndex = 0;
            this.two_Delay.SelectedIndex = 0;
            this.three_Delay.SelectedIndex = 0;
            this.four_Delay.SelectedIndex = 0;
            this.five_Delay.SelectedIndex = 0;
            this.six_Delay.SelectedIndex = 0;
            this.seven_Delay.SelectedIndex = 0;
            this.eight_Delay.SelectedIndex = 0;

        }


        #endregion

        #region 串口检测

        //串口检测
        private void SearchPort()
        {
            try
            {
                //获取当前计算机的所有串行接口名
                string[] ports = SerialPort.GetPortNames();

                foreach (string port in ports)
                {
                    cmb_SerialPort.Items.Add(port);
                }
                if (ports.Length == 1)
                {
                    cmb_SerialPort.Text = ports[0];
                }
                else if (ports.Length > 1)
                {
                    cmb_SerialPort.Text = ports[1];
                }
                else
                {
                    MessageBox.Show("没有发现可用端口！");
                }
            }
            catch (Exception e)
            {
                return;
            }
        }
        bool Send_Status = false;
        //串口打开
        private void btn_Open_Click(object sender, EventArgs e)
        {
            if (cmb_SerialPort.Items.Count != 0)
            {
                try
                {
                    if (btn_Open.Text == "打开串口")
                    {
                        if (Port1.IsOpen == false)
                        {
                            Port1.PortName = cmb_SerialPort.Text;
                            Port1.BaudRate = Convert.ToInt32(cmb_BaudRate.Text);
                            Port1.Handshake = Handshake.None;
                            Port1.Parity = Parity.None;
                            Port1.DataBits = 8;
                            Port1.StopBits = StopBits.One;
                            Port1.RtsEnable = true;
                            Send_Status = true;

                            Port1.Open();
                            MessageBox.Show("打开成功！");
                            tsl_Show.Text = cmb_SerialPort.Text + " 串口已打开                                                                                     ";
                            btn_Open.Text = "关闭串口";

                            btn_Auto.Text = "自动";
                            btn_Back.Enabled = false;
                            btn_Forward.Enabled = false;
                            btn_Stop.Enabled = false;
                        }
                    }
                    else if (Port1.IsOpen == true)
                    {
                        Port1.Close();
                        btn_Open.Text = "打开串口";
                        tsl_Show.Text = "串口已关闭                                                                                    ";

                        btn_Auto.Enabled = false;
                        btn_Back.Enabled = false;
                        btn_Forward.Enabled = false;
                        btn_Stop.Enabled = false;
                    }
                }
                catch
                {
                    MessageBox.Show("端口打开失败，请检查端口是否被占用！", "错误提示");
                }
            }
            else
            {
                MessageBox.Show("没有发现可用端口！", "错误提示");
            }
        }

        ////串口检测
        private void btn_Search_Click(object sender, EventArgs e)
        {
            //从combobox1中移除所有项
            cmb_SerialPort.Items.Clear();

            SearchPort();
        }

        #endregion

        #region 控制小车

        byte[] Port_Buffer = new byte[6];

        //前进 
        //用串口发送数据 
        //0xff
        private void btn_Forward_Click(object sender, EventArgs e)
        {
            if (Send_status)
            {
                if (Port1.IsOpen)
                {
                    //开始位
                    Port_Buffer[0] = 0xf3;
                    //方向，前进 
                    Port_Buffer[1] = 0xff;
                    //速度位
                    Port_Buffer[2] = 0xe1;
                    //待定
                    Port_Buffer[3] = 0xd1;
                    //待定
                    Port_Buffer[4] = 0xfc;
                    //结束位
                    Port_Buffer[5] = 0xfc;
                    //串口写入
                    Port1.Write(Port_Buffer, 0, Port_Buffer.Length);
                }
                else
                {
                    MessageBox.Show("请先打开串口");
                }
            }
        }

        //后退
        private void btn_Back_Click(object sender, EventArgs e)
        {
            if (Send_status)
            {
                if (Port1.IsOpen)
                {
                    Port_Buffer[0] = 0xf3;
                    Port_Buffer[1] = 0xfd;  //
                    Port_Buffer[2] = 0xe1;
                    Port_Buffer[3] = 0xd1;
                    Port_Buffer[4] = 0xd1;
                    Port_Buffer[5] = 0xfc;
                    Port1.Write(Port_Buffer, 0, Port_Buffer.Length);
                }
                else
                {
                    MessageBox.Show("请先打开串口");
                }
            }
        }

        private void btn_Stop_Click(object sender, EventArgs e)//停止
        {
            Send_status = true;

            if (Port1.IsOpen)
            {
                Port_Buffer[0] = 0xf3;
                Port_Buffer[1] = 0xfe;  //
                Port_Buffer[2] = 0xe1;
                Port_Buffer[3] = 0xd1;
                Port_Buffer[4] = 0xd1;
                Port_Buffer[5] = 0xfc;
                Port1.Write(Port_Buffer, 0, Port_Buffer.Length);
            }
            else
            {
                MessageBox.Show("请先打开串口");
            }

        }
        #endregion

        #region 接收数据

        //声明委托
        public delegate void TichText();
        //接受下位机传的数据拼接
        string strx;

        StringBuilder builder = new StringBuilder();
        //接受8个数据位
        char[] CHAR = new char[8];
        //字节长度
        int BytCount;
        //串口数据接收
        private void Port1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //将Deleg方法委托给TichText
            TichText text = Deleg;
            //接受数据拼接单元
            char CH;
            try
            {
                while (Port1.BytesToRead > 0)
                {
                    //  MessageBox.Show("字节数", Port1.BytesToRead.ToString(), MessageBoxButtons.OK);
                    do
                    {
                        BytCount = Port1.BytesToRead;
                    }
                    while (BytCount < 8);

                    while (Port1.BytesToRead > 0)
                    {
                        CH = (char)Port1.ReadByte();

                        builder.Append(CH);
                    }
                }
                this.Invoke(text);
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }

        }

        //用于数据处理
        public void Deleg()
        {
            strx = builder.ToString();

            if (strx.Length == 8)
            {
                CHAR = strx.ToCharArray();

                txt_CarNum.Clear();
                txt_ScrnSpeed.Clear();
                txt_Fan.Clear();
                txt_CardNum.Clear();
                txt_Person.Clear();
                txt_CarSpeed.Clear();

                builder.Remove(0, builder.Length);

                //屏幕数据
                if (Convert.ToInt32(CHAR[0]) == 0xf9 && Convert.ToInt32(CHAR[7]) == 0xfc)
                {
                    txt_CarNum.Text = Convert.ToInt32(CHAR[1]).ToString();
                    txt_ScrnSpeed.Text = Convert.ToInt32(CHAR[2]).ToString();
                    txt_Fan.Text = Convert.ToInt32(CHAR[3]).ToString();

                }
                //RFID数据
                if (Convert.ToInt32(CHAR[0]) == 0xf7 && Convert.ToInt32(CHAR[7]) == 0xfc)
                {
                    txt_CardNum.Text = Convert.ToInt32(CHAR[1]).ToString();

                }
                //串口数据
                if (Convert.ToInt32(CHAR[0]) == 0xf5 && Convert.ToInt32(CHAR[7]) == 0xfc)
                {
                    txt_CarSpeed.Text = Convert.ToInt32(CHAR[2]).ToString();
                    txt_Person.Text = Convert.ToInt32(CHAR[3]).ToString();
                }
            }
        }


        #endregion

        #region 菜单
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 自定义放大
        private void Form1_Resize(object sender, EventArgs e) //调用Resize事件
        {
            float newx = (this.Width) / X;//当前宽度与变化前宽度之比
            float newy = this.Height / Y;//当前高度与变化前宽度之比
            setControls(newx, newy, this);
        }

        private void setControls(float newx, float newy, Control cons)//实现控件以及字体的缩放
        {
            foreach (Control con in cons.Controls)
            {
                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
                float a = Convert.ToSingle(mytag[0]) * newx;
                con.Width = (int)a;
                a = Convert.ToSingle(mytag[1]) * newy;
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;
                con.Top = (int)(a);
                Single currentSize = Convert.ToSingle(mytag[4]) * newy;
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);//递归
                }
            }
        }

        //获得控件的长度、宽度、位置、字体大小的数据
        private void setTag(Control cons)//Control类，定义控件的基类
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;//获取或设置包含有关控件的数据的对象
                if (con.Controls.Count > 0)
                    setTag(con);//递归算法
            }
        }

        #endregion

        private void 摄像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Preview p = new Preview();
            p.Show();
        }

        private void btn_Auto_Click(object sender, EventArgs e)
        {

            if (btn_Auto.Text == "自动")
            {

                btn_Auto.Text = "手动";
                btn_Back.Enabled = true;
                btn_Forward.Enabled = true;
                btn_Stop.Enabled = true;

            }
            else
            {
                return;
            }
        }

    }
}