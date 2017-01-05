using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
using System.Data;
using System.IO;
using System.Net;
namespace WoYingFinaceService
{
    public class GetReatNowPrice
    {
        public GetReatNowPrice()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        /// <summary>
        /// ����
        /// </summary>
        static AllSettings setting = new AllSettings();
        /// <summary>
        /// DBF��ȡ��Դ
        /// </summary>
        public static OdbcConnection conn_old;
        /// <summary>
        /// DBF������Դ
        /// </summary>
        public static OdbcConnection conn_new;
        /// <summary>
        /// ��̬�ַ���
        /// </summary>
        public static StringBuilder sbs;
        /// <summary>
        /// ѭ��DBF�α�
        /// </summary>
        public static OdbcDataReader dr;

        /// <summary>
        /// �������(������һ���ؼ�datagridview)
        /// </summary>
        /// <param name="strHQ">�����ַ</param>
        /// <returns></returns>
        public static DataSet GetHQ(string strHQ)
        {
            string connStr = OdbcHelper.ConnDBF_Pre + strHQ + OdbcHelper.ConnDBF_Next;
            string sql = "select * from " + strHQ;
            return OdbcHelper.ExecuteQuery(connStr, CommandType.Text, sql);

        }
        /// <summary>
        /// ð�ݷ�����--���ļ�����ʱ��(ʱ����С����---12:01  12:02)
        /// </summary>
        /// <param name="fi"></param>
        public static void SortCreateTime(FileInfo[] fi)
        {
            int i, j;
            FileInfo temp;
            bool done = false;
            j = 1;
            #region �߼�
            while ((j < fi.Length) && (!done))
            {
                done = true;
                for (i = 0; i < fi.Length - j; i++)
                {
                    if (DateTime.Compare(fi[i].CreationTime, fi[i + 1].CreationTime) > 0)
                    {
                        done = false;
                        temp = fi[i];
                        fi[i] = fi[i + 1];
                        fi[i + 1] = temp;
                    }
                }
                j++;
            }
            #endregion
        }

