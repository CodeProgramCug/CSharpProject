using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace WoYingFinaceService
{
    /// <summary>
    /// SecondeLine ��ժҪ˵��
    /// </summary>
    public class SecondeLine
    {
        /// <summary>
        /// ����һ����ʱ��
        /// </summary>
        /// <param name="TXTpath">�ļ�·��</param>
        /// <param name="dsi">������Ϣ</param>
        public void CreateSencond(string TXTpath, SencondLineInfo dsi)
        {
            if (!File.Exists(TXTpath))
            {
                FileStream sm = File.Create(TXTpath);
                sm.Close();
            }
            FileStream stream = new FileStream(TXTpath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            BinaryWriter b_reader = new BinaryWriter(stream);
            b_reader.Write(dsi.ShiFen.ToString("mmss"));
            b_reader.Write(dsi.ChengJiao);
            b_reader.Write(dsi.ZhangDie);
            b_reader.Write(dsi.FuDu);
            b_reader.Write(dsi.MaiRu);
            b_reader.Write(dsi.MaiChu);
            b_reader.Write(dsi.Liang);
            b_reader.Write(dsi.BiShu);
            b_reader.Write(dsi.Backup);
            b_reader.Write(dsi.time.ToString("HHmmss"));
            stream.Close();
        }
        /// <summary>
        /// ����һ����ʱ��
        /// </summary>
        /// <param name="TXTpath">�ļ�·��</param>
        /// <param name="dsi">������Ϣ</param>
        public void CreateSencond(string TXTpath,string BTS)
        {
            if (!File.Exists(TXTpath))
            {
                FileStream sm = File.Create(TXTpath);
                sm.Close();
            }
            //StreamWriter sw = new StreamWriter(TXTpath,true,Encoding.Default);
            //sw.WriteLine(BTS);
            //sw.Close();
            FileStream stream = new FileStream(TXTpath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            BinaryWriter b_reader = new BinaryWriter(stream);
            b_reader.Write(BTS);

            stream.Close();
        }
        /// <summary>
        /// ��ȡ���ڵ�����
        /// </summary>
        /// <param name="DateStr">����</param>
        /// <param name="p_strFileName">��Ʊ�ļ�</param>
        /// <returns></returns>
        public List<SencondLineInfo> GetAreaLineToNow(DateTime DateStr, string p_strFileName)
        {
            List<SencondLineInfo> ldsi = new List<SencondLineInfo>();
            int MyDateTime = 0;
            //��ȡ�����µ�ƫ�Ƶ�ַ
            int StartPostion = GetDayPostion(DateStr, p_strFileName);
            int CountValue = 0;
            FileStream stream = new FileStream(p_strFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader b_reader = new BinaryReader(stream);
            stream.Position = (long)StartPostion * 40;
            while (stream.CanRead && stream.Position < stream.Length)
            {
                SencondLineInfo dsi = new SencondLineInfo();
                dsi.ShiFen = b_reader.ReadInt32();
                dsi.ChengJiao = b_reader.ReadInt32();
                dsi.ZhangDie = b_reader.ReadInt32();
                dsi.FuDu = b_reader.ReadInt32();
                dsi.MaiRu = b_reader.ReadInt32();
                dsi.MaiChu = b_reader.ReadInt32();
                dsi.Liang = b_reader.ReadInt32();
                dsi.BiShu = b_reader.ReadInt32();
                dsi.Backup = b_reader.ReadInt32();
                try
                {
                    MyDateTime = b_reader.ReadInt32();
                }
                catch (Exception ex)
                {

                }
                DateTime day = new DateTime();
                try
                {
                    int yeari = MyDateTime / 10000;
                    int monthi = (MyDateTime % 10000) / 100;
                    int dayi = MyDateTime % 100;
                    day = new DateTime(yeari, monthi, dayi);
                }
                catch (Exception exp)
                {

                }
                dsi.time = day;
                ldsi.Add(dsi);
                CountValue++;
            }
            return ldsi;
        }

        /// <summary>
        /// ��ȡK�ߣ������������
        /// </summary>
        /// <param name="p_strFileName"></param>
        public List<SencondLineInfo> GetLine(string p_strFileName)
        {
            List<SencondLineInfo> ldsi = new List<SencondLineInfo>();
            int CountValue = 0;
            FileStream stream = new FileStream(p_strFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader b_reader = new BinaryReader(stream);
            int MyDateTime=0;
            while (stream.CanRead && stream.Position < stream.Length)
            {
                SencondLineInfo dsi = new SencondLineInfo();
                dsi.ShiFen = b_reader.ReadInt32();
                dsi.ChengJiao = b_reader.ReadInt32();
                dsi.ZhangDie = b_reader.ReadInt32();
                dsi.FuDu = b_reader.ReadInt32();
                dsi.MaiRu = b_reader.ReadInt32();
                dsi.MaiChu = b_reader.ReadInt32();
                dsi.Liang = b_reader.ReadInt32();
                dsi.BiShu = b_reader.ReadInt32();
                dsi.Backup = b_reader.ReadInt32();
                try
                {
                    MyDateTime = b_reader.ReadInt32();
                }
                catch (Exception ex)
                {

                }
                DateTime day = new DateTime();
                try
                {
                    int yeari = MyDateTime / 10000;
                    int monthi = (MyDateTime % 10000) / 100;
                    int dayi = MyDateTime % 100;
                    day = new DateTime(yeari, monthi, dayi);
                }
                catch (Exception exp)
                {

                }
                dsi.time = day;
                ldsi.Add(dsi);
                CountValue++;
            }
            return ldsi;

        }
        /// <summary>
        /// �۰뷽����ȡƫ��λ��
        /// </summary>
        /// <param name="DateStr">����</param>
        /// <param name="p_strFileName">�ļ�·��</param>
        /// <returns></returns>
        public int GetDayPostion(DateTime DateStr, string p_strFileName)
        {
            int MyDateTime = 0;
            //����
            int rowLength = 40;
            //����
            int MyDate = int.Parse(DateStr.ToString("HHmm"));
            FileStream stream = new FileStream(p_strFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader b_reader = new BinaryReader(stream);
            //�ļ�����
            long fileLength = stream.Length;
            //��¼����
            int recCount = (int)(fileLength / rowLength);
            //��ʼλ��
            int recStart = 0;
            //����λ��
            int recEnd = recCount;
            //ѡ���λ��
            int curRowNo = 0;
            //���λ��
            int result;
            //���۰�
            curRowNo = (recStart + recEnd) / 2;
            //��Ҫѭ��
            bool NeedLoop = true;
            while (NeedLoop)
            {
                // Console.WriteLine(curRowNo.ToString() +"-"+ DateTime.ToString());
                stream.Position = (long)(curRowNo * rowLength);
                MyDateTime = b_reader.ReadInt32();

                //���ȡ��ʱ��С���۰�ʱ�䣬�α�����
                if (MyDate < MyDateTime)
                {
                    recEnd = curRowNo;
                    curRowNo = (recStart + curRowNo) / 2;

                    if (curRowNo == 0)
                    {
                        stream.Position = (long)(curRowNo * rowLength);
                        MyDateTime = b_reader.ReadInt32();
                        if (MyDateTime == MyDate)
                        {
                            return 0;
                        }
                        else
                        { break; }
                    }
                }
                //���ȡ��ʱ������۰�ʱ�䣬�α�����
                if (MyDate > MyDateTime)
                {
                    recStart = curRowNo;
                    curRowNo = (curRowNo + recEnd) / 2;

                    if ((recEnd - curRowNo) <= 1)
                    {
                        stream.Position = (long)(curRowNo * rowLength);
                        MyDateTime = b_reader.ReadInt32();
                        if (MyDateTime == MyDate)
                        {
                            return curRowNo;
                        }
                        else
                        { break; }
                    }
                }
                //�����ȣ�λ�þ��ڴ���
                if (MyDate == MyDateTime)
                {
                    NeedLoop = false;
                    return curRowNo;
                }
            }
            stream.Close();
            return -1;



        }
        public SecondeLine()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
    }
    public class SencondLineInfo
    {
        /// <summary>
        /// ʱ��
        /// </summary>
        public DateTime time = new DateTime();
        /// <summary>
        /// ʱ�� 930(9:20) 1020(10:20)
        /// </summary>
        public int ShiFen = 930;
        /// <summary>
        /// �ɽ��� 2310Ϊ23.10(������100)
        /// </summary>
        public int ChengJiao = 0;
        /// <summary>
        /// �ǵ� 10Ϊ10/100(0.10),4Ϊ4/100(0.04)
        /// </summary>
        public int ZhangDie = 0;
        /// <summary>
        /// ���� 10Ϊ10/100(0.10),4Ϊ4/100(0.04)
        /// </summary>
        public int FuDu = 0;
        /// <summary>
        /// ��ʱ����۸� 2310Ϊ23.10(������100)
        /// </summary>
        public int MaiRu = 0;
        /// <summary>
        /// ��ʱ�����۸� 2310Ϊ23.10(������100)
        /// </summary>
        public int MaiChu = 0;
        /// <summary>
        /// ��������Ϊ����������ʱ����
        /// </summary>
        public int Liang = 0;
        /// <summary>
        /// ������level1�Ϻ�û�б���
        /// </summary>
        public int BiShu = 0;
        /// <summary>
        /// ��ע����1Ϊ���룬0Ϊ����
        /// </summary>
        public int Backup = 0;
    }

}
