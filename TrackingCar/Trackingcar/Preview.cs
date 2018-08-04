using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Runtime.InteropServices;

namespace PreviewDemo
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class Preview : System.Windows.Forms.Form
	{
        private uint iLastErr = 0;
		private Int32 m_lUserID = -1;
		private bool m_bInitSDK = false;
        private bool m_bRecord = false;
		private Int32 m_lRealHandle = -1;
        private string str;
		private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnPreview;
		private System.Windows.Forms.PictureBox RealPlayWnd;
        private TextBox textBoxIP;
        private TextBox textBoxPort;
        private TextBox textBoxUserName;
        private TextBox textBoxPassword;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Button btnBMP;
        private Button btnJPEG;
        private Label label13;
        private TextBox textBoxChannel;
        private Button btnRecord;
        private Button btnPTZ;

        //获取系统当前时间
        string year = DateTime.Now.Year.ToString();
        string month = DateTime.Now.Month.ToString();
        string day = DateTime.Now.Day.ToString();
        string hour = DateTime.Now.Hour.ToString();
        string minute = DateTime.Now.Minute.ToString();
        string second = DateTime.Now.Second.ToString();

		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Preview()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			m_bInitSDK = CHCNetSDK.NET_DVR_Init();

			if (m_bInitSDK == false)
			{
				MessageBox.Show("NET_DVR_Init error!");
                //return;
			}
			else
			{
                //保存SDK日志 To save the SDK log
                CHCNetSDK.NET_DVR_SetLogToFile(3, "C:\\SdkLog\\", true);
			}
			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if (m_lRealHandle >= 0)
			{
				CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle);
			}
			if (m_lUserID >= 0)
			{
				CHCNetSDK.NET_DVR_Logout(m_lUserID);
			}
			if (m_bInitSDK == true)
			{
				CHCNetSDK.NET_DVR_Cleanup();
			}
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.RealPlayWnd = new System.Windows.Forms.PictureBox();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnBMP = new System.Windows.Forms.Button();
            this.btnJPEG = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxChannel = new System.Windows.Forms.TextBox();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnPTZ = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.RealPlayWnd)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(435, 38);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(78, 50);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "连接";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(17, 571);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(76, 34);
            this.btnPreview.TabIndex = 7;
            this.btnPreview.Text = "开启预览";
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // RealPlayWnd
            // 
            this.RealPlayWnd.BackColor = System.Drawing.SystemColors.WindowText;
            this.RealPlayWnd.Location = new System.Drawing.Point(18, 104);
            this.RealPlayWnd.Name = "RealPlayWnd";
            this.RealPlayWnd.Size = new System.Drawing.Size(495, 395);
            this.RealPlayWnd.TabIndex = 4;
            this.RealPlayWnd.TabStop = false;
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(78, 24);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.ReadOnly = true;
            this.textBoxIP.Size = new System.Drawing.Size(114, 21);
            this.textBoxIP.TabIndex = 2;
            this.textBoxIP.Text = "192.168.1.102";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(308, 24);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.ReadOnly = true;
            this.textBoxPort.Size = new System.Drawing.Size(112, 21);
            this.textBoxPort.TabIndex = 3;
            this.textBoxPort.Text = "8000";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(78, 70);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.ReadOnly = true;
            this.textBoxUserName.Size = new System.Drawing.Size(114, 21);
            this.textBoxUserName.TabIndex = 4;
            this.textBoxUserName.Text = "admin";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxPassword.Location = new System.Drawing.Point(308, 70);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.ReadOnly = true;
            this.textBoxPassword.Size = new System.Drawing.Size(112, 21);
            this.textBoxPassword.TabIndex = 5;
            this.textBoxPassword.Text = "admin123456";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "设备IP";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(236, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "设备端口";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "用户名";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(238, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "密码";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 550);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 12);
            this.label9.TabIndex = 13;
            // 
            // btnBMP
            // 
            this.btnBMP.Location = new System.Drawing.Point(110, 572);
            this.btnBMP.Name = "btnBMP";
            this.btnBMP.Size = new System.Drawing.Size(79, 34);
            this.btnBMP.TabIndex = 8;
            this.btnBMP.Text = "BMP抓图";
            this.btnBMP.UseVisualStyleBackColor = true;
            this.btnBMP.Click += new System.EventHandler(this.btnBMP_Click);
            // 
            // btnJPEG
            // 
            this.btnJPEG.Location = new System.Drawing.Point(208, 571);
            this.btnJPEG.Name = "btnJPEG";
            this.btnJPEG.Size = new System.Drawing.Size(97, 34);
            this.btnJPEG.TabIndex = 9;
            this.btnJPEG.Text = "JPEG抓图";
            this.btnJPEG.UseVisualStyleBackColor = true;
            this.btnJPEG.Click += new System.EventHandler(this.btnJPEG_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(18, 526);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(83, 12);
            this.label13.TabIndex = 19;
            this.label13.Text = "预览/抓图通道";
            // 
            // textBoxChannel
            // 
            this.textBoxChannel.Location = new System.Drawing.Point(107, 523);
            this.textBoxChannel.Name = "textBoxChannel";
            this.textBoxChannel.ReadOnly = true;
            this.textBoxChannel.Size = new System.Drawing.Size(85, 21);
            this.textBoxChannel.TabIndex = 6;
            this.textBoxChannel.Text = "1";
            // 
            // btnRecord
            // 
            this.btnRecord.Location = new System.Drawing.Point(319, 571);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(100, 34);
            this.btnRecord.TabIndex = 10;
            this.btnRecord.Text = "开始录像";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnPTZ
            // 
            this.btnPTZ.Location = new System.Drawing.Point(438, 571);
            this.btnPTZ.Name = "btnPTZ";
            this.btnPTZ.Size = new System.Drawing.Size(75, 34);
            this.btnPTZ.TabIndex = 23;
            this.btnPTZ.Text = "云台控制";
            this.btnPTZ.UseVisualStyleBackColor = true;
            this.btnPTZ.Click += new System.EventHandler(this.btnPTZ_Click);
            // 
            // Preview
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(531, 657);
            this.Controls.Add(this.btnPTZ);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.textBoxChannel);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnJPEG);
            this.Controls.Add(this.btnBMP);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUserName);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.RealPlayWnd);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnLogin);
            this.Name = "Preview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "摄像头操作";
            this.Load += new System.EventHandler(this.Preview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RealPlayWnd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        ///// <summary>
        ///// 应用程序的主入口点。
        ///// </summary>
        //[STAThread]
        //static void Main()
        //{
        //    Application.Run(new Preview());
        //}

		private void textBox1_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void btnLogin_Click(object sender, System.EventArgs e)
		{
			if (textBoxIP.Text == "" || textBoxPort.Text == "" ||
				textBoxUserName.Text == "" || textBoxPassword.Text == "")
			{
				MessageBox.Show("请输入IP，端口号，登录名和密码");
				return;
			}
            if (m_lUserID < 0)
            {
                string DVRIPAddress = textBoxIP.Text; //设备IP地址或者域名
                Int16 DVRPortNumber = Int16.Parse(textBoxPort.Text);//设备服务端口号
                string DVRUserName = textBoxUserName.Text;//设备登录用户名
                string DVRPassword = textBoxPassword.Text;//设备登录密码

                CHCNetSDK.NET_DVR_DEVICEINFO_V30 DeviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V30();

                //登录设备 Login the device
                m_lUserID = CHCNetSDK.NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref DeviceInfo);
                if (m_lUserID < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "连接失败, 错误号= " + iLastErr; //登录失败，输出错误号
                    MessageBox.Show(str);
                    return;
                }
                else
                {
                    //登录成功
                    MessageBox.Show("连接成功!");
                    btnLogin.Text = "断开连接";
                }

            }
            else
            {
                //注销登录 Logout the device
                if (m_lRealHandle >= 0)
                {
                    MessageBox.Show("请先停止预览");
                    return;
                }

                if (!CHCNetSDK.NET_DVR_Logout(m_lUserID))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "断开连接失败, 错误号= " + iLastErr;
                    MessageBox.Show(str);
                    return;           
                }
                m_lUserID = -1;
                btnLogin.Text = "连接";
            }
            return;
		}

		private void btnPreview_Click(object sender, System.EventArgs e)
		{
            if(m_lUserID < 0)
            {
                MessageBox.Show("请先连接后再进行操作");
                return;
            }

            if (m_lRealHandle < 0)
            {
                CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
                lpPreviewInfo.hPlayWnd = RealPlayWnd.Handle;//预览窗口
                lpPreviewInfo.lChannel = Int16.Parse(textBoxChannel.Text);//预te览的设备通道
                lpPreviewInfo.dwStreamType = 0;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
                lpPreviewInfo.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
                lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
                lpPreviewInfo.dwDisplayBufNum = 15; //播放库播放缓冲区最大缓冲帧数

                CHCNetSDK.REALDATACALLBACK RealData = new CHCNetSDK.REALDATACALLBACK(RealDataCallBack);//预览实时流回调函数
                IntPtr pUser = new IntPtr();//用户数据

                //打开预览 Start live view 
                m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, null/*RealData*/, pUser);
                if (m_lRealHandle < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "开启预览失败, 错误号" + iLastErr; //预览失败，输出错误号
                    MessageBox.Show(str);
                    return;
                }
                else
                {
                    //预览成功
                    btnPreview.Text = "停止预览";
                }
            }
            else
            {
                //停止预览 Stop live view 
                if (!CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "停止预览失败, 错误号" + iLastErr;
                    MessageBox.Show(str);
                    return;
                }
                m_lRealHandle = -1;
                btnPreview.Text = "开启预览";

            }
            return;
		}

		public void RealDataCallBack(Int32 lRealHandle, UInt32 dwDataType, ref byte pBuffer, UInt32 dwBufSize, IntPtr pUser)
		{
		}

        private void btnBMP_Click(object sender, EventArgs e)
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            string sBmpPicFileName;
            //图片保存路径和文件名 the path and file name to save
            sBmpPicFileName = currentTime.Year + "_" + currentTime.Month + "_" + 
                currentTime.Day + "_" + currentTime.Hour + "_"+currentTime.Minute+
                "_"+currentTime.Second+"-"+"BMP_test.bmp"; 

            //BMP抓图 Capture a BMP picture
            if (!CHCNetSDK.NET_DVR_CapturePicture(m_lRealHandle, sBmpPicFileName))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "BMP抓图失败, 错误号" + iLastErr;
                MessageBox.Show(str);
                return;
            }
            else
            {
                str = "BMP抓图成功，图片名称为" + sBmpPicFileName;
                MessageBox.Show(str); 
            }
            return;
        }

        private void btnJPEG_Click(object sender, EventArgs e)
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            string sJpegPicFileName;
            //图片保存路径和文件名 the path and file name to save
            sJpegPicFileName = currentTime.Year + "_" + currentTime.Month + "_" +
                currentTime.Day + "_" + currentTime.Hour + "_" + currentTime.Minute +
                "_" + currentTime.Second + "-" + "JPEG_test.jpg";

            int lChannel = Int16.Parse(textBoxChannel.Text); //通道号 Channel number

            CHCNetSDK.NET_DVR_JPEGPARA lpJpegPara = new CHCNetSDK.NET_DVR_JPEGPARA();
            lpJpegPara.wPicQuality = 0; //图像质量 Image quality
            lpJpegPara.wPicSize = 0xff; //抓图分辨率 Picture size: 2- 4CIF，0xff- Auto(使用当前码流分辨率)，抓图分辨率需要设备支持，更多取值请参考SDK文档

            //JPEG抓图 Capture a JPEG picture
            if (!CHCNetSDK.NET_DVR_CaptureJPEGPicture(m_lUserID, lChannel, ref lpJpegPara, sJpegPicFileName))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "JPEG抓图失败，错误号" + iLastErr;
                MessageBox.Show(str);
                return;
            }
            else
            {
                str = "JPEG抓图成功，文件名为" + sJpegPicFileName;
                MessageBox.Show(str);
            }
            return;
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            //录像保存路径和文件名 the path and file name to save
            string sVideoFileName;
            sVideoFileName = currentTime.Year + "_" + currentTime.Month + "_" +
                currentTime.Day + "_" + currentTime.Hour + "_" + currentTime.Minute +
                "_" + currentTime.Second + "-" + "Record_test.mp4";

            if (m_bRecord == false)
            {
                //强制I帧 Make a I frame
                int lChannel = Int16.Parse(textBoxChannel.Text); //通道号 Channel number
                CHCNetSDK.NET_DVR_MakeKeyFrame(m_lUserID, lChannel);

                //开始录像 Start recording
                if (!CHCNetSDK.NET_DVR_SaveRealData(m_lRealHandle, sVideoFileName))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "录像保存失败，错误号" + iLastErr;
                    MessageBox.Show(str);
                    return;
                }
                else
                {                  
                    btnRecord.Text = "停止录像";
                    m_bRecord = true;
                }
            }
            else
            {
                //停止录像 Stop recording
                if (!CHCNetSDK.NET_DVR_StopSaveRealData(m_lRealHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "停止录像失败，错误号" + iLastErr;
                    MessageBox.Show(str);
                    return;
                }
                else
                {
                    str = "保存录像成功，文件名为" + sVideoFileName;
                    MessageBox.Show(str);
                    btnRecord.Text = "开始录像";
                    m_bRecord = false;
                }            
            }

            return;
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            //停止预览 Stop live view 
            if (m_lRealHandle >= 0)
            {
                CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle);
                m_lRealHandle = -1;
            }

            //注销登录 Logout the device
            if (m_lUserID >= 0)
            {
                CHCNetSDK.NET_DVR_Logout(m_lUserID);
                m_lUserID = -1;
            }

            CHCNetSDK.NET_DVR_Cleanup();

            //Application.Exit();
        }

        private void btnPTZ_Click(object sender, EventArgs e)
        {
            PTZControl dlg = new PTZControl();
            dlg.m_lUserID = m_lUserID;
            dlg.m_lChannel = 1;
            dlg.m_lRealHandle = m_lRealHandle;
            dlg.ShowDialog();
        }

        private void Preview_Load(object sender, EventArgs e)
        {

        }
	}
}


