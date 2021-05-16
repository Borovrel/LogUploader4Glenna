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
            this.components = new System.ComponentModel.Container();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker4 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker5 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker6 = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nachUpdatesPrüfenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backWorkerWatching = new System.ComponentModel.BackgroundWorker();
            this.toolTipUploadLastLog = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnThread = new System.Windows.Forms.Button();
            this.btnUploadLastLog = new System.Windows.Forms.Button();
            this.btnWatchDirectory = new System.Windows.Forms.Button();
            this.txtBoxMaxLogSize = new System.Windows.Forms.TextBox();
            this.txtBoxLogOrdner = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBarUpload = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.combBoxContent = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.txtBoxZeitraum = new System.Windows.Forms.TextBox();
            this.btnUploadMultipleThreads = new System.Windows.Forms.Button();
            this.txtBoxUploadedLogs = new System.Windows.Forms.TextBox();
            this.btnUploadOneThread = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnUploadPanel2 = new System.Windows.Forms.Button();
            this.cBoxWhisper = new System.Windows.Forms.CheckBox();
            this.cBoxBoneskinner = new System.Windows.Forms.CheckBox();
            this.cBoxFraenir = new System.Windows.Forms.CheckBox();
            this.cBoxFallen = new System.Windows.Forms.CheckBox();
            this.cBoxIC = new System.Windows.Forms.CheckBox();
            this.cBoxQadim2 = new System.Windows.Forms.CheckBox();
            this.cBoxSabir = new System.Windows.Forms.CheckBox();
            this.cBoxAdina = new System.Windows.Forms.CheckBox();
            this.cBoxQadim = new System.Windows.Forms.CheckBox();
            this.cBoxTwins = new System.Windows.Forms.CheckBox();
            this.cBoxCA = new System.Windows.Forms.CheckBox();
            this.cBoxDhuum = new System.Windows.Forms.CheckBox();
            this.cBoxEyes = new System.Windows.Forms.CheckBox();
            this.cBoxEater = new System.Windows.Forms.CheckBox();
            this.cBoxKing = new System.Windows.Forms.CheckBox();
            this.cBoxRiver = new System.Windows.Forms.CheckBox();
            this.cBoxSH = new System.Windows.Forms.CheckBox();
            this.cBoxDeimos = new System.Windows.Forms.CheckBox();
            this.cBoxSam = new System.Windows.Forms.CheckBox();
            this.cBoxMo = new System.Windows.Forms.CheckBox();
            this.cBoxCairn = new System.Windows.Forms.CheckBox();
            this.cBoxXera = new System.Windows.Forms.CheckBox();
            this.cBoxTC = new System.Windows.Forms.CheckBox();
            this.cBoxKC = new System.Windows.Forms.CheckBox();
            this.cBoxMatt = new System.Windows.Forms.CheckBox();
            this.cBoxTrio = new System.Windows.Forms.CheckBox();
            this.cBoxSloth = new System.Windows.Forms.CheckBox();
            this.cBoxSab = new System.Windows.Forms.CheckBox();
            this.cBoxGorse = new System.Windows.Forms.CheckBox();
            this.cBoxVG = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.nachUpdatesPrüfenToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(849, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            this.dateiToolStripMenuItem.Visible = false;
            // 
            // nachUpdatesPrüfenToolStripMenuItem
            // 
            this.nachUpdatesPrüfenToolStripMenuItem.Name = "nachUpdatesPrüfenToolStripMenuItem";
            this.nachUpdatesPrüfenToolStripMenuItem.Size = new System.Drawing.Size(131, 20);
            this.nachUpdatesPrüfenToolStripMenuItem.Text = "Nach Updates Prüfen";
            this.nachUpdatesPrüfenToolStripMenuItem.Click += new System.EventHandler(this.nachUpdatesPrüfenToolStripMenuItem_Click);
            // 
            // backWorkerWatching
            // 
            this.backWorkerWatching.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backWorkerWatching_DoWork);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(834, 484);
            this.tabControl1.TabIndex = 21;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(826, 458);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Classic";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnThread);
            this.panel1.Controls.Add(this.btnUploadLastLog);
            this.panel1.Controls.Add(this.btnWatchDirectory);
            this.panel1.Controls.Add(this.txtBoxMaxLogSize);
            this.panel1.Controls.Add(this.txtBoxLogOrdner);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.progressBarUpload);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.combBoxContent);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.txtBoxZeitraum);
            this.panel1.Controls.Add(this.btnUploadMultipleThreads);
            this.panel1.Controls.Add(this.txtBoxUploadedLogs);
            this.panel1.Controls.Add(this.btnUploadOneThread);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(791, 444);
            this.panel1.TabIndex = 20;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btnThread
            // 
            this.btnThread.Location = new System.Drawing.Point(700, 41);
            this.btnThread.Name = "btnThread";
            this.btnThread.Size = new System.Drawing.Size(75, 23);
            this.btnThread.TabIndex = 20;
            this.btnThread.Text = "Thread";
            this.btnThread.UseVisualStyleBackColor = true;
            this.btnThread.Click += new System.EventHandler(this.btnThread_Click);
            // 
            // btnUploadLastLog
            // 
            this.btnUploadLastLog.Location = new System.Drawing.Point(524, 400);
            this.btnUploadLastLog.Name = "btnUploadLastLog";
            this.btnUploadLastLog.Size = new System.Drawing.Size(170, 23);
            this.btnUploadLastLog.TabIndex = 19;
            this.btnUploadLastLog.Text = "Lade letzten Log hoch";
            this.btnUploadLastLog.UseVisualStyleBackColor = true;
            this.btnUploadLastLog.Click += new System.EventHandler(this.btnUploadLastLog_Click);
            // 
            // btnWatchDirectory
            // 
            this.btnWatchDirectory.Location = new System.Drawing.Point(364, 9);
            this.btnWatchDirectory.Name = "btnWatchDirectory";
            this.btnWatchDirectory.Size = new System.Drawing.Size(121, 23);
            this.btnWatchDirectory.TabIndex = 9;
            this.btnWatchDirectory.Text = "Starte Überwachung";
            this.btnWatchDirectory.UseVisualStyleBackColor = true;
            this.btnWatchDirectory.Visible = false;
            this.btnWatchDirectory.Click += new System.EventHandler(this.btnWatchDirectory_Click);
            // 
            // txtBoxMaxLogSize
            // 
            this.txtBoxMaxLogSize.Location = new System.Drawing.Point(259, 39);
            this.txtBoxMaxLogSize.Name = "txtBoxMaxLogSize";
            this.txtBoxMaxLogSize.Size = new System.Drawing.Size(44, 20);
            this.txtBoxMaxLogSize.TabIndex = 18;
            this.txtBoxMaxLogSize.Text = "100";
            this.txtBoxMaxLogSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBoxMaxLogSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxMaxLogSize_KeyPress);
            // 
            // txtBoxLogOrdner
            // 
            this.txtBoxLogOrdner.Location = new System.Drawing.Point(66, 12);
            this.txtBoxLogOrdner.Name = "txtBoxLogOrdner";
            this.txtBoxLogOrdner.Size = new System.Drawing.Size(292, 20);
            this.txtBoxLogOrdner.TabIndex = 4;
            this.txtBoxLogOrdner.TextChanged += new System.EventHandler(this.txtBoxLogOrdner_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(125, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Minimale Loggröße in KB";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Log Ordner";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton4);
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Location = new System.Drawing.Point(518, 71);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 119);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ordnerstruktur";
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(1, 43);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(130, 17);
            this.radioButton4.TabIndex = 1;
            this.radioButton4.Text = "Unterordner Charakter";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Checked = true;
            this.radioButton3.Location = new System.Drawing.Point(1, 20);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(153, 17);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Nur nach Bossen aufgeteilt";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Zeitraum";
            // 
            // progressBarUpload
            // 
            this.progressBarUpload.Location = new System.Drawing.Point(3, 400);
            this.progressBarUpload.Name = "progressBarUpload";
            this.progressBarUpload.Size = new System.Drawing.Size(509, 23);
            this.progressBarUpload.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 372);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Uploadstatus";
            // 
            // combBoxContent
            // 
            this.combBoxContent.FormattingEnabled = true;
            this.combBoxContent.Items.AddRange(new object[] {
            "PvE Raidbosse",
            "WvW",
            "PvE Übungsbereich",
            "Fraktale"});
            this.combBoxContent.Location = new System.Drawing.Point(521, 235);
            this.combBoxContent.Name = "combBoxContent";
            this.combBoxContent.Size = new System.Drawing.Size(173, 21);
            this.combBoxContent.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(92, 372);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "%";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Location = new System.Drawing.Point(518, 262);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(193, 100);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Uploadeinstellung";
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
            this.radioButton2.Text = "Lade Logs neu hoch";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // txtBoxZeitraum
            // 
            this.txtBoxZeitraum.Location = new System.Drawing.Point(66, 38);
            this.txtBoxZeitraum.Name = "txtBoxZeitraum";
            this.txtBoxZeitraum.Size = new System.Drawing.Size(42, 20);
            this.txtBoxZeitraum.TabIndex = 5;
            this.txtBoxZeitraum.Text = "0";
            this.txtBoxZeitraum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBoxZeitraum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxZeitraum_KeyPress);
            // 
            // btnUploadMultipleThreads
            // 
            this.btnUploadMultipleThreads.Location = new System.Drawing.Point(515, 41);
            this.btnUploadMultipleThreads.Name = "btnUploadMultipleThreads";
            this.btnUploadMultipleThreads.Size = new System.Drawing.Size(179, 23);
            this.btnUploadMultipleThreads.TabIndex = 8;
            this.btnUploadMultipleThreads.Text = "Lade Logs hoch mehrere Threads";
            this.btnUploadMultipleThreads.UseVisualStyleBackColor = true;
            this.btnUploadMultipleThreads.Visible = false;
            this.btnUploadMultipleThreads.Click += new System.EventHandler(this.btnUploadMultipleThreads_Click);
            // 
            // txtBoxUploadedLogs
            // 
            this.txtBoxUploadedLogs.Location = new System.Drawing.Point(3, 64);
            this.txtBoxUploadedLogs.Multiline = true;
            this.txtBoxUploadedLogs.Name = "txtBoxUploadedLogs";
            this.txtBoxUploadedLogs.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtBoxUploadedLogs.Size = new System.Drawing.Size(509, 298);
            this.txtBoxUploadedLogs.TabIndex = 6;
            // 
            // btnUploadOneThread
            // 
            this.btnUploadOneThread.Location = new System.Drawing.Point(515, 8);
            this.btnUploadOneThread.Name = "btnUploadOneThread";
            this.btnUploadOneThread.Size = new System.Drawing.Size(179, 23);
            this.btnUploadOneThread.TabIndex = 7;
            this.btnUploadOneThread.Text = "Lade Logs hoch 1 Thread";
            this.btnUploadOneThread.UseVisualStyleBackColor = true;
            this.btnUploadOneThread.Click += new System.EventHandler(this.btnUploadOneThread_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.progressBar1);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.btnUploadPanel2);
            this.tabPage2.Controls.Add(this.cBoxWhisper);
            this.tabPage2.Controls.Add(this.cBoxBoneskinner);
            this.tabPage2.Controls.Add(this.cBoxFraenir);
            this.tabPage2.Controls.Add(this.cBoxFallen);
            this.tabPage2.Controls.Add(this.cBoxIC);
            this.tabPage2.Controls.Add(this.cBoxQadim2);
            this.tabPage2.Controls.Add(this.cBoxSabir);
            this.tabPage2.Controls.Add(this.cBoxAdina);
            this.tabPage2.Controls.Add(this.cBoxQadim);
            this.tabPage2.Controls.Add(this.cBoxTwins);
            this.tabPage2.Controls.Add(this.cBoxCA);
            this.tabPage2.Controls.Add(this.cBoxDhuum);
            this.tabPage2.Controls.Add(this.cBoxEyes);
            this.tabPage2.Controls.Add(this.cBoxEater);
            this.tabPage2.Controls.Add(this.cBoxKing);
            this.tabPage2.Controls.Add(this.cBoxRiver);
            this.tabPage2.Controls.Add(this.cBoxSH);
            this.tabPage2.Controls.Add(this.cBoxDeimos);
            this.tabPage2.Controls.Add(this.cBoxSam);
            this.tabPage2.Controls.Add(this.cBoxMo);
            this.tabPage2.Controls.Add(this.cBoxCairn);
            this.tabPage2.Controls.Add(this.cBoxXera);
            this.tabPage2.Controls.Add(this.cBoxTC);
            this.tabPage2.Controls.Add(this.cBoxKC);
            this.tabPage2.Controls.Add(this.cBoxMatt);
            this.tabPage2.Controls.Add(this.cBoxTrio);
            this.tabPage2.Controls.Add(this.cBoxSloth);
            this.tabPage2.Controls.Add(this.cBoxSab);
            this.tabPage2.Controls.Add(this.cBoxGorse);
            this.tabPage2.Controls.Add(this.cBoxVG);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(826, 458);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "SelectBosses";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(11, 427);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(509, 23);
            this.progressBar1.TabIndex = 40;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 399);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 38;
            this.label7.Text = "Uploadstatus";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(100, 399);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 13);
            this.label8.TabIndex = 39;
            this.label8.Text = "%";
            // 
            // btnUploadPanel2
            // 
            this.btnUploadPanel2.Location = new System.Drawing.Point(445, 394);
            this.btnUploadPanel2.Name = "btnUploadPanel2";
            this.btnUploadPanel2.Size = new System.Drawing.Size(75, 23);
            this.btnUploadPanel2.TabIndex = 37;
            this.btnUploadPanel2.Text = "Hochladen";
            this.btnUploadPanel2.UseVisualStyleBackColor = true;
            this.btnUploadPanel2.Click += new System.EventHandler(this.btnUploadPanel2_Click);
            // 
            // cBoxWhisper
            // 
            this.cBoxWhisper.AutoSize = true;
            this.cBoxWhisper.Location = new System.Drawing.Point(335, 163);
            this.cBoxWhisper.Name = "cBoxWhisper";
            this.cBoxWhisper.Size = new System.Drawing.Size(65, 17);
            this.cBoxWhisper.TabIndex = 36;
            this.cBoxWhisper.Text = "Whisper";
            this.cBoxWhisper.UseVisualStyleBackColor = true;
            // 
            // cBoxBoneskinner
            // 
            this.cBoxBoneskinner.AutoSize = true;
            this.cBoxBoneskinner.Location = new System.Drawing.Point(335, 136);
            this.cBoxBoneskinner.Name = "cBoxBoneskinner";
            this.cBoxBoneskinner.Size = new System.Drawing.Size(85, 17);
            this.cBoxBoneskinner.TabIndex = 35;
            this.cBoxBoneskinner.Text = "Boneskinner";
            this.cBoxBoneskinner.UseVisualStyleBackColor = true;
            // 
            // cBoxFraenir
            // 
            this.cBoxFraenir.AutoSize = true;
            this.cBoxFraenir.Location = new System.Drawing.Point(335, 109);
            this.cBoxFraenir.Name = "cBoxFraenir";
            this.cBoxFraenir.Size = new System.Drawing.Size(58, 17);
            this.cBoxFraenir.TabIndex = 34;
            this.cBoxFraenir.Text = "Fraenir";
            this.cBoxFraenir.UseVisualStyleBackColor = true;
            // 
            // cBoxFallen
            // 
            this.cBoxFallen.AutoSize = true;
            this.cBoxFallen.Location = new System.Drawing.Point(335, 82);
            this.cBoxFallen.Name = "cBoxFallen";
            this.cBoxFallen.Size = new System.Drawing.Size(54, 17);
            this.cBoxFallen.TabIndex = 33;
            this.cBoxFallen.Text = "Fallen";
            this.cBoxFallen.UseVisualStyleBackColor = true;
            // 
            // cBoxIC
            // 
            this.cBoxIC.AutoSize = true;
            this.cBoxIC.Location = new System.Drawing.Point(335, 55);
            this.cBoxIC.Name = "cBoxIC";
            this.cBoxIC.Size = new System.Drawing.Size(36, 17);
            this.cBoxIC.TabIndex = 32;
            this.cBoxIC.Text = "IC";
            this.cBoxIC.UseVisualStyleBackColor = true;
            // 
            // cBoxQadim2
            // 
            this.cBoxQadim2.AutoSize = true;
            this.cBoxQadim2.Location = new System.Drawing.Point(158, 352);
            this.cBoxQadim2.Name = "cBoxQadim2";
            this.cBoxQadim2.Size = new System.Drawing.Size(74, 17);
            this.cBoxQadim2.TabIndex = 31;
            this.cBoxQadim2.Text = "Qadim 2.0";
            this.cBoxQadim2.UseVisualStyleBackColor = true;
            // 
            // cBoxSabir
            // 
            this.cBoxSabir.AutoSize = true;
            this.cBoxSabir.Location = new System.Drawing.Point(158, 325);
            this.cBoxSabir.Name = "cBoxSabir";
            this.cBoxSabir.Size = new System.Drawing.Size(50, 17);
            this.cBoxSabir.TabIndex = 30;
            this.cBoxSabir.Text = "Sabir";
            this.cBoxSabir.UseVisualStyleBackColor = true;
            // 
            // cBoxAdina
            // 
            this.cBoxAdina.AutoSize = true;
            this.cBoxAdina.Location = new System.Drawing.Point(158, 298);
            this.cBoxAdina.Name = "cBoxAdina";
            this.cBoxAdina.Size = new System.Drawing.Size(53, 17);
            this.cBoxAdina.TabIndex = 29;
            this.cBoxAdina.Text = "Adina";
            this.cBoxAdina.UseVisualStyleBackColor = true;
            // 
            // cBoxQadim
            // 
            this.cBoxQadim.AutoSize = true;
            this.cBoxQadim.Location = new System.Drawing.Point(158, 271);
            this.cBoxQadim.Name = "cBoxQadim";
            this.cBoxQadim.Size = new System.Drawing.Size(56, 17);
            this.cBoxQadim.TabIndex = 28;
            this.cBoxQadim.Text = "Qadim";
            this.cBoxQadim.UseVisualStyleBackColor = true;
            // 
            // cBoxTwins
            // 
            this.cBoxTwins.AutoSize = true;
            this.cBoxTwins.Location = new System.Drawing.Point(158, 244);
            this.cBoxTwins.Name = "cBoxTwins";
            this.cBoxTwins.Size = new System.Drawing.Size(54, 17);
            this.cBoxTwins.TabIndex = 27;
            this.cBoxTwins.Text = "Twins";
            this.cBoxTwins.UseVisualStyleBackColor = true;
            // 
            // cBoxCA
            // 
            this.cBoxCA.AutoSize = true;
            this.cBoxCA.Location = new System.Drawing.Point(158, 217);
            this.cBoxCA.Name = "cBoxCA";
            this.cBoxCA.Size = new System.Drawing.Size(40, 17);
            this.cBoxCA.TabIndex = 26;
            this.cBoxCA.Text = "CA";
            this.cBoxCA.UseVisualStyleBackColor = true;
            // 
            // cBoxDhuum
            // 
            this.cBoxDhuum.AutoSize = true;
            this.cBoxDhuum.Location = new System.Drawing.Point(158, 190);
            this.cBoxDhuum.Name = "cBoxDhuum";
            this.cBoxDhuum.Size = new System.Drawing.Size(60, 17);
            this.cBoxDhuum.TabIndex = 25;
            this.cBoxDhuum.Text = "Dhuum";
            this.cBoxDhuum.UseVisualStyleBackColor = true;
            // 
            // cBoxEyes
            // 
            this.cBoxEyes.AutoSize = true;
            this.cBoxEyes.Location = new System.Drawing.Point(158, 163);
            this.cBoxEyes.Name = "cBoxEyes";
            this.cBoxEyes.Size = new System.Drawing.Size(49, 17);
            this.cBoxEyes.TabIndex = 24;
            this.cBoxEyes.Text = "Eyes";
            this.cBoxEyes.UseVisualStyleBackColor = true;
            // 
            // cBoxEater
            // 
            this.cBoxEater.AutoSize = true;
            this.cBoxEater.Location = new System.Drawing.Point(158, 136);
            this.cBoxEater.Name = "cBoxEater";
            this.cBoxEater.Size = new System.Drawing.Size(51, 17);
            this.cBoxEater.TabIndex = 23;
            this.cBoxEater.Text = "Eater";
            this.cBoxEater.UseVisualStyleBackColor = true;
            // 
            // cBoxKing
            // 
            this.cBoxKing.AutoSize = true;
            this.cBoxKing.Location = new System.Drawing.Point(158, 109);
            this.cBoxKing.Name = "cBoxKing";
            this.cBoxKing.Size = new System.Drawing.Size(47, 17);
            this.cBoxKing.TabIndex = 22;
            this.cBoxKing.Text = "King";
            this.cBoxKing.UseVisualStyleBackColor = true;
            // 
            // cBoxRiver
            // 
            this.cBoxRiver.AutoSize = true;
            this.cBoxRiver.Location = new System.Drawing.Point(158, 82);
            this.cBoxRiver.Name = "cBoxRiver";
            this.cBoxRiver.Size = new System.Drawing.Size(51, 17);
            this.cBoxRiver.TabIndex = 21;
            this.cBoxRiver.Text = "River";
            this.cBoxRiver.UseVisualStyleBackColor = true;
            // 
            // cBoxSH
            // 
            this.cBoxSH.AutoSize = true;
            this.cBoxSH.Location = new System.Drawing.Point(158, 55);
            this.cBoxSH.Name = "cBoxSH";
            this.cBoxSH.Size = new System.Drawing.Size(97, 17);
            this.cBoxSH.TabIndex = 20;
            this.cBoxSH.Text = "Soulless Horror";
            this.cBoxSH.UseVisualStyleBackColor = true;
            // 
            // cBoxDeimos
            // 
            this.cBoxDeimos.AutoSize = true;
            this.cBoxDeimos.Location = new System.Drawing.Point(9, 379);
            this.cBoxDeimos.Name = "cBoxDeimos";
            this.cBoxDeimos.Size = new System.Drawing.Size(61, 17);
            this.cBoxDeimos.TabIndex = 19;
            this.cBoxDeimos.Text = "Deimos";
            this.cBoxDeimos.UseVisualStyleBackColor = true;
            // 
            // cBoxSam
            // 
            this.cBoxSam.AutoSize = true;
            this.cBoxSam.Location = new System.Drawing.Point(9, 352);
            this.cBoxSam.Name = "cBoxSam";
            this.cBoxSam.Size = new System.Drawing.Size(68, 17);
            this.cBoxSam.TabIndex = 18;
            this.cBoxSam.Text = "Samarog";
            this.cBoxSam.UseVisualStyleBackColor = true;
            // 
            // cBoxMo
            // 
            this.cBoxMo.AutoSize = true;
            this.cBoxMo.Location = new System.Drawing.Point(9, 325);
            this.cBoxMo.Name = "cBoxMo";
            this.cBoxMo.Size = new System.Drawing.Size(43, 17);
            this.cBoxMo.TabIndex = 17;
            this.cBoxMo.Text = "MO";
            this.cBoxMo.UseVisualStyleBackColor = true;
            // 
            // cBoxCairn
            // 
            this.cBoxCairn.AutoSize = true;
            this.cBoxCairn.Location = new System.Drawing.Point(9, 298);
            this.cBoxCairn.Name = "cBoxCairn";
            this.cBoxCairn.Size = new System.Drawing.Size(50, 17);
            this.cBoxCairn.TabIndex = 16;
            this.cBoxCairn.Text = "Cairn";
            this.cBoxCairn.UseVisualStyleBackColor = true;
            // 
            // cBoxXera
            // 
            this.cBoxXera.AutoSize = true;
            this.cBoxXera.Location = new System.Drawing.Point(9, 271);
            this.cBoxXera.Name = "cBoxXera";
            this.cBoxXera.Size = new System.Drawing.Size(48, 17);
            this.cBoxXera.TabIndex = 15;
            this.cBoxXera.Text = "Xera";
            this.cBoxXera.UseVisualStyleBackColor = true;
            // 
            // cBoxTC
            // 
            this.cBoxTC.AutoSize = true;
            this.cBoxTC.Location = new System.Drawing.Point(9, 244);
            this.cBoxTC.Name = "cBoxTC";
            this.cBoxTC.Size = new System.Drawing.Size(95, 17);
            this.cBoxTC.TabIndex = 14;
            this.cBoxTC.Text = "Twisted Castle";
            this.cBoxTC.UseVisualStyleBackColor = true;
            // 
            // cBoxKC
            // 
            this.cBoxKC.AutoSize = true;
            this.cBoxKC.Location = new System.Drawing.Point(9, 217);
            this.cBoxKC.Name = "cBoxKC";
            this.cBoxKC.Size = new System.Drawing.Size(40, 17);
            this.cBoxKC.TabIndex = 13;
            this.cBoxKC.Text = "KC";
            this.cBoxKC.UseVisualStyleBackColor = true;
            // 
            // cBoxMatt
            // 
            this.cBoxMatt.AutoSize = true;
            this.cBoxMatt.Location = new System.Drawing.Point(9, 190);
            this.cBoxMatt.Name = "cBoxMatt";
            this.cBoxMatt.Size = new System.Drawing.Size(66, 17);
            this.cBoxMatt.TabIndex = 12;
            this.cBoxMatt.Text = "Matthias";
            this.cBoxMatt.UseVisualStyleBackColor = true;
            // 
            // cBoxTrio
            // 
            this.cBoxTrio.AutoSize = true;
            this.cBoxTrio.Location = new System.Drawing.Point(9, 163);
            this.cBoxTrio.Name = "cBoxTrio";
            this.cBoxTrio.Size = new System.Drawing.Size(44, 17);
            this.cBoxTrio.TabIndex = 11;
            this.cBoxTrio.Text = "Trio";
            this.cBoxTrio.UseVisualStyleBackColor = true;
            // 
            // cBoxSloth
            // 
            this.cBoxSloth.AutoSize = true;
            this.cBoxSloth.Location = new System.Drawing.Point(9, 136);
            this.cBoxSloth.Name = "cBoxSloth";
            this.cBoxSloth.Size = new System.Drawing.Size(50, 17);
            this.cBoxSloth.TabIndex = 10;
            this.cBoxSloth.Text = "Sloth";
            this.cBoxSloth.UseVisualStyleBackColor = true;
            // 
            // cBoxSab
            // 
            this.cBoxSab.AutoSize = true;
            this.cBoxSab.Location = new System.Drawing.Point(9, 109);
            this.cBoxSab.Name = "cBoxSab";
            this.cBoxSab.Size = new System.Drawing.Size(66, 17);
            this.cBoxSab.TabIndex = 9;
            this.cBoxSab.Text = "Sabetha";
            this.cBoxSab.UseVisualStyleBackColor = true;
            // 
            // cBoxGorse
            // 
            this.cBoxGorse.AutoSize = true;
            this.cBoxGorse.Location = new System.Drawing.Point(9, 82);
            this.cBoxGorse.Name = "cBoxGorse";
            this.cBoxGorse.Size = new System.Drawing.Size(68, 17);
            this.cBoxGorse.TabIndex = 8;
            this.cBoxGorse.Text = "Gorseval";
            this.cBoxGorse.UseVisualStyleBackColor = true;
            // 
            // cBoxVG
            // 
            this.cBoxVG.AutoSize = true;
            this.cBoxVG.Location = new System.Drawing.Point(9, 55);
            this.cBoxVG.Name = "cBoxVG";
            this.cBoxVG.Size = new System.Drawing.Size(85, 17);
            this.cBoxVG.TabIndex = 7;
            this.cBoxVG.Text = "Tal-Wächter";
            this.cBoxVG.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(72, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(292, 20);
            this.textBox1.TabIndex = 6;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Log Ordner";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 521);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "LogUploader4Glenna";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.ComponentModel.BackgroundWorker backgroundWorker4;
        private System.ComponentModel.BackgroundWorker backgroundWorker5;
        private System.ComponentModel.BackgroundWorker backgroundWorker6;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nachUpdatesPrüfenToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backWorkerWatching;
        private System.Windows.Forms.ToolTip toolTipUploadLastLog;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnUploadLastLog;
        private System.Windows.Forms.Button btnWatchDirectory;
        private System.Windows.Forms.TextBox txtBoxMaxLogSize;
        private System.Windows.Forms.TextBox txtBoxLogOrdner;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBarUpload;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox combBoxContent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.TextBox txtBoxZeitraum;
        private System.Windows.Forms.Button btnUploadMultipleThreads;
        private System.Windows.Forms.TextBox txtBoxUploadedLogs;
        private System.Windows.Forms.Button btnUploadOneThread;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox cBoxDeimos;
        private System.Windows.Forms.CheckBox cBoxSam;
        private System.Windows.Forms.CheckBox cBoxMo;
        private System.Windows.Forms.CheckBox cBoxCairn;
        private System.Windows.Forms.CheckBox cBoxXera;
        private System.Windows.Forms.CheckBox cBoxTC;
        private System.Windows.Forms.CheckBox cBoxKC;
        private System.Windows.Forms.CheckBox cBoxMatt;
        private System.Windows.Forms.CheckBox cBoxTrio;
        private System.Windows.Forms.CheckBox cBoxSloth;
        private System.Windows.Forms.CheckBox cBoxSab;
        private System.Windows.Forms.CheckBox cBoxGorse;
        private System.Windows.Forms.CheckBox cBoxVG;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cBoxWhisper;
        private System.Windows.Forms.CheckBox cBoxBoneskinner;
        private System.Windows.Forms.CheckBox cBoxFraenir;
        private System.Windows.Forms.CheckBox cBoxFallen;
        private System.Windows.Forms.CheckBox cBoxIC;
        private System.Windows.Forms.CheckBox cBoxQadim2;
        private System.Windows.Forms.CheckBox cBoxSabir;
        private System.Windows.Forms.CheckBox cBoxAdina;
        private System.Windows.Forms.CheckBox cBoxQadim;
        private System.Windows.Forms.CheckBox cBoxTwins;
        private System.Windows.Forms.CheckBox cBoxCA;
        private System.Windows.Forms.CheckBox cBoxDhuum;
        private System.Windows.Forms.CheckBox cBoxEyes;
        private System.Windows.Forms.CheckBox cBoxEater;
        private System.Windows.Forms.CheckBox cBoxKing;
        private System.Windows.Forms.CheckBox cBoxRiver;
        private System.Windows.Forms.CheckBox cBoxSH;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnUploadPanel2;
        private System.Windows.Forms.Button btnThread;
    }
}

