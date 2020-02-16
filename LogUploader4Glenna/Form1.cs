using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            
        }

        private void BefuelleListe()
        {
            var files = Directory.GetFiles(txtBoxLogOrdner.Text, "*.zevtc", SearchOption.AllDirectories);

            List<LogInfos> zaehelendeLogs = new List<LogInfos>();
            zaehelendeLogs.Clear();

            List<LogInfos> wvwlogs = new List<LogInfos>();
            wvwlogs.Clear();

            txtBoxUploadedLogs.Clear();
            foreach (string file in files)
            {
                var info = new FileInfo(file);
                if(combBoxContent.SelectedIndex == 0)
                {
                    //Nur PvE Logs
                    if (info.DirectoryName.Contains("WvW"))
                    {
                        continue; //Überspringt alle Einträge, welche im WvW Pfad liegen
                    }
                    if(info.Length < 100)
                    {
                        continue; //überspringt alle Einträge, deren Größe kleiner als 100 KB sind
                    }
                    if(info.CreationTime > DateTime.Today.AddDays(-(int.Parse(txtBoxZeitraum.Text))))
                    {
                        //Das Erstelldatum muss im Zeitraum liegen
                        LogInfos loginfos = new LogInfos();

                        loginfos.boss = Path.GetFileName(Path.GetDirectoryName(info.DirectoryName));
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
                Console.WriteLine("So viel: " + listeLogTries.Count);
                Console.WriteLine("Jeder Log hat so viel gewicht: " + (1 / listeLogTries.Count) * 100);
                Console.WriteLine(prozentualabgeschlossen + "%");
            }

            label4.Text = "100%";
            MessageBox.Show("Alle Logs wurden hochgeladen");

        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            BefuelleListe();
            double prozentualabgeschlossen = 0;

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
                    backgroundWorker4.RunWorkerAsync(listeLogTries[0]);
                    listeLogTries.RemoveAt(0);
                }

                if (!backgroundWorker5.IsBusy & listeLogTries.Count > 0)
                {
                    backgroundWorker5.RunWorkerAsync(listeLogTries[0]);
                    listeLogTries.RemoveAt(0);
                }
                if (!backgroundWorker6.IsBusy & listeLogTries.Count > 0)
                {
                    backgroundWorker6.RunWorkerAsync(listeLogTries[0]);
                    listeLogTries.RemoveAt(0);
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

            label4.Text = "100";
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

        private void backgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void backgroundWorker4_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void backgroundWorker5_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void backgroundWorker6_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void btnWatchDirectory_Click(object sender, EventArgs e)
        {
            ordnerUeberwachungLaueft = !ordnerUeberwachungLaueft;

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
    }
}
