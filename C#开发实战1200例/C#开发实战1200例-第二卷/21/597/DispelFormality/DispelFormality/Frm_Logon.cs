using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;//ע����ĺ����ռ�
using System.IO;//FileStream��������ռ�

namespace DispelFormality
{
    public partial class Frm_Logon : Form
    {
        public Frm_Logon()
        {
            InitializeComponent();
        }

        public string HighValue = "";//�ߵ���Ϣ��ֵ
        public string HighSgin = "";//�ߵ���Ϣ�ı�ʶ
        public string IfShow = "";//�Ƿ���ʾ���ܴ���
        public bool Bypast = false;//�жϳ����ǲ�����
        public string NewDate = "";//��¼���ڣ��ж��Ƿ��޸Ĺ�����
        public string TemporarilyDate="";//��ȡע����м�����������������ʱʱ��

        /// <summary>
        /// �Ƚ�ǰһ�������Ƿ���ں�һ������
        /// </summary>
        /// <param Date_1="string">����</param> 
        /// <param Date_2="string">����</param>  
        public bool DateCompare(string Date_1,string Date_2)
        {
            string[] D1;
            string[] D2;
            bool Comp = false;
            D1 = Date_1.Split(Convert.ToChar('-'));
            D2 = Date_2.Split(Convert.ToChar('-'));
            for (int i = 0; i < D1.Length; i++)
            {
                if (Convert.ToInt32(D1[i]) > Convert.ToInt32(D2[i]))
                {
                    Comp = true;
                    break;
                }
            }
            return Comp;
        }

        /// <summary>
        /// �Ƚ��������ڵ��·ݲ�
        /// </summary>
        /// <param Old_Date="DateTime">����</param> 
        /// <param New_Date="DateTime">����</param>  
        public int MonthJob(DateTime Old_Date, DateTime New_Date)
        {
            int OY = Old_Date.Year;//��
            int OM = Old_Date.Month;//��
            int OD = Old_Date.Day;//��
            int NY = New_Date.Year;
            int NM = New_Date.Month;
            int ND = New_Date.Day;
            int fY = 0;//���/����
            int fM = 0;//�½�/����
            int Months = 0;//��¼�·ݵĸ���
            int d = ND - OD;
            if (NM > OM)
            {
                if (d > 0)
                {

                    fM = 1;
                }

                if (d < 0)
                    fM = -1;
            }
            int m = NM + fM - OM;
            if (m < 0)
            {
                fY = -1;
                m = 12 + m;
            }
            int y = NY + fY - OY;

            Months = y * 12 + m;
            return Months;
        }

        /// <summary>
        /// �Ƚ��������ڵ�������
        /// </summary>
        /// <param Old_Date="DateTime">������</param> 
        /// <param New_Date="DateTime">������</param>  
        public int DayJob(DateTime Old_Date, DateTime New_Date)
        {
            TimeSpan TMs = New_Date - Old_Date;
            int ms = TMs.Days;
            return ms;
        }

        /// <summary>
        /// ��ȡע����е���Ϣ
        /// </summary>
        /// <param Field="string">ע����е���Ϣ</param> 
        public bool ReadRegistered(string Field)
        {
            string Cauda = Field;
            IfShow = Cauda.Substring(Cauda.Length - 1, 1);
            if (Cauda.Length <= 1)
                return false;
            switch (Cauda.Substring(Cauda.Length - 2, 1))
            {
                case "D":
                    {
                        HighValue = Cauda.Substring(0, Cauda.Length - 2);//�ߵ���Ϣ��ֵ
                        HighSgin = "D";//�ߵ���Ϣ�ı�ʶ
                        break;
                    }
                case "M":
                    {
                        HighValue = Cauda.Substring(0, Cauda.Length - 2);//�ߵ���Ϣ��ֵ
                        HighSgin = "M";//�ߵ���Ϣ�ı�ʶ
                        break;
                    }
                case "A":
                    {
                        HighValue = Cauda.Substring(0, Cauda.Length - 2);//�ߵ���Ϣ��ֵ
                        HighSgin = "A";//�ߵ���Ϣ�ı�ʶ
                        break;
                    }
                case "C":
                    {
                        HighValue = Cauda.Substring(0, Cauda.Length - 2);//�ߵ���Ϣ��ֵ
                        HighSgin = "C";//�ߵ���Ϣ�ı�ʶ
                        break;
                    }

            }

            return true;
        }

