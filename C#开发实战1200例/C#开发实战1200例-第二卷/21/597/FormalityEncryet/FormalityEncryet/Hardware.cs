using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Management;//��������ô����.net�����System.Management
using System.IO;//ΪFileStream��ӵ������ռ�
using System.Collections;//ΪArrayList��������ռ�
using System.Security.Cryptography;//MD5�������ռ�
using System.Net;//���������Ϣ�������ռ�

namespace FormalityEncryet
{
    class Hardware
    {
        /// <summary> 
        /// Hardware_Mac ��ժҪ˵���� 
        /// </summary> 
        public class HardwareInfo
        {

            public static int ArrInt = 0;
            public static string PFileDir = "";
            public static string PFileN = "";
            
            
            //ȡ������ 
            public string GetHostName()
            {
                return System.Net.Dns.GetHostName();
            }

            #region  ��ȡ������
            /// <summary>
            /// ��ȡ������
            /// </summary>
            public String GetBIOSNumber()
            {
                // ��ʾ������
                string hostname = Dns.GetHostName();
                // ��ʾÿ��IP��ַ
                IPHostEntry hostent = Dns.GetHostEntry(hostname); // ������Ϣ
                Array addrs = hostent.AddressList;            // IP��ַ����
                IEnumerator it = addrs.GetEnumerator();       // ����������������ռ�using System.Collections;
                while (it.MoveNext())
                {   // ѭ������һ��IP ��ַ
                    IPAddress ip = (IPAddress)it.Current;      //���IP��ַ����������ռ�using System.Net;
                    return ip.ToString();
                }
                return "";
            }
            #endregion

            /// <summary>
            /// ��ȡCPU���к�
            /// </summary>
            /// <returns>CPU���к�</returns>
            public String GetCpuID()
            {
                try
                {
                    ManagementClass mc = new ManagementClass("Win32_Processor");
                    ManagementObjectCollection moc = mc.GetInstances();

                    String strCpuID = null;
                    foreach (ManagementObject mo in moc)
                    {
                        strCpuID = mo.Properties["ProcessorId"].Value.ToString();
                        mo.Dispose();
                        break;
                    }
                    moc.Dispose();
                    mc.Dispose();
                    return strCpuID;
                }
                catch
                {
                    return "";
                }

            }

            /// <summary>
            /// ��ȡ����Ӳ����ַ
            /// </summary>
            /// <returns>����Ӳ����ַ</returns>
            public String GetNetworkCard()
            {
                try
                {
                    ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                    ManagementObjectCollection moc2 = mc.GetInstances();
                    string StrNetworkCard = null;
                    foreach (ManagementObject mo in moc2)
                    {
                        if ((bool)mo["IPEnabled"] == true)
                        {
                            StrNetworkCard = mo["MacAddress"].ToString();
                            mo.Dispose();
                            break;
                        }
                        mo.Dispose();
                    }
                    moc2.Dispose();
                    mc.Dispose();
                    return StrNetworkCard;
                }
                catch
                {
                    return "";
                }
            }

            /// <summary>
            /// ��ȡ���ؼ������Ӳ���̷�
            /// </summary>
            /// <param cBox="ComboBox">ComboBox�ؼ�</param>
            public void GetHardDisk(ComboBox cBox)
            {
                try
                {
                    cBox.Items.Clear();
                    ManagementClass mcHD = new ManagementClass("win32_logicaldisk");
                    ManagementObjectCollection mocHD = mcHD.GetInstances();
                    foreach (ManagementObject mo in mocHD) //����Ӳ����Ϣ
                    {
                        cBox.Items.Add(mo["DeviceID"].ToString());//���Ӳ�̵��̷�����
                        mo.Dispose();
                    }
                    mcHD.Dispose();
                }
                catch { }
            }

