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
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 设置
        /// </summary>
        static AllSettings setting = new AllSettings();
        /// <summary>
        /// DBF读取来源
        /// </summary>
        public static OdbcConnection conn_old;
        /// <summary>
        /// DBF更新来源
        /// </summary>
        public static OdbcConnection conn_new;
        /// <summary>
        /// 动态字符串
        /// </summary>
        public static StringBuilder sbs;
        /// <summary>
        /// 循环DBF游标
        /// </summary>
        public static OdbcDataReader dr;

        /// <summary>
        /// 获得行情(被绑定在一个控件datagridview)
        /// </summary>
        /// <param name="strHQ">行情地址</param>
        /// <returns></returns>
        public static DataSet GetHQ(string strHQ)
        {
            string connStr = OdbcHelper.ConnDBF_Pre + strHQ + OdbcHelper.ConnDBF_Next;
            string sql = "select * from " + strHQ;
            return OdbcHelper.ExecuteQuery(connStr, CommandType.Text, sql);

        }
        /// <summary>
        /// 冒泡法排序--按文件创建时间(时间由小到大---12:01  12:02)
        /// </summary>
        /// <param name="fi"></param>
        public static void SortCreateTime(FileInfo[] fi)
        {
            int i, j;
            FileInfo temp;
            bool done = false;
            j = 1;
            #region 逻辑
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
        /// 类型转换--转换为double
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
        /// 类型转换--转换为string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetString(object obj)
        {
            return obj.ToString();
        }

        /// <summary>
        /// 下载行情到本地成为文件队列(文件以"文件名+时间"命名)
        /// </summary>
        /// <param name="strSHPath">上交所源行情</param>
        /// <param name="strSZPath">深交所源行情</param>
        /// <param name="strSHLine">上交所行情队列</param>
        /// <param name="strSZLine">深交所行情队列</param>
        //public static string DownLoadHQ(string strSHPath, string strSZPath, string strSHLine, string strSZLine)
        //{
        //    string message = "";
        //    WebClient wc = new WebClient();
        //    wc.Credentials = new NetworkCredential(setting.Username, setting.Password);
        //    string strTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        //    wc.DownloadFile(setting.Url + strSHPath, strSHLine + "SHOW2003_" + strTime + ".DBF");
        //    wc.DownloadFile(setting.Url + strSZPath, strSZLine + "SJSHQ_" + strTime + ".DBF");
        //    message = "下载沪深行情成功！";
        //    return message;
        //}
        /// <summary>
        /// 下载行情
        /// </summary>
        /// <param name="strRemotePath">远程连接地址</param>
        /// <param name="strHQLine">文件队列地址</param>
        /// <param name="index">索引(1:上海；2:深圳)</param>
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
                    message = "下载上海行情成功！";
                    break;
                case 2:
                    wc.DownloadFile(setting.Url + strRemotePath, strHQLine + "SJSHQ_" + DateTime.Now.ToString("yyyyMMddHHmmssff") + ".DBF");
                    message = "下载深圳行情成功！";
                    break;
            }
            return message;
        }

        /// <summary>
        /// 返回连接
        /// </summary>
        /// <param name="strLinkFile"></param>
        /// <returns></returns>
        public static OdbcConnection GetConnection(string strLinkFile)
        {
            return new OdbcConnection(OdbcHelper.ConnDBF_Pre + strLinkFile + OdbcHelper.ConnDBF_Next);
        }
        /// <summary>
        /// 获取股票信息
        /// </summary>
        public static void GetStockInfo()
        {          
             UpdateSHHQ(CommonSetting.RemotePath + "SHOW2003.DBF");         
             UpdateSZHQ(CommonSetting.RemotePath + "SJSHQ.DBF");
        }
        /// <summary>
        /// 初始化数据库
        /// </summary>
        public static void InitDataBase()
        {
            string sqlstr = "truncate table  SHASE";
            SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnStringZX, CommandType.Text, sqlstr, null);
             sqlstr = "truncate table  SZNSE";
            SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnStringZX, CommandType.Text, sqlstr, null);
        }
        /// <summary>
        /// 更新上海行情
        /// </summary>
        /// <param name="strSourceFile">源行情(来自行情队列)(如:SHOW2003_20100410102323.DBF)</param>
        /// <param name="strTargetFile">要更新的行情(SHOW2003.DBF)</param>
        public static string UpdateSHHQ(string strSourceFile)
        {
            #region 逻辑
            string outfilepath = CommonSetting.RealTimeLinePath + @"SHASE\";
            string sql = "select * from " + strSourceFile ;
            conn_old = GetConnection(strSourceFile);
            sbs = new StringBuilder();
            //try
            //{
                conn_old.Open();

                dr = OdbcHelper.ExecuteReader(conn_old, CommandType.Text, sql);

                #region 循环
                SecondeLine SL = new SecondeLine();
          
                while (dr.Read())
                {
                    //string mycode = dr["s1"].ToString();
                    ////sbs.Append(GetString(dr["s1"])); sbs.Append(",");//证券代码 -0
                    ////sbs.Append(GetString(dr["s2"])); sbs.Append(",");//证券名称-
                    ////sbs.Append(GetDouble(dr["s3"])); sbs.Append(",");//昨收盘价格-
                    //sbs.Append(dr["s4"]); sbs.Append(",");//今开盘价格-
                    //sbs.Append(dr["s8"]); sbs.Append(",");//最新价-
                    ////sbs.Append(GetDouble(dr["s11"])); sbs.Append(",");//成交数量-
                    ////sbs.Append(GetDouble(dr["s5"])); sbs.Append(",");//今成交金额-
                    //sbs.Append("0"); sbs.Append(","); //成交笔数-成交数量
                    ////sbs.Append(GetDouble(dr["s6"])); sbs.Append(",");//最高价-
                    ////sbs.Append(GetDouble(dr["s7"])); sbs.Append(",");//最低价-
                    ////sbs.Append(GetDouble(dr["s13"])); sbs.Append(",");//市盈率-

                    ////sbs.Append(GetDouble(dr["s32"])); sbs.Append(",");//卖价5
                    ////sbs.Append(GetDouble(dr["s33"])); sbs.Append(",");//卖量5
                    ////sbs.Append(GetDouble(dr["s30"])); sbs.Append(",");//卖价4
                    ////sbs.Append(GetDouble(dr["s31"])); sbs.Append(",");//卖量4
                    ////sbs.Append(GetDouble(dr["s24"])); sbs.Append(",");//卖价3
                    ////sbs.Append(GetDouble(dr["s25"])); sbs.Append(",");//卖量3
                    ////sbs.Append(GetDouble(dr["s22"])); sbs.Append(",");//卖价2
                    ////sbs.Append(GetDouble(dr["s23"])); sbs.Append(",");//卖量2
                    //sbs.Append(dr["s10"]); sbs.Append(",");//当前卖出价-卖价1
                    //sbs.Append(dr["s21"]); sbs.Append(",");//卖量1
                    //sbs.Append(dr["s9"]); sbs.Append(",");//当前买入价-买价1
                    //sbs.Append(dr["s15"]); sbs.Append(",");//买量1
                    ////sbs.Append(GetDouble(dr["s16"])); sbs.Append(",");//买价2
                    ////sbs.Append(GetDouble(dr["s17"])); sbs.Append(",");//买量2
                    ////sbs.Append(GetDouble(dr["s18"])); sbs.Append(",");//买价3
                    ////sbs.Append(GetDouble(dr["s19"])); sbs.Append(",");//买量3
                    ////sbs.Append(GetDouble(dr["s26"])); sbs.Append(",");//买价4
                    ////sbs.Append(GetDouble(dr["s27"])); sbs.Append(",");//买量4
                    ////sbs.Append(GetDouble(dr["s28"])); sbs.Append(",");//买价5
                    ////sbs.Append(GetDouble(dr["s29"])); sbs.Append(",");//买量5
                    ////sbs.Append(DateTime.Now.ToString("HH:mm:ss"));

                    string ccj = dr["s8"].ToString();//成交价
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
        /// 更新深圳行情
        /// </summary>
        /// <param name="strSourceFile">源行情(来自行情队列)(如:SJSHQ_20100410102323.DBF)</param>
        /// <param name="strTargetFile">要更新的行情(SJSHQ.DBF)</param>
        public static string UpdateSZHQ(string strSourceFile)
        {
            #region 逻辑

            string outfilepath = CommonSetting.RealTimeLinePath + @"SZNSE\";
            string sql = "select * from " + strSourceFile ;

            conn_old = GetConnection(strSourceFile);
            sbs = new StringBuilder();
            //try
            //{
                conn_old.Open();

                dr = OdbcHelper.ExecuteReader(conn_old, CommandType.Text, sql);
                #region 循环
                SecondeLine SL = new SecondeLine();
               while (dr.Read())
                {

                    //string mycode = dr["HQZQDM"].ToString();
                    ////sbs.Append(GetString(dr["HQZQDM"])); sbs.Append(",");//证券代码
                    ////sbs.Append(GetString(dr["HQZQJC"])); sbs.Append(",");//证券简称
                    ////sbs.Append(GetDouble(dr["HQZRSP"])); sbs.Append(",");//昨日收盘价
                    //sbs.Append(dr["HQJRKP"]); sbs.Append(",");//今日开盘价
                    //sbs.Append(dr["HQZJCJ"]); sbs.Append(",");//最近成交价
                    ////sbs.Append(GetDouble(dr["HQCJSL"])); sbs.Append(",");//成交数量
                    ////sbs.Append(GetDouble(dr["HQCJJE"])); sbs.Append(",");//成交金额
                    //sbs.Append(dr["HQCJBS"]); sbs.Append(",");//成交笔数
                    ////sbs.Append(GetDouble(dr["HQZGCJ"])); sbs.Append(",");//最高成交价
                    ////sbs.Append(GetDouble(dr["HQZDCJ"])); sbs.Append(",");//最低成交价
                    ////sbs.Append(GetDouble(dr["HQSYL1"])); sbs.Append(",");//市盈率1
                    //////sbs.Append( GetDouble(dr["HQSYL2"])); sbs.Append( ",");//市盈率2
                    //////sbs.Append( GetDouble(dr["HQJSD1"])); sbs.Append( ",");//价格升跌1
                    //////sbs.Append( GetDouble(dr["HQJSD2"])); sbs.Append( ",");//价格升跌2
                    //////sbs.Append( GetDouble(dr["HQHYCC"])); sbs.Append( ",");//合约持仓量
                    ////sbs.Append(GetDouble(dr["HQSJW5"])); sbs.Append(",");//卖价位五
                    ////sbs.Append(GetDouble(dr["HQSSL5"])); sbs.Append(",");//卖数量五
                    ////sbs.Append(GetDouble(dr["HQSJW4"])); sbs.Append(",");//卖价位四
                    ////sbs.Append(GetDouble(dr["HQSSL4"])); sbs.Append(",");//卖数量四
                    ////sbs.Append(GetDouble(dr["HQSJW3"])); sbs.Append(",");//卖价位三
                    ////sbs.Append(GetDouble(dr["HQSSL3"])); sbs.Append(",");//卖数量三
                    ////sbs.Append(GetDouble(dr["HQSJW2"])); sbs.Append(",");//卖价位二
                    ////sbs.Append(GetDouble(dr["HQSSL2"])); sbs.Append(",");//卖数量二
                    //sbs.Append(dr["HQSJW1"]); sbs.Append(",");//卖价位一/叫卖揭示价
                    //sbs.Append(dr["HQSSL1"]); sbs.Append(",");//卖数量一

                    //sbs.Append(dr["HQBJW1"]); sbs.Append(",");//买价位一/叫买揭示价
                    //sbs.Append(dr["HQBSL1"]); sbs.Append(",");//买数量一
                    ////sbs.Append(GetDouble(dr["HQBJW2"])); sbs.Append(",");//买价位二
                    ////sbs.Append(GetDouble(dr["HQBSL2"])); sbs.Append(",");//买数量二
                    ////sbs.Append(GetDouble(dr["HQBJW3"])); sbs.Append(",");//买价位三
                    ////sbs.Append(GetDouble(dr["HQBSL3"])); sbs.Append(",");//买数量三
                    ////sbs.Append(GetDouble(dr["HQBJW4"])); sbs.Append(",");//买价位四
                    ////sbs.Append(GetDouble(dr["HQBSL4"])); sbs.Append(",");//买数量四
                    ////sbs.Append(GetDouble(dr["HQBJW5"])); sbs.Append(",");//买价位五
                    ////sbs.Append(GetDouble(dr["HQBSL5"])); sbs.Append(""); //买数量五

                    ////sbs.Append(DateTime.Now.ToString("HH:mm:ss"));
                    string ccj = dr["HQZJCJ"].ToString();//成交价
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
            //    Console.WriteLine("深圳读取异常：" + exp.Message);
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
    /// CommonSetting 的摘要说明
    /// </summary>
    public class CommonSetting
    {
        public CommonSetting()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 当日成交线路径
        /// </summary>
        public static string RealTimeLinePath = AppDomain.CurrentDomain.BaseDirectory + @"App_data\RealTimeLine\";
        /// <summary>
        /// 实时行情传输路径
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
        /// 用户名
        /// </summary>
        public string Username
        {
            set { _username = value; }
            get { return _username; }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }

        /// <summary>
        /// FTP路径
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }

        /// <summary>
        /// 上交所行情
        /// </summary>
        public string Show2003
        {
            set { _show2003 = value; }
            get { return _show2003; }
        }

        /// <summary>
        /// 深交所行情
        /// </summary>
        public string Sjshq
        {
            set { _sjshq = value; }
            get { return _sjshq; }
        }
    }
}
