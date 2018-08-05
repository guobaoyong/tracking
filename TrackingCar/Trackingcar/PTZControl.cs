using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PreviewDemo
{
    public partial class PTZControl : Form
    {
        //初始窗体长宽
        private float X, Y;
        public int m_lUserID = -1;
        public int m_lRealHandle = -1;
        public int m_lChannel = -1;
        private bool bAuto = false;
        public PTZControl()
        {
            InitializeComponent();
        }

        private void PTZControl_Load(object sender, EventArgs e)
        {
            comboBoxSpeed.SelectedIndex = 3;
            if (m_lRealHandle > -1)
            {
                checkBoxPreview.Checked = true;
            }
            else 
            {
                checkBoxPreview.Checked = false;
            }

            //自定义放大
            this.Resize += new EventHandler(Form1_Resize);//执行Form1_Resize方法
            X = this.Width;
            Y = this.Height;
            setTag(this);
        }

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


        private void btnLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_LEFT, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else 
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, m_lChannel, CHCNetSDK.PAN_LEFT, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }           
        }

        private void btnLeft_MouseUp(object sender, MouseEventArgs e)
        {
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_LEFT, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, m_lChannel, CHCNetSDK.PAN_LEFT, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }             
        }

        private void btnUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.TILT_UP, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, m_lChannel, CHCNetSDK.TILT_UP, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
        }

        private void btnUp_MouseUp(object sender, MouseEventArgs e)
        {
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.TILT_UP, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, m_lChannel, CHCNetSDK.TILT_UP, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
        }

        private void btnRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_RIGHT, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, m_lChannel, CHCNetSDK.PAN_RIGHT, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }            
        }

        private void btnRight_MouseUp(object sender, MouseEventArgs e)
        {
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_RIGHT, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, m_lChannel, CHCNetSDK.PAN_RIGHT, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }             
        }

        private void btnDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.TILT_DOWN, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, m_lChannel, CHCNetSDK.TILT_DOWN, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }             
        }

        private void btnDown_MouseUp(object sender, MouseEventArgs e)
        {
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.TILT_DOWN, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, m_lChannel, CHCNetSDK.TILT_DOWN, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }            
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            if (checkBoxPreview.Checked)
            {
                if (!bAuto)
                {
                    CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_AUTO, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
                    btnAuto.Text = "Stop";
                    bAuto = true;
                }
                else
                {
                    CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_AUTO, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
                    btnAuto.Text = "Auto";
                    bAuto = false;
                }
            }
            else
            {
                if (!bAuto)
                {
                    CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, m_lChannel, CHCNetSDK.PAN_AUTO, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
                    btnAuto.Text = "Stop";
                    bAuto = true;
                }
                else
                {
                    CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, m_lChannel, CHCNetSDK.PAN_AUTO, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
                    btnAuto.Text = "Auto";
                    bAuto = false;
                }            
            } 
        }
    }
}
