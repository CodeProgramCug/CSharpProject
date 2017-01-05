using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
using System.Data;
namespace WoYingFinaceService
{
    public class OdbcHelper
    {
        /// <summary>
        /// ����Դǰ�벿��
        /// </summary>
        public static string ConnDBF_Pre = "Driver={Microsoft Visual FoxPro Driver};SourceType=DBF;SourceDB=";
        /// <summary>
        /// ����Դ��벿��
        /// </summary>
        public static string ConnDBF_Next = ";Exclusive=No;NULL=NO;Collate=Machine;BACKGROUNDFETCH=NO;DELETED=NO";

        /// <summary>
        /// ���ؽ����
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static DataSet ExecuteQuery(string connectionString, CommandType cmdType, string cmdText)
        {
            using (OdbcConnection conn = new OdbcConnection(connectionString))
            {
                using (OdbcCommand cmd = new OdbcCommand())
                {
                    PrepareCommand(cmd, conn, cmdType, cmdText);
                    using (OdbcDataAdapter da = new OdbcDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                        return ds;
                    }
                }
            }
        }

        /// <summary>
        /// ִ�� Transact-SQL ��䲢������Ӱ���������
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText)
        {
            OdbcCommand cmd = new OdbcCommand();
            using (OdbcConnection conn = new OdbcConnection(connectionString))
            {
                PrepareCommand(cmd, conn, cmdType, cmdText);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// ִ�� Transact-SQL ��䲢������Ӱ���������
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(OdbcConnection connection, CommandType cmdType, string cmdText)
        {
            OdbcCommand cmd = new OdbcCommand();
            PrepareCommand(cmd, connection, cmdType, cmdText);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// ִ�� Transact-SQL ��䲢���ؽ������
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static OdbcDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText)
        {
            OdbcCommand cmd = new OdbcCommand();
            OdbcConnection conn = new OdbcConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, cmdType, cmdText);
                OdbcDataReader odr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return odr;
            }
            catch (Exception exp)
            {
                conn.Close();
                Console.WriteLine("�쳣��" + exp.Message);
                throw;
            }
        }

        /// <summary>
        /// ִ�� Transact-SQL ��䲢���ؽ������
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static OdbcDataReader ExecuteReader(OdbcConnection connection, CommandType cmdType, string cmdText)
        {
            OdbcCommand cmd = new OdbcCommand();
            try
            {
                PrepareCommand(cmd, connection, cmdType, cmdText);
                OdbcDataReader odr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return odr;
            }
            catch (Exception exp)
            {
                connection.Close();
                Console.WriteLine("�쳣��" + exp.Message);
                throw;
            }
        }

        /// <summary>
        /// ��ָ�������ݿ������ַ���ִ��һ���������һ�����ݼ��ĵ�һ��
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText)
        {
            OdbcCommand cmd = new OdbcCommand();

            using (OdbcConnection conn = new OdbcConnection(connectionString))
            {
                PrepareCommand(cmd, conn, cmdType, cmdText);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// ��ָ�������ݿ������ַ���ִ��һ���������һ�����ݼ��ĵ�һ��
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static object ExecuteScalar(OdbcConnection connection, CommandType cmdType, string cmdText)
        {
            OdbcCommand cmd = new OdbcCommand();

            PrepareCommand(cmd, connection, cmdType, cmdText);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// ����Ҫִ�е�����
        /// </summary>
        private static void PrepareCommand(OdbcCommand cmd, OdbcConnection conn, CommandType cmdType, string cmdText)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            //cmd.CommandText = cmdText.Replace("?", "@").Replace(":", "@");
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
        }
    }
}
