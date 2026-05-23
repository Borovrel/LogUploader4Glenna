namespace LogUploader4Glenna
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.nachUpdatesPrüfenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTipUploadLastLog = new System.Windows.Forms.ToolTip(this.components);

            this.groupBoxKonfig = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxLogOrdner = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxZeitraum = new System.Windows.Forms.TextBox();
            this.labelZeitraumHint = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBoxMaxLogSize = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.combBoxContent = new System.Windows.Forms.ComboBox();

            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();

            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();

            this.groupBoxErgebnis = new System.Windows.Forms.GroupBox();
            this.txtBoxUploadedLogs = new System.Windows.Forms.TextBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.progressBarUpload = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();

            this.btnUploadHoch = new System.Windows.Forms.Button();
            this.btnUploadLastLog = new System.Windows.Forms.Button();

            this.menuStrip1.SuspendLayout();
            this.groupBoxKonfig.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxErgebnis.SuspendLayout();
            this.SuspendLayout();

            //
            // menuStrip1
            //
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.nachUpdatesPrüfenToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(760, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            //
            // nachUpdatesPrüfenToolStripMenuItem
            //
            this.nachUpdatesPrüfenToolStripMenuItem.Name = "nachUpdatesPrüfenToolStripMenuItem";
            this.nachUpdatesPrüfenToolStripMenuItem.Size = new System.Drawing.Size(140, 20);
            this.nachUpdatesPrüfenToolStripMenuItem.Text = "Nach Updates prüfen";
            this.nachUpdatesPrüfenToolStripMenuItem.Click += new System.EventHandler(this.nachUpdatesPrüfenToolStripMenuItem_Click);

            //
            // groupBoxKonfig
            //
            this.groupBoxKonfig.Controls.Add(this.label1);
            this.groupBoxKonfig.Controls.Add(this.txtBoxLogOrdner);
            this.groupBoxKonfig.Controls.Add(this.label2);
            this.groupBoxKonfig.Controls.Add(this.txtBoxZeitraum);
            this.groupBoxKonfig.Controls.Add(this.labelZeitraumHint);
            this.groupBoxKonfig.Controls.Add(this.label5);
            this.groupBoxKonfig.Controls.Add(this.txtBoxMaxLogSize);
            this.groupBoxKonfig.Controls.Add(this.label3);
            this.groupBoxKonfig.Controls.Add(this.combBoxContent);
            this.groupBoxKonfig.Location = new System.Drawing.Point(12, 32);
            this.groupBoxKonfig.Name = "groupBoxKonfig";
            this.groupBoxKonfig.Size = new System.Drawing.Size(736, 130);
            this.groupBoxKonfig.TabIndex = 1;
            this.groupBoxKonfig.TabStop = false;
            this.groupBoxKonfig.Text = "Konfiguration";
            this.groupBoxKonfig.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Log-Ordner:";
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            //
            // txtBoxLogOrdner
            //
            this.txtBoxLogOrdner.Location = new System.Drawing.Point(110, 25);
            this.txtBoxLogOrdner.Name = "txtBoxLogOrdner";
            this.txtBoxLogOrdner.Size = new System.Drawing.Size(610, 23);
            this.txtBoxLogOrdner.TabIndex = 1;
            this.txtBoxLogOrdner.Font = new System.Drawing.Font("Segoe UI", 9F);
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Zeitraum (Tage):";
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            //
            // txtBoxZeitraum
            //
            this.txtBoxZeitraum.Location = new System.Drawing.Point(110, 59);
            this.txtBoxZeitraum.Name = "txtBoxZeitraum";
            this.txtBoxZeitraum.Size = new System.Drawing.Size(60, 23);
            this.txtBoxZeitraum.TabIndex = 3;
            this.txtBoxZeitraum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBoxZeitraum.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtBoxZeitraum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxZeitraum_KeyPress);
            //
            // labelZeitraumHint
            //
            this.labelZeitraumHint.AutoSize = true;
            this.labelZeitraumHint.Location = new System.Drawing.Point(178, 62);
            this.labelZeitraumHint.Name = "labelZeitraumHint";
            this.labelZeitraumHint.Size = new System.Drawing.Size(195, 15);
            this.labelZeitraumHint.TabIndex = 4;
            this.labelZeitraumHint.Text = "leer = seit letztem Reset (Mo 14:00)";
            this.labelZeitraumHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelZeitraumHint.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            //
            // label5
            //
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(420, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "Min. Größe (KB):";
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            //
            // txtBoxMaxLogSize
            //
            this.txtBoxMaxLogSize.Location = new System.Drawing.Point(540, 59);
            this.txtBoxMaxLogSize.Name = "txtBoxMaxLogSize";
            this.txtBoxMaxLogSize.Size = new System.Drawing.Size(60, 23);
            this.txtBoxMaxLogSize.TabIndex = 6;
            this.txtBoxMaxLogSize.Text = "100";
            this.txtBoxMaxLogSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBoxMaxLogSize.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtBoxMaxLogSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxMaxLogSize_KeyPress);
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Inhalt:";
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            //
            // combBoxContent
            //
            this.combBoxContent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combBoxContent.FormattingEnabled = true;
            this.combBoxContent.Items.AddRange(new object[] {
                "PvE Raidbosse",
                "WvW",
                "PvE Übungsbereich",
                "Fraktale"});
            this.combBoxContent.Location = new System.Drawing.Point(110, 92);
            this.combBoxContent.Name = "combBoxContent";
            this.combBoxContent.Size = new System.Drawing.Size(200, 23);
            this.combBoxContent.TabIndex = 8;
            this.combBoxContent.Font = new System.Drawing.Font("Segoe UI", 9F);

            //
            // groupBox2
            //
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Controls.Add(this.radioButton4);
            this.groupBox2.Location = new System.Drawing.Point(12, 172);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(360, 85);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ordnerstruktur";
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            //
            // radioButton3
            //
            this.radioButton3.AutoSize = true;
            this.radioButton3.Checked = true;
            this.radioButton3.Location = new System.Drawing.Point(12, 25);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(180, 19);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Nur nach Bossen aufgeteilt";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.Font = new System.Drawing.Font("Segoe UI", 9F);
            //
            // radioButton4
            //
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(12, 50);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(160, 19);
            this.radioButton4.TabIndex = 1;
            this.radioButton4.Text = "Unterordner Charakter";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.Font = new System.Drawing.Font("Segoe UI", 9F);

            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Location = new System.Drawing.Point(388, 172);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 85);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Upload-Einstellung";
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            //
            // radioButton1
            //
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(12, 25);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(220, 19);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Lade keine Logs mehrfach hoch";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            //
            // radioButton2
            //
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(12, 50);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(140, 19);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Lade Logs neu hoch";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Font = new System.Drawing.Font("Segoe UI", 9F);

            //
            // groupBoxErgebnis
            //
            this.groupBoxErgebnis.Controls.Add(this.txtBoxUploadedLogs);
            this.groupBoxErgebnis.Controls.Add(this.labelStatus);
            this.groupBoxErgebnis.Controls.Add(this.progressBarUpload);
            this.groupBoxErgebnis.Controls.Add(this.label4);
            this.groupBoxErgebnis.Location = new System.Drawing.Point(12, 267);
            this.groupBoxErgebnis.Name = "groupBoxErgebnis";
            this.groupBoxErgebnis.Size = new System.Drawing.Size(736, 235);
            this.groupBoxErgebnis.TabIndex = 4;
            this.groupBoxErgebnis.TabStop = false;
            this.groupBoxErgebnis.Text = "Ergebnis";
            this.groupBoxErgebnis.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            //
            // txtBoxUploadedLogs
            //
            this.txtBoxUploadedLogs.Location = new System.Drawing.Point(12, 25);
            this.txtBoxUploadedLogs.Multiline = true;
            this.txtBoxUploadedLogs.Name = "txtBoxUploadedLogs";
            this.txtBoxUploadedLogs.ReadOnly = true;
            this.txtBoxUploadedLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBoxUploadedLogs.Size = new System.Drawing.Size(712, 165);
            this.txtBoxUploadedLogs.TabIndex = 0;
            this.txtBoxUploadedLogs.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtBoxUploadedLogs.BackColor = System.Drawing.Color.White;
            //
            // labelStatus
            //
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(12, 200);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(48, 15);
            this.labelStatus.TabIndex = 1;
            this.labelStatus.Text = "Status:";
            this.labelStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            //
            // progressBarUpload
            //
            this.progressBarUpload.Location = new System.Drawing.Point(70, 198);
            this.progressBarUpload.Name = "progressBarUpload";
            this.progressBarUpload.Size = new System.Drawing.Size(580, 20);
            this.progressBarUpload.TabIndex = 2;
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(660, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "0%";
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);

            //
            // btnUploadHoch (primary)
            //
            this.btnUploadHoch.Location = new System.Drawing.Point(12, 514);
            this.btnUploadHoch.Name = "btnUploadHoch";
            this.btnUploadHoch.Size = new System.Drawing.Size(220, 36);
            this.btnUploadHoch.TabIndex = 5;
            this.btnUploadHoch.Text = "Logs hochladen";
            this.btnUploadHoch.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnUploadHoch.ForeColor = System.Drawing.Color.White;
            this.btnUploadHoch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadHoch.FlatAppearance.BorderSize = 0;
            this.btnUploadHoch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUploadHoch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUploadHoch.Click += new System.EventHandler(this.btnUploadHoch_Click);

            //
            // btnUploadLastLog (secondary)
            //
            this.btnUploadLastLog.Location = new System.Drawing.Point(528, 514);
            this.btnUploadLastLog.Name = "btnUploadLastLog";
            this.btnUploadLastLog.Size = new System.Drawing.Size(220, 36);
            this.btnUploadLastLog.TabIndex = 6;
            this.btnUploadLastLog.Text = "Lade letzten Log hoch";
            this.btnUploadLastLog.UseVisualStyleBackColor = true;
            this.btnUploadLastLog.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnUploadLastLog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUploadLastLog.Click += new System.EventHandler(this.btnUploadLastLog_Click);

            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 562);
            this.Controls.Add(this.btnUploadLastLog);
            this.Controls.Add(this.btnUploadHoch);
            this.Controls.Add(this.groupBoxErgebnis);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBoxKonfig);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "LogUploader4Glenna";
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);

            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBoxKonfig.ResumeLayout(false);
            this.groupBoxKonfig.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxErgebnis.ResumeLayout(false);
            this.groupBoxErgebnis.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem nachUpdatesPrüfenToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTipUploadLastLog;

        private System.Windows.Forms.GroupBox groupBoxKonfig;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxLogOrdner;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxZeitraum;
        private System.Windows.Forms.Label labelZeitraumHint;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBoxMaxLogSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox combBoxContent;

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;

        private System.Windows.Forms.GroupBox groupBoxErgebnis;
        private System.Windows.Forms.TextBox txtBoxUploadedLogs;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ProgressBar progressBarUpload;
        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.Button btnUploadHoch;
        private System.Windows.Forms.Button btnUploadLastLog;
    }
}