            /// <summary>
            /// ����Ӳ�����к�
            /// </summary>
            /// <param Disk="string">�̷�</param>
            /// <returns>Ӳ�����к�</returns>
            public String GetHardDiskID(string Disk)
            {
                try
                {
                    String strHardDiskID = null;
                    String DiskStr = Disk.Substring(0, 1) + ":";
                    ManagementClass mcHD = new ManagementClass("win32_logicaldisk");
                    ManagementObjectCollection mocHD = mcHD.GetInstances();
                    foreach (ManagementObject mo in mocHD) //����Ӳ����Ϣ
                    {
                        if (mo["DeviceID"].ToString() == DiskStr)//���Ӳ�̵���ָ�����̷�
                        {
                            strHardDiskID = mo["VolumeSerialNumber"].ToString();//��ȡ��ǰӲ�̵����к�
                            mo.Dispose();
                            break;
                        }
                        mo.Dispose();
                    }
                    mcHD.Dispose();
                    return strHardDiskID;
                }
                catch
                {
                    return "";
                }
            }

            /// <summary>
            /// ���ַ������м���
            /// </summary>
            /// <param former="string">�����ַ���</param>
            /// <param spoon="string">��Կ</param>
            /// <param n="int">��Կ��ʶ</param>
            /// <returns>���ܺ���ַ���</returns>
            public string Encrypt(string former, string spoon,int n)
            {
                byte[] FByteArray = Encoding.Default.GetBytes(former);//���ַ��������ֽ�����
                byte[] SByteArray = Encoding.Default.GetBytes(spoon);
                int Aleng = 0;
                if (FByteArray.Length > SByteArray.Length)//��ȡ�ֽ��������󳤶�
                    Aleng = FByteArray.Length;
                else
                    Aleng = SByteArray.Length;
                char[] charData = new char[Aleng];//����ָ�����ȵ��ַ�����
                for (int i = 0; i < FByteArray.Length; i++)//���ֽ������еĵ����ֽڽ����������
                {
                    FByteArray[i] = Convert.ToByte(Convert.ToInt32(FByteArray[i]) ^ Convert.ToInt32(SByteArray[n]));
                }

                Decoder d = Encoding.UTF8.GetDecoder();//��ȡһ��������
                d.GetChars(FByteArray, 0, FByteArray.Length, charData, 0);//�������ֽ�����ת��Ϊ�ַ�����
                d.Reset();//����������Ϊ��ʼ״̬
                string Zpp = "";
                for (int i = 0; i < charData.Length; i++)//���ַ�������ϳ��ַ���
                {
                    Zpp = Zpp + charData[i].ToString();
                }
                n = n + 1;
                if (n < SByteArray.Length-1)
                    Encrypt(Zpp, spoon, n);//���к����ĵݹ����
                return Zpp;
            }


            /// <summary>
            /// �����������ɼ����ַ���
            /// </summary>
            /// <param GroupB="GroupBox">GroupBox�ؼ�</param>
            /// <param Comb="ComboBox">ComboBox�ؼ�</param>
            /// <returns>���ܺ���ַ���</returns>
            public String CreatePass(GroupBox GroupB, ComboBox Comb)
            {
                ArrInt = 0;
                string PrassSum = null;
                ArrayList List = new ArrayList();
                foreach (Control Gb in GroupB.Controls)
                {
                    if (Gb is CheckBox)
                    {
                        if (((CheckBox)Gb).Checked == true)
                        {
                            switch (Convert.ToInt32(((CheckBox)Gb).Tag))
                            {
                                case 0://�������к�
                                    {
                                        PrassSum = GetBIOSNumber();
                                        if (PrassSum.Trim() == "")
                                            MessageBox.Show("�޷���ȡ�������кš�");
                                        break;
                                    }
                                case 1://CPU���к�
                                    {
                                        PrassSum = GetCpuID();
                                        if (PrassSum.Trim() == "")
                                            MessageBox.Show("�޷���ȡCPU���кš�");
                                        break;
                                    }
                                case 2://����Ӳ����ַ
                                    {
                                        PrassSum = GetNetworkCard();
                                        if (PrassSum.Trim() == "")
                                            MessageBox.Show("�޷���ȡ����Ӳ����ַ��");
                                        break;
                                    }
                                case 3://Ӳ�����к�
                                    {
                                        PrassSum = GetHardDiskID(Comb.Text);
                                        if (PrassSum.Trim() == "")
                                            MessageBox.Show("�޷���ȡ" + Comb.Text + "�����кš�");
                                        break;
                                    }
                            }
                            if (PrassSum.Trim() != "")
                            {
                                ArrInt = ArrInt + 1;
                                List.Add(ArrInt);
                                List[ArrInt - 1] = PrassSum;
                                PrassSum = null;
                            }
                        }

                    }
                }

                if (List.Count == 0)
                {
                    MessageBox.Show("��ѡ����ܵ�������");
                    return "";
                }
                int Ci = 1;
                PrassSum = List[0].ToString();
                for (int i = Ci; i < List.Count; i++)
                {
                    PrassSum = Encrypt(PrassSum, List[i].ToString(), 0);
                }

                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] hdcode1 = System.Text.Encoding.UTF8.GetBytes(PrassSum + "new");
                byte[] hdcode2 = md5.ComputeHash(hdcode1);
                md5.Clear();
                char[] charData = new char[hdcode2.Length];//����һ���ַ���
                Decoder d = Encoding.UTF8.GetDecoder();//ʵ����һ��������
                d.GetChars(hdcode2, 0, hdcode2.Length, charData, 0);//�������ֽ�����ת��Ϊ�ַ�����
                PrassSum = "";
                for (int i = 0; i < charData.Length; i++)//���ַ�������ϳ��ַ���
                {
                    PrassSum = PrassSum + charData[i].ToString();
                }
                return PrassSum;
            }

