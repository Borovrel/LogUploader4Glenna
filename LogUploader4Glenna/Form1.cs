using Newtonsoft.Json;
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

            textBox1.Text = txtBoxLogOrdner.Text;
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
                        switch (combBoxContent.SelectedItem)
                        {
                            case "PvE Raidbosse":
                                if(item.Contains("Golem") || item.Contains("WvW")|| item.Contains("Siax") || item.Contains("Mama") || item.Contains("Enso") || item.Contains("Skorvald") || item.Contains("Artsariiv") || item.Contains("Arkk"))
                                {
                                    continue;
                                }
                                break;
                            case "PvE Übungsbereich":
                                if (!item.Contains("Golem"))
                                {
                                    continue;
                                }
                                break;

                            case "WvW":
                                if (!item.Contains("WvW"))
                                {
                                    continue;
                                }
                                break;

                            case "Fraktale":
                                if (!item.Contains("Siax") || !item.Contains("Mama") || !item.Contains("Enso") || !item.Contains("Skorvald") || !item.Contains("Artsariiv") || !item.Contains("Arkk"))
                                {
                                    continue;
                                }
                                break;
                            default:
                                //MessageBox.Show("Unerwartet");
                                break;
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
                        //if (!item.Contains("WvW"))
                        //{
                        //    continue; //Überspringt alle Logs die nicht mit dem WvW zu tun haben
                        //}
                        //if (info.CreationTime > DateTime.Today.AddDays(-(int.Parse(txtBoxZeitraum.Text))))
                        //{
                        //    //Muss noch ausimplementiert werden
                        //    //wvwlogs.Add(info.FullName);
                        //}
                        //if(combBoxContent.SelectedItem == "PvE Übungsbereich")
                        //{
                        //
                        //}
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



            //MessageBox.Show("Anzahl Logs " + zaehelendeLogs.Count);

            //Falls WvW ausgewählt wurde:
            if (combBoxContent.SelectedIndex == 1)
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
            if (zaehelendeLogs.Count > 0)
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
                if (vorherigerBoss == "")
                {
                    vorherigerBoss = item.boss;
                }
                if (vorherigerBoss == item.boss)
                {
                    bossversuch += 1;

                    logInfos2.versuch = bossversuch;
                    logInfos2.pfad = item.pfad;
                }
                if (vorherigerBoss != item.boss)
                {
                    bossversuch = 1;
                    vorherigerBoss = item.boss;

                    logInfos2 = new LogInfos2();
                    logInfos2.boss = item.boss;
                    logInfos2.versuch = 1;
                    logInfos2.pfad = item.pfad;
                }

                listeLogTries.Add(logInfos2);
                Console.WriteLine("Gesamtanzahl: " + zaehelendeLogs.Count);
            }
            #endregion
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
            if (listeLogTriesCount > 0)
            {
                prozentualerAnteilProLog = 100 / listeLogTriesCount;
            }

            //PVE
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

        private void UploadV3()
        {
            BefuelleListe();
            double prozentualabgeschlossen = 0;
            /*
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
             */
            if (listeLogTriesCount >= 4)
            {
                //Mindestens 4 Logs
                List<LogInfos2> myFirstList = new List<LogInfos2>();
                #region Listen befüllen
                for (int i = 0; i < listeLogTriesCount; i++)
                {
                    if (i % 4 == 0)
                    {
                        myFirstList.Add(listeLogTries[i]);
                    }
                }
                List<LogInfos2> mySectList = new List<LogInfos2>();
                for (int i = 0; i < listeLogTriesCount; i++)
                {
                    if (i % 4 == 1)
                    {
                        mySectList.Add(listeLogTries[i]);
                    }
                }
                List<LogInfos2> myThirdList = new List<LogInfos2>();
                for (int i = 0; i < listeLogTriesCount; i++)
                {
                    if (i % 4 == 2)
                    {
                        myThirdList.Add(listeLogTries[i]);
                    }
                }
                List<LogInfos2> myFourthList = new List<LogInfos2>();
                for (int i = 0; i < listeLogTriesCount; i++)
                {
                    if (i % 4 == 3)
                    {
                        myFourthList.Add(listeLogTries[i]);
                    }
                }
                #endregion
                Thread firstThread = new Thread(delegate () { UploadV3_2(myFirstList); });
                firstThread.Start();
                Thread secThread = new Thread(delegate () { UploadV3_2(mySectList); });
                secThread.Start();
                Thread thirdThread = new Thread(delegate () { UploadV3_2(myThirdList); });
                thirdThread.Start();
                Thread fourthThread = new Thread(delegate () { UploadV3_2(myFourthList); });
                fourthThread.Start();
            }
            if ((4 > listeLogTriesCount) && (listeLogTriesCount >= 2))
            {
                //Maximal 3 Logs
                List<LogInfos2> myFirstList = new List<LogInfos2>();
                #region Listen befüllen
                for (int i = 0; i < listeLogTriesCount; i++)
                {
                    if (i % 4 == 0)
                    {
                        myFirstList.Add(listeLogTries[i]);
                    }
                }
                List<LogInfos2> mySectList = new List<LogInfos2>();
                for (int i = 0; i < listeLogTriesCount; i++)
                {
                    if (i % 4 == 1)
                    {
                        mySectList.Add(listeLogTries[i]);
                    }
                }
                List<LogInfos2> myThirdList = new List<LogInfos2>();
                for (int i = 0; i < listeLogTriesCount; i++)
                {
                    if (i % 4 == 2)
                    {
                        myThirdList.Add(listeLogTries[i]);
                    }
                }
                #endregion
                Thread firstThread = new Thread(delegate () { UploadV3_2(myFirstList); });
                firstThread.Start();
                Thread secThread = new Thread(delegate () { UploadV3_2(mySectList); });
                secThread.Start();
                Thread thirdThread = new Thread(delegate () { UploadV3_2(myThirdList); });
                thirdThread.Start();
            }
        }

        private void UploadV3_2(List<LogInfos2> logs)
        {
            foreach (LogInfos2 item in logs)
            {
                if (txtBoxUploadedLogs.InvokeRequired)
                {
                    MethodInvoker mi = delegate
                    {
                        txtBoxUploadedLogs.AppendText(UploadLog(item.pfad) + " " + item.versuch + "\r\n");
                        prozentualabgeschlossen = prozentualabgeschlossen + (100 / listeLogTries.Count);
                        label4.Text = prozentualabgeschlossen + "%";
                        progressBarUpload.Value = (int)prozentualabgeschlossen;
                    };
                    Invoke(mi);
                }
                else
                {
                    txtBoxUploadedLogs.AppendText(UploadLog(item.pfad) + " " + item.versuch + "\r\n");
                    prozentualabgeschlossen = prozentualabgeschlossen + (100 / listeLogTries.Count);
                    label4.Text = prozentualabgeschlossen + "%";
                    progressBarUpload.Value = (int)prozentualabgeschlossen;
                }
            }
            //prozentualerAnteilProLog
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


            ReportAusgabe reportAusgabe = JsonConvert.DeserializeObject<ReportAusgabe>(content);
            if(combBoxContent.SelectedItem == "PvE Übungsbereich")
            {

                return reportAusgabe.permalink;
            }
            else
            {
                if (reportAusgabe.encounter.boss.Contains("Kitty Golem"))
                {
                    return "";
                }
            }
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
            anzahl = liste.Count() / 4;

            for (int i = 0; i < anzahl; i++)
            {
                liste1.Add(liste[i]);
            }
            for (int i = anzahl; i < liste1.Count() + anzahl; i++)
            {
                liste2.Add(liste[i]);
            }
            for (int i = anzahl + liste2.Count(); i < liste1.Count() + liste2.Count() + anzahl; i++)
            {
                liste3.Add(liste[i]);
            }
            for (int i = anzahl + liste2.Count() + liste3.Count(); i < liste.Count(); i++)
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

                if (!backgroundWorker5.IsBusy & liste3.Count > 0)
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
                for (int i = listeLogTries.Count - 1; i >= 0; i--)
                {
                    if (LineExistsInFile(@Directory.GetCurrentDirectory() + "/Upgeloaded/logs.txt", listeLogTries[i].pfad))
                    {
                        listeLogTries.RemoveAt(i);
                    }
                }
            }

            bool useOtherMethod = false;

            if (useOtherMethod)
            {
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
            label4.Text = abgearbeitet + "%";
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
                MessageBox.Show("Log: " + txtBoxLogOrdner.Text + e.Name);
                txtBoxUploadedLogs.AppendText(UploadLog(txtBoxLogOrdner.Text + e.Name));

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

            if (neueVersion != aktuelleVersion)
            {
                MessageBox.Show("Es gibt eine neue Version.\nNeu: " + neueVersion + "\nAktuelle Version: " + aktuelleVersion);
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
            string ExtractPath = Directory.GetCurrentDirectory() + "/Uploader4GlennaExtract";
            ZipFile.ExtractToDirectory(zipPath, ExtractPath);

            XmlDocument doc = new XmlDocument();
            doc.Load(Directory.GetCurrentDirectory() + "/Uploader4GlennaExtract/LogUploader4Glenna-master/LogUploader4Glenna/Version.xml");
            XmlElement root = doc.DocumentElement;

            neueVersion = root.FirstChild.InnerXml;
            #endregion

            #region Löschen
            try
            {
                File.Delete(Directory.GetCurrentDirectory() + "\\Uploader4Glenna.zip");
                Directory.Delete(ExtractPath, true);
            }
            catch (Exception Error)
            {
                MessageBox.Show("Beim Löschen ist etwas schiefgelaufen: " + Error);
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

        private void ChangeLayoutPanel(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            txtBoxLogOrdner.Text = textBox1.Text;
        }

        private void txtBoxLogOrdner_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = txtBoxLogOrdner.Text;
        }

        private void btnUploadPanel2_Click(object sender, EventArgs e)
        {
            BefuelleListe();
            //if(checkBox1)
            //checkbox1-30
            if (listeLogTries.Count == 0)
                return;

            LogInfos2 loginfo2 = new LogInfos2();

            #region Wing 1-4
            if (!cBoxVG.Checked)
            {
                //Muss später noch verbessert werden
                for (int i = 0; i < listeLogTries.Count; i++)
                {
                    if (listeLogTries[i].boss.Contains("Tal-Wächter"))
                    {
                        listeLogTries.RemoveAt(i);
                        i--;
                    }

                }
                //if(listeLogTries.Find(t => t.boss == "Tal-Wächter").FirstOrDefault())
                //{
                //
                //}
                //if(listeLogTries.Remove(new LogInfos2())
            }
            if (!cBoxGorse.Checked)
            {
                Panel2SubRoutineBosseFiltern("Gorseval der Facettenreiche");
            }
            if (!cBoxSab.Checked)
            {
                Panel2SubRoutineBosseFiltern("Sabetha die Saboteurin");
            }
            if (!cBoxSloth.Checked)
            {
                Panel2SubRoutineBosseFiltern("Faultierion");
            }
            if (!cBoxTrio.Checked)
            {
                Panel2SubRoutineBosseFiltern("Berg");
            }
            if (!cBoxMatt.Checked)
            {
                Panel2SubRoutineBosseFiltern("Matthias Gabrel");
            }
            if (!cBoxKC.Checked)
            {
                Panel2SubRoutineBosseFiltern("Festenkonstrukt");
            }
            if (!cBoxTC.Checked)
            {
                Panel2SubRoutineBosseFiltern("Spukende Statue");
            }
            if (!cBoxXera.Checked)
            {
                Panel2SubRoutineBosseFiltern("Xera");
            }
            if (!cBoxCairn.Checked)
            {
                Panel2SubRoutineBosseFiltern("Cairn der Unbeugsame");
            }
            if (!cBoxMo.Checked)
            {
                Panel2SubRoutineBosseFiltern("Mursaat-Aufseher");
            }
            if (!cBoxSam.Checked)
            {
                Panel2SubRoutineBosseFiltern("Samarog");
            }
            if (!cBoxDeimos.Checked)
            {
                Panel2SubRoutineBosseFiltern("Deimos");
            }
            #endregion
            #region Wing 5-7
            if (!cBoxSH.Checked)
            {
                Panel2SubRoutineBosseFiltern("Seelenloser Schrecken");
            }
            if (!cBoxRiver.Checked)
            {
                Panel2SubRoutineBosseFiltern("Desmina");
            }
            if (!cBoxKing.Checked)
            {
                Panel2SubRoutineBosseFiltern("Bezwungener König");
            }
            if (!cBoxEater.Checked)
            {
                Panel2SubRoutineBosseFiltern("Seelenverzehrer");
            }
            if (!cBoxEyes.Checked)
            {
                Panel2SubRoutineBosseFiltern("Auge des Schicksals");
                Panel2SubRoutineBosseFiltern("Auge des Urteils");
            }
            if (!cBoxDhuum.Checked)
            {
                Panel2SubRoutineBosseFiltern("Dhuum");
            }
            if (!cBoxCA.Checked)
            {
                Panel2SubRoutineBosseFiltern("Beschworene Verschmelzung");
            }
            if (!cBoxTwins.Checked)
            {
                Panel2SubRoutineBosseFiltern("Nikare");
            }
            if (!cBoxQadim.Checked)
            {
                Panel2SubRoutineBosseFiltern("Qadim");
            }
            if (!cBoxAdina.Checked)
            {
                Panel2SubRoutineBosseFiltern("Kardinal Adina");
            }
            if (!cBoxSabir.Checked)
            {
                Panel2SubRoutineBosseFiltern("Kardinal Sabir");
            }
            if (!cBoxQadim2.Checked)
            {
                Panel2SubRoutineBosseFiltern("Qadim der Unvergleichliche");
            }
            #endregion
            #region Angriffsmissionen
            if (!cBoxBoneskinner.Checked)
            {
                Panel2SubRoutineBosseFiltern("Knochenhäuter");
            }
            if (!cBoxIC.Checked)
            {
                Panel2SubRoutineBosseFiltern("Eisbrut-Konstrukt");
            }
            if (!cBoxFraenir.Checked)
            {
                Panel2SubRoutineBosseFiltern("Fraenir Jormags");
            }
            if (!cBoxFallen.Checked)
            {
                Panel2SubRoutineBosseFiltern("Klaue der Gefallenen"); //Kann mich hier auch irren
            }
            if (!cBoxWhisper.Checked)
            {
                Panel2SubRoutineBosseFiltern("Geflüster des Jormag");
            }
            #endregion

            MessageBox.Show("Übrig: " + listeLogTries.Count.ToString());

            double prozentualabgeschlossen = 0;


            #region

            ListeLogsForSecondThread = listeLogTries;

            //if (ListeLogsForSecondThread.Count % 2 == 0)
            //{
            //    //Es bleibt kein Rest übrig
            //    ersterTeil = (ListeLogsForSecondThread.Count / 2);
            //}
            //else
            //{
            //    ersterTeil = (ListeLogsForSecondThread.Count / 2) - 1;
            //}
            //zweiterTeil = ListeLogsForSecondThread.Count - ersterTeil;
            //
            //ThreadStart threadStart = new ThreadStart(SecondThread);
            //Thread mythread = new Thread(threadStart);
            //mythread.Start();
            //
            //ThreadStart threadStart2 = new ThreadStart(ThirdThread);
            //Thread mythread2 = new Thread(threadStart2);
            //mythread2.Start();
            thS1 = new ThreadStart(UploadLogsSelected);
            th1 = new Thread(thS1);
            th1.Start();

            //MessageBox.Show("Alle Logs wurden hochgeladen");
            #endregion

        }
        Thread th1;
        ThreadStart thS1;

        private void UploadLogsSelected()
        {
            string Uploadtext = "";
            prozentualabgeschlossen = 0;
            tabControl1.SelectTab(0);

            foreach (var log in listeLogTries)
            {
                //if (txtBoxUploadedLogs.InvokeRequired)
                //{
                Uploadtext = UploadLog(log.pfad) + " " + log.versuch + "\r\n";
                if (txtBoxUploadedLogs.InvokeRequired)
                    txtBoxUploadedLogs.Invoke((MethodInvoker)delegate {
                        //Console.WriteLine("Boss: " + log.boss + " und Versuch: " + log.versuch + " und Pfad: " + log.pfad);

                        txtBoxUploadedLogs.AppendText(Uploadtext);
                        prozentualabgeschlossen = prozentualabgeschlossen + (100 / listeLogTries.Count);
                        label4.Text = prozentualabgeschlossen + "%";
                        progressBarUpload.Value = (int)prozentualabgeschlossen;
                        Console.WriteLine("So viel: " + listeLogTries.Count);
                        Console.WriteLine("Jeder Log hat so viel gewicht: " + (1 / listeLogTries.Count) * 100);
                        Console.WriteLine(prozentualabgeschlossen + "%");
                    });
                //}
                //Console.WriteLine("Boss: " + log.boss + " und Versuch: " + log.versuch + " und Pfad: " + log.pfad);
                //
                //txtBoxUploadedLogs.AppendText(UploadLog(log.pfad) + " " + log.versuch + "\r\n");
                //prozentualabgeschlossen = prozentualabgeschlossen + (100 / listeLogTries.Count);
                //label4.Text = prozentualabgeschlossen + "%";
                //progressBarUpload.Value = (int)prozentualabgeschlossen;
                //Console.WriteLine("So viel: " + listeLogTries.Count);
                //Console.WriteLine("Jeder Log hat so viel gewicht: " + (1 / listeLogTries.Count) * 100);
                //Console.WriteLine(prozentualabgeschlossen + "%");
            }
            //Nur sinnvoll wenn mit den 2 Threads gearbeitet wird
            //while (true)
            //{
            //    Thread.Sleep(100);
            //    if (mythread.ThreadState == ThreadState.Stopped)
            //        break;
            //}

            label4.Text = "100%";
            progressBarUpload.Value = 100;
            Update();
        }

        double ersterTeil = 0;
        double zweiterTeil = 0;
        List<LogInfos2> ListeLogsForSecondThread = new List<LogInfos2>();
        private void SecondThread()
        {
            for (int i = 0; i < ersterTeil; i++)
            {
                if (txtBoxUploadedLogs.InvokeRequired)
                {
                    txtBoxUploadedLogs.Invoke((MethodInvoker)delegate {
                        Console.WriteLine("Boss: " + listeLogTries[i].boss + " und Versuch: " + listeLogTries[i].versuch + " und Pfad: " + listeLogTries[i].pfad);

                        txtBoxUploadedLogs.AppendText(UploadLog(listeLogTries[i].pfad) + " " + listeLogTries[i].versuch + "\r\n");
                        prozentualabgeschlossen = prozentualabgeschlossen + (100 / listeLogTries.Count);
                        label4.Text = prozentualabgeschlossen + "%";
                        progressBarUpload.Value = (int)prozentualabgeschlossen;
                        Console.WriteLine("So viel: " + listeLogTries.Count);
                        Console.WriteLine("Jeder Log hat so viel gewicht: " + (1 / listeLogTries.Count) * 100);
                        Console.WriteLine(prozentualabgeschlossen + "%");
                    });
                }
            }


        }
        double prozentualabgeschlossen = 0;
        private void ThirdThread()
        {

            for (int i = (int)ersterTeil; i < ListeLogsForSecondThread.Count; i++)
            {
                if (txtBoxUploadedLogs.InvokeRequired)
                {
                    txtBoxUploadedLogs.Invoke((MethodInvoker)delegate {
                        Console.WriteLine("Boss: " + listeLogTries[i].boss + " und Versuch: " + listeLogTries[i].versuch + " und Pfad: " + listeLogTries[i].pfad);

                        txtBoxUploadedLogs.AppendText(UploadLog(listeLogTries[i].pfad) + " " + listeLogTries[i].versuch + "\r\n");
                        prozentualabgeschlossen = prozentualabgeschlossen + (100 / listeLogTries.Count);
                        label4.Text = prozentualabgeschlossen + "%";
                        progressBarUpload.Value = (int)prozentualabgeschlossen;
                        Console.WriteLine("So viel: " + listeLogTries.Count);
                        Console.WriteLine("Jeder Log hat so viel gewicht: " + (1 / listeLogTries.Count) * 100);
                        Console.WriteLine(prozentualabgeschlossen + "%");
                    });
                }
            }
        }

        private void Panel2SubRoutineBosseFiltern(string Bossname)
        {
            for (int i = 0; i < listeLogTries.Count; i++)
            {
                if (listeLogTries[i].boss.Contains(Bossname))
                {
                    listeLogTries.RemoveAt(i);
                    i--;
                    return;
                }

            }
        }

        private void btnThread_Click(object sender, EventArgs e)
        {
            UploadV3();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.SetDesktopLocation(Properties.Settings.Default.WindowsPosX,Properties.Settings.Default.WindowsPosY);
            Top = Properties.Settings.Default.WindowsPosY;
            Left = Properties.Settings.Default.WindowsPosX;
            if(Properties.Settings.Default.Ordnerstruktur == 0)
            {
                radioButton3.Checked = true;
            }
            if(Properties.Settings.Default.Ordnerstruktur == 1)
            {
                radioButton4.Checked = true;
            }
            Update();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.WindowsPosX = Left;
            Properties.Settings.Default.WindowsPosY = Top;
            if (radioButton3.Checked)
            {
                Properties.Settings.Default.Ordnerstruktur = 0;
            }
            if (radioButton4.Checked)
            {
                Properties.Settings.Default.Ordnerstruktur = 1;
            }
            Properties.Settings.Default.Save();
        }
    } 
}