        /// <summary>
        /// ����ת��--ת��Ϊdouble
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int GetDouble(object obj)
        {
            try
            {
                double myvalue = double.Parse(obj.ToString());
                myvalue = myvalue * 100;
                string value = "0";
                value = myvalue.ToString();
                int valuedotp = value.LastIndexOf(".");
                if (valuedotp != -1)
                {
                    value = value.Substring(0, valuedotp);
                }
                return int.Parse(value);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// ����ת��--ת��Ϊstring
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetString(object obj)
        {
            return obj.ToString();
        }

        /// <summary>
        /// �������鵽���س�Ϊ�ļ�����(�ļ���"�ļ���+ʱ��"����)
        /// </summary>
        /// <param name="strSHPath">�Ͻ���Դ����</param>
        /// <param name="strSZPath">���Դ����</param>
        /// <param name="strSHLine">�Ͻ����������</param>
        /// <param name="strSZLine">����������</param>
        //public static string DownLoadHQ(string strSHPath, string strSZPath, string strSHLine, string strSZLine)
        //{
        //    string message = "";
        //    WebClient wc = new WebClient();
        //    wc.Credentials = new NetworkCredential(setting.Username, setting.Password);
        //    string strTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        //    wc.DownloadFile(setting.Url + strSHPath, strSHLine + "SHOW2003_" + strTime + ".DBF");
        //    wc.DownloadFile(setting.Url + strSZPath, strSZLine + "SJSHQ_" + strTime + ".DBF");
        //    message = "���ػ�������ɹ���";
        //    return message;
        //}
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="strRemotePath">Զ�����ӵ�ַ</param>
        /// <param name="strHQLine">�ļ����е�ַ</param>
        /// <param name="index">����(1:�Ϻ���2:����)</param>
        /// <returns>string</returns>
        public static string DownLoadHQ(string strRemotePath, string strHQLine, int index)
        {
            string message = "";
            WebClient wc = new WebClient();
            wc.Credentials = new NetworkCredential(setting.Username, setting.Password);
            switch (index)
            {
                case 1:
                    wc.DownloadFile(setting.Url + strRemotePath, strHQLine + "SHOW2003_" + DateTime.Now.ToString("yyyyMMddHHmmssff") + ".DBF");
                    message = "�����Ϻ�����ɹ���";
                    break;
                case 2:
                    wc.DownloadFile(setting.Url + strRemotePath, strHQLine + "SJSHQ_" + DateTime.Now.ToString("yyyyMMddHHmmssff") + ".DBF");
                    message = "������������ɹ���";
                    break;
            }
            return message;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="strLinkFile"></param>
        /// <returns></returns>
        public static OdbcConnection GetConnection(string strLinkFile)
        {
            return new OdbcConnection(OdbcHelper.ConnDBF_Pre + strLinkFile + OdbcHelper.ConnDBF_Next);
        }
        /// <summary>
        /// ��ȡ��Ʊ��Ϣ
        /// </summary>
        public static void GetStockInfo()
        {          
             UpdateSHHQ(CommonSetting.RemotePath + "SHOW2003.DBF");         
             UpdateSZHQ(CommonSetting.RemotePath + "SJSHQ.DBF");
        }
        /// <summary>
        /// ��ʼ�����ݿ�
        /// </summary>
        public static void InitDataBase()
        {
            string sqlstr = "truncate table  SHASE";
            SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnStringZX, CommandType.Text, sqlstr, null);
             sqlstr = "truncate table  SZNSE";
            SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnStringZX, CommandType.Text, sqlstr, null);
        }
        /// <summary>
        /// �����Ϻ�����
        /// </summary>
        /// <param name="strSourceFile">Դ����(�����������)(��:SHOW2003_20100410102323.DBF)</param>
        /// <param name="strTargetFile">Ҫ���µ�����(SHOW2003.DBF)</param>
        public static string UpdateSHHQ(string strSourceFile)
        {
            #region �߼�
            string outfilepath = CommonSetting.RealTimeLinePath + @"SHASE\";
            string sql = "select * from " + strSourceFile ;
            conn_old = GetConnection(strSourceFile);
            sbs = new StringBuilder();
            //try
            //{
                conn_old.Open();

                dr = OdbcHelper.ExecuteReader(conn_old, CommandType.Text, sql);

                #region ѭ��
                SecondeLine SL = new SecondeLine();
          
                while (dr.Read())
                {
                    //string mycode = dr["s1"].ToString();
                    ////sbs.Append(GetString(dr["s1"])); sbs.Append(",");//֤ȯ���� -0
                    ////sbs.Append(GetString(dr["s2"])); sbs.Append(",");//֤ȯ����-
                    ////sbs.Append(GetDouble(dr["s3"])); sbs.Append(",");//�����̼۸�-
                    //sbs.Append(dr["s4"]); sbs.Append(",");//���̼۸�-
                    //sbs.Append(dr["s8"]); sbs.Append(",");//���¼�-
                    ////sbs.Append(GetDouble(dr["s11"])); sbs.Append(",");//�ɽ�����-
                    ////sbs.Append(GetDouble(dr["s5"])); sbs.Append(",");//��ɽ����-
                    //sbs.Append("0"); sbs.Append(","); //�ɽ�����-�ɽ�����
                    ////sbs.Append(GetDouble(dr["s6"])); sbs.Append(",");//��߼�-
                    ////sbs.Append(GetDouble(dr["s7"])); sbs.Append(",");//��ͼ�-
                    ////sbs.Append(GetDouble(dr["s13"])); sbs.Append(",");//��ӯ��-

                    ////sbs.Append(GetDouble(dr["s32"])); sbs.Append(",");//����5
                    ////sbs.Append(GetDouble(dr["s33"])); sbs.Append(",");//����5
                    ////sbs.Append(GetDouble(dr["s30"])); sbs.Append(",");//����4
                    ////sbs.Append(GetDouble(dr["s31"])); sbs.Append(",");//����4
                    ////sbs.Append(GetDouble(dr["s24"])); sbs.Append(",");//����3
                    ////sbs.Append(GetDouble(dr["s25"])); sbs.Append(",");//����3
                    ////sbs.Append(GetDouble(dr["s22"])); sbs.Append(",");//����2
                    ////sbs.Append(GetDouble(dr["s23"])); sbs.Append(",");//����2
                    //sbs.Append(dr["s10"]); sbs.Append(",");//��ǰ������-����1
                    //sbs.Append(dr["s21"]); sbs.Append(",");//����1
                    //sbs.Append(dr["s9"]); sbs.Append(",");//��ǰ�����-���1
                    //sbs.Append(dr["s15"]); sbs.Append(",");//����1
                    ////sbs.Append(GetDouble(dr["s16"])); sbs.Append(",");//���2
                    ////sbs.Append(GetDouble(dr["s17"])); sbs.Append(",");//����2
                    ////sbs.Append(GetDouble(dr["s18"])); sbs.Append(",");//���3
                    ////sbs.Append(GetDouble(dr["s19"])); sbs.Append(",");//����3
                    ////sbs.Append(GetDouble(dr["s26"])); sbs.Append(",");//���4
                    ////sbs.Append(GetDouble(dr["s27"])); sbs.Append(",");//����4
                    ////sbs.Append(GetDouble(dr["s28"])); sbs.Append(",");//���5
                    ////sbs.Append(GetDouble(dr["s29"])); sbs.Append(",");//����5
                    ////sbs.Append(DateTime.Now.ToString("HH:mm:ss"));

                    string ccj = dr["s8"].ToString();//�ɽ���
                    string kpj = dr["s4"].ToString();
                    string mrj = dr["s9"].ToString();
                    string mcj = dr["s10"].ToString();
                    string mrl = dr["s15"].ToString();
                    string mcl = dr["s21"].ToString();
                    string gpdm = dr["s1"].ToString();
                    string bs = "0";
                    string sqlstr = "insert into SHASE(cjj,kpj,mrj,mcj,mrl,mcl,gpdm,bs) values(" + ccj + "," + kpj + "," + mrj + "," + mcj + "," + mrl + "," + mcl + ",'" + gpdm + "'," + bs + ")";
                    SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnStringZX, CommandType.Text, sqlstr, null);
                    //  SL.CreateSencond(outfilepath + mycode + ".scd", sbs.ToString());
                 


                }
                #endregion
            //}
            //catch (Exception exp)
            //{
            //    sbs.Append(exp.Message);
            //}
            //finally
            //{
            //    conn_old.Close();

            //}
            #endregion
            return sbs.ToString();
        }

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="strSourceFile">Դ����(�����������)(��:SJSHQ_20100410102323.DBF)</param>
        /// <param name="strTargetFile">Ҫ���µ�����(SJSHQ.DBF)</param>
        public static string UpdateSZHQ(string strSourceFile)
        {
            #region �߼�

            string outfilepath = CommonSetting.RealTimeLinePath + @"SZNSE\";
            string sql = "select * from " + strSourceFile ;

            conn_old = GetConnection(strSourceFile);
            sbs = new StringBuilder();
            //try
            //{
                conn_old.Open();

                dr = OdbcHelper.ExecuteReader(conn_old, CommandType.Text, sql);
                #region ѭ��
                SecondeLine SL = new SecondeLine();
               while (dr.Read())
                {

                    //string mycode = dr["HQZQDM"].ToString();
                    ////sbs.Append(GetString(dr["HQZQDM"])); sbs.Append(",");//֤ȯ����
                    ////sbs.Append(GetString(dr["HQZQJC"])); sbs.Append(",");//֤ȯ���
                    ////sbs.Append(GetDouble(dr["HQZRSP"])); sbs.Append(",");//�������̼�
                    //sbs.Append(dr["HQJRKP"]); sbs.Append(",");//���տ��̼�
                    //sbs.Append(dr["HQZJCJ"]); sbs.Append(",");//����ɽ���
                    ////sbs.Append(GetDouble(dr["HQCJSL"])); sbs.Append(",");//�ɽ�����
                    ////sbs.Append(GetDouble(dr["HQCJJE"])); sbs.Append(",");//�ɽ����
                    //sbs.Append(dr["HQCJBS"]); sbs.Append(",");//�ɽ�����
                    ////sbs.Append(GetDouble(dr["HQZGCJ"])); sbs.Append(",");//��߳ɽ���
                    ////sbs.Append(GetDouble(dr["HQZDCJ"])); sbs.Append(",");//��ͳɽ���
                    ////sbs.Append(GetDouble(dr["HQSYL1"])); sbs.Append(",");//��ӯ��1
                    //////sbs.Append( GetDouble(dr["HQSYL2"])); sbs.Append( ",");//��ӯ��2
                    //////sbs.Append( GetDouble(dr["HQJSD1"])); sbs.Append( ",");//�۸�����1
                    //////sbs.Append( GetDouble(dr["HQJSD2"])); sbs.Append( ",");//�۸�����2
                    //////sbs.Append( GetDouble(dr["HQHYCC"])); sbs.Append( ",");//��Լ�ֲ���
                    ////sbs.Append(GetDouble(dr["HQSJW5"])); sbs.Append(",");//����λ��
                    ////sbs.Append(GetDouble(dr["HQSSL5"])); sbs.Append(",");//��������
                    ////sbs.Append(GetDouble(dr["HQSJW4"])); sbs.Append(",");//����λ��
                    ////sbs.Append(GetDouble(dr["HQSSL4"])); sbs.Append(",");//��������
                    ////sbs.Append(GetDouble(dr["HQSJW3"])); sbs.Append(",");//����λ��
                    ////sbs.Append(GetDouble(dr["HQSSL3"])); sbs.Append(",");//��������
                    ////sbs.Append(GetDouble(dr["HQSJW2"])); sbs.Append(",");//����λ��
                    ////sbs.Append(GetDouble(dr["HQSSL2"])); sbs.Append(",");//��������
                    //sbs.Append(dr["HQSJW1"]); sbs.Append(",");//����λһ/������ʾ��
                    //sbs.Append(dr["HQSSL1"]); sbs.Append(",");//������һ

                    //sbs.Append(dr["HQBJW1"]); sbs.Append(",");//���λһ/�����ʾ��
                    //sbs.Append(dr["HQBSL1"]); sbs.Append(",");//������һ
                    ////sbs.Append(GetDouble(dr["HQBJW2"])); sbs.Append(",");//���λ��
                    ////sbs.Append(GetDouble(dr["HQBSL2"])); sbs.Append(",");//��������
                    ////sbs.Append(GetDouble(dr["HQBJW3"])); sbs.Append(",");//���λ��
                    ////sbs.Append(GetDouble(dr["HQBSL3"])); sbs.Append(",");//��������
                    ////sbs.Append(GetDouble(dr["HQBJW4"])); sbs.Append(",");//���λ��
                    ////sbs.Append(GetDouble(dr["HQBSL4"])); sbs.Append(",");//��������
                    ////sbs.Append(GetDouble(dr["HQBJW5"])); sbs.Append(",");//���λ��
                    ////sbs.Append(GetDouble(dr["HQBSL5"])); sbs.Append(""); //��������

                    ////sbs.Append(DateTime.Now.ToString("HH:mm:ss"));
                    string ccj = dr["HQZJCJ"].ToString();//�ɽ���
                    string kpj = dr["HQJRKP"].ToString();
                    string mrj = dr["HQBJW1"].ToString();
                    string mcj = dr["HQSJW1"].ToString();
                    string mrl = dr["HQBSL1"].ToString();
                    string mcl = dr["HQSSL1"].ToString();
                    string gpdm = dr["HQZQDM"].ToString();
                    string bs = dr["HQCJBS"].ToString();
                    string sqlstr = "insert into SZNSE(cjj,kpj,mrj,mcj,mrl,mcl,gpdm,bs) values(" + ccj + "," + kpj + "," + mrj + "," + mcj + "," + mrl + "," + mcl + ",'" + gpdm + "'," + bs + ")";
                    SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnStringZX, CommandType.Text, sqlstr, null);
                   //  SL.CreateSencond(outfilepath + mycode + ".scd", sbs.ToString());
                

                }
                #endregion
            //}
            //catch (Exception exp)
            //{
            //    Console.WriteLine("���ڶ�ȡ�쳣��" + exp.Message);
            //}
            //finally
            //{
            //    conn_old.Close();

