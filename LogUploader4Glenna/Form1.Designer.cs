namespace LogUploader4Glenna
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBoxLogOrdner = new System.Windows.Forms.TextBox();
            this.txtBoxZeitraum = new System.Windows.Forms.TextBox();
            this.txtBoxUploadedLogs = new System.Windows.Forms.TextBox();
            this.btnUploadOneThread = new System.Windows.Forms.Button();
            this.btnUploadMultipleThreads = new System.Windows.Forms.Button();
            this.btnWatchDirectory = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.combBoxContent = new System.Windows.Forms.ComboBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker4 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker5 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker6 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Log Ordner";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Zeitraum";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 372);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Uploadstatus";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(105, 372);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "%";
            // 
            // txtBoxLogOrdner
            // 
            this.txtBoxLogOrdner.Location = new System.Drawing.Point(79, 12);
            this.txtBoxLogOrdner.Name = "txtBoxLogOrdner";
            this.txtBoxLogOrdner.Size = new System.Drawing.Size(292, 20);
            this.txtBoxLogOrdner.TabIndex = 4;
            // 
            // txtBoxZeitraum
            // 
            this.txtBoxZeitraum.Location = new System.Drawing.Point(79, 38);
            this.txtBoxZeitraum.Name = "txtBoxZeitraum";
            this.txtBoxZeitraum.Size = new System.Drawing.Size(42, 20);
            this.txtBoxZeitraum.TabIndex = 5;
            this.txtBoxZeitraum.Text = "0";
            this.txtBoxZeitraum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBoxZeitraum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxZeitraum_KeyPress);
            // 
            // txtBoxUploadedLogs
            // 
            this.txtBoxUploadedLogs.Location = new System.Drawing.Point(16, 64);
            this.txtBoxUploadedLogs.Multiline = true;
            this.txtBoxUploadedLogs.Name = "txtBoxUploadedLogs";
            this.txtBoxUploadedLogs.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtBoxUploadedLogs.Size = new System.Drawing.Size(509, 298);
            this.txtBoxUploadedLogs.TabIndex = 6;
            // 
            // btnUploadOneThread
            // 
            this.btnUploadOneThread.Location = new System.Drawing.Point(528, 8);
            this.btnUploadOneThread.Name = "btnUploadOneThread";
            this.btnUploadOneThread.Size = new System.Drawing.Size(179, 23);
            this.btnUploadOneThread.TabIndex = 7;
            this.btnUploadOneThread.Text = "Lade Logs hoch 1 Thread";
            this.btnUploadOneThread.UseVisualStyleBackColor = true;
            this.btnUploadOneThread.Click += new System.EventHandler(this.btnUploadOneThread_Click);
            // 
            // btnUploadMultipleThreads
            // 
            this.btnUploadMultipleThreads.Location = new System.Drawing.Point(528, 41);
            this.btnUploadMultipleThreads.Name = "btnUploadMultipleThreads";
            this.btnUploadMultipleThreads.Size = new System.Drawing.Size(179, 23);
            this.btnUploadMultipleThreads.TabIndex = 8;
            this.btnUploadMultipleThreads.Text = "Lade Logs hoch mehrere Threads";
            this.btnUploadMultipleThreads.UseVisualStyleBackColor = true;
            this.btnUploadMultipleThreads.Click += new System.EventHandler(this.btnUploadMultipleThreads_Click);
            // 
            // btnWatchDirectory
            // 
            this.btnWatchDirectory.Location = new System.Drawing.Point(377, 10);
            this.btnWatchDirectory.Name = "btnWatchDirectory";
            this.btnWatchDirectory.Size = new System.Drawing.Size(121, 23);
            this.btnWatchDirectory.TabIndex = 9;
            this.btnWatchDirectory.Text = "Starte Überwachung";
            this.btnWatchDirectory.UseVisualStyleBackColor = true;
            this.btnWatchDirectory.Click += new System.EventHandler(this.btnWatchDirectory_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(178, 17);
            this.radioButton1.TabIndex = 10;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Lade keine Logs mehrfach hoch";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 43);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(123, 17);
            this.radioButton2.TabIndex = 11;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Lade Logs neu hoch";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Location = new System.Drawing.Point(528, 224);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(193, 100);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Uploadeinstellung";
            // 
            // combBoxContent
            // 
            this.combBoxContent.FormattingEnabled = true;
            this.combBoxContent.Items.AddRange(new object[] {
            "PvE Raidbosse",
            "WvW"});
            this.combBoxContent.Location = new System.Drawing.Point(534, 168);
            this.combBoxContent.Name = "combBoxContent";
            this.combBoxContent.Size = new System.Drawing.Size(173, 21);
            this.combBoxContent.TabIndex = 13;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // backgroundWorker3
            // 
            this.backgroundWorker3.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker3_DoWork);
            this.backgroundWorker3.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker3_RunWorkerCompleted);
            // 
            // backgroundWorker4
            // 
            this.backgroundWorker4.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker4_DoWork);
            this.backgroundWorker4.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker4_RunWorkerCompleted);
            // 
            // backgroundWorker5
            // 
            this.backgroundWorker5.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker5_DoWork);
            this.backgroundWorker5.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker5_RunWorkerCompleted);
            // 
            // backgroundWorker6
            // 
            this.backgroundWorker6.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker6_DoWork);
            this.backgroundWorker6.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker6_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 450);
            this.Controls.Add(this.combBoxContent);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnWatchDirectory);
            this.Controls.Add(this.btnUploadMultipleThreads);
            this.Controls.Add(this.btnUploadOneThread);
            this.Controls.Add(this.txtBoxUploadedLogs);
            this.Controls.Add(this.txtBoxZeitraum);
            this.Controls.Add(this.txtBoxLogOrdner);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "LogUploader4Glenna";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBoxLogOrdner;
        private System.Windows.Forms.TextBox txtBoxZeitraum;
        private System.Windows.Forms.TextBox txtBoxUploadedLogs;
        private System.Windows.Forms.Button btnUploadOneThread;
        private System.Windows.Forms.Button btnUploadMultipleThreads;
        private System.Windows.Forms.Button btnWatchDirectory;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox combBoxContent;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.ComponentModel.BackgroundWorker backgroundWorker4;
        private System.ComponentModel.BackgroundWorker backgroundWorker5;
        private System.ComponentModel.BackgroundWorker backgroundWorker6;
    }
}

