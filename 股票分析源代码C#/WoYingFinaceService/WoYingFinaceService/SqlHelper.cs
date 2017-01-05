using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace WoYingFinaceService
{
    

    /// <summary>
    /// SqlHelper����ר���ṩ������û����ڸ����ܡ��������������ϰ��sql���ݲ���
    /// </summary>
    public abstract class SqlHelper
    {
        /// <summary>
        /// ���ݿ������ַ���(Ǯ��)
        /// </summary>
        public static string SqlConnStringZX = "server=(local);database=iwinddatabase;uid=sa;pwd=1;";
       


        /// <summary>
        /// ����߼����ĵ�·��
        /// </summary>
        public static string MessageFilePath = AppDomain.CurrentDomain.BaseDirectory;
        // ���ڻ��������HASH��
        // public static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());
        /// <summary>
        /// ����һ�����ݱ�(Fill)1
        /// </summary>
        /// <param name="connectionString">�����ַ���</param>
        /// <param name="cmdType">����</param>
        /// <param name="cmdText">����</param>
        /// <param name="TableName">DataTable����</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectTable(string connectionString, CommandType cmdType, string cmdText, string TableName)
        {

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter dpt = new SqlDataAdapter();
            using (SqlConnection Conn = new SqlConnection(connectionString))
            {
                cmd.CommandType = cmdType;
                cmd.CommandText = cmdText;
                dpt.SelectCommand = cmd;
                DataSet DsResoult = new DataSet();
                dpt.Fill(DsResoult, TableName);
                dpt.Dispose();
                Conn.Close();
                return DsResoult.Tables[TableName];
            }
        }
        /// <summary>
        /// ����һ�����ݱ�(Fill)2
        /// </summary>
        /// <param name="connectionString">�����ַ���</param>
        /// <param name="cmdType">����</param>
        /// <param name="cmdText">����</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectTable(string connectionString, CommandType cmdType, string cmdText)
        {

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter dpt = new SqlDataAdapter();
            using (SqlConnection Conn = new SqlConnection(connectionString))
            {
                cmd.CommandType = cmdType;
                cmd.CommandText = cmdText;
                dpt.SelectCommand = cmd;
                cmd.Connection = Conn;
                DataSet DsResoult = new DataSet();
                dpt.Fill(DsResoult, "TempTable");
                dpt.Dispose();
                Conn.Close();
                return DsResoult.Tables["TempTable"];
            }
        }
        /// <summary>
        /// ����һ�����ݱ�(Fill)3
        /// </summary>
        /// <param name="connectionString">�����ַ���</param>
        /// <param name="cmdType">����</param>
        /// <param name="cmdText">����</param>
        /// <param name="commandParameters">ִ���������ò����ļ���</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectTable(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter dpt = new SqlDataAdapter();
            using (SqlConnection Conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, Conn, null, cmdType, cmdText, commandParameters);
                cmd.CommandType = cmdType;
                cmd.CommandText = cmdText;
                dpt.SelectCommand = cmd;
                DataSet DsResoult = new DataSet();
                dpt.Fill(DsResoult, "TableName");
                cmd.Parameters.Clear();
                dpt.Dispose();
                Conn.Close();
                return DsResoult.Tables["TableName"];
            }
        }
        /// <summary>
        /// ����һ�����ݱ�(Fill)4
        /// </summary>
        /// <param name="connectionString">�����ַ���</param>
        /// <param name="cmdType">����</param>
        /// <param name="cmdText">����</param>
        /// <param name="commandParameters">ִ���������ò����ļ���</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectTable(string connectionString, CommandType cmdType, string cmdText, string TableName, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter dpt = new SqlDataAdapter();
            using (SqlConnection Conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, Conn, null, cmdType, cmdText, commandParameters);
                cmd.CommandType = cmdType;
                cmd.CommandText = cmdText;
                dpt.SelectCommand = cmd;
                DataSet DsResoult = new DataSet();
                dpt.Fill(DsResoult, TableName);
                cmd.Parameters.Clear();
                dpt.Dispose();
                Conn.Close();
                return DsResoult.Tables[TableName];
            }
        }
        /// <summary>
        /// ����һ�����ݱ�(Fill)4
        /// </summary>
        /// <param name="connectionString">�����ַ���</param>
        /// <param name="cmdType">����</param>
        /// <param name="cmdText">����</param>
        /// <param name="commandParameters">ִ���������ò����ļ���</param>
        /// <returns>DataTable</returns>
        public static DataSet SelectSet(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter dpt = new SqlDataAdapter();
            using (SqlConnection Conn = new SqlConnection(connectionString))
            {


                PrepareCommand(cmd, Conn, null, cmdType, cmdText, commandParameters);
                cmd.CommandType = cmdType;
                cmd.CommandText = cmdText;
                dpt.SelectCommand = cmd;
                DataSet DsResoult = new DataSet();
                dpt.Fill(DsResoult, "TableName");
                cmd.Parameters.Clear();
                dpt.Dispose();
                Conn.Close();
                return DsResoult;
            }
        }
        /// <summary>
        ///  �������ӵ����ݿ��ü������ִ��һ��sql������������ݼ���
        /// </summary>
        /// <param name="connectionString">һ����Ч�������ַ���</param>
        /// <param name="commandType">��������(�洢����, �ı�, �ȵ�)</param>
        /// <param name="commandText">�洢�������ƻ���sql�������</param>
        /// <param name="commandParameters">ִ���������ò����ļ���</param>
        /// <returns>ִ��������Ӱ�������</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// �����е����ݿ�����ִ��һ��sql������������ݼ���
        /// </summary>
        /// <param name="conn">һ�����е����ݿ�����</param>
        /// <param name="commandType">��������(�洢����, �ı�, �ȵ�)</param>
        /// <param name="commandText">�洢�������ƻ���sql�������</param>
        /// <param name="commandParameters">ִ���������ò����ļ���</param>
        /// <returns>ִ��������Ӱ�������</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        ///ʹ�����е�SQL����ִ��һ��sql������������ݼ���
        /// </summary>
        /// <remarks>
        ///����:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">һ�����е�����</param>
        /// <param name="commandType">��������(�洢����, �ı�, �ȵ�)</param>
        /// <param name="commandText">�洢�������ƻ���sql�������</param>
        /// <param name="commandParameters">ִ���������ò����ļ���</param>
        /// <returns>ִ��������Ӱ�������</returns>
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// ��ִ�е����ݿ�����ִ��һ���������ݼ���sql����
        /// </summary>
        /// <remarks>
        /// ����:  
        ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">һ����Ч�������ַ���</param>
        /// <param name="commandType">��������(�洢����, �ı�, �ȵ�)</param>
        /// <param name="commandText">�洢�������ƻ���sql�������</param>
        /// <param name="commandParameters">ִ���������ò����ļ���</param>
        /// <returns>��������Ķ�ȡ��</returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            //����һ��SqlCommand����
            SqlCommand cmd = new SqlCommand();
            //����һ��SqlConnection����
            SqlConnection conn = new SqlConnection(connectionString);

            //������������һ��try/catch�ṹִ��sql�ı�����/�洢���̣���Ϊ��������������һ���쳣����Ҫ�ر����ӣ���Ϊû�ж�ȡ�����ڣ�
            //���commandBehaviour.CloseConnection �Ͳ���ִ��
            try
            {
                //���� PrepareCommand �������� SqlCommand �������ò���
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                //���� SqlCommand  �� ExecuteReader ����
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //�������
                cmd.Parameters.Clear();
                return reader;
            }
            catch
            {
                //�ر����ӣ��׳��쳣
                conn.Close();
                throw;
            }
        }
        /// <summary>
        /// ��ִ�е����ݿ�����ִ��һ���������ݼ���sql����
        /// </summary>
        /// <remarks>
        /// ����:  
        ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">һ����Ч������</param>
        /// <param name="commandType">��������(�洢����, �ı�, �ȵ�)</param>
        /// <param name="commandText">�洢�������ƻ���sql�������</param>
        /// <param name="commandParameters">ִ���������ò����ļ���</param>
        /// <returns>��������Ķ�ȡ��</returns>
        public static SqlDataReader ExecuteReader(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            //����һ��SqlCommand����
            SqlCommand cmd = new SqlCommand();
            //����һ��SqlConnection����


            //������������һ��try/catch�ṹִ��sql�ı�����/�洢���̣���Ϊ��������������һ���쳣����Ҫ�ر����ӣ���Ϊû�ж�ȡ�����ڣ�
            //���commandBehaviour.CloseConnection �Ͳ���ִ��
            try
            {
                //���� PrepareCommand �������� SqlCommand �������ò���
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                //���� SqlCommand  �� ExecuteReader ����
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //�������
                cmd.Parameters.Clear();
                return reader;
            }
            catch
            {
                //�ر����ӣ��׳��쳣
                conn.Close();
                throw;
            }


        }
        /// <summary>
        /// ��ִ�е����ݿ�����ִ��һ���������ݼ���sql����
        /// </summary>
        /// <remarks>
        /// ����:  
        ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">һ����Ч�������ַ���</param>
        /// <param name="commandType">��������(�洢����, �ı�, �ȵ�)</param>
        /// <param name="commandText">�洢�������ƻ���sql�������</param>
        /// <param name="commandParameters">ִ���������ò����ļ���</param>
        /// <returns>��������Ķ�ȡ��</returns>
        public static SqlDataReader ExecuteReader(SqlTransaction Tran, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            //����һ��SqlCommand����
            SqlCommand cmd = new SqlCommand();
            //����һ��SqlConnection����
            SqlConnection conn = Tran.Connection;

            //������������һ��try/catch�ṹִ��sql�ı�����/�洢���̣���Ϊ��������������һ���쳣����Ҫ�ر����ӣ���Ϊû�ж�ȡ�����ڣ�
            //���commandBehaviour.CloseConnection �Ͳ���ִ��
            try
            {
                //���� PrepareCommand �������� SqlCommand �������ò���
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                //���� SqlCommand  �� ExecuteReader ����
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //�������
                cmd.Parameters.Clear();
                return reader;
            }
            catch
            {
                //�ر����ӣ��׳��쳣
                conn.Close();
                throw;
            }
        }
        /// <summary>
        /// ��ָ�������ݿ������ַ���ִ��һ���������һ�����ݼ��ĵ�һ��
        /// </summary>
        /// <remarks>
        ///����:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        ///<param name="connectionString">һ����Ч�������ַ���</param>
        /// <param name="commandType">��������(�洢����, �ı�, �ȵ�)</param>
        /// <param name="commandText">�洢�������ƻ���sql�������</param>
        /// <param name="commandParameters">ִ���������ò����ļ���</param>
        /// <returns>�� Convert.To{Type}������ת��Ϊ��Ҫ�� </returns>
        public static object ExecuteScalar(SqlTransaction Tran, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = Tran.Connection)
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }
        /// <summary>
        /// ��ָ�������ݿ������ַ���ִ��һ���������һ�����ݼ��ĵ�һ��
        /// </summary>
        /// <remarks>
        ///����:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        ///<param name="connectionString">һ����Ч�������ַ���</param>
        /// <param name="commandType">��������(�洢����, �ı�, �ȵ�)</param>
        /// <param name="commandText">�洢�������ƻ���sql�������</param>
        /// <param name="commandParameters">ִ���������ò����ļ���</param>
        /// <returns>�� Convert.To{Type}������ת��Ϊ��Ҫ�� </returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// ��ָ�������ݿ�����ִ��һ���������һ�����ݼ��ĵ�һ��
        /// </summary>
        /// <remarks>
        /// ����:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">һ�����ڵ����ݿ�����</param>
        /// <param name="commandType">��������(�洢����, �ı�, �ȵ�)</param>
        /// <param name="commandText">�洢�������ƻ���sql�������</param>
        /// <param name="commandParameters">ִ���������ò����ļ���</param>
        /// <returns>�� Convert.To{Type}������ת��Ϊ��Ҫ�� </returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }


        /// <summary>
        /// ׼��ִ��һ������
        /// </summary>
        /// <param name="cmd">sql����</param>
        /// <param name="conn">Sql����</param>
        /// <param name="trans">Sql����</param>
        /// <param name="cmdType">������������ �洢���̻����ı�</param>
        /// <param name="cmdText">�����ı�,���磺Select * from Products</param>
        /// <param name="cmdParms">ִ������Ĳ���</param>
        public static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
    }


}
