using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WYStockRealView
{
    public partial class MoreKLineForm : Form
    {
        public MoreKLineForm()
        {
            InitializeComponent();
            ControlsOverride();
        }
        /// <summary>
        /// �ָ�����ʾ��
        /// </summary>
        ToolTip FGFTSK = new ToolTip();
        /// <summary>
        /// �ָ������ص�
        /// </summary>
        Label FGXTSD = new Label();
        public void ControlsOverride()
        {
            FGXTSD.Text = ">>";
            FGXTSD.Height = 14;
            FGXTSD.Width = 14;
            FGXTSD.AutoSize = true;
            FGXTSD.Location = new Point(-6, splitter1.Height / 2 - FGXTSD.Height);
            FGXTSD.Font = new System.Drawing.Font("Marlett", 9, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)2);
            FGXTSD.ForeColor = Color.Blue;
            FGXTSD.Cursor = Cursors.Hand;
            FGFTSK.SetToolTip(FGXTSD, "�����ұ�����");
            splitter1.Controls.Add(FGXTSD);
            FGXTSD.Click += new EventHandler(ShowHide);
            splitter1.Resize += new EventHandler(splitter_Resize);
        }
        private void ShowHide(object sender, EventArgs e)
        {
           
            if (FGXTSD.Text == ">>")
            {
                FGXTSD.Text = "<<";
                FGFTSK.SetToolTip(FGXTSD, "��ʾ�ұ�����");
                PriceVolPanel.Visible = false;
         MessageBox.Show(DisplayPicPanel.Width.ToString());
            }
            else
            {
                FGXTSD.Text = ">>";
                FGFTSK.SetToolTip(FGXTSD, "�����ұ�����");
                PriceVolPanel.Visible = true;
                MessageBox.Show(DisplayPicPanel.Width.ToString());
            }
        }
        private void splitter_Resize(object o, EventArgs e)
        {
            FGXTSD.Location = new Point(-6, splitter1.Height / 2 - FGXTSD.Height);
        }

        private void RealTimeForm_Load(object sender, EventArgs e)
        {

        }
    }
}