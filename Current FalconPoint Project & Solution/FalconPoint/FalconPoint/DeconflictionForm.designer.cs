namespace FalconPoint
{
    partial class DeconflictionForm
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
            System.Windows.Forms.Button CancelButton;
            this.ApplyAllButton = new System.Windows.Forms.Button();
            this.m_OverlayList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ApplySelectedButton = new System.Windows.Forms.Button();
            CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            CancelButton.Location = new System.Drawing.Point(349, 191);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new System.Drawing.Size(75, 23);
            CancelButton.TabIndex = 3;
            CancelButton.Text = "Close";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ApplyAllButton
            // 
            this.ApplyAllButton.Location = new System.Drawing.Point(15, 191);
            this.ApplyAllButton.Name = "ApplyAllButton";
            this.ApplyAllButton.Size = new System.Drawing.Size(90, 23);
            this.ApplyAllButton.TabIndex = 0;
            this.ApplyAllButton.Text = "Apply All";
            this.ApplyAllButton.UseVisualStyleBackColor = true;
            this.ApplyAllButton.Click += new System.EventHandler(this.ApplyAllButton_Click);
            // 
            // m_OverlayList
            // 
            this.m_OverlayList.DisplayMember = "OverlayList";
            this.m_OverlayList.FormattingEnabled = true;
            this.m_OverlayList.Location = new System.Drawing.Point(12, 25);
            this.m_OverlayList.Name = "m_OverlayList";
            this.m_OverlayList.Size = new System.Drawing.Size(412, 160);
            this.m_OverlayList.TabIndex = 1;
            this.m_OverlayList.ValueMember = "OverlayHandles";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select overlays to apply preferences to:";
            // 
            // ApplySelectedButton
            // 
            this.ApplySelectedButton.Location = new System.Drawing.Point(111, 191);
            this.ApplySelectedButton.Name = "ApplySelectedButton";
            this.ApplySelectedButton.Size = new System.Drawing.Size(90, 23);
            this.ApplySelectedButton.TabIndex = 0;
            this.ApplySelectedButton.Text = "Apply Selected";
            this.ApplySelectedButton.UseVisualStyleBackColor = true;
            this.ApplySelectedButton.Click += new System.EventHandler(this.ApplySelectedButton_Click);
            // 
            // DeconflictionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 225);
            this.Controls.Add(CancelButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_OverlayList);
            this.Controls.Add(this.ApplySelectedButton);
            this.Controls.Add(this.ApplyAllButton);
            this.Name = "DeconflictionForm";
            this.Text = "DeconflictionForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ApplyAllButton;
        private System.Windows.Forms.ListBox m_OverlayList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ApplySelectedButton;
    }
}