using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace DispelFormality
{
    public partial class Frm_Dispel : Form
    {
        public Frm_Dispel()
        {
            InitializeComponent();
        }

        public string StrPass = "";//����
        public string HighValue = "";//�ߵ���Ϣ��ֵ
        public string HighSgin = "";//�ߵ���Ϣ�ı�ʶ

        public string enrolValse = "";//��¼�޶�ʱ��
        public string NewDate = "";//��¼���ڣ��ж��Ƿ��޸Ĺ�����
        public string TemporarilyDate = "";//��ȡע����м�����������������ʱʱ��
        public string IfShow = "";//�Ƿ���ʾ���ܴ���
        public bool Bypast = false;//�жϳ����ǲ�����

        /// <summary>
        /// ��ȡ��ǰ��ִ���ļ����ļ�β����Ϣ
        /// </summary>
        /// <param Prass="string">����</param> 
        public string ReadEXEFile()
        {
            byte[] byData = new byte[100];//����һ��FileStreamҪ�õ��ֽ���
            char[] charData = new char[100];//����һ���ַ���

            try
            {
                FileStream aFile = new FileStream(Application.ExecutablePath, FileMode.OpenOrCreate, FileAccess.Read);//ʵ����һ��FileStream������������data.txt�ļ�������������
                aFile.Seek(-100, SeekOrigin.End);//���ļ�ָ��ָ���ļ�β�����ļ���ʼλ����ǰ100λ�ֽ���ָ���ֽ�
                aFile.Read(byData, 0, 100);//��ȡFileStream������ָ���ļ����ֽ�������
            }
            catch
            {
                MessageBox.Show("��ȡEXE�ļ�ʱ����������");
                return "";
            }
            Decoder d = Encoding.UTF8.GetDecoder();//ʵ����һ��������
            d.GetChars(byData, 0, byData.Length, charData, 0);//�������ֽ�����ת��Ϊ�ַ�����
            string Zpp = "";
            for (int i = 0; i < charData.Length; i++)//���ַ���ϳ��ַ���
            {
                Zpp = Zpp + charData[i].ToString();
            }
            Zpp = Zpp.Replace("\0", "");//���ַ��������\0�滻Ϊ��

            return Zpp.Trim();
        }

        /// <summary>
        /// ��ȡ�ļ�β���ĸߵ���Ϣ
        /// </summary>
        /// <param Field="string">�ļ�β����Ϣ</param> 
        public string ReadAltitude(string Field)
        {
            string Cauda = "";
            StrPass = Field;
            if (Field.LastIndexOf(",") > -1)
            {
                Cauda = Field.Substring(Field.LastIndexOf(",")+1, Field.Length - Field.LastIndexOf(",")-1);
                switch (Cauda.Substring(Cauda.Length - 1, 1))
                {
                    case "D":
                        {
                            StrPass = Field.Substring(0, Field.LastIndexOf(","));//����
                            HighValue = Cauda.Substring(0, Cauda.Length-1);//�ߵ���Ϣ��ֵ
                            HighSgin = "D";//�ߵ���Ϣ�ı�ʶ
                            break;
                        }
                    case "M":
                        {
                            StrPass = Field.Substring(0, Field.LastIndexOf(","));//����
                            HighValue = Cauda.Substring(0, Cauda.Length - 1);//�ߵ���Ϣ��ֵ
                            HighSgin = "M";//�ߵ���Ϣ�ı�ʶ
                            break;
                        }
                    case "A":
                        {
                            StrPass = Field.Substring(0, Field.LastIndexOf(","));//����
                            HighValue = Cauda.Substring(0, Cauda.Length - 1);//�ߵ���Ϣ��ֵ
                            HighSgin = "A";//�ߵ���Ϣ�ı�ʶ
                            break;
                        }
                    case "C":
                        {
                            StrPass = Field.Substring(0, Field.LastIndexOf(","));//����
                            HighValue = Cauda.Substring(0, Cauda.Length - 1);//�ߵ���Ϣ��ֵ
                            HighSgin = "C";//�ߵ���Ϣ�ı�ʶ
                            break;
                        }
                }
            }
            return StrPass;
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

        /// <summary>
        /// �Ƚ�ǰһ�������Ƿ���ں�һ������
        /// </summary>
        /// <param Date_1="string">����</param> 
        /// <param Date_2="string">����</param>  
        public bool DateCompare(string Date_1, string Date_2)
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
        /// �޸�ע���
        /// </summary>
        /// <param Field="string">��ǰ��ִ���ļ�������</param> 
        public void AmendEnrol(string FDir)
        {
            int job = 0;
            RegistryKey retkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("software", true).CreateSubKey("LB").CreateSubKey(FDir).CreateSubKey("Altitude");

            //�ж�Ӧ�ó����ʹ������
            switch (HighSgin)
            {
                case "D"://����
                    {
                        if (DateCompare(System.DateTime.Now.ToShortDateString().Trim(), HighValue.Trim()))
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





        private void button_OK_Click(object sender, EventArgs e)
        {
            string temStr = "";
            string TPrass = "";
            string PPrass = "";
            string FDir = "";
            string Fshow = "";
            string Str_Altitude = "";
            if (textBox_Dispel.Text.Length == 0)
            {
                MessageBox.Show("��������롣");
                return;
            }
            temStr = textBox_Dispel.Text;
            PPrass = ReadEXEFile();//��ȡβ����Ϣ
            PPrass = ReadAltitude(PPrass);//����������߼���Ϣ
            TPrass = textBox_Dispel.Text.Trim();//��¼���������
            textBox_Dispel.Text = TPrass;
            if (PPrass == textBox_Dispel.Text)//�ж������Ƿ���ȷ
            {
                if (checkBox_Show.Checked == true)//�´������Ƿ���ʾ�ô���
                {
                    Fshow = "T";
                }
                else
                {
                    Fshow = "F";
                }
                //��ӵ�ע����·����HKEY_CURRENT_USER-Software-LB
                FDir = Application.ExecutablePath;
                FDir = FDir.Substring(FDir.LastIndexOf("\\") + 1, FDir.Length - FDir.LastIndexOf("\\") - 1);
                RegistryKey retkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("software", true).CreateSubKey("LB").CreateSubKey(FDir).CreateSubKey("Altitude");//��ע��������Altitude�ļ���
                enrolValse = "";//��¼���ע����ֵ
                NewDate = "";//��¼��ǰ��ʱ��ֵ
                TemporarilyDate = "";//��ʱ��¼ʱ��
                foreach (string sVName in retkey.GetValueNames())//��ȡע�����Altitude�ļ����е��ļ���
                {
                    if (sVName == "UserName")//�����UserName�ļ�
                    {
                        enrolValse = retkey.GetValue(sVName).ToString();//��ȡ�ļ��е���Ϣ
                    }
                    if (sVName == "DateCounter")//�����DateCounter�ļ�
                    {
                        NewDate = retkey.GetValue(sVName).ToString();//��ȡ�ļ��е���Ϣ
                    }
                    if (sVName == "DateMonth")//�����DateMonth�ļ�
                    {
                        TemporarilyDate = retkey.GetValue(sVName).ToString();//��ȡ�ļ��е���Ϣ
                    }
                }
                if (HighSgin == "C")//����ǰ����д�������EXE�ļ�
                    HighValue = Convert.ToString(Convert.ToInt32(HighValue) - 1);//���ܴ�����1
                Str_Altitude = HighValue + HighSgin + Fshow;//��ȡ�޸ĺ����Ϣ
                if (enrolValse == "" || NewDate == "" || TemporarilyDate == "")//�����һ��ֵΪ��
                {
                    retkey.SetValue("UserName", Str_Altitude.Trim());//�޸�ע����е���Ϣ
                    retkey.SetValue("DateCounter", System.DateTime.Now.ToString());
                    retkey.SetValue("DateMonth", System.DateTime.Now.ToShortDateString());
                }
                else
                {
                    ReadRegistered(enrolValse);//��ȡҪд��ע����е���Ϣ
                    AmendEnrol(FDir);//�޸�ע����е���Ϣ
                }
                this.DialogResult = DialogResult.OK;//ʹ��ǰ����ķ���ֵΪOK
                this.Close();//�رյ�ǰ����
            }
            else
                textBox_Dispel.Text = temStr;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}