            //}
            return sbs.ToString();
            #endregion
        }
    }
    /// <summary>
    /// CommonSetting ��ժҪ˵��
    /// </summary>
    public class CommonSetting
    {
        public CommonSetting()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        /// <summary>
        /// ���ճɽ���·��
        /// </summary>
        public static string RealTimeLinePath = AppDomain.CurrentDomain.BaseDirectory + @"App_data\RealTimeLine\";
        /// <summary>
        /// ʵʱ���鴫��·��
        /// </summary>
        public static string RemotePath = AppDomain.CurrentDomain.BaseDirectory + @"App_data\remote\";
    }
    class AllSettings
    {
        private string _username = "Administrator";
        private string _password = "1";
        private string _url = "ftp://192.168.1.5";
        private string _show2003 = "SHOW2003.DBF";
        private string _sjshq = "SJSHQ.DBF";

        /// <summary>
        /// �û���
        /// </summary>
        public string Username
        {
            set { _username = value; }
            get { return _username; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }

        /// <summary>
        /// FTP·��
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }

        /// <summary>
        /// �Ͻ�������
        /// </summary>
        public string Show2003
        {
            set { _show2003 = value; }
            get { return _show2003; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public string Sjshq
        {
            set { _sjshq = value; }
            get { return _sjshq; }
        }
    }
}
