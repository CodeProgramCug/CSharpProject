using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
namespace StockMonitor
{

    ////0֤ȯ����
    ////1֤ȯ���
    ////2�������̼�
    ////3���տ��̼�
    ////4����ɽ���
    ////5�ɽ�����
    ////6�ɽ����
    ////7�ɽ�����
    ////8��߳ɽ���
    ////9��ͳɽ���
    ////10��ӯ��1
    ////11����λ��
    ////12��������
    ////13����λ��
    ////14��������
    ////15����λ��
    ////16��������
    ////17����λ��
    ////18��������
    ////19����λһ/������ʾ��
    ////20������һ
    ////21���λһ/�����ʾ��
    ////22������һ
    ////23���λ��
    ////24��������
    ////25���λ��
    ////26��������
    ////27���λ��
    ////28��������
    ////29���λ��
    ////30��������
    /*
     ί��

ί��--�����Ժ���һ��ʱ�������������ǿ�ȵ�ָ��,����㹫ʽΪ�� 
  ί�ȣ���(ί������-ί������)�£�ί��������ί������)����100% 

  ί���������������и���ί������������֮�������֮�ܺ͡� 

ί���������������и���ί������������֮�������֮�ܺ͡� 

  ί��ֵ�仯��ΧΪ��100%��-100%�� 

  ��ί��ֵΪ��ֵ����ί������,˵���г�����ǿ������ί��ֵΪ��ֵ���Ҹ�ֵ��,˵���г����̽�ǿ��ί��ֵ��-100%����100%,˵����������ǿ,�����𽥼�����һ�����̡��෴, �ӣ�100%��-100%,˵�������𽥼���,��������ǿ��һ�����̡� 

������

�������ʡ�Ҳ�ơ���ת�ʡ���ָ��һ��ʱ�����г��й�Ʊת��������Ƶ�ʣ��Ƿ�ӳ��Ʊ��ͨ��ǿ����ָ��֮һ������㹫ʽΪ�� 

  ��ת��(������)��(ĳһ��ʱ���ڵĳɽ���)/(�����ܹ���)x100% 

  ���磬ĳֻ��Ʊ��һ�����ڳɽ���2000��ɣ����ù�Ʊ���ܹɱ�Ϊ l�ڹɣ���ù�Ʊ������µĻ�����Ϊ20%�����ҹ�����Ʊ��Ϊ���ڶ����г���ͨ����ṫ�ڹɺͲ����ڶ����г���ͨ�Ĺ��ҹɺͷ��˹��������֣�һ��ֻ�Կ���ͨ���ֵĹ�Ʊ���㻻���ʣ��Ը���ʵ��׼ȷ�ط�ӳ����Ʊ����ͨ�ԡ������ּ��㷽ʽ����������ֻ��Ʊ����ͨ�ɱ����Ϊ200O�����任���ʸߴ�100%���ڹ��⣬ͨ������ĳһ��ʱ�ڵĳɽ������ĳһʱ���ϵ���ֵ֮��ı�ֵ��������ת�ʡ� 

  �����ʵĸߵ�������ζ��������������� 

  (l)��Ʊ�Ļ�����Խ�ߣ���ζ�Ÿ�ֻ��Ʊ�Ľ�ͶԽ��Ծ�����ǹ����ֻ��Ʊ����ԸԽ�ߣ��������Źɣ���֮����Ʊ�Ļ�����Խ�ͣ��������ֻ��Ʊ���˹�ע���������Źɡ� 

  (2)�����ʸ�һ����ζ�Ź�Ʊ��ͨ�Ժã������г��Ƚ����ף�������������򲻵������������������󣬾��н�ǿ�ı���������Ȼ��ֵ��ע����ǣ������ʽϸߵĹ�Ʊ������Ҳ�Ƕ����ʽ�׷��Ķ���Ͷ���Խ�ǿ���ɼ�����ϴ󣬷���Ҳ��Խϴ� 

  (3)����������ɼ��������ϣ����Զ�δ���Ĺɼ�����һ����Ԥ����жϡ�ĳֻ��Ʊ�Ļ�����ͻȻ�������ɽ����Ŵ󣬿�����ζ����Ͷ�����ڴ���������ɼۿ��ܻ���֮������ĳֻ��Ʊ����������һ��ʱ�ں󣬻�������Ѹ�������������������һЩ������Ҫ���֣��ɼۿ��ܻ��µ��� 

  һ����ԣ������г��Ļ�����Ҫ���ڳ����г��Ļ����ʡ������ԭ�����������г���ģ���ſ죬�����й�Ʊ�϶࣬�ټ���Ͷ����Ͷ�����ǿ��ʹ�����г���Ͷ�ϻ�Ծ�������ʵĸߵͻ�ȡ�������¼���������أ� 

  (l)���׷�ʽ��֤ȯ�г��Ľ��׷�ʽ�������˿�ͷ�������ϰ徺�ۡ�΢����ϡ����͵��Լ��д�ϵȴ��˹������Եĸ����׶Ρ����ż����ֶε�����������������ܵ�����ǿ���г�����������Ǳ���õ�������չ��������Ҳ��֮�нϴ���ߡ� 

  (2)�����ڡ�һ����ԣ�������Խ�̣�������Խ�ߡ� 

  (3)Ͷ���߽ṹ���Ը���Ͷ����Ϊ�����֤ȯ�г��������������ϸߣ��Ի���Ȼ���Ͷ����Ϊ�����֤ȯ�г�����������Խϵ͡� 

  ���������Ҫ֤ȯ�г��Ļ����ʸ�����ͬ�������Զ�����֮�£��й����еĻ�����λ�ڸ���ǰ�С� 

����

����--�Ǻ�����Գɽ�����ָ�ꡣ���ǿ��к�ÿ���ӵ�ƽ���ɽ������ȥ5��������ÿ����ƽ���ɽ���֮�ȡ� 

  ����㹫ʽΪ�����ȣ��ֳɽ�����/��(��ȥ5��������ƽ��ÿ���ӳɽ���)�������ۼƿ���ʱ��(��)�� 

�����ȴ���1ʱ,˵������ÿ���ӵ�ƽ���ɽ������ڹ�ȥ5�յ�ƽ��ֵ,���ױȹ�ȥ5�ջ𱬣�������С��1ʱ,˵�����ճɽ���С�ڹ�ȥ5�յ�ƽ��ˮƽ�� 
����=(���켴ʱ�ɽ���/����������ۼ�N����)/(ǰ�����ܳ��� /1200����) 
���

�����ָ���̺���߼ۡ���ͼ�֮��ľ���ֵ��ɼ۵İٷֱȡ�



���֪ʶ:���̡�����

���̣�������۳ɽ��Ľ��ף�����ɽ�����ͳ�Ƽ������̡�

 
���̣��������۳ɽ��Ľ��ס�������ͳ�Ƽ������̡����̣��������������� ������������ж�����������ǿ���������������������̣��������������ǿ���� ������������������˵������������ǿ�� 

ͨ�����̡����������Ĵ�С�ͱ�����Ͷ����ͨ�����ܷ��������Ե����̶໹�������Ե����̶࣬���ںܶ�ʱ����Է���ׯ�Ҷ�����һ������Ч�Ķ���ָ�ꡣ 

��Ͷ������ʹ�����̺�����ʱ��Ҫע���Ϲɼ��ڵ�λ����λ�͸�λ�ĳɽ�����Լ��ùɵ��ܳɽ����������Ϊ���̡����̵�����������������ʱ�䶼��Ч�������ʱ�����̴󣬹ɼ۲���һ�����ǣ����̴󣬹ɼ�Ҳ����һ���µ��� 

ׯ�ҿ����������̡����̵�������������ƭ���ڴ�����ʵ���У����Ƿ������������ 

1���ɼ۾����˽ϳ�ʱ��������µ����ɼ۴��ڽϵͼ�λ���ɽ�������ή�����˺󣬳ɽ����ºͷ��������������������ӣ����������������ɼ۽��������ǣ���������Ͽɿ��� 

2���ڹɼ۾����˽ϳ�ʱ����������ǣ��ɼ۴��ڽϸ߼�λ���ɽ����޴󣬲������ټ������ӣ��������������Ŵ󣬴��������������ɼ۽����ܼ����µ��� 

3���ڹɼ����������У�ʱ���ᷢ�����̴�����С������������������ɼ�һ�������ǡ���Ϊ��Щʱ��ׯ���ü����׵����ɼ۴����ϵ�λ�ã�Ȼ������1����2�����������Լ����Լ�����������ɹɼ���ʱ���̻�С����������ʱ�����̽����Դ������̣�ʹͶ������Ϊׯ���ڳԻ������׷����룬������չɼۼ����µ��� 

4���ڹɼ����ǹ����У�ʱ���ᷢ�����̴�����С���������������ʾ�ɼ�һ�����µ�����Ϊ��Щʱ��ׯ���ü����򵥽��ɼ�����һ����Եĸ�λ��Ȼ���ڹɼ�С��������1����2���򵥣�һЩ����Ϊ�ɼۻ��µ����׷��Խ����������Ʊ����ׯ�ҷֲ��ҵ������׵�ͨͨ���ߡ����������ߺ��λ���򵥵��ַ���������ʾ���̴�����С���ﵽ��ƭͶ���ߵ�Ŀ�ģ�����������Ѹ�ټ����Ƹ߹ɼۡ� 

5���ɼ��������˽ϴ���Ƿ�����ĳ�����̴������ӣ����ɼ�ȴ���ǣ�Ͷ����Ҫ����ׯ���������׼�������� 

6�����ɼ����µ��˽ϴ�ķ��ȣ���ĳ�����̴������ӣ����ɼ�ȴ������Ͷ����Ҫ����ׯ��������󣬼ٴ�ѹ��Ի�. 

     */
    public partial class StockPriceList : UserControl
    {
        public StockPriceList()
        {
            InitializeComponent();
        }
        public string GetValueURL = "http://www.iwind.com.cn/iwind/RealTimeList/GetNowPrice.ashx?stockcode=";
        public List<StockMonitor.DayLineReader.StockWeightInfo> lsdalrswi = null;
        private void StockPriceList_Load(object sender, EventArgs e)
        {
            //string WgtPath = AppDomain.CurrentDomain.BaseDirectory + @"history/SHASE/weight/600030.wgt";
            //List<StockMonitor.DayLineReader.StockWeightInfo> LS = StockMonitor.DayLineReader.WGTReader.ReadStockWeights(WgtPath);
        }
        private void BTDDBL_Click(object sender, EventArgs e)
        {
         
        }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string WebBack(string stockcode)
        {
            try
            {
                string url = GetValueURL + stockcode;
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.Method = "GET";
                req.KeepAlive = true;
                using (WebResponse wr = req.GetResponse())
                {
                   
                      StreamReader reader = new StreamReader(wr.GetResponseStream(), Encoding.UTF8);
                string result = reader.ReadToEnd();
                return result;
                }
            }
            catch (Exception expp)
            {
                return expp.Message;
            }
        }
        public void ReadOnePanel(string stockcode)
        {
                   string[] RtStr=WebBack(stockcode).Split(new string[]{","},StringSplitOptions.RemoveEmptyEntries);
                   labelStockCode.Text = RtStr[0];////0֤ȯ����
                   label1StockCode.Text = RtStr[1];////1֤ȯ���
                  //NowPrice_Label.Text = RtStr[2];////2�������̼�
                  //NowPrice_Label.Text = RtStr[3];////3���տ��̼�
                  //NowPrice_Label.Text = RtStr[4];////4����ɽ���
                  //NowPrice_Label.Text = RtStr[5];////5�ɽ�����
                  //NowPrice_Label.Text = RtStr[6];////6�ɽ����
                  //NowPrice_Label.Text = RtStr[7];////7�ɽ�����
                  //NowPrice_Label.Text = RtStr[8];////8��߳ɽ���
                  //NowPrice_Label.Text = RtStr[9];////9��ͳɽ���
                  //NowPrice_Label.Text = RtStr[10];////10��ӯ��

                  SellValue5_label.Text = double.Parse(RtStr[11]).ToString("#0.00");////11����λ��
                  SellValuePlus5_label.Text = (double.Parse(RtStr[12]) / 100).ToString();////12��������
                  SellValue4_label.Text =double.Parse(RtStr[13]).ToString("#0.00");////13����λ��
                  SellValuePlus4_label.Text = (double.Parse(RtStr[14]) / 100).ToString();////14��������
                  SellValue3_label.Text =double.Parse( RtStr[15]).ToString("#0.00");////15����λ��
                  SellValuePlus3_label.Text = (double.Parse(RtStr[16]) / 100).ToString();////16��������
                  SellValue2_label.Text = double.Parse(RtStr[17]).ToString("#0.00");////17����λ��
                  SellValuePlus2_label.Text = (double.Parse(RtStr[18]) / 100).ToString();////18��������
                  SellValue1_label.Text = double.Parse(RtStr[19]).ToString("#0.00");////19����λһ/������ʾ��
                  SellValuePlus1_label.Text = (double.Parse(RtStr[20]) / 100).ToString();////20������һ

                  BuyValue1_label.Text = double.Parse(RtStr[21]).ToString("#0.00");////21���λһ/�����ʾ��
                  BuyValuePlus1_label.Text = (double.Parse(RtStr[22]) / 100).ToString();////22������һ
                  BuyValue2_label.Text = double.Parse(RtStr[23]).ToString("#0.00");////23���λ��
                  BuyValuePlus2_label.Text = (double.Parse(RtStr[24]) / 100).ToString();////24��������
                  BuyValue3_label.Text = double.Parse(RtStr[25]).ToString("#0.00");////25���λ��
                  BuyValuePlus3_label.Text = (double.Parse(RtStr[26]) / 100).ToString();////26��������
                  BuyValue4_label.Text = double.Parse(RtStr[27]).ToString("#0.00");////27���λ��
                  BuyValuePlus4_label.Text = (double.Parse(RtStr[28]) / 100).ToString();////28��������
                  BuyValue5_label.Text = double.Parse(RtStr[29]).ToString("#0.00");////29���λ��
                  BuyValuePlus5_label.Text = (double.Parse(RtStr[30]) / 100).ToString();////30��������
                  #region ����
                  NowPrice_Label.Text = double.Parse(RtStr[4]).ToString("#0.00");//�ɽ�
                  double ZDValue=double.Parse(RtStr[4]) - double.Parse(RtStr[2]);
                  ZhangDie_Lable.Text = ZDValue.ToString("#0.00");//�ǵ�
                  labelZF.Text = (ZDValue * 100 / double.Parse(RtStr[2])).ToString("#0.00") + "%";
                  labelLow.Text = double.Parse(RtStr[9]).ToString("#0.00");////9��ͳɽ���
                  labelHigh.Text = double.Parse(RtStr[8]).ToString("#0.00");////8��߳ɽ���
                  labelOpen.Text = double.Parse(RtStr[3]).ToString("#0.00");////3���տ��̼�
                  labelShiYing.Text = double.Parse(RtStr[10]).ToString("#0.00");////10��ӯ��
                  labelJinE.Text = (double.Parse(RtStr[6]) / 10000).ToString("#0");////6�ɽ����
                  labelvol.Text = (int.Parse(RtStr[5]) / 100).ToString();////5�ɽ�����
                  #region ͣ��
                  double nv=double.Parse(RtStr[3]);
                  double tb = nv * 0.1;
                  string ztjg = (tb + nv).ToString("#0.000");
                   string dtjg=(nv - tb).ToString("#0.000");
                   labelUpToStop.Text = ztjg.Substring(0, ztjg.Length - 1);
                   labelDownToStop.Text = dtjg.Substring(0, dtjg.Length - 1);
                  #endregion
                   #region ί�б���
                   int SellVol = 0;
                   SellVol+=int.Parse(RtStr[12]);////12��������
                   SellVol+= int.Parse(RtStr[14]);////14��������
                   SellVol+= int.Parse(RtStr[16]);////16��������
                   SellVol+= int.Parse(RtStr[18]);////18��������
                   SellVol+= int.Parse(RtStr[20]);////20������һ   
                   int BuyVol = 0;
                   BuyVol+=int.Parse(RtStr[22]);////22������һ
                   BuyVol += int.Parse(RtStr[24]);////24��������
                   BuyVol += int.Parse(RtStr[26]);////26��������
                   BuyVol += int.Parse(RtStr[28]);////28��������
                   BuyVol += int.Parse(RtStr[30]);////30��������
                   labelWTBL.Text = ((((double)BuyVol - (double)SellVol) / ((double)BuyVol + (double)SellVol)) * 100).ToString("#0.00")+"%";
                   Ave_Label.Text = (BuyVol + SellVol) / 1000;//����
                   #endregion
                   #region ������
                     StockMonitor.DayLineReader.StockWeightInfo MyIF=  StockMonitor.DayLineReader.WGTReader.GetWeightInfo(DateTime.Now, lsdalrswi);
                     double LTG =double.Parse(MyIF.m_freeStockCount.ToString());
                     double HSV = double.Parse(RtStr[5])/100 / LTG;
                     HSV_Lable.Text = HSV.ToString("#0.00")+"%";
                   #endregion




                  #endregion
               }
    }
}
