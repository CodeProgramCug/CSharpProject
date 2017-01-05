namespace StockMonitor.Forms
{
    partial class TestStockForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestStockForm));
            this.PriceVolPanel = new System.Windows.Forms.Panel();
            this.Test = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.DisplayPicPanel = new System.Windows.Forms.Panel();
            this.chartGraph1 = new ImageOfStock.ChartGraph();
            this.label1 = new System.Windows.Forms.Label();
            this.PriceVolPanel.SuspendLayout();
            this.DisplayPicPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // PriceVolPanel
            // 
            this.PriceVolPanel.BackColor = System.Drawing.Color.Black;
            this.PriceVolPanel.Controls.Add(this.Test);
            this.PriceVolPanel.Controls.Add(this.label2);
            this.PriceVolPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.PriceVolPanel.Location = new System.Drawing.Point(805, 0);
            this.PriceVolPanel.Name = "PriceVolPanel";
            this.PriceVolPanel.Size = new System.Drawing.Size(219, 570);
            this.PriceVolPanel.TabIndex = 3;
            // 
            // Test
            // 
            this.Test.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Test.Location = new System.Drawing.Point(6, 535);
            this.Test.Name = "Test";
            this.Test.Size = new System.Drawing.Size(75, 23);
            this.Test.TabIndex = 1;
            this.Test.Text = "Test";
            this.Test.UseVisualStyleBackColor = true;
            this.Test.Click += new System.EventHandler(this.Test_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(77, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "�۸�����";
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(802, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 570);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // DisplayPicPanel
            // 
            this.DisplayPicPanel.BackColor = System.Drawing.Color.Black;
            this.DisplayPicPanel.Controls.Add(this.chartGraph1);
            this.DisplayPicPanel.Controls.Add(this.label1);
            this.DisplayPicPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisplayPicPanel.Location = new System.Drawing.Point(0, 0);
            this.DisplayPicPanel.Name = "DisplayPicPanel";
            this.DisplayPicPanel.Size = new System.Drawing.Size(802, 570);
            this.DisplayPicPanel.TabIndex = 5;
            // 
            // chartGraph1
            // 
            this.chartGraph1.AxisSpace = 0;
            this.chartGraph1.CanDragSeries = false;
            this.chartGraph1.CrossOverIndex = 0;
            this.chartGraph1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartGraph1.FirstVisibleRecord = 0;
            this.chartGraph1.LastVisibleRecord = 0;
            this.chartGraph1.LeftPixSpace = 0;
            this.chartGraph1.Location = new System.Drawing.Point(0, 0);
            this.chartGraph1.Name = "chartGraph1";
            this.chartGraph1.ProcessBarValue = 0;
            this.chartGraph1.RightPixSpace = 0;
            this.chartGraph1.ScrollLeftStep = 1;
            this.chartGraph1.ScrollRightStep = 1;
            this.chartGraph1.ShowCrossHair = false;
            this.chartGraph1.ShowLeftScale = false;
            this.chartGraph1.ShowRightScale = false;
            this.chartGraph1.Size = new System.Drawing.Size(802, 570);
            this.chartGraph1.TabIndex = 7;
            this.chartGraph1.Text = "chartGraph2";
            this.chartGraph1.TimekeyField = null;
            this.chartGraph1.UseScrollAddSpeed = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(319, 181);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ͼ������";
            // 
            // TestStockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 570);
            this.Controls.Add(this.DisplayPicPanel);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.PriceVolPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TestStockForm";
            this.Text = "��Ӯ�Ƹ��ն�";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.PriceVolPanel.ResumeLayout(false);
            this.PriceVolPanel.PerformLayout();
            this.DisplayPicPanel.ResumeLayout(false);
            this.DisplayPicPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PriceVolPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel DisplayPicPanel;
        private System.Windows.Forms.Label label1;
        private ImageOfStock.ChartGraph chartGraph1;
        private System.Windows.Forms.Button Test;
    }
}