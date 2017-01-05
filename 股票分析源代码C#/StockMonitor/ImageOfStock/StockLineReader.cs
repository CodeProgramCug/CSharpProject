using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageOfStock
{
    /*���ߵ�������Ϊ40��׼
     * �۰���������Ķ�ȡK�߷�Χ��ʽ
     * string codepath=AppDomain.CurrentDomain.BaseDirectory + @"history\SZNSE\day\000012.day";
     * ��ȡ��������
     * ��������
     * 
     * SDSDLR.GetAllLineCount(codepath).ToString()
     * long count = SDSDLR.GetDayToNowCount(DateTime.Parse("2010-07-01"), codepath);
     * long count = SDSDLR.GetDayToDayCount(DateTime.Parse("2010-07-01"),DateTime.Parse("2010-07-07"), codepath);
     * 
     * ���K��
     * ��������
     * SDSDLR.GetLine(AppDomain.CurrentDomain.BaseDirectory + @"history\SZNSE\day\000012.day");
     * SDSDLR.GetAreaLineToNow(DateTime.Parse("2010-07-01"), codepath);
     * SDSDLR.GetAreaLineToNow(DateTime.Parse("2010-07-01"),DateTime.Parse("2010-07-06"), codepath);
     * 
     * ָ�귽��
     * ��������
     * double my = SDSDLR.StockValueMA("CLOSE", 50, DateTime.Parse("1992-06-02"), codepath);
     * 
     *����Ӣ������Ƽ����޹�˾
     *������
     *�
     */

    /// <summary>
    /// ��Ʊ�����������
    /// </summary>
    /// <param name="DSI"></param>
    public delegate void StockInfoLayOut(DayStockInfo DSI,int process);
    /// <summary>
    /// ��Ʊ��������������
    /// </summary>
    /// <param name="DSI"></param>
    public delegate void StockInfoLayOutOver(DayStockInfo DSI);
    /// <summary>
    /// ��Ʊ������Ϣ
    /// </summary>
    public class DayStockInfo
    {
        /// <summary>
        /// ʱ��
        /// </summary>
        public DateTime day=new DateTime();
        /// <summary>
        /// ����
        /// </summary>
        public int OpenPrice = 0;
        /// <summary>
        /// ���
        /// </summary>
        public int HighestPrice = 0;
        /// <summary>
        /// ���
        /// </summary>
        public int LowestPrice = 0;
        /// <summary>
        /// ����
        /// </summary>
        public int ClosePrice=0;
        /// <summary>
        /// �ɽ����
        /// </summary>
        public int Amount = 0;
        /// <summary>
        /// �ɽ���
        /// </summary>
        public int TransCount = 0;

        public int Padding1 = 0;

        public int Padding2 = 0;
        
        public int Padding3 = 0;
    }
    /// <summary>
    /// �������
    /// </summary>
    public enum DAYLINETYPE
    {
        /// <summary>
        /// ����
        /// </summary>
       CLOSE=0,
        /// <summary>
        /// ����
        /// </summary>
        OPEN=1, 
        /// <summary>
        /// ��
        /// </summary>
        VOL=2,   
        /// <summary>
        /// �ܽ��
        /// </summary>
        AMOUNT=3, 
        /// <summary>
        /// ��߼۸�
        /// </summary>
        HIGH=4, 
        /// <summary>
        /// ��ͼ۸�
        /// </summary>
        LOW=5 
    }
}