            /// <summary>
            /// ������д��EXE�ļ���
            /// </summary>
            /// <param StrDir="string">EXE�ļ���·��</param>
            /// <param Prass="string">��������</param>
            public void WriteEXE(string StrDir, string Prass)
            {
                byte[] byData = new byte[100];//����һ��FileStreamҪ�õ��ֽ���
                char[] charData = new char[100];//����һ���ַ���
                try
                {
                    Prass = Prass.Trim();
                    FileStream aFile = new FileStream(StrDir, FileMode.Open);//����һ��FileStream������������data.txt�ļ�������������
                    charData = Prass.ToCharArray();//���ַ����ڵ��ַ����Ƶ��ַ�����
                    aFile.Seek(0, SeekOrigin.End);//��ָ���Ƶ��ļ�β
                    Encoder el = Encoding.UTF8.GetEncoder();//������
                    el.GetBytes(charData, 0, charData.Length, byData, 0, true);//���ַ�������뵽�ֽ�������
                    aFile.Write(byData, 0, byData.Length);//���ֽ�д�뵽�ļ���
                    aFile.Dispose();
                }
                catch
                {
                    MessageBox.Show("EXE�ļ�����ʧ�ܡ�");
                }
            }

            /// <summary>
            /// ����TXT�ļ�
            /// </summary>
            /// <param Prass="string">��������</param>            
            public void CreateTXT(string Prass)
            {
                FileStream aFile;
                string TemDir = PFileDir.Substring(0, PFileDir.LastIndexOf("\\"));//��ȡ��ǰ��ִ���ļ���·�����������ļ�����
                TemDir = TemDir + "\\" + PFileN + ".TXT";//�ڿ�ִ���ļ���·�������һ��ͬ����TXT�ļ�
                byte[] byData = new byte[100];//����һ��FileStreamҪ�õ��ֽ���
                char[] charData = new char[100];//����һ���ַ���
                try
                {
                    aFile = new FileStream(TemDir, FileMode.CreateNew);//����һ��FileStream������������data.txt�ļ�������������
                }
                catch
                {
                    aFile = new FileStream(TemDir, FileMode.Truncate);
                }
                try
                {
                    Prass = "���룺" + Prass.Trim();
                    charData = Prass.ToCharArray();//���ַ����ڵ��ַ����Ƶ��ַ�����
                    aFile.Seek(0, SeekOrigin.Begin);//��ָ���Ƶ��ļ���
                    Encoder el = Encoding.UTF8.GetEncoder();//������
                    el.GetBytes(charData, 0, charData.Length, byData, 0, true);//������ת�����ֽ�
                    aFile.Write(byData, 0, byData.Length);//������д���ı���
                    aFile.Dispose();
                }
                catch
                {
                    MessageBox.Show("TXT�ļ�����ʧ�ܡ�");
                }
            }

        }

    }
}
