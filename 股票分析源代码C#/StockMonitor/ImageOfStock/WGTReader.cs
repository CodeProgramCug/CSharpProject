using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace StockMonitor.DayLineReader
{
    /*
     *��ȡȨϢ����
     *����������StockMonitor.DayLineReader.WGTReader.ReadStockWeights(AppDomain.CurrentDomain.BaseDirectory + @"history/SHASE/weight/600030.wgt");
     *����Ӣ������Ƽ����޹�˾
     *������
     *�
     * ��ν��Ȩ���ǶԹɼۺͳɽ�������ȨϢ�޸�,���չ�Ʊ��ʵ���ǵ����ƹɼ�����ͼ,
     * ���ѳɽ�������Ϊ��ͬ�Ĺɱ��ھ�����Ʊ��Ȩ����Ϣ֮�󣬹ɼ���֮�����˱仯��
     * ��ʵ�ʳɱ���û�б仯��
     * �磺ԭ��20Ԫ�Ĺ�Ʊ��ʮ��ʮ֮��Ϊ10Ԫ����ʵ�ʻ����൱��20Ԫ��
     * ��K��ͼ�Ͽ������λ���ƺܵͣ����ܿ��ܾ���һ����ʷ��λ��
     * ����ĳ��Ʊ��Ȩǰ����ͨ��Ϊ5000��ɣ��۸�Ϊ10Ԫ���ɽ���Ϊ500��ɣ�
     * ������Ϊ10%��10��10֮���Ȩ����Ϊ5Ԫ����ͨ��Ϊ1�ڹɣ���Ȩ�����߳���Ȩ����
     * �������� 5.5Ԫ������10%���ɽ���Ϊ1000��ɣ�������Ҳ��10%(��ǰһ��������Ⱦ���ͬ���ĳɽ���ˮƽ)��
     * ��Ȩ�����ɼ�Ϊ11Ԫ�������ǰһ�յ�10Ԫ������10%���ɽ���Ϊ500��ɣ������ڹɼ�����ͼ����ʵ��ӳ�˹ɼ��ǵ���
     * ͬʱ�ɽ����ڳ�Ȩǰ��Ҳ���пɱ��ԡ�
     * 
     * ǰ��Ȩ����Ȩ��۸�[(��Ȩǰ�۸�-�ֽ����)����(��)�ɼ۸����ͨ�ɷݱ䶯����]��(1����ͨ�ɷݱ䶯����) 
     * ��Ȩ����Ȩ��۸񣽸�Ȩǰ�۸��(1����ͨ�ɷݱ䶯����)-��(��)�ɼ۸����ͨ�ɷݱ䶯�������ֽ����
     */
    /// <summary>
    /// ��ȡȨϢ������
    /// </summary>
    public class WGTReader
    {
        /// <summary>
        /// ��ȡȨϢ����
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="p_strMarket"></param>
         public static List<StockWeightInfo> ReadStockWeights(string strPath)
        {
            string[] parts = strPath.Split('\\');
            string strStockCode = null;
            for (int i = parts.Length - 1; i >= 0;i-- )
            {
                string strTemp = parts[i];
                if (strTemp.ToUpper().EndsWith(".WGT"))
                {
                    strStockCode = strTemp.Substring(0, strTemp.Length - 4) ;
                    break;
                }
            }

           //Console.WriteLine("Read stock weight from file '" + strPath + "'");
            FileStream stream = new FileStream(strPath, FileMode.Open, FileAccess.Read);
            BinaryReader b_reader = new BinaryReader(stream);
            List<StockWeightInfo> weightInfos = new List<StockWeightInfo>();
            try
            {
                while (stream.CanRead && stream.Position < stream.Length)
                {
                    int[] oneRow = new int[9];
                    for( int i=0;i<9;i++)
                    {
                        oneRow[i] = b_reader.ReadInt32();
                    }
                    if (oneRow[8] != 0)
                    {
                        throw new Exception("Last entry is not empty");
                    }
         
                    int nYear = oneRow[0] >> 20;
                    int nMon = (int)(((uint)(oneRow[0] << 12))>> 28);
                    int nDay = (oneRow[0] & 0xffff)>> 11;

                    DateTime wgtDate;
                    if (nYear == 0 && nMon == 0 && nDay == 0)
                        wgtDate = DateTime.MinValue;
                    else
                            wgtDate = new DateTime(nYear, nMon, nDay);
                    StockWeightInfo wgtInfo = new StockWeightInfo();
                    wgtInfo.m_date = wgtDate;
                    wgtInfo.m_stockCountAsGift = oneRow[1];/**////10000.0f;
                    wgtInfo.m_stockCountForSell = oneRow[2];/**////10000.0f;
                    wgtInfo.m_priceForSell = oneRow[3];/**////1000.0f;
                    wgtInfo.m_bonus = oneRow[4];/**////1000.0f;
                    wgtInfo.m_stockCountOfIncreasement = oneRow[5];/**////10000.0f;
                    wgtInfo.m_stockOwnership = (ulong)oneRow[6];
                    wgtInfo.m_freeStockCount = (ulong)oneRow[7];
                    if (!weightInfos.Contains(wgtInfo))
                    {
                        weightInfos.Add(wgtInfo);
                        //Console.WriteLine();
                        //Console.Write("ʱ��:" + wgtInfo.m_date.ToString() + ",��ͨ��:" + wgtInfo.m_freeStockCount + ",m_priceForSell:" + wgtInfo.m_priceForSell + ",�ֺ�:" + wgtInfo.m_bonus + ",�͹���:" + wgtInfo.m_stockCountAsGift + ",m_stockCountForSell:" + wgtInfo.m_stockCountForSell + ",ת����:" + wgtInfo.m_stockCountOfIncreasement + ",�ܹɱ�:" + wgtInfo.m_stockOwnership);//���Բ���
                    }
                }
                weightInfos.Sort();
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine("Unexpected end of stream");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
  
            }
            finally
            {
                stream.Close();
            }
            return weightInfos;
        }
        /// <summary>
        /// ��ȡ��Ӧ�����µ�ȨϢ����
        /// </summary>
        /// <param name="Dtm"></param>
        /// <param name="LSWI"></param>
        /// <returns></returns>
        public static StockWeightInfo GetWeightInfo(DateTime Dtm,List<StockWeightInfo> LSWI)
        {
            //2010-05-08

            //1985-10-29
            //1988-10-22
            //2005-10-10
            //2010-04-08
            //2010-10-10
            //2011-05-01
            StockWeightInfo RtInfo=new StockWeightInfo();
            foreach (StockWeightInfo SWI in LSWI)
            {
                if (Dtm >= SWI.m_date)//
                {
                    RtInfo = SWI;
                }
            }
            return RtInfo;
        }

    }
    /// <summary>
    /// ȨϢ����
    /// </summary>
    public class StockWeightInfo
    { 
        /// <summary>
        /// ʱ��
        /// </summary>
        public DateTime m_date = DateTime.Now;
        /// <summary>
        /// �͹��� (35000��ʾ���ն˾�Ϊ3.500)
        /// </summary>
        public int m_stockCountAsGift =0;
        /// <summary>
        /// �����
        /// </summary>
        public int m_stockCountForSell = 0;
        /// <summary>
        /// ��ɼ�
        /// </summary>
        public int m_priceForSell = 0;
        /// <summary>
        /// �ֺ� (37��ʾ���ն�Ϊ0.0370,1100->1.1000)
        /// </summary>
        public int m_bonus = 0;
        /// <summary>
        /// ת���� (100000��ʾ���ն�Ϊ10.000)
        /// </summary>
        public int m_stockCountOfIncreasement = 0;
        /// <summary>
        /// �ܹɱ� (��λΪ����)
        /// </summary>
        public ulong m_stockOwnership = 0;
        /// <summary>
        /// ��ͨ�� (��λΪ����)
        /// </summary>
        public ulong m_freeStockCount =0;
    }
}
