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
	/// Form1 ��ժҪ˵����
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

        //��ȡϵͳ��ǰʱ��
        string year = DateTime.Now.Year.ToString();
        string month = DateTime.Now.Month.ToString();
        string day = DateTime.Now.Day.ToString();
        string hour = DateTime.Now.Hour.ToString();
        string minute = DateTime.Now.Minute.ToString();
        string second = DateTime.Now.Second.ToString();

		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Preview()
		{
			//
			// Windows ���������֧���������
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
                //����SDK��־ To save the SDK log
                CHCNetSDK.NET_DVR_SetLogToFile(3, "C:\\SdkLog\\", true);
			}
			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
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

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
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
            this.btnLogin.Text = "����";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(17, 571);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(76, 34);
            this.btnPreview.TabIndex = 7;
            this.btnPreview.Text = "����Ԥ��";
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
            this.label5.Text = "�豸IP";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(236, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "�豸�˿�";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "�û���";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(238, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "����";
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
            this.btnBMP.Text = "BMPץͼ";
            this.btnBMP.UseVisualStyleBackColor = true;
            this.btnBMP.Click += new System.EventHandler(this.btnBMP_Click);
            // 
            // btnJPEG
            // 
            this.btnJPEG.Location = new System.Drawing.Point(208, 571);
            this.btnJPEG.Name = "btnJPEG";
            this.btnJPEG.Size = new System.Drawing.Size(97, 34);
            this.btnJPEG.TabIndex = 9;
            this.btnJPEG.Text = "JPEGץͼ";
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
            this.label13.Text = "Ԥ��/ץͼͨ��";
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
            this.btnRecord.Text = "��ʼ¼��";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnPTZ
            // 
            this.btnPTZ.Location = new System.Drawing.Point(438, 571);
            this.btnPTZ.Name = "btnPTZ";
            this.btnPTZ.Size = new System.Drawing.Size(75, 34);
            this.btnPTZ.TabIndex = 23;
            this.btnPTZ.Text = "��̨����";
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
            this.Text = "����ͷ����";
            this.Load += new System.EventHandler(this.Preview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RealPlayWnd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        ///// <summary>
        ///// Ӧ�ó��������ڵ㡣
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
				MessageBox.Show("������IP���˿ںţ���¼��������");
				return;
			}
            if (m_lUserID < 0)
            {
                string DVRIPAddress = textBoxIP.Text; //�豸IP��ַ��������
                Int16 DVRPortNumber = Int16.Parse(textBoxPort.Text);//�豸����˿ں�
                string DVRUserName = textBoxUserName.Text;//�豸��¼�û���
                string DVRPassword = textBoxPassword.Text;//�豸��¼����

                CHCNetSDK.NET_DVR_DEVICEINFO_V30 DeviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V30();

                //��¼�豸 Login the device
                m_lUserID = CHCNetSDK.NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref DeviceInfo);
                if (m_lUserID < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "����ʧ��, �����= " + iLastErr; //��¼ʧ�ܣ���������
                    MessageBox.Show(str);
                    return;
                }
                else
                {
                    //��¼�ɹ�
                    MessageBox.Show("���ӳɹ�!");
                    btnLogin.Text = "�Ͽ�����";
                }

            }
            else
            {
                //ע����¼ Logout the device
                if (m_lRealHandle >= 0)
                {
                    MessageBox.Show("����ֹͣԤ��");
                    return;
                }

                if (!CHCNetSDK.NET_DVR_Logout(m_lUserID))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "�Ͽ�����ʧ��, �����= " + iLastErr;
                    MessageBox.Show(str);
                    return;           
                }
                m_lUserID = -1;
                btnLogin.Text = "����";
            }
            return;
		}

		private void btnPreview_Click(object sender, System.EventArgs e)
		{
            if(m_lUserID < 0)
            {
                MessageBox.Show("�������Ӻ��ٽ��в���");
                return;
            }

            if (m_lRealHandle < 0)
            {
                CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
                lpPreviewInfo.hPlayWnd = RealPlayWnd.Handle;//Ԥ������
                lpPreviewInfo.lChannel = Int16.Parse(textBoxChannel.Text);//Ԥte�����豸ͨ��
                lpPreviewInfo.dwStreamType = 0;//�������ͣ�0-��������1-��������2-����3��3-����4���Դ�����
                lpPreviewInfo.dwLinkMode = 0;//���ӷ�ʽ��0- TCP��ʽ��1- UDP��ʽ��2- �ಥ��ʽ��3- RTP��ʽ��4-RTP/RTSP��5-RSTP/HTTP 
                lpPreviewInfo.bBlocked = true; //0- ������ȡ����1- ����ȡ��
                lpPreviewInfo.dwDisplayBufNum = 15; //���ſⲥ�Ż�������󻺳�֡��

                CHCNetSDK.REALDATACALLBACK RealData = new CHCNetSDK.REALDATACALLBACK(RealDataCallBack);//Ԥ��ʵʱ���ص�����
                IntPtr pUser = new IntPtr();//�û�����

                //��Ԥ�� Start live view 
                m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, null/*RealData*/, pUser);
                if (m_lRealHandle < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "����Ԥ��ʧ��, �����" + iLastErr; //Ԥ��ʧ�ܣ���������
                    MessageBox.Show(str);
                    return;
                }
                else
                {
                    //Ԥ���ɹ�
                    btnPreview.Text = "ֹͣԤ��";
                }
            }
            else
            {
                //ֹͣԤ�� Stop live view 
                if (!CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "ֹͣԤ��ʧ��, �����" + iLastErr;
                    MessageBox.Show(str);
                    return;
                }
                m_lRealHandle = -1;
                btnPreview.Text = "����Ԥ��";

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
            //ͼƬ����·�����ļ��� the path and file name to save
            sBmpPicFileName = currentTime.Year + "_" + currentTime.Month + "_" + 
                currentTime.Day + "_" + currentTime.Hour + "_"+currentTime.Minute+
                "_"+currentTime.Second+"-"+"BMP_test.bmp"; 

            //BMPץͼ Capture a BMP picture
            if (!CHCNetSDK.NET_DVR_CapturePicture(m_lRealHandle, sBmpPicFileName))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "BMPץͼʧ��, �����" + iLastErr;
                MessageBox.Show(str);
                return;
            }
            else
            {
                str = "BMPץͼ�ɹ���ͼƬ����Ϊ" + sBmpPicFileName;
                MessageBox.Show(str); 
            }
            return;
        }

        private void btnJPEG_Click(object sender, EventArgs e)
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            string sJpegPicFileName;
            //ͼƬ����·�����ļ��� the path and file name to save
            sJpegPicFileName = currentTime.Year + "_" + currentTime.Month + "_" +
                currentTime.Day + "_" + currentTime.Hour + "_" + currentTime.Minute +
                "_" + currentTime.Second + "-" + "JPEG_test.jpg";

            int lChannel = Int16.Parse(textBoxChannel.Text); //ͨ���� Channel number

            CHCNetSDK.NET_DVR_JPEGPARA lpJpegPara = new CHCNetSDK.NET_DVR_JPEGPARA();
            lpJpegPara.wPicQuality = 0; //ͼ������ Image quality
            lpJpegPara.wPicSize = 0xff; //ץͼ�ֱ��� Picture size: 2- 4CIF��0xff- Auto(ʹ�õ�ǰ�����ֱ���)��ץͼ�ֱ�����Ҫ�豸֧�֣�����ȡֵ��ο�SDK�ĵ�

            //JPEGץͼ Capture a JPEG picture
            if (!CHCNetSDK.NET_DVR_CaptureJPEGPicture(m_lUserID, lChannel, ref lpJpegPara, sJpegPicFileName))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "JPEGץͼʧ�ܣ������" + iLastErr;
                MessageBox.Show(str);
                return;
            }
            else
            {
                str = "JPEGץͼ�ɹ����ļ���Ϊ" + sJpegPicFileName;
                MessageBox.Show(str);
            }
            return;
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            //¼�񱣴�·�����ļ��� the path and file name to save
            string sVideoFileName;
            sVideoFileName = currentTime.Year + "_" + currentTime.Month + "_" +
                currentTime.Day + "_" + currentTime.Hour + "_" + currentTime.Minute +
                "_" + currentTime.Second + "-" + "Record_test.mp4";

            if (m_bRecord == false)
            {
                //ǿ��I֡ Make a I frame
                int lChannel = Int16.Parse(textBoxChannel.Text); //ͨ���� Channel number
                CHCNetSDK.NET_DVR_MakeKeyFrame(m_lUserID, lChannel);

                //��ʼ¼�� Start recording
                if (!CHCNetSDK.NET_DVR_SaveRealData(m_lRealHandle, sVideoFileName))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "¼�񱣴�ʧ�ܣ������" + iLastErr;
                    MessageBox.Show(str);
                    return;
                }
                else
                {                  
                    btnRecord.Text = "ֹͣ¼��";
                    m_bRecord = true;
                }
            }
            else
            {
                //ֹͣ¼�� Stop recording
                if (!CHCNetSDK.NET_DVR_StopSaveRealData(m_lRealHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "ֹͣ¼��ʧ�ܣ������" + iLastErr;
                    MessageBox.Show(str);
                    return;
                }
                else
                {
                    str = "����¼��ɹ����ļ���Ϊ" + sVideoFileName;
                    MessageBox.Show(str);
                    btnRecord.Text = "��ʼ¼��";
                    m_bRecord = false;
                }            
            }

            return;
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            //ֹͣԤ�� Stop live view 
            if (m_lRealHandle >= 0)
            {
                CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle);
                m_lRealHandle = -1;
            }

            //ע����¼ Logout the device
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


