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

namespace Trackingcar
{
    public partial class Form1 : Form
    {

        ////声明读写INI文件的API函数
        //[DllImport("kernel32")]
        //private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        //[DllImport("kernel32")]

        public bool Send_status = true; //发送状态

        public Form1()
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;   //防止跨线程访问出错，好多地方会用到 


        }

        SerialPort Port1 = new SerialPort();//串行端口实例化

        public int BaudRate;//波特率

        private void Form1_Load(object sender, EventArgs e)
        {
            direction_Init();
            delayTime_Init();
            timePresent_Init();
            status_Init();

        }

        #region 初始化

        private void direction_Init()
        {
            List<string> dire = new List<string>();
            dire.Add("正序");
            dire.Add("倒序");
            cmb_Direction.Text = dire[0];
            cmb_Direction.Items.Add(dire[0]);
            cmb_Direction.Items.Add(dire[1]);
        }

        private void timePresent_Init()
        {
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();
            string time = DateTime.UtcNow.ToString();

            tsl_Date.Text = "      " + year + "/" + month + "/" + day + "           ";
        }

        private void status_Init()
        {

            txt_CarNum.Text = "";
            txt_CardNum.Text = "";
            txt_CarSpeed.Text = "";
            txt_Person.Text = "";
            txt_ScrnSpeed.Text = "";
            txt_Fan.Text = "";


        }

        #endregion

        #region 延时下拉框

        private void delayTime_Init()
        {

            one_Delay.Text = "0 分钟";
            two_Delay.Text = "0 分钟";
            three_Delay.Text = "0 分钟";
            four_Delay.Text = "0 分钟";
            five_Delay.Text = "0 分钟";
            six_Delay.Text = "0 分钟";
            seven_Delay.Text = "0 分钟";
            eight_Delay.Text = "0 分钟";
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

        }


        #endregion

        #region 串口检测
        private void SearchPort()//串口检测
        {
            try
            {
                string[] ports = SerialPort.GetPortNames();//获取当前计算机的所有串行接口名

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
            catch
            {
                return;
            }
        }

        private void btn_Open_Click(object sender, EventArgs e)//串口打开
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

                            Port1.Open();
                            MessageBox.Show("打开成功！");
                            tsl_Show.Text = cmb_SerialPort.Text + " 串口已打开                                                                                     ";
                            btn_Open.Text = "关闭串口";
                        }
                    }
                    else if (Port1.IsOpen == true)
                    {
                        Port1.Close();
                        btn_Open.Text = "打开串口";

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

        private void btn_Search_Click(object sender, EventArgs e)
        {
            cmb_SerialPort.Items.Clear();//从combobox1中移除所有项
            SearchPort();//串口检测
        }

        #endregion

        #region 控制小车

        byte[] Port_Buffer = new byte[6];

        private void btn_Forward_Click(object sender, EventArgs e)//前进 //用串口发送数据 //0xff
        {
            if (Send_status)
            {
                if (Port1.IsOpen)
                {
                    Port_Buffer[0] = 0xf5;
                    Port_Buffer[1] = 0xff;  //
                    Port_Buffer[2] = 0xe1;
                    Port_Buffer[3] = 0xd1;
                    Port_Buffer[4] = 0xfc;
                    Port1.Write(Port_Buffer, 0, Port_Buffer.Length);
                }
                else
                {
                    MessageBox.Show("请先打开串口");
                }
            }
        }

        private void btn_Back_Click(object sender, EventArgs e)//后退
        {
            if (Send_status)
            {
                if (Port1.IsOpen)
                {
                    Port_Buffer[0] = 0xf5;
                    Port_Buffer[1] = 0xfe;  //
                    Port_Buffer[2] = 0xe1;
                    Port_Buffer[3] = 0xd1;
                    Port_Buffer[4] = 0xfc;
                    Port1.Write(Port_Buffer, 0, Port_Buffer.Length);
                }
                else
                {
                    MessageBox.Show("请先打开串口");
                }
                //  RobotEngine2.SendCMD(controlType, CMD_Backward, comm);
                //  Send_status = false;
            }
        }

        private void btn_Stop_Click(object sender, EventArgs e)//停止
        {
            Send_status = true;

            if (Port1.IsOpen)
            {
                Port_Buffer[0] = 0xf5;
                Port_Buffer[1] = 0xfd;  //
                Port_Buffer[2] = 0xe1;
                Port_Buffer[3] = 0xd1;
                Port_Buffer[4] = 0xfc;
                Port1.Write(Port_Buffer, 0, Port_Buffer.Length);
            }
            else
            {
                MessageBox.Show("请先打开串口");
            }

        }
        #endregion

        #region 接收数据

        public delegate void TichText();  //声明委托
        string strx;
        StringBuilder builder = new StringBuilder();
        char[] CHAR = new char[8];
        int BytCount;
        public void Deleg()   //用于数据处理
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

                if (Convert.ToInt32(CHAR[0]) == 0xf9)
                {
                    txt_CarNum.Text = Convert.ToInt32(CHAR[1]).ToString();
                    txt_ScrnSpeed.Text = Convert.ToInt32(CHAR[2]).ToString();
                    txt_Fan.Text = Convert.ToInt32(CHAR[3]).ToString();

                    return;
                }
                if (Convert.ToInt32(CHAR[0]) == 0xf7)
                {
                    txt_CardNum.Text = Convert.ToInt32(CHAR[1]).ToString();

                    return;
                }
                if (Convert.ToInt32(CHAR[0]) == 0xf5)
                {

                    txt_CarSpeed.Text = Convert.ToInt32(CHAR[2]).ToString();

                    txt_Person.Text = Convert.ToInt32(CHAR[3]).ToString();
                    return;
                }
            }
        }

        private void Port1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            TichText text = Deleg;
            char CH;
            try
            {
                while (Port1.BytesToRead > 0)
                {
                    //  MessageBox.Show("字节数", Port1.BytesToRead.ToString(), MessageBoxButtons.OK);

                    do
                    {
                        BytCount = Port1.BytesToRead;
                    } while (BytCount < 8);
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

        #endregion

        #region 菜单
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void btn_Save_Click(object sender, EventArgs e)
        {

        }

        private void btn_Change_Click(object sender, EventArgs e)
        {

        }

        private void btn_Forward_Click_1(object sender, EventArgs e)
        {

        }

        private void btn_Stop_Click_1(object sender, EventArgs e)
        {

        }

        private void btn_Back_Click_1(object sender, EventArgs e)
        {

        }


    }
}

