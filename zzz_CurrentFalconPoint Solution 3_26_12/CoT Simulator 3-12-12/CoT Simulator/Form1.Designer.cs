namespace CoT_Simulator
{
    partial class Form1
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TransmitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TB_IP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_port = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.BUT_StartTransmit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_UID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CB_loop = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.TransmitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(302, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.openToolStripMenuItem.Text = "Open CoT File";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.Open_File_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.Close_Click);
            // 
            // TransmitToolStripMenuItem
            // 
            this.TransmitToolStripMenuItem.Name = "TransmitToolStripMenuItem";
            this.TransmitToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.TransmitToolStripMenuItem.Text = "Transmit";
            this.TransmitToolStripMenuItem.Click += new System.EventHandler(this.TransmitToolStripMenuItem_Click);
            // 
            // TB_IP
            // 
            this.TB_IP.Location = new System.Drawing.Point(16, 52);
            this.TB_IP.Name = "TB_IP";
            this.TB_IP.Size = new System.Drawing.Size(184, 20);
            this.TB_IP.TabIndex = 4;
            this.TB_IP.Text = "192.168.1.103";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Connect to IP Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(203, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Port";
            // 
            // TB_port
            // 
            this.TB_port.Location = new System.Drawing.Point(206, 52);
            this.TB_port.Name = "TB_port";
            this.TB_port.Size = new System.Drawing.Size(73, 20);
            this.TB_port.TabIndex = 6;
            this.TB_port.Text = "3000";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // BUT_StartTransmit
            // 
            this.BUT_StartTransmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BUT_StartTransmit.Location = new System.Drawing.Point(16, 194);
            this.BUT_StartTransmit.Name = "BUT_StartTransmit";
            this.BUT_StartTransmit.Size = new System.Drawing.Size(263, 77);
            this.BUT_StartTransmit.TabIndex = 9;
            this.BUT_StartTransmit.Text = "Start Transmit";
            this.BUT_StartTransmit.UseVisualStyleBackColor = true;
            this.BUT_StartTransmit.Click += new System.EventHandler(this.BUT_StartTransmit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Unique ID\r\n";
            // 
            // TB_UID
            // 
            this.TB_UID.Location = new System.Drawing.Point(16, 102);
            this.TB_UID.Name = "TB_UID";
            this.TB_UID.Size = new System.Drawing.Size(153, 20);
            this.TB_UID.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(182, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "...";
            this.label4.Click += new System.EventHandler(this.random_num_button);
            // 
            // CB_loop
            // 
            this.CB_loop.AutoSize = true;
            this.CB_loop.Checked = true;
            this.CB_loop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_loop.Location = new System.Drawing.Point(16, 141);
            this.CB_loop.Name = "CB_loop";
            this.CB_loop.Size = new System.Drawing.Size(75, 17);
            this.CB_loop.TabIndex = 17;
            this.CB_loop.Text = "Loop File?";
            this.CB_loop.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 284);
            this.Controls.Add(this.CB_loop);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TB_UID);
            this.Controls.Add(this.BUT_StartTransmit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TB_port);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TB_IP);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "COT Simulator";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TransmitToolStripMenuItem;
        private System.Windows.Forms.TextBox TB_IP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_port;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button BUT_StartTransmit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TB_UID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox CB_loop;

    }
}