        //���ܳ������ӷ���
        public bool sdkf()
        {
            string FDir = "";
            bool pp = false;
            //bool enrolIf = false;
            string enrolValse = "";
            int job = 0;
            FDir = Application.ExecutablePath;
            FDir = FDir.Substring(FDir.LastIndexOf("\\") + 1, FDir.Length - FDir.LastIndexOf("\\") - 1);
            RegistryKey retkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("software", true).CreateSubKey("LB").CreateSubKey(FDir);
            
            //��ȡע����е���Ϣ
            foreach (string strRNum in retkey.GetSubKeyNames())
            {
                RegistryKey sikey = retkey.OpenSubKey(strRNum);//���Ӽ�
                foreach (string sVName in sikey.GetValueNames())
                {
                    if (sVName == "UserName")
                    {
                        enrolValse = sikey.GetValue(sVName).ToString();
                    }
                    if (sVName == "DateCounter")
                    {
                        NewDate = sikey.GetValue(sVName).ToString();
                    }
                    if (sVName == "DateMonth")
                    {
                        TemporarilyDate = sikey.GetValue(sVName).ToString();
                    }
                }
            }
            if (NewDate!="")
                if (Convert.ToDateTime(NewDate) > System.DateTime.Now)
                {
                    Bypast = true;
                    return true;
                }
            if (enrolValse.Length == 0)
                return false;
            ReadRegistered(enrolValse);//�ֽ�ע����е���Ϣ
            if (IfShow=="T")//(enrolValse.LastIndexOf("T") > -1)
                pp = true;
            else
                pp = false;

            //�޸�ע���
            if (pp == false)
            {
                FDir = Application.ExecutablePath;
                FDir = FDir.Substring(FDir.LastIndexOf("\\") + 1, FDir.Length - FDir.LastIndexOf("\\") - 1);
                retkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("software", true).CreateSubKey("LB").CreateSubKey(FDir).CreateSubKey("Altitude");

                //�ж�Ӧ�ó����ʹ������
                switch (HighSgin)
                {
                    case "D"://����
                        {
                            if (DateCompare(System.DateTime.Now.ToShortDateString().Trim(),HighValue.Trim()))
                                Bypast = true;
                            break;
                        }
                    case "M"://����
                        {
                            if (Convert.ToInt32(HighValue) <= 0)
                                Bypast = true;
                            else
                            {
                                job = MonthJob(Convert.ToDateTime(TemporarilyDate), Convert.ToDateTime(System.DateTime.Now.ToShortDateString()));
                                if (job > 0)
                                    retkey.SetValue("DateMonth", System.DateTime.Now.ToString());
                            }
                            break;
                        }
                    case "A"://����
                        {
                            
                            if (Convert.ToInt32(HighValue) <= 0)
                                Bypast = true;
                            else
                            {
                                job = DayJob(Convert.ToDateTime(TemporarilyDate), Convert.ToDateTime(System.DateTime.Now.ToShortDateString()));
                                if (job > 0)
                                    retkey.SetValue("DateMonth", System.DateTime.Now.ToString());
                            }
                            break;
                        }
                    case "C"://����
                        {
                            if (Convert.ToInt32(HighValue) <= 0)
                                Bypast = true;
                            else
                                job = 1;
                            break;
                        }
                }
                if (HighSgin == "M" || HighSgin == "A" || HighSgin == "C")
                {
                    job = Convert.ToInt32(HighValue) - job;
                    retkey.SetValue("UserName", job.ToString() + HighSgin + IfShow);
                }
                retkey.SetValue("DateCounter", System.DateTime.Now.ToString());
                
            }

             return pp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Frm_Logon_Load(object sender, EventArgs e)
        {
            if (sdkf() == false)
            {
                if (Bypast==true)
                    Application.Exit();
                Frm_Dispel FrmDispel = new Frm_Dispel();
                if (FrmDispel.ShowDialog() == DialogResult.OK)
                {
                    //�Ե�¼���������Ӧ�Ĳ���
                }
                else
                    Application.Exit();
 
            }
            if (Bypast == true)
                Application.Exit();
        }
    }
}