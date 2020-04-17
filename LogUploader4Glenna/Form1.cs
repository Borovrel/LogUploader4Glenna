using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace LogUploader4Glenna
{
    public partial class Form1 : Form
    {
        //public List<LogInfos2> hochgeladeneLogs;
        public struct LogInfos
        {
            public string boss;
            public string pfad;
            public DateTime erstellDatum;

        }

        public struct LogInfos2
        {
            public string boss;
            public string pfad;
            public int versuch;
        }

        public List<LogInfos2> listeLogTries = new List<LogInfos2>();
        public int listeLogTriesCount;
        public double prozentualerAnteilProLog;
        private bool ordnerUeberwachungLaueft;

        public Form1()
        {
            string environmentOrdner = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ordnerUeberwachungLaueft = false;
            InitializeComponent();
            txtBoxLogOrdner.Text = environmentOrdner + "\\Guild Wars 2\\addons\\arcdps\\arcdps.cbtlogs\\";

            CheckForIllegalCrossThreadCalls = false;

            ErstelleVerzeichnisse();
            combBoxContent.SelectedIndex = 0;

            toolTipUploadLastLog.SetToolTip(btnUploadLastLog, "Lädt ausschließlich den letzten gefunden PvE Log hoch. Unabhängig von der Einstellung, ob der Log nicht doppelt hochgeladen werden soll.");
            
        }
        /*
        private string Bossordner(ref string Ordernpfad)
        {
            string ausgabe = Ordernpfad;

            if(Ordernpfad != txtBoxLogOrdner.Text)
            {
                Console.WriteLine(Ordernpfad+"\n"+txtBoxLogOrdner.Text);
                var Directories = Directory.GetDirectories(txtBoxLogOrdner.Text);
                foreach (var item in Directories)
                {
                    if(item == Ordernpfad)
                    {

                    }
                }
                Ordernpfad = Bossordner(ref Ordernpfad);
            }

            return ausgabe;
        }
        */

        private void ListeAndersBefuellen(ref List<LogInfos> listeDerLogs)
        {
            var Directories = Directory.GetDirectories(txtBoxLogOrdner.Text);

            txtBoxUploadedLogs.Clear();

            foreach (var item in Directories)
            {
                var files = Directory.GetFiles(item, "*.zevtc", SearchOption.AllDirectories);
                if (files.Count() == 0)
                {
                    files = Directory.GetFiles(item, "*.evtc", SearchOption.AllDirectories);
                }

                foreach (var file in files)
                {
                    var info = new FileInfo(file);
                    if (combBoxContent.SelectedIndex == 0)
                    {
                        //Nur PvE Logs
                        if (item.Contains("WvW"))
                        {
                            continue; //Überspringt alle Einträge, welche im WvW Pfad liegen
                        }

                        if (info.Length <= int.Parse(txtBoxMaxLogSize.Text))
                        {
                            continue; //überspringt alle Einträge, deren Größe kleiner als 100 KB sind
                        }
                        if (info.CreationTime > DateTime.Today.AddDays(-(int.Parse(txtBoxZeitraum.Text))))
                        {
                            //Das Erstelldatum muss im Zeitraum liegen
                            LogInfos loginfos = new LogInfos();
                            loginfos.boss = item;

                            loginfos.erstellDatum = info.CreationTime;
                            loginfos.pfad = info.FullName;

                            listeDerLogs.Add(loginfos); //Fügt der Liste hinzu
                        }
                    }
                    else
                    {
                        if (item.Contains("WvW"))
                        {
                            continue; //Überspringt alle Logs die nicht mit dem WvW zu tun haben
                        }
                        if (info.CreationTime > DateTime.Today.AddDays(-(int.Parse(txtBoxZeitraum.Text))))
                        {
                            //Muss noch ausimplementiert werden
                            //wvwlogs.Add(info.FullName);
                        }
                    }
                }
            }
        }
        private void BefuelleListe()
        {
            label4.Text = "0%";
            progressBarUpload.Value = 0;
            Update();


            List<LogInfos> zaehelendeLogs = new List<LogInfos>();
            zaehelendeLogs.Clear();

            List<LogInfos> wvwlogs = new List<LogInfos>();
            wvwlogs.Clear();

            bool neuenWeg = false;

            if (neuenWeg)
            {
                ListeAndersBefuellen(ref zaehelendeLogs);
            }
            else
            {
                var files = Directory.GetFiles(txtBoxLogOrdner.Text, "*.zevtc", SearchOption.AllDirectories);
                if (files.Count() == 0)
                {
                    files = Directory.GetFiles(txtBoxLogOrdner.Text, "*.evtc", SearchOption.AllDirectories);
                }

                foreach (string file in files)
                {
                    var info = new FileInfo(file);
                    if (combBoxContent.SelectedIndex == 0)
                    {
                        //Nur PvE Logs
                        if (info.DirectoryName.Contains("WvW"))
                        {
                            continue; //Überspringt alle Einträge, welche im WvW Pfad liegen
                        }

                        if (info.Length <= int.Parse(txtBoxMaxLogSize.Text))
                        {
                            continue; //überspringt alle Einträge, deren Größe kleiner als 100 KB sind
                        }
                        if (info.CreationTime > DateTime.Today.AddDays(-(int.Parse(txtBoxZeitraum.Text))))
                        {
                            //Das Erstelldatum muss im Zeitraum liegen
                            LogInfos loginfos = new LogInfos();
                            string bossi = info.DirectoryName;
                            //loginfos.boss = Bossordner(ref bossi);

                            if (radioButton3.Checked)
                            {
                                loginfos.boss = info.DirectoryName;
                            }
                            if (radioButton4.Checked)
                            {
                                loginfos.boss = Path.GetFileName(Path.GetDirectoryName(info.DirectoryName));
                            }

                            loginfos.erstellDatum = info.CreationTime;
                            loginfos.pfad = info.FullName;

                            zaehelendeLogs.Add(loginfos); //Fügt der Liste hinzu
                        }
                    }
                    else
                    {
                        if (!info.DirectoryName.Contains("WvW"))
                        {
                            continue; //Überspringt alle Logs die nicht mit dem WvW zu tun haben
                        }
                        if (info.CreationTime > DateTime.Today.AddDays(-(int.Parse(txtBoxZeitraum.Text))))
                        {
                            //Muss noch ausimplementiert werden
                            //wvwlogs.Add(info.FullName);
                        }
                    }
                }
            }
            
            /*
            foreach (var item in zaehelendeLogs)
            {
                MessageBox.Show("Boss: "+item.boss);
            }
            */
            
            
            
            MessageBox.Show("Anzahl Logs "+zaehelendeLogs.Count);

            //Falls WvW ausgewählt wurde:
            if(combBoxContent.SelectedIndex == 1)
            {
                foreach (var item in wvwlogs)
                {
                    //txtBoxUploadedLogs.AppendText(UploadLog(item) + "\r\n");
                }
                return;
            }

            #region PvE
            int bossversuch = 0;
            string vorherigerBoss = "";
            int durchlauf = 0;
            LogInfos2 logInfos2 = new LogInfos2();
            if(zaehelendeLogs.Count > 0)
            {
                logInfos2.boss = zaehelendeLogs[0].boss;
            }
            else
            {
                MessageBox.Show("Es wurden keine Logs zum Hochladen gefunden!");
                return;
            }

            foreach (var item in zaehelendeLogs)
            {
                durchlauf += 1;
                if(vorherigerBoss == "")
                {
                    vorherigerBoss = item.boss;
                }
                if(vorherigerBoss == item.boss)
                {
                    bossversuch += 1;

                    logInfos2.versuch = bossversuch;
                    logInfos2.pfad = item.pfad;
                }
                if(vorherigerBoss != item.boss)
                {
                    bossversuch = 1;
                    vorherigerBoss = item.boss;

                    logInfos2 = new LogInfos2();
                    logInfos2.boss = item.boss;
                    logInfos2.versuch = 1;
                    logInfos2.pfad = item.pfad;
                }

                listeLogTries.Add(logInfos2);
                Console.WriteLine("Gesamtanzahl: "+zaehelendeLogs.Count);
            }

            #region Entferne die vorherigen Versuche
            string vorherigerBoss2 = "";
            List<LogInfos2> blacklist = new List<LogInfos2>();
            blacklist.Clear();
            IEnumerable<LogInfos2> sortDescending = from w in listeLogTries
                                                    orderby w.boss, w.versuch descending
                                                    select w;

            foreach (var log in sortDescending)
            {
                if (vorherigerBoss2 == log.boss)
                {
                    blacklist.Add(log);
                }
                else
                {
                    vorherigerBoss2 = log.boss;
                }
            }
            foreach (var item in blacklist)
            {
                listeLogTries.Remove(item);
            }
            #endregion

            #region Behandlung doppelter Upload
            //Verhindere Doppelten Upload
            if (radioButton1.Checked)
            {
                for (int i = listeLogTries.Count - 1; i >= 0; i--)
                {
                    if (LineExistsInFile(@Directory.GetCurrentDirectory() + "/Upgeloaded/logs.txt", listeLogTries[i].pfad))
                    {
                        listeLogTries.RemoveAt(i);
                    }
                }
            }
            #endregion

            listeLogTriesCount = listeLogTries.Count;
            prozentualerAnteilProLog = 0.0;
            if(listeLogTriesCount > 0)
            {
                prozentualerAnteilProLog = 100 / listeLogTriesCount;
            }
                
            #endregion //PVE
        }

        private void ErstelleVerzeichnisse()
        {
            //Gibt es das Verzeichnis? Wenn nein => Anlegen

            if (!Directory.Exists(Directory.GetCurrentDirectory() + "/Upgeloaded/"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Upgeloaded");
            }

            //Erstelle die Datei
            if (!File.Exists(Directory.GetCurrentDirectory() + "/Upgeloaded/logs.txt"))
            {
                var myFile = File.Create(Directory.GetCurrentDirectory() + "/Upgeloaded/logs.txt");
                myFile.Close();
            }
        }

        private bool LineExistsInFile(string filepath, string searchingline)
        {
            string aktuelleZeileText = "";
            StreamReader reader = new StreamReader(filepath);
            while (reader.Peek() >= 0)
            {
                aktuelleZeileText = reader.ReadLine();
                if (searchingline == aktuelleZeileText)
                {
                    return true;
                }
            }
            reader.Close();

            return false;
        }

        private void SchreibeBereitsUpgeloaded(string pfad)
        {
            #region Quelle
            //Quelle: https://docs.microsoft.com/de-de/dotnet/csharp/programming-guide/file-system/how-to-write-to-a-text-file
            #endregion

            //ErstelleVerzeichnisse();


            //Hänge an Datei an
            StreamWriter file = new StreamWriter(@Directory.GetCurrentDirectory() + "/Upgeloaded/logs.txt", true); //true steht für anhängen
            file.WriteLine(pfad);
            file.Close();
        }

        private void backgroundWorkerJobUplaod(LogInfos2 loginfo)
        {

            if (radioButton1.Checked) //Lade keine Logs mehrfach hoch!
            {
                if (LineExistsInFile(@Directory.GetCurrentDirectory() + "/Upgeloaded/logs.txt", loginfo.pfad)) //Wenn der Logs schon existiert
                {
                    MessageBox.Show("Log wurde übersprungen");
                    label4.Text = prozentualerAnteilProLog * (listeLogTriesCount - listeLogTries.Count) + "%";
                    return;
                }
            }
            string upload = UploadLog(loginfo.pfad);
            int maximaleUploadVersuche = 10;
            int aktuellerUploadVersuch = 1;
            while (upload == "" && aktuellerUploadVersuch <= maximaleUploadVersuche)
            {
                upload = UploadLog(loginfo.pfad);
                aktuellerUploadVersuch++;
            }
            //textBox1.AppendText(UploadLog(loginfo.pfad) + " " + loginfo.versuch + "\r\n");
            txtBoxUploadedLogs.AppendText(upload + " " + loginfo.versuch + "\r\n");
            SchreibeBereitsUpgeloaded(loginfo.pfad);
            if (listeLogTries.Count > 0)
                label4.Text = prozentualerAnteilProLog * (listeLogTriesCount - listeLogTries.Count) + "%";

        }

        private string UploadLog(string filepfad)
        {
            //filepfad angeben: bsp: C:\Users\Feex\Documents\Guild Wars 2\addons\arcdps\arcdps.cbtlogs\Tal-Wächter\Zhoemton\20191208-191004.zevtc
            filepfad = SlashUmkehren(filepfad);

            var client = new RestClient("https://dps.report/uploadContent?json=1");
            var request = new RestRequest();
            request.AddFile("file", filepfad);
            var response = client.Post(request);
            var content = response.Content; //raw content as string

            string loglink = "";
            loglink = content;

            try
            {
                loglink = loglink.Substring(loglink.IndexOf("dps.report")); //Schneidet den ersten Teil weg
                loglink = loglink.Substring(0, loglink.IndexOf("\","));
                loglink = loglink.Trim('\\');
            }
            catch (Exception Error)
            {
                loglink = "";
            }

            if (loglink != "")
            {
                //Clipboard.SetData(DataFormats.Text,(Object)loglink);
                //MessageBox.Show("Copied to Clipboard");
                /*
                 System.Threading.ThreadStateException: "Für den aktuellen Thread muss der STA-Modus (Single Thread Apartment) festgelegt werden, 
                 bevor OLE-Aufrufe ausgeführt werden können. Stellen Sie sicher, dass die Hauptfunktion mit STAThreadAttribute gekennzeichnet ist."
                 */
                loglink = BackSlashEntfernen(loglink);
                return "https://" + loglink;
            }
            else
            {
                return "";
            }


        }
        #region Nützlich
        private string BackSlashEntfernen(string Umtauschstring)
        {
            string umtausch = "";
            umtausch = Umtauschstring.Replace("\\", "");
            return umtausch;
        }

        private string SlashUmkehren(string Umtauschstring)
        {
            string umtausch = "";
            umtausch = Umtauschstring.Replace("\\", "/");
            return umtausch;
        }
        #endregion

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BefuelleListe();
            //MessageBox.Show("Anzhal "+listeLogTries.Count);

            double prozentualabgeschlossen = 0;
            foreach (var log in listeLogTries)
            {

                Console.WriteLine("Boss: " + log.boss + " und Versuch: " + log.versuch + " und Pfad: " + log.pfad);

                txtBoxUploadedLogs.AppendText(UploadLog(log.pfad) + " " + log.versuch + "\r\n");
                prozentualabgeschlossen = prozentualabgeschlossen + (100 / listeLogTries.Count);
                label4.Text = prozentualabgeschlossen + "%";
                progressBarUpload.Value = (int)prozentualabgeschlossen;
                Console.WriteLine("So viel: " + listeLogTries.Count);
                Console.WriteLine("Jeder Log hat so viel gewicht: " + (1 / listeLogTries.Count) * 100);
                Console.WriteLine(prozentualabgeschlossen + "%");
            }

            label4.Text = "100%";
            progressBarUpload.Value = 100;
            Update();
            MessageBox.Show("Alle Logs wurden hochgeladen");

        }

        private void UseDifferentListsForUpload(ref List<LogInfos2> liste)
        {
            //Aufsplitten
            List<LogInfos2> liste1 = new List<LogInfos2>();
            List<LogInfos2> liste2 = new List<LogInfos2>();
            List<LogInfos2> liste3 = new List<LogInfos2>();
            List<LogInfos2> liste4 = new List<LogInfos2>();

            //while()
            int anzahl = 0;
            anzahl = liste.Count()/4;

            for (int i = 0; i < anzahl; i++)
            {
                liste1.Add(liste[i]);
            }
            for (int i = anzahl; i < liste1.Count()+anzahl; i++)
            {
                liste2.Add(liste[i]);
            }
            for (int i = anzahl+liste2.Count(); i < liste1.Count()+liste2.Count() + anzahl; i++)
            {
                liste3.Add(liste[i]);
            }
            for (int i = anzahl+liste2.Count()+liste3.Count(); i < liste.Count(); i++)
            {
                liste4.Add(liste[i]);
            }
            //liste.Clear();

            while (true)
            {
                if (!backgroundWorker3.IsBusy & liste1.Count > 0)
                {
                    backgroundWorker3.RunWorkerAsync(liste1[0]);
                    liste1.RemoveAt(0);
                    //MessageBox.Show("Upload abgeschlossen");
                }

                if (!backgroundWorker4.IsBusy & liste2.Count > 0)
                {
                    backgroundWorker4.RunWorkerAsync(liste2[0]);
                    liste2.RemoveAt(0);
                }

                if (!backgroundWorker5.IsBusy & liste3.Count >0 )
                {
                    backgroundWorker5.RunWorkerAsync(liste3[0]);
                    liste3.RemoveAt(0);
                }
                if (!backgroundWorker6.IsBusy & liste4.Count > 0)
                {
                    backgroundWorker6.RunWorkerAsync(liste4[0]);
                    liste4.RemoveAt(0);
                }
                try
                {
                    Thread.Sleep(50);
                }
                catch (Exception Error)
                {
                    Console.WriteLine(Error.ToString());
                }
                //Console.WriteLine("listelogtries noch so viel "+listeLogTries.Count);
                //progressBar1.Value = (int)prozentualabgeschlossen;
                //this.Update();
                if (!backgroundWorker3.IsBusy & !backgroundWorker4.IsBusy & !backgroundWorker5.IsBusy & !backgroundWorker6.IsBusy)
                    break;
            }

        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            BefuelleListe();
            double prozentualabgeschlossen = 0.0;

            if (radioButton1.Checked) //Keine Logs doppelt
            {
                for (int i = listeLogTries.Count-1; i >= 0; i--)
                {
                    if (LineExistsInFile(@Directory.GetCurrentDirectory() + "/Upgeloaded/logs.txt", listeLogTries[i].pfad))
                    {
                        listeLogTries.RemoveAt(i);
                    }
                }
            }

            bool useOtherMethod = false;

            if(useOtherMethod){
                UseDifferentListsForUpload(ref listeLogTries);
            }
            else
            {
                while (true)
                {
                    if (!backgroundWorker3.IsBusy & listeLogTries.Count > 0)
                    {
                        Console.WriteLine("Vorher " + listeLogTries.Count);
                        backgroundWorker3.RunWorkerAsync(listeLogTries[0]);
                        listeLogTries.RemoveAt(0);
                        Console.WriteLine("Nachher " + listeLogTries.Count);
                        //MessageBox.Show("Upload abgeschlossen");
                    }

                    if (!backgroundWorker4.IsBusy & listeLogTries.Count > 0)
                    {
                        Console.WriteLine("Vorher " + listeLogTries.Count);
                        backgroundWorker4.RunWorkerAsync(listeLogTries[0]);
                        listeLogTries.RemoveAt(0);
                        Console.WriteLine("Nachher " + listeLogTries.Count);
                    }

                    if (!backgroundWorker5.IsBusy & listeLogTries.Count > 0)
                    {
                        Console.WriteLine("Vorher " + listeLogTries.Count);
                        backgroundWorker5.RunWorkerAsync(listeLogTries[0]);
                        listeLogTries.RemoveAt(0);
                        Console.WriteLine("Nachher " + listeLogTries.Count);
                    }
                    if (!backgroundWorker6.IsBusy & listeLogTries.Count > 0)
                    {
                        Console.WriteLine("Vorher " + listeLogTries.Count);
                        backgroundWorker6.RunWorkerAsync(listeLogTries[0]);
                        listeLogTries.RemoveAt(0);
                        Console.WriteLine("Nachher " + listeLogTries.Count);
                    }
                    try
                    {
                        Thread.Sleep(50);
                    }
                    catch (Exception Error)
                    {
                        Console.WriteLine(Error.ToString());
                    }
                    //Console.WriteLine("listelogtries noch so viel "+listeLogTries.Count);
                    //progressBar1.Value = (int)prozentualabgeschlossen;
                    //this.Update();
                    if (!backgroundWorker3.IsBusy & !backgroundWorker4.IsBusy & !backgroundWorker5.IsBusy & !backgroundWorker6.IsBusy & listeLogTries.Count == 0)
                        break;
                }
            }
            

            

            label4.Text = "100";
            progressBarUpload.Value = 100;
            Update();
            MessageBox.Show("Alle Logs wurden hochgeladen");
        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            LogInfos2 loginfo = new LogInfos2();
            loginfo = (LogInfos2)e.Argument;

            backgroundWorkerJobUplaod(loginfo);
        }

        private void backgroundWorker4_DoWork(object sender, DoWorkEventArgs e)
        {
            LogInfos2 loginfo = new LogInfos2();
            loginfo = (LogInfos2)e.Argument;

            backgroundWorkerJobUplaod(loginfo);
        }

        private void backgroundWorker5_DoWork(object sender, DoWorkEventArgs e)
        {
            LogInfos2 loginfo = new LogInfos2();
            loginfo = (LogInfos2)e.Argument;

            backgroundWorkerJobUplaod(loginfo);
        }

        private void backgroundWorker6_DoWork(object sender, DoWorkEventArgs e)
        {
            LogInfos2 loginfo = new LogInfos2();
            loginfo = (LogInfos2)e.Argument;

            backgroundWorkerJobUplaod(loginfo);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        double abgearbeitet = 0.0;
        private void backgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            abgearbeitet = prozentualerAnteilProLog + abgearbeitet;
            label4.Text = abgearbeitet +"%";
            progressBarUpload.Value = (int)abgearbeitet;
        }

        private void backgroundWorker4_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            abgearbeitet = prozentualerAnteilProLog + abgearbeitet;
            label4.Text = abgearbeitet + "%";
            progressBarUpload.Value = (int)abgearbeitet;
        }

        private void backgroundWorker5_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            abgearbeitet = prozentualerAnteilProLog + abgearbeitet;
            label4.Text = abgearbeitet + "%";
            progressBarUpload.Value = (int)abgearbeitet;
        }

        private void backgroundWorker6_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            abgearbeitet = prozentualerAnteilProLog + abgearbeitet;
            label4.Text = abgearbeitet + "%";
            progressBarUpload.Value = (int)abgearbeitet;
        }

        public FileSystemWatcher fsW;

        private void btnWatchDirectory_Click(object sender, EventArgs e)
        {
            ordnerUeberwachungLaueft = !ordnerUeberwachungLaueft;
            fsW = new FileSystemWatcher();
            fsW.Path = txtBoxLogOrdner.Text;
            //fsW.Filter = "*.evtc|*.zevtc";
            fsW.Filter = "*.zevtc";

            fsW.IncludeSubdirectories = true;

            // Events definieren
            fsW.Changed += new FileSystemEventHandler(fsW_Changed);
            fsW.Created += new FileSystemEventHandler(fsW_Created);
            fsW.Deleted += new FileSystemEventHandler(fsW_Deleted);
            fsW.Renamed += new RenamedEventHandler(fsW_Renamed);

            //Passe den Anzeigetext an
            if (ordnerUeberwachungLaueft)
            {
                btnWatchDirectory.Text = "Stoppe Überwachung";
                Update();
            }
            else
            {
                btnWatchDirectory.Text = "Starte Überwachung";
                Update();
            }
        }

        // Handler für alle Events
        void fsW_Renamed(object sender, RenamedEventArgs e)
        {
            MessageBox.Show("Umbenannt: " + e.Name);
        }

        void fsW_Changed(object sender, FileSystemEventArgs e)
        {
            //MessageBox.Show("Umbenannt: " + e.Name);
        }

        void fsW_Deleted(object sender, FileSystemEventArgs e)
        {
            //MessageBox.Show("Gelöscht: " + e.Name);
        }

        void fsW_Created(object sender, FileSystemEventArgs e)
        {
            if (ordnerUeberwachungLaueft)
            {
                MessageBox.Show("Log: "+txtBoxLogOrdner.Text+e.Name);
                txtBoxUploadedLogs.AppendText(UploadLog(txtBoxLogOrdner.Text+e.Name));
                
            }
        }

        private void btnUploadOneThread_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void btnUploadMultipleThreads_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker2.IsBusy)
            {
                backgroundWorker2.RunWorkerAsync();
            }
        }

        private void txtBoxZeitraum_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void nachUpdatesPrüfenToolStripMenuItem_Click(object sender, EventArgs e)
        {
                //ggfs. später versuchen über md5 hash zu ermitteln
            
            //Logik:
            string aktuelleVersion = ""; //Muss ausgelesen werden aus aktueller Datei
            XmlDocument doc = new XmlDocument();
            doc.Load("Version.xml");

            XmlElement root = doc.DocumentElement;

            string versionAktuell1 = "";
            versionAktuell1 = root.FirstChild.InnerXml;
            aktuelleVersion = versionAktuell1;

            string neueVersion = holeNeueVersion();

            if(neueVersion != aktuelleVersion)
            {
                MessageBox.Show("Es gibt eine neue Version.\nNeu: "+neueVersion+"\nAktuelle Version: "+aktuelleVersion);
                //Ggfs. später eine Möglichkeit anbieten zu Downloaden und dauerhaft zu speichern.
            }
            else
            {
                MessageBox.Show("Du besitzt schon die aktuellste Version");
            }
        }

        private string holeNeueVersion()
        {
            string neueVersion = "";
            #region File Download
            WebClient webclient = new WebClient();
            string remoteUri = "https://github.com/Borovrel/LogUploader4Glenna/archive/master.zip";
            webclient.DownloadFile(new Uri(remoteUri), "Uploader4Glenna.zip");
            Console.WriteLine("File fertig gedownloadet.");
            #endregion

            #region Vergleich
            string zipPath = "Uploader4Glenna.zip";
            string ExtractPath = Directory.GetCurrentDirectory()+"/Uploader4GlennaExtract";
            ZipFile.ExtractToDirectory(zipPath, ExtractPath);

            XmlDocument doc = new XmlDocument();
            doc.Load(Directory.GetCurrentDirectory()+ "/Uploader4GlennaExtract/LogUploader4Glenna-master/LogUploader4Glenna/Version.xml");
            XmlElement root = doc.DocumentElement;

            neueVersion = root.FirstChild.InnerXml;
            #endregion

            #region Löschen
            try
            {
                File.Delete(Directory.GetCurrentDirectory()+ "\\Uploader4Glenna.zip");
                Directory.Delete(ExtractPath,true);
            }catch(Exception Error)
            {
                MessageBox.Show("Beim Löschen ist etwas schiefgelaufen: "+Error);
            }
            #endregion

            return neueVersion;


        }

        private void txtBoxMaxLogSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void backWorkerWatching_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void btnUploadLastLog_Click(object sender, EventArgs e)
        {
            List<LogInfos> zaehelendeLogs = new List<LogInfos>();
            zaehelendeLogs.Clear();

            ListeAndersBefuellen(ref zaehelendeLogs);
            //MessageBox.Show("Anzahl "+zaehelendeLogs.Count);
            List<LogInfos> sortedList = zaehelendeLogs.OrderByDescending(o => o.erstellDatum).ToList();
            if (sortedList.Count > 0)
            {
                //MessageBox.Show(sortedList[0].boss+" mit Datum "+sortedList[0].erstellDatum);
                txtBoxUploadedLogs.AppendText(UploadLog(sortedList[0].pfad) + "\r\n");
                MessageBox.Show("Der letzte Log wurde hochgeladen. (Ohne benötigte Versuche!)");
            }
                

        }
    }
}