/*      全局错误码表
        NET_DVR_NOERROR = 0;//没有错误
        NET_DVR_PASSWORD_ERROR = 1;//用户名密码错误
        NET_DVR_NOENOUGHPRI = 2;//权限不足
        NET_DVR_NOINIT = 3;//没有初始化
        NET_DVR_CHANNEL_ERROR = 4;//通道号错误
        NET_DVR_OVER_MAXLINK = 5;//连接到DVR的客户端个数超过最大
        NET_DVR_VERSIONNOMATCH = 6;//版本不匹配
        NET_DVR_NETWORK_FAIL_CONNECT = 7;//连接服务器失败
        NET_DVR_NETWORK_SEND_ERROR = 8;//向服务器发送失败
        NET_DVR_NETWORK_RECV_ERROR = 9;//从服务器接收数据失败
        NET_DVR_NETWORK_RECV_TIMEOUT = 10;//从服务器接收数据超时
        NET_DVR_NETWORK_ERRORDATA = 11;//传送的数据有误
        NET_DVR_ORDER_ERROR = 12;//调用次序错误
        NET_DVR_OPERNOPERMIT = 13;//无此权限
        NET_DVR_COMMANDTIMEOUT = 14;//DVR命令执行超时
        NET_DVR_ERRORSERIALPORT = 15;//串口号错误
        NET_DVR_ERRORALARMPORT = 16;//报警端口错误
        NET_DVR_PARAMETER_ERROR = 17;//参数错误
        NET_DVR_CHAN_EXCEPTION = 18;//服务器通道处于错误状态
        NET_DVR_NODISK = 19;//没有硬盘
        NET_DVR_ERRORDISKNUM = 20;//硬盘号错误
        NET_DVR_DISK_FULL = 21;//服务器硬盘满
        NET_DVR_DISK_ERROR = 22;//服务器硬盘出错
        NET_DVR_NOSUPPORT = 23;//服务器不支持
        NET_DVR_BUSY = 24;//服务器忙
        NET_DVR_MODIFY_FAIL = 25;//服务器修改不成功
        NET_DVR_PASSWORD_FORMAT_ERROR = 26;//密码输入格式不正确
        NET_DVR_DISK_FORMATING = 27;//硬盘正在格式化，不能启动操作
        NET_DVR_DVRNORESOURCE = 28;//DVR资源不足
        NET_DVR_DVROPRATEFAILED = 29;//DVR操作失败
        NET_DVR_OPENHOSTSOUND_FAIL = 30;//打开PC声音失败
        NET_DVR_DVRVOICEOPENED = 31;//服务器语音对讲被占用
        NET_DVR_TIMEINPUTERROR = 32;//时间输入不正确
        NET_DVR_NOSPECFILE = 33;//回放时服务器没有指定的文件
        NET_DVR_CREATEFILE_ERROR = 34;//创建文件出错
        NET_DVR_FILEOPENFAIL = 35;//打开文件出错
        NET_DVR_OPERNOTFINISH = 36; //上次的操作还没有完成
        NET_DVR_GETPLAYTIMEFAIL = 37;//获取当前播放的时间出错
        NET_DVR_PLAYFAIL = 38;//播放出错
        NET_DVR_FILEFORMAT_ERROR = 39;//文件格式不正确
        NET_DVR_DIR_ERROR = 40;//路径错误
        NET_DVR_ALLOC_RESOURCE_ERROR = 41;//资源分配错误
        NET_DVR_AUDIO_MODE_ERROR = 42;//声卡模式错误
        NET_DVR_NOENOUGH_BUF = 43;//缓冲区太小
        NET_DVR_CREATESOCKET_ERROR = 44;//创建SOCKET出错
        NET_DVR_SETSOCKET_ERROR = 45;//设置SOCKET出错
        NET_DVR_MAX_NUM = 46;//个数达到最大
        NET_DVR_USERNOTEXIST = 47;//用户不存在
        NET_DVR_WRITEFLASHERROR = 48;//写FLASH出错
        NET_DVR_UPGRADEFAIL = 49;//DVR升级失败
        NET_DVR_CARDHAVEINIT = 50;//解码卡已经初始化过
        NET_DVR_PLAYERFAILED = 51;//调用播放库中某个函数失败
        NET_DVR_MAX_USERNUM = 52;//设备端用户数达到最大
        NET_DVR_GETLOCALIPANDMACFAIL = 53;//获得客户端的IP地址或物理地址失败
        NET_DVR_NOENCODEING = 54;//该通道没有编码
        NET_DVR_IPMISMATCH = 55;//IP地址不匹配
        NET_DVR_MACMISMATCH = 56;//MAC地址不匹配
        NET_DVR_UPGRADELANGMISMATCH = 57;//升级文件语言不匹配
        NET_DVR_MAX_PLAYERPORT = 58;//播放器路数达到最大
        NET_DVR_NOSPACEBACKUP = 59;//备份设备中没有足够空间进行备份
        NET_DVR_NODEVICEBACKUP = 60;//没有找到指定的备份设备
        NET_DVR_PICTURE_BITS_ERROR = 61;//图像素位数不符，限24色
        NET_DVR_PICTURE_DIMENSION_ERROR = 62;//图片高*宽超限， 限128*256
        NET_DVR_PICTURE_SIZ_ERROR = 63;//图片大小超限，限100K
        NET_DVR_LOADPLAYERSDKFAILED = 64;//载入当前目录下Player Sdk出错
        NET_DVR_LOADPLAYERSDKPROC_ERROR = 65;//找不到Player Sdk中某个函数入口
        NET_DVR_LOADDSSDKFAILED = 66;//载入当前目录下DSsdk出错
        NET_DVR_LOADDSSDKPROC_ERROR = 67;//找不到DsSdk中某个函数入口
        NET_DVR_DSSDK_ERROR = 68;//调用硬解码库DsSdk中某个函数失败
        NET_DVR_VOICEMONOPOLIZE = 69;//声卡被独占
        NET_DVR_JOINMULTICASTFAILED = 70;//加入多播组失败
        NET_DVR_CREATEDIR_ERROR = 71;//建立日志文件目录失败
        NET_DVR_BINDSOCKET_ERROR = 72;//绑定套接字失败
        NET_DVR_SOCKETCLOSE_ERROR = 73;//socket连接中断，此错误通常是由于连接中断或目的地不可达
        NET_DVR_USERID_ISUSING = 74;//注销时用户ID正在进行某操作
        NET_DVR_SOCKETLISTEN_ERROR = 75;//监听失败
        NET_DVR_PROGRAM_EXCEPTION = 76;//程序异常
        NET_DVR_WRITEFILE_FAILED = 77;//写文件失败
        NET_DVR_FORMAT_READONLY = 78;//禁止格式化只读硬盘
        NET_DVR_WITHSAMEUSERNAME = 79;//用户配置结构中存在相同的用户名
        NET_DVR_DEVICETYPE_ERROR = 80;//导入参数时设备型号不匹配
        NET_DVR_LANGUAGE_ERROR = 81;//导入参数时语言不匹配
        NET_DVR_PARAVERSION_ERROR = 82;//导入参数时软件版本不匹配
        NET_DVR_IPCHAN_NOTALIVE = 83; //预览时外接IP通道不在线
        NET_DVR_RTSP_SDK_ERROR = 84;//加载高清IPC通讯库StreamTransClient.dll失败
        NET_DVR_CONVERT_SDK_ERROR = 85;//加载转码库失败
        NET_DVR_IPC_COUNT_OVERFLOW = 86;//超出最大的ip接入通道数
*/