/*      ȫ�ִ������
        NET_DVR_NOERROR = 0;//û�д���
        NET_DVR_PASSWORD_ERROR = 1;//�û����������
        NET_DVR_NOENOUGHPRI = 2;//Ȩ�޲���
        NET_DVR_NOINIT = 3;//û�г�ʼ��
        NET_DVR_CHANNEL_ERROR = 4;//ͨ���Ŵ���
        NET_DVR_OVER_MAXLINK = 5;//���ӵ�DVR�Ŀͻ��˸����������
        NET_DVR_VERSIONNOMATCH = 6;//�汾��ƥ��
        NET_DVR_NETWORK_FAIL_CONNECT = 7;//���ӷ�����ʧ��
        NET_DVR_NETWORK_SEND_ERROR = 8;//�����������ʧ��
        NET_DVR_NETWORK_RECV_ERROR = 9;//�ӷ�������������ʧ��
        NET_DVR_NETWORK_RECV_TIMEOUT = 10;//�ӷ������������ݳ�ʱ
        NET_DVR_NETWORK_ERRORDATA = 11;//���͵���������
        NET_DVR_ORDER_ERROR = 12;//���ô������
        NET_DVR_OPERNOPERMIT = 13;//�޴�Ȩ��
        NET_DVR_COMMANDTIMEOUT = 14;//DVR����ִ�г�ʱ
        NET_DVR_ERRORSERIALPORT = 15;//���ںŴ���
        NET_DVR_ERRORALARMPORT = 16;//�����˿ڴ���
        NET_DVR_PARAMETER_ERROR = 17;//��������
        NET_DVR_CHAN_EXCEPTION = 18;//������ͨ�����ڴ���״̬
        NET_DVR_NODISK = 19;//û��Ӳ��
        NET_DVR_ERRORDISKNUM = 20;//Ӳ�̺Ŵ���
        NET_DVR_DISK_FULL = 21;//������Ӳ����
        NET_DVR_DISK_ERROR = 22;//������Ӳ�̳���
        NET_DVR_NOSUPPORT = 23;//��������֧��
        NET_DVR_BUSY = 24;//������æ
        NET_DVR_MODIFY_FAIL = 25;//�������޸Ĳ��ɹ�
        NET_DVR_PASSWORD_FORMAT_ERROR = 26;//���������ʽ����ȷ
        NET_DVR_DISK_FORMATING = 27;//Ӳ�����ڸ�ʽ����������������
        NET_DVR_DVRNORESOURCE = 28;//DVR��Դ����
        NET_DVR_DVROPRATEFAILED = 29;//DVR����ʧ��
        NET_DVR_OPENHOSTSOUND_FAIL = 30;//��PC����ʧ��
        NET_DVR_DVRVOICEOPENED = 31;//�����������Խ���ռ��
        NET_DVR_TIMEINPUTERROR = 32;//ʱ�����벻��ȷ
        NET_DVR_NOSPECFILE = 33;//�ط�ʱ������û��ָ�����ļ�
        NET_DVR_CREATEFILE_ERROR = 34;//�����ļ�����
        NET_DVR_FILEOPENFAIL = 35;//���ļ�����
        NET_DVR_OPERNOTFINISH = 36; //�ϴεĲ�����û�����
        NET_DVR_GETPLAYTIMEFAIL = 37;//��ȡ��ǰ���ŵ�ʱ�����
        NET_DVR_PLAYFAIL = 38;//���ų���
        NET_DVR_FILEFORMAT_ERROR = 39;//�ļ���ʽ����ȷ
        NET_DVR_DIR_ERROR = 40;//·������
        NET_DVR_ALLOC_RESOURCE_ERROR = 41;//��Դ�������
        NET_DVR_AUDIO_MODE_ERROR = 42;//����ģʽ����
        NET_DVR_NOENOUGH_BUF = 43;//������̫С
        NET_DVR_CREATESOCKET_ERROR = 44;//����SOCKET����
        NET_DVR_SETSOCKET_ERROR = 45;//����SOCKET����
        NET_DVR_MAX_NUM = 46;//�����ﵽ���
        NET_DVR_USERNOTEXIST = 47;//�û�������
        NET_DVR_WRITEFLASHERROR = 48;//дFLASH����
        NET_DVR_UPGRADEFAIL = 49;//DVR����ʧ��
        NET_DVR_CARDHAVEINIT = 50;//���뿨�Ѿ���ʼ����
        NET_DVR_PLAYERFAILED = 51;//���ò��ſ���ĳ������ʧ��
        NET_DVR_MAX_USERNUM = 52;//�豸���û����ﵽ���
        NET_DVR_GETLOCALIPANDMACFAIL = 53;//��ÿͻ��˵�IP��ַ�������ַʧ��
        NET_DVR_NOENCODEING = 54;//��ͨ��û�б���
        NET_DVR_IPMISMATCH = 55;//IP��ַ��ƥ��
        NET_DVR_MACMISMATCH = 56;//MAC��ַ��ƥ��
        NET_DVR_UPGRADELANGMISMATCH = 57;//�����ļ����Բ�ƥ��
        NET_DVR_MAX_PLAYERPORT = 58;//������·���ﵽ���
        NET_DVR_NOSPACEBACKUP = 59;//�����豸��û���㹻�ռ���б���
        NET_DVR_NODEVICEBACKUP = 60;//û���ҵ�ָ���ı����豸
        NET_DVR_PICTURE_BITS_ERROR = 61;//ͼ����λ����������24ɫ
        NET_DVR_PICTURE_DIMENSION_ERROR = 62;//ͼƬ��*���ޣ� ��128*256
        NET_DVR_PICTURE_SIZ_ERROR = 63;//ͼƬ��С���ޣ���100K
        NET_DVR_LOADPLAYERSDKFAILED = 64;//���뵱ǰĿ¼��Player Sdk����
        NET_DVR_LOADPLAYERSDKPROC_ERROR = 65;//�Ҳ���Player Sdk��ĳ���������
        NET_DVR_LOADDSSDKFAILED = 66;//���뵱ǰĿ¼��DSsdk����
        NET_DVR_LOADDSSDKPROC_ERROR = 67;//�Ҳ���DsSdk��ĳ���������
        NET_DVR_DSSDK_ERROR = 68;//����Ӳ�����DsSdk��ĳ������ʧ��
        NET_DVR_VOICEMONOPOLIZE = 69;//��������ռ
        NET_DVR_JOINMULTICASTFAILED = 70;//����ಥ��ʧ��
        NET_DVR_CREATEDIR_ERROR = 71;//������־�ļ�Ŀ¼ʧ��
        NET_DVR_BINDSOCKET_ERROR = 72;//���׽���ʧ��
        NET_DVR_SOCKETCLOSE_ERROR = 73;//socket�����жϣ��˴���ͨ�������������жϻ�Ŀ�ĵز��ɴ�
        NET_DVR_USERID_ISUSING = 74;//ע��ʱ�û�ID���ڽ���ĳ����
        NET_DVR_SOCKETLISTEN_ERROR = 75;//����ʧ��
        NET_DVR_PROGRAM_EXCEPTION = 76;//�����쳣
        NET_DVR_WRITEFILE_FAILED = 77;//д�ļ�ʧ��
        NET_DVR_FORMAT_READONLY = 78;//��ֹ��ʽ��ֻ��Ӳ��
        NET_DVR_WITHSAMEUSERNAME = 79;//�û����ýṹ�д�����ͬ���û���
        NET_DVR_DEVICETYPE_ERROR = 80;//�������ʱ�豸�ͺŲ�ƥ��
        NET_DVR_LANGUAGE_ERROR = 81;//�������ʱ���Բ�ƥ��
        NET_DVR_PARAVERSION_ERROR = 82;//�������ʱ����汾��ƥ��
        NET_DVR_IPCHAN_NOTALIVE = 83; //Ԥ��ʱ���IPͨ��������
        NET_DVR_RTSP_SDK_ERROR = 84;//���ظ���IPCͨѶ��StreamTransClient.dllʧ��
        NET_DVR_CONVERT_SDK_ERROR = 85;//����ת���ʧ��
        NET_DVR_IPC_COUNT_OVERFLOW = 86;//��������ip����ͨ����
*/