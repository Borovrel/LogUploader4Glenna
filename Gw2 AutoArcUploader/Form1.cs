using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gw2_AutoArcUploader
{
    public partial class Form1 : Form
    {
        #region Threading
        //Quelle: https://www.youtube.com/watch?v=9AIApJmbulY&ab_channel=CeeSharp
        public delegate void delUpdateUITextBox(string text);
        ThreadStart threadStart;
        Thread myUpdateThread;

        #endregion

        public string userToken = "";

        public List<LogInfos2> hochgeladeneLogs;

        public Form1()
        {
            string environmentOrdner=Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            InitializeComponent();

            txtBoxLogOrdner.Text = environmentOrdner+ "\\Guild Wars 2\\addons\\arcdps\\arcdps.cbtlogs\\";

            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);

            CheckForIllegalCrossThreadCalls = false;
            hochgeladeneLogs = new List<LogInfos2>();


            ErstelleVerzeichnisse();
        }

        public FileSystemWatcher fsW;

        private void btnStart_Click(object sender, EventArgs e)
        {
            fsW = new FileSystemWatcher();
            fsW.Path = txtBoxLogOrdner.Text;
            fsW.Filter = "*.zevtc";

            fsW.IncludeSubdirectories = true;

            // Events definieren
            fsW.Changed += new FileSystemEventHandler(fsW_Changed);
            fsW.Created += new FileSystemEventHandler(fsW_Created);
            fsW.Deleted += new FileSystemEventHandler(fsW_Deleted);
            fsW.Renamed += new RenamedEventHandler(fsW_Renamed);

            // Filesystemwatcher aktivieren
            fsW.EnableRaisingEvents = true;

            backgroundWorker1.RunWorkerAsync();
        }

        // Handler für alle Events
        void fsW_Renamed(object sender, RenamedEventArgs e)
        {
            //MessageBox.Show("Umbenannt: " + e.Name);
        }

        void fsW_Deleted(object sender, FileSystemEventArgs e)
        {
            //MessageBox.Show("Gelöscht: " + e.Name);
        }

        void fsW_Created(object sender, FileSystemEventArgs e)
        {
            //Muss hochladen
            //MessageBox.Show(txtBoxLogOrdner.Text+e.Name);
            string loglink = UploadLog(txtBoxLogOrdner.Text+e.Name);
            neuertext = loglink;
            //backgroundWorker1.RunWorkerAsync();
            //textBox1.AppendText(loglink);
            /*
             * Ungültiger threadübergreifender Vorgang: Der Zugriff auf das Steuerelement textBox1 erfolgte von einem anderen Thread als dem Thread, für den es erstellt wurde
             */

            return;
            #region Weg mit dem Zeug
            WebRequest requestObjGet = WebRequest.Create("https://dps.report/getUserToken");
            requestObjGet.Method = "GET";
            HttpWebResponse responseObjGet = null;
            responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();

            string strresulttest = null;
            using (Stream stream = responseObjGet.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                strresulttest = sr.ReadToEnd();
                sr.Close();
            }
            userToken = strresulttest;
            MessageBox.Show("Erstellt: " + e.Name); //Dateipfad + Name
            MessageBox.Show("Token: "+strresulttest); //Ergebnis des eingelesenen Tokens

            //UploadStuff(txtBoxLogOrdner.Text + e.Name);

            //Hochladen
            string strUrl = String.Format("https://dps.report/uploadContent?json=1");
            WebRequest requestObjPost = WebRequest.Create(strUrl);
            requestObjPost.Method = "POST";
            requestObjPost.ContentType = "application/json";

            string postData = "{json=1,rotation_weap1=1,generator=ei,userToken="+userToken+"}";
            Object jsonObject = new Object();
            jsonObject = "https://dps.report/uploadContent?json=1";



            //return;
            using (var streamWriter = new StreamWriter(requestObjPost.GetRequestStream()))
            {
                streamWriter.Write(postData);
                streamWriter.Flush();
                streamWriter.Close();

                //Unzulässig 403 Fehler
                var httpResponse = (HttpWebResponse)requestObjPost.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result2 = streamReader.ReadToEnd();
                    MessageBox.Show(result2.ToString());
                }
            }
            #endregion
        }

        public string UploadFileOther()
        {
            //Hochladen
            string strUrl = String.Format("https://dps.report/uploadContent/json=1");
            WebRequest requestObjPost = WebRequest.Create(strUrl);
            requestObjPost.Method = "POST";
            requestObjPost.ContentType = "application/json";

            string postData = "{json=1,rotation_weap1=1,generator=ei,userToken=" + userToken + "}";
            Object jsonObject = new Object();
            jsonObject = "https://dps.report/uploadContent?json=1";

            /*
             */

            return "";
        }

        void fsW_Changed(object sender, FileSystemEventArgs e)
        {
            //MessageBox.Show("Geändert: " + e.Name);
        }

        #region Brauch ich nicht
        public string UploadFile()
        {
            string file = "C:\\User\\Feex\\Documents\\Guild Wars 2\\addons\\arcdps\\arcdps.cbtlogs\\Dhuum\\Zhoemton\\20200119-204856.zevtc";
            string file2 = @"C:\Users\Feex\Documents\Guild Wars 2\addons\arcdps\arcdps.cbtlogs\Dhuum\Zhoemton\20200205-200535.zevtc";
            //Python:
            //Request.session als Session()
            //session.mount 
            //HttpStatusCode.

            //WebRequest

            #region AndererVersuch

            byte[] bytes2;
            using(FileStream fs = new FileStream(file2, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = System.IO.File.ReadAllBytes(file2);
                fs.Read(bytes, 0, System.Convert.ToInt32(fs.Length));

                fs.Close();
                bytes2 = bytes;
                MessageBox.Show(bytes.ToString());
            }

            MessageBox.Show(bytes2.ToString());

            //WebRequest webR = WebRequest.Create(file2);
            //WebRequest webR = WebRequest.Create("https://dps.report/uploadContent/json=1");
            WebRequest webR = WebRequest.Create("https://dps.report/uploadContent?json=1&rotation_weap1=1&generator=rh");
            webR.Method = "POST";
            webR.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(webR.GetRequestStream()))
            {
                streamWriter.Write(bytes2);
                streamWriter.Flush();
                streamWriter.Close();

                //Unzulässig 403 Fehler
                //var httpResponse = (HttpWebResponse)webR.GetResponse();

                byte[] bytey = Encoding.UTF8.GetBytes(file2);
                Stream dataStream = webR.GetRequestStream();
                dataStream.Write(bytey, 0, bytey.Length);
                dataStream.Close();

                WebResponse myWebResponse = webR.GetResponse();

                //WebResponse myWebResponse = webR.GetResponse();
                
                using (var streamReader = new StreamReader(myWebResponse.GetResponseStream()))
                {
                    var result2 = streamReader.ReadToEnd();
                    var result3 = "Ah";
                    MessageBox.Show("result3: "+result3);
                    MessageBox.Show(result2);
                }

                /*using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result2 = streamReader.ReadToEnd();
                    MessageBox.Show(result2.ToString());
                }*/
                MessageBox.Show("Finito");
            }

            #endregion

            #region Deutsches Video
            //
            ////Instanz wird erstellt
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://dps.report/uploadContent?json=1");
            ////request.Accept = "image/*"; //Würde jedes Bild als Rückgabe akzeptieren
            ////Einstellungen werden vorgenommen
            //request.AllowAutoRedirect = false;
            //request.ContentType = "application/json";
            //request.KeepAlive = false;
            //request.Method = "POST";
            //
            ////Request wird abgesendet
            ////HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //
            #endregion

            return "";
            WebRequest requestObj = WebRequest.Create("https://dps.report/uploadContent?json=1");
            requestObj.Method = "POST";
            requestObj.ContentType = "application/json";

            return "";
        }
        void UploadStuff(string filename)
        {
            string strUrl = String.Format("https://dps.report/uploadContent");
            WebRequest requestObjPost = WebRequest.Create(strUrl);
            requestObjPost.Method = "POST";
            requestObjPost.ContentType = "application/json";

            string postData = "{json=1,rotation_weap1=1,generator=ei,userToken=" + userToken + "}";
            Object jsonObject = new Object();

            WebClient webClient = new WebClient();
            byte[] responseArray = webClient.UploadFile("https://dps.report/uploadContent?json=1 file=", filename);


            //
            //Stream stream = new Stream();
            //
            //using(FileStream fileStream = new FileStream(filename,FileMode.Open, FileAccess.Read))
            //{
            //    byte[] buffer = new byte[1024];
            //    int byteRead = 0;
            //    long bytesSoFar = 0;
            //    while((byteRead = fileStream.Read(buffer,0,buffer.Length)) != 0)
            //    {
            //        stream.Write(buffer, 0, bytesRead);
            //    }
            //}
            //
        }

        #endregion

        #region Sarus Input
        /*
        private async Task<System.IO.Stream> Upload(string actionUrl, Stream paramFileStream)
        {
            if(paramFileStream == null)
            {
                MessageBox.Show("FileStream ist leer");
                return null;
            }
            HttpContent fileStreamContent = new StreamContent(paramFileStream);

            #region Video
            //https://www.youtube.com/watch?v=EPSjxg4Rzs8&ab_channel=SoftwareDev
            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                //new KeyValuePair<string, string>("file",@"C:\Users\Feex\Documents\Guild Wars 2\addons\arcdps\arcdps.cbtlogs\Dhuum\Zhoemton\20200205-200535.zevtc")
                new KeyValuePair<string, string>("file=","C:/Users/Feex/Documents/Guild Wars 2/addons/arcdps/arcdps.cbtlogs/Dhuum/Zhoemton/20200205-200535.zevtc")
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            #endregion

            using (var client = new HttpClient())
            using(var formData = new MultipartFormDataContent())
            {
                //formData.Add(stringContent, "param1", "param1");
                //formData.Add(fileStreamContent, "file", @"C:\Users\Feex\Documents\Guild Wars 2\addons\arcdps\arcdps.cbtlogs\Dhuum\Zhoemton\20200205-200535.zevtc");
                //formData.Add(fileStreamContent, "file", @"C:\Users\Feex\Documents\Guild Wars 2\addons\arcdps\arcdps.cbtlogs\Dhuum\Zhoemton\20200205-200535.zevtc");
                //20191204 - 194747.zevtc

                HttpContent stringContent = new StringContent("20191204 - 194747.zevtc");
                // <input type="text" name="filename" />
                formData.Add(stringContent, "filename", "filename");
                // <input type="file" name="file1" />
                formData.Add(fileStreamContent, "file1", "file1");

                //formData.Add(bytesContent, "file2", "file2");
                try
                {
                    var response = await client.PostAsync(actionUrl, formData);
                    //var response = await client.PostAsync(actionUrl, fileStreamContent);
                    //var response = await client.PostAsync(actionUrl, q);
                    //var response = client.PostAsync(actionUrl, fileStreamContent).Result;
                    if (response.StatusCode == HttpStatusCode.Forbidden)
                    {
                        MessageBox.Show("Server lehnt die Anforderung ab, weil sie Verweigert wird (403) Forbidden");
                    }
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Kein Erfolg");
                        return null;
                    }
                    MessageBox.Show("Response: " + response.ToString());
                    MessageBox.Show("Erfolg");
                    return await response.Content.ReadAsStreamAsync();      
                }
                catch(Exception Error)
                {
                    MessageBox.Show(Error.Message);
                }
                return null;
                //MessageBox.Show(""+formData);
                //var response = await client.PostAsync(actionUrl, formData);
                
                
            }
        }
        */
        #endregion

        
        #region Wichtig! Upload Funktion mit Link Rückgabe, Backslash entfernen und Slash umkehren

        
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
                return "https://"+loglink;
            }
            else
            {
                return "";
            }

            
        }

        private string BackSlashEntfernen(string Umtauschstring)
        {
            string umtausch = "";
            umtausch = Umtauschstring.Replace("\\","");
            return umtausch;
        }

        private string SlashUmkehren(string Umtauschstring)
        {
            string umtausch = "";
            umtausch = Umtauschstring.Replace("\\","/");
            return umtausch;
        }
        #endregion

        #region RestSHARP
        //Quelle: https://stackoverflow.com/questions/4015324/how-to-make-http-post-web-request
        private void btnRestsharpUpload_Click(object sender, EventArgs e)
        {
            string filepfad = SlashUmkehren(@"C:\Users\Feex\Documents\Guild Wars 2\addons\arcdps\arcdps.cbtlogs\Qadim\Zhoemton") + "/";
            string urlDPSREPORT = "";
            //MessageBox.Show(filepfad//);
            var client = new RestClient("https://dps.report/uploadContent?json=1");
            var request = new RestRequest(); //resource/{id}
            request.AddFile("file", filepfad + "20200108-203919.zevtc");
            var response = client.Post(request);
            var content = response.Content; //raw content as string
            //var response2 = client.permalink;

            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(content);
            
            if (jsonString.Contains("permalink"))
            {
                //MessageBox.Show("Permalink gefunden: "+jsonString.get);
                MessageBox.Show("Permalink gefunden");
            }
            

            string loglink="";
            loglink = content;
            int start=loglink.IndexOf("permalink\": \"");
            int ende = loglink.IndexOf("uploadTime:");
            string zusammen = "Start "+start+", Ende "+ende+", Ergebnis: "+loglink.Substring(start+13,content.Length-start-13);
            string einsi = loglink.Substring(loglink.IndexOf("dps.report"));
            //einsi = einsi.TrimEnd(',');
            einsi = einsi.Substring(0, einsi.IndexOf("\","));
            einsi = einsi.Trim('\\');
            MessageBox.Show(einsi);
            //loglink.Split()
            MessageBox.Show("Fertig");
            MessageBox.Show(content.ToString());
            textBox1.AppendText(einsi);
        }

        #endregion

        string neuertext = "";
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime dateTime1 = new DateTime();
            DateTime dateTime2 = new DateTime();
            dateTime1 = DateTime.Now;
            label3.Text = "0%";
            List<string> gefiltertelogs;// = new List<string>();
            //https://www.youtube.com/watch?v=9mUuJIKq40M&ab_channel=IAmTimCorey
            var files2 = Directory.GetFiles(txtBoxLogOrdner.Text, "*.zevtc", SearchOption.AllDirectories);

            List<LogInfos> zaehlendeLogs = new List<LogInfos>();
            zaehlendeLogs.Clear();
            //progressBar1.Value = 0;

            List<string> wvwlogs = new List<string>();
            wvwlogs.Clear();
            textBox1.Clear();

            foreach (string file in files2)
            {
                var info = new FileInfo(file);
                if (comboBox1.SelectedIndex == 0)
                {
                    if (info.DirectoryName.Contains("WvW"))
                    {
                        continue; //Überspringt alle Einträge, welche WvW im Pfad haben
                    }
                    if (info.Length < 100)
                    {
                        continue; //überspringt alle Einträge, deren Größe kleiner als 100 Kb sind
                    }
                    if (info.CreationTime > DateTime.Today.AddDays(-(int.Parse(textBox2.Text))))
                    {
                        //Zählt nur die Logs, die innerhalb der letzten 7 Tage erstellt wurden.
                        //Console.WriteLine("info: "+info.CreationTime);
                        //Console.WriteLine(Path.GetDirectoryName(file));
                        LogInfos loginfos = new LogInfos();

                        Console.WriteLine(Path.GetFileName(Path.GetDirectoryName(info.DirectoryName)));
                        loginfos.boss = Path.GetFileName(Path.GetDirectoryName(info.DirectoryName));
                        loginfos.erstellDatum = info.CreationTime;
                        loginfos.pfad = info.FullName;

                        //Console.WriteLine(info.Directory);
                        zaehlendeLogs.Add(loginfos);
                    }
                }
                else
                {
                    if (!info.DirectoryName.Contains("WvW"))
                    {
                        continue; //Überspringt alle Einträge, welche nicht im WvW Pfad liegen
                    }
                    if(info.CreationTime > DateTime.Today.AddDays(-(int.Parse(textBox2.Text)))){

                        wvwlogs.Add(info.FullName);
                    }
                }
            }
            
            if(comboBox1.SelectedIndex == 1)
            {
                foreach (var item in wvwlogs)
                {
                    textBox1.AppendText(UploadLog(item)+"\r\n");
                }
                return;
            }

            progressBar1.Maximum = zaehlendeLogs.Count;
            int bossversuch = 0;
            string vorherigerBoss = "";
            int durchlauf = 0;
            LogInfos2 logInfos2 = new LogInfos2();
            if (zaehlendeLogs.Count > 0)
            {
                logInfos2.boss = zaehlendeLogs[0].boss;
            }
            List<LogInfos2> listeLogTries = new List<LogInfos2>();
            foreach (var item in zaehlendeLogs)
            {
                durchlauf += 1;
                if (vorherigerBoss == "")
                    vorherigerBoss = item.boss;

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
                Console.WriteLine("Gesamtanzahl: " + zaehlendeLogs.Count);
                //textBox1.AppendText(UploadLog(item.pfad)+"\r\n");
                //progressBar1.Value = (durchlauf /zaehlendeLogs.Count);
            }
            #region Auflistung aller Bosse ohne Versuche

            List<string> bosse = new List<string>();
            string vorherigerBoss2 = "";
            foreach (var item in zaehlendeLogs)
            {
                if (item.boss != vorherigerBoss2)
                {
                    bosse.Add(item.boss);
                    vorherigerBoss2 = item.boss;

                }
            }
            Console.WriteLine("Bosse:");
            foreach (var item in bosse)
            {
                Console.WriteLine(item);
            }
            #endregion
            List<LogInfos2> blacklist = new List<LogInfos2>();
            string voherigerboss3 = "";

            IEnumerable<LogInfos2> sortDescending = from w in listeLogTries
                                                    orderby w.boss, w.versuch descending
                                                    select w;

            foreach (var log in sortDescending/*listeLogTries*//*.OrderBy(x => x.versuch)*/)
            {
                //Console.WriteLine("Descending: "+log.versuch+" - "+log.boss);
                if (voherigerboss3 == log.boss)
                {
                    //Es ist der gleiche Boss
                    //listeLogTries.Remove(log);
                    blacklist.Add(log);
                    //voherigerboss3 = log.boss;
                }
                else
                {
                    voherigerboss3 = log.boss;
                }


            }
            foreach (var item in blacklist)
            {
                listeLogTries.Remove(item);
            }

            //FELZ

            //Prüfe Einstellungen
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

            //hochgeladeneLogs.Clear();
            if (radioButton1.Enabled)
            {
                foreach (var item in hochgeladeneLogs)
                {
                    listeLogTries.Remove(item);
                }

            }
            if (radioButton2.Enabled)
            {
                //Führe so aus wie sonst auch
            }
            double prozentualabgeschlossen = 0;
            foreach (var log in listeLogTries)
            {
                
                Console.WriteLine("Boss: " + log.boss + " und Versuch: " + log.versuch + " und Pfad: " + log.pfad);

                textBox1.AppendText(UploadLog(log.pfad) + " "+ log.versuch + "\r\n");
                prozentualabgeschlossen = prozentualabgeschlossen+(100 / listeLogTries.Count);
                label3.Text = prozentualabgeschlossen+"%";
                Console.WriteLine("So viel: "+listeLogTries.Count);
                Console.WriteLine("Jeder Log hat so viel gewicht: "+(1/listeLogTries.Count)*100);
                Console.WriteLine(prozentualabgeschlossen+"%");
            }

            //FELZ
            foreach (var item in listeLogTries)
            {
                hochgeladeneLogs.Add(item);
            }
            label3.Text = "100%";
            MessageBox.Show("Alle Logs wurden hochgeladen");
            dateTime2 = DateTime.Now;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            
        }

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

        private void btnUploadMultiple_Click(object sender, EventArgs e)
        {

            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
            
            return;
            
            //string[] files = Directory.GetFiles(txtBoxLogOrdner.Text, "*.zevtc",SearchOption.AllDirectories);
            List<string> gefiltertelogs;// = new List<string>();
            //https://www.youtube.com/watch?v=9mUuJIKq40M&ab_channel=IAmTimCorey
            var files2 = Directory.GetFiles(txtBoxLogOrdner.Text, "*.zevtc", SearchOption.AllDirectories);

            List<LogInfos> zaehlendeLogs = new List<LogInfos>();
            zaehlendeLogs.Clear();
            progressBar1.Value = 0;

            foreach (string file in files2)
            {
                var info = new FileInfo(file);
                //if(info.CreationTime()==datte)
                if(info.DirectoryName.Contains("WvW"))
                {
                    continue; //Überspringt alle Einträge, welche WvW im Pfad haben
                }
                //Console.WriteLine(file.Length);
                if(info.Length < 100)
                {
                    continue; //überspringt alle Einträge, deren Größe kleiner als 100 Kb sind
                }
                if(info.CreationTime > DateTime.Today.AddDays(-(int.Parse(textBox2.Text))))
                {
                    //Zählt nur die Logs, die innerhalb der letzten 7 Tage erstellt wurden.
                    //Console.WriteLine("info: "+info.CreationTime);
                    //Console.WriteLine(Path.GetDirectoryName(file));
                    LogInfos loginfos = new LogInfos();
                    
                    Console.WriteLine(Path.GetFileName(Path.GetDirectoryName(info.DirectoryName)));
                    loginfos.boss = Path.GetFileName(Path.GetDirectoryName(info.DirectoryName));
                    loginfos.erstellDatum = info.CreationTime;
                    loginfos.pfad = info.FullName;
                    //Muss die Bosse unterscheiden
                    if (info.DirectoryName.Contains("Dhuum"))
                    {

                    }


                    //Console.WriteLine(info.Directory);
                    zaehlendeLogs.Add(loginfos);
                }
                
            }
            progressBar1.Maximum = zaehlendeLogs.Count;
            int bossversuch = 0;
            string vorherigerBoss = "";
            int durchlauf = 0;
            LogInfos2 logInfos2 = new LogInfos2();
            if (zaehlendeLogs.Count > 0)
            {
                logInfos2.boss = zaehlendeLogs[0].boss;
            }
            List<LogInfos2> listeLogTries = new List<LogInfos2>();
            foreach (var item in zaehlendeLogs)
            {
                durchlauf += 1;
                if (vorherigerBoss == "")
                    vorherigerBoss = item.boss;

                if(vorherigerBoss == item.boss)
                {
                    bossversuch += 1;

                    //LogInfos2 loginfos2 = new LogInfos2();
                    //logInfos2.boss = item.boss;
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
                Console.WriteLine("Gesamtanzahl: "+zaehlendeLogs.Count);
                //textBox1.AppendText(UploadLog(item.pfad)+"\r\n");
                //progressBar1.Value = (durchlauf /zaehlendeLogs.Count);
            }
            #region Auflistung aller Bosse ohne Versuche

            List<string> bosse = new List<string>();
            string vorherigerBoss2="";
            foreach (var item in zaehlendeLogs)
            {
                if(item.boss != vorherigerBoss2)
                {
                    bosse.Add(item.boss);
                    vorherigerBoss2 = item.boss;
                    
                }
            }
            Console.WriteLine("Bosse:");
            foreach (var item in bosse)
            {
                Console.WriteLine(item);
            }
            #endregion
            List<LogInfos2> blacklist = new List<LogInfos2>();
            string voherigerboss3 = "";

            IEnumerable<LogInfos2> sortDescending = from w in listeLogTries
                                                    orderby w.boss,w.versuch descending
                                                    select w;

            foreach (var log in sortDescending/*listeLogTries*//*.OrderBy(x => x.versuch)*/)
            {
                //Console.WriteLine("Descending: "+log.versuch+" - "+log.boss);
                if(voherigerboss3 == log.boss)
                {
                    //Es ist der gleiche Boss
                    //listeLogTries.Remove(log);
                    blacklist.Add(log);
                    //voherigerboss3 = log.boss;
                }
                else
                {
                    voherigerboss3 = log.boss;
                }
                
                
            }
            foreach (var item in blacklist)
            {
                listeLogTries.Remove(item);
            }


            foreach (var log in listeLogTries)
            {
                Console.WriteLine("Boss: " + log.boss + " und Versuch: " + log.versuch + " und Pfad: " + log.pfad);
                
                textBox1.AppendText(UploadLog(log.pfad) + " " + log.boss + " " + log.versuch+"\r\n");
            }
            /*try
            {
                gefiltertelogs = Directory.GetFiles().Where(x => x.CreationTime.Date == DateTime.Today.AddDays(-1)).Select(x => x.Name).ToList();
            }catch(Exception Error)
            {
                MessageBox.Show(Error.ToString());
            }
            

            foreach (var item in files)
            {
                //if(item.)
            }
            MessageBox.Show("");*/
        }

        
        public string eintrag1_1 = "";

        private void updateUI()
        {
            textBox1.AppendText(eintrag1_1);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //if(int.TryParse(textBox2.Text, out))
            //int a = int.Parse(textBox2.Text);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if(textBox2.Text == "")
            {
                textBox2.Text = "1";
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //progressBar1.Value = e.ProgressPercentage;
            //progressBar1.Update();

        }

        private void backgroundWorker1_RunWorkerCompleted_1(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Es wurden alle gefundenen Logs hochgeladen.","Nachricht",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private bool LineExistsInFile(string filepath,string searchingline)
        {
            string aktuelleZeileText = "";
            StreamReader reader = new StreamReader(filepath);
            while(reader.Peek() >= 0)
            {
                aktuelleZeileText = reader.ReadLine();
                Console.WriteLine("Vergleiche");
                Console.WriteLine(aktuelleZeileText);
                Console.WriteLine("mit +"+searchingline);
                if(searchingline == aktuelleZeileText)
                {
                    return true;
                }
            }
            reader.Close();

            return false;
        }

        private void ErstelleVerzeichnisse()
        {
            //Gibt es das Verzeichnis? Wenn nein => Anlegen
            
            if (!Directory.Exists(Directory.GetCurrentDirectory()+"/Upgeloaded/"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory()+"/Upgeloaded");
            }

            //Erstelle die Datei
            if (!File.Exists(Directory.GetCurrentDirectory()+"/Upgeloaded/logs.txt"))
            {
                var myFile = File.Create(Directory.GetCurrentDirectory()+"/Upgeloaded/logs.txt");
                myFile.Close();
            }
        }

        private void backgroundWorkerJobUplaod(LogInfos2 loginfo)
        {

            if (radioButton1.Checked) //Lade keine Logs mehrfach hoch!
            {
                if (LineExistsInFile(@Directory.GetCurrentDirectory()+"/Upgeloaded/logs.txt", loginfo.pfad)) //Wenn der Logs schon existiert
                {
                    MessageBox.Show("Log wurde übersprungen");
                    label3.Text = prozentualerAnteilProLog * (listeLogTriesCount - listeLogTries.Count) + "%";
                    return;
                }
            }
            string upload = UploadLog(loginfo.pfad);
            int maximaleUploadVersuche = 10;
            int aktuellerUploadVersuch = 1;
            while(upload == "" && aktuellerUploadVersuch <= maximaleUploadVersuche)
            {
                upload = UploadLog(loginfo.pfad);
                aktuellerUploadVersuch++;
            }
            //textBox1.AppendText(UploadLog(loginfo.pfad) + " " + loginfo.versuch + "\r\n");
            textBox1.AppendText(upload + " " + loginfo.versuch + "\r\n");
            SchreibeBereitsUpgeloaded(loginfo.pfad);
            if (listeLogTries.Count > 0)
                label3.Text = prozentualerAnteilProLog * (listeLogTriesCount - listeLogTries.Count) + "%";

        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            //string eins = Convert.ToString(listeObj[0]); //Pfad
            //string zwei = Convert.ToString(listeObj[1]); //Versuch
            //listeLogTries
            //List<LogInfos2> listeLogs = new List<LogInfos2>();
            //listeLogs = (LogInfos2)e.Argument;

            

            LogInfos2 loginfo = new LogInfos2();
            loginfo = (LogInfos2)e.Argument;

            backgroundWorkerJobUplaod(loginfo);
            return;

            #region Unnötig
            textBox1.AppendText(UploadLog(loginfo.pfad) + " " + loginfo.versuch + "\r\n");
            if (listeLogTries.Count > 0)
                //label3.Text = (100 / listeLogTries.Count)+"%";
                label3.Text = prozentualerAnteilProLog*(listeLogTriesCount-listeLogTries.Count)+"%";
            //label3.Text = (100*(Count-listeLogTriesCount) / listeLogTriesCount) + "%";
            //Console.WriteLine("Thread 2");
            //try
            //{
            //    Thread.Sleep(1000);
            //}catch(Exception Error)
            //{
            //    Console.WriteLine("Error in Thread");
            //}
            //textBox1.AppendText(UploadLog)

            //Aufräumen
            //listeObj.RemoveAt(0);
            //listeObj.RemoveAt(0);
            /*
             foreach (var log in listeLogTries)
            {
                
                Console.WriteLine("Boss: " + log.boss + " und Versuch: " + log.versuch + " und Pfad: " + log.pfad);

                textBox1.AppendText(UploadLog(log.pfad) + " "+ log.versuch + "\r\n");
                prozentualabgeschlossen = prozentualabgeschlossen+(100 / listeLogTries.Count);
                //prozentualabgeschlossen = prozentualabgeschlossen * 100;
                //prozentualabgeschlossen += 10;
                label3.Text = prozentualabgeschlossen+"%";
                Console.WriteLine("So viel: "+listeLogTries.Count);
                Console.WriteLine("Jeder Log hat so viel gewicht: "+(1/listeLogTries.Count)*100);
                Console.WriteLine(prozentualabgeschlossen+"%");
                //progressBar1.Value = progressBar1.Value + (1/listeLogTries.Count)*100;
                //progressBar1.Update();
            }
             */
        #endregion
        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            //string eins = Convert.ToString(listeObj[0]); //Pfad
            //string zwei = Convert.ToString(listeObj[1]); //Versuch

            //textBox1.AppendText(UploadLog(eins) + " " + zwei + "\r\n");
            LogInfos2 loginfo = new LogInfos2();
            loginfo = (LogInfos2)e.Argument;

            backgroundWorkerJobUplaod(loginfo);
            return;

            #region Unnötig
            textBox1.AppendText(UploadLog(loginfo.pfad) + " " + loginfo.versuch + "\r\n");
            if (listeLogTries.Count > 0)
                //label3.Text = (100 / listeLogTries.Count) + "%";
                label3.Text = prozentualerAnteilProLog * (listeLogTriesCount - listeLogTries.Count) + "%";
            //Console.WriteLine("Thread 3");
            //try
            //{
            //    Thread.Sleep(1000);
            //}
            //catch (Exception Error)
            //{
            //    Console.WriteLine("Error in Thread");
            //}

            //Aufräumen
            //listeObj.RemoveAt(0);
            //listeObj.RemoveAt(0);

            /*
             foreach (var log in listeLogTries)
            {
                
                Console.WriteLine("Boss: " + log.boss + " und Versuch: " + log.versuch + " und Pfad: " + log.pfad);

                textBox1.AppendText(UploadLog(log.pfad) + " "+ log.versuch + "\r\n");
                prozentualabgeschlossen = prozentualabgeschlossen+(100 / listeLogTries.Count);
                //prozentualabgeschlossen = prozentualabgeschlossen * 100;
                //prozentualabgeschlossen += 10;
                label3.Text = prozentualabgeschlossen+"%";
                Console.WriteLine("So viel: "+listeLogTries.Count);
                Console.WriteLine("Jeder Log hat so viel gewicht: "+(1/listeLogTries.Count)*100);
                Console.WriteLine(prozentualabgeschlossen+"%");
                //progressBar1.Value = progressBar1.Value + (1/listeLogTries.Count)*100;
                //progressBar1.Update();
            }
             */
            #endregion
        }

        private void backgroundWorker4_DoWork(object sender, DoWorkEventArgs e)
        {
            LogInfos2 loginfo = new LogInfos2();
            loginfo = (LogInfos2)e.Argument;

            backgroundWorkerJobUplaod(loginfo);
            return;

            #region Unnötig
            textBox1.AppendText(UploadLog(loginfo.pfad) + " " + loginfo.versuch + "\r\n");
            if (listeLogTries.Count > 0)
                //label3.Text = (100 / listeLogTries.Count) + "%";
                label3.Text = prozentualerAnteilProLog * (listeLogTriesCount - listeLogTries.Count) + "%";
            #endregion
        }

        private void backgroundWorker5_DoWork(object sender, DoWorkEventArgs e)
        {
            LogInfos2 loginfo = new LogInfos2();
            loginfo = (LogInfos2)e.Argument;

            backgroundWorkerJobUplaod(loginfo);
            return;

            #region Unnötig
            textBox1.AppendText(UploadLog(loginfo.pfad) + " " + loginfo.versuch + "\r\n");
            
            if(listeLogTries.Count >0)
                //label3.Text = (100 / listeLogTries.Count) + "%";
                label3.Text = prozentualerAnteilProLog * (listeLogTriesCount - listeLogTries.Count) + "%";
            #endregion
        }
        public List<LogInfos2> listeLogTries = new List<LogInfos2>();
        public int listeLogTriesCount;
        public double prozentualerAnteilProLog;
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime datetime1 = new DateTime();
            DateTime datetime2 = new DateTime();
            datetime1 = DateTime.Now;
            label3.Text = "0%";
            List<string> gefiltertelogs;// = new List<string>();
            //https://www.youtube.com/watch?v=9mUuJIKq40M&ab_channel=IAmTimCorey
            var files2 = Directory.GetFiles(txtBoxLogOrdner.Text, "*.zevtc", SearchOption.AllDirectories);

            List<LogInfos> zaehlendeLogs = new List<LogInfos>();
            zaehlendeLogs.Clear();
            //progressBar1.Value = 0;

            List<string> wvwlogs = new List<string>();
            wvwlogs.Clear();
            textBox1.Clear();

            foreach (string file in files2)
            {
                var info = new FileInfo(file);
                //if(info.CreationTime()==datte)
                if (comboBox1.SelectedIndex == 0)
                {
                    if (info.DirectoryName.Contains("WvW"))
                    {
                        continue; //Überspringt alle Einträge, welche WvW im Pfad haben
                    }
                    //Console.WriteLine(file.Length);
                    if (info.Length < 100)
                    {
                        continue; //überspringt alle Einträge, deren Größe kleiner als 100 Kb sind
                    }
                    if (info.CreationTime > DateTime.Today.AddDays(-(int.Parse(textBox2.Text))))
                    {
                        //Zählt nur die Logs, die innerhalb der letzten 7 Tage erstellt wurden.
                        //Console.WriteLine("info: "+info.CreationTime);
                        //Console.WriteLine(Path.GetDirectoryName(file));
                        LogInfos loginfos = new LogInfos();

                        Console.WriteLine(Path.GetFileName(Path.GetDirectoryName(info.DirectoryName)));
                        loginfos.boss = Path.GetFileName(Path.GetDirectoryName(info.DirectoryName));
                        loginfos.erstellDatum = info.CreationTime;
                        loginfos.pfad = info.FullName;
                        //Muss die Bosse unterscheiden
                        if (info.DirectoryName.Contains("Dhuum"))
                        {

                        }


                        //Console.WriteLine(info.Directory);
                        zaehlendeLogs.Add(loginfos);
                    }
                }
                else
                {
                    if (!info.DirectoryName.Contains("WvW"))
                    {
                        continue; //Überspringt alle Einträge, welche nicht im WvW Pfad liegen
                    }
                    if (info.CreationTime > DateTime.Today.AddDays(-(int.Parse(textBox2.Text))))
                    {

                        wvwlogs.Add(info.FullName);
                    }
                    /*
                    foreach (var log in listeLogTries)
                    {
                        Console.WriteLine("Boss: " + log.boss + " und Versuch: " + log.versuch + " und Pfad: " + log.pfad);

                        textBox1.AppendText(UploadLog(log.pfad) + " " + log.versuch + "\r\n");

                        progressBar1.Value = progressBar1.Value + (1 / listeLogTries.Count) * 100;
                        progressBar1.Update();
                    }*/
                }


            }

            if (comboBox1.SelectedIndex == 1)
            {
                foreach (var item in wvwlogs)
                {
                    textBox1.AppendText(UploadLog(item) + "\r\n");
                }
                return;
            }

            progressBar1.Maximum = zaehlendeLogs.Count;
            int bossversuch = 0;
            string vorherigerBoss = "";
            int durchlauf = 0;
            LogInfos2 logInfos2 = new LogInfos2();
            if (zaehlendeLogs.Count > 0)
            {
                logInfos2.boss = zaehlendeLogs[0].boss;
            }
            //List<LogInfos2> listeLogTries = new List<LogInfos2>();
            foreach (var item in zaehlendeLogs)
            {
                durchlauf += 1;
                if (vorherigerBoss == "")
                    vorherigerBoss = item.boss;

                if (vorherigerBoss == item.boss)
                {
                    bossversuch += 1;

                    //LogInfos2 loginfos2 = new LogInfos2();
                    //logInfos2.boss = item.boss;
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
                Console.WriteLine("Gesamtanzahl: " + zaehlendeLogs.Count);
                //textBox1.AppendText(UploadLog(item.pfad)+"\r\n");
                //progressBar1.Value = (durchlauf /zaehlendeLogs.Count);
            }
            #region Auflistung aller Bosse ohne Versuche

            List<string> bosse = new List<string>();
            string vorherigerBoss2 = "";
            foreach (var item in zaehlendeLogs)
            {
                if (item.boss != vorherigerBoss2)
                {
                    bosse.Add(item.boss);
                    vorherigerBoss2 = item.boss;

                }
            }
            Console.WriteLine("Bosse:");
            foreach (var item in bosse)
            {
                Console.WriteLine(item);
            }
            #endregion
            List<LogInfos2> blacklist = new List<LogInfos2>();
            string voherigerboss3 = "";

            IEnumerable<LogInfos2> sortDescending = from w in listeLogTries
                                                    orderby w.boss, w.versuch descending
                                                    select w;

            foreach (var log in sortDescending/*listeLogTries*//*.OrderBy(x => x.versuch)*/)
            {
                //Console.WriteLine("Descending: "+log.versuch+" - "+log.boss);
                if (voherigerboss3 == log.boss)
                {
                    //Es ist der gleiche Boss
                    //listeLogTries.Remove(log);
                    blacklist.Add(log);
                    //voherigerboss3 = log.boss;
                }
                else
                {
                    voherigerboss3 = log.boss;
                }


            }
            foreach (var item in blacklist)
            {
                listeLogTries.Remove(item);
            }

            //FELZ
            //hochgeladeneLogs.Clear();
            if (radioButton1.Enabled)
            {
                foreach (var item in hochgeladeneLogs)
                {
                    listeLogTries.Remove(item);
                }

            }
            if (radioButton2.Enabled)
            {
                //Führe so aus wie sonst auch
            }
            double prozentualabgeschlossen = 0;

            listeLogTriesCount = listeLogTries.Count;
            if (listeLogTries.Count == 0)
            {
                MessageBox.Show("Es gab keine Logs zum Hochladen.");
                return;
            }
            prozentualerAnteilProLog = (100/listeLogTries.Count);
            backgroundWorker6.RunWorkerAsync();
            
        }
        public List<Object> listeObj = new List<object>();

        private void backgroundWorker6_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime dateTime = new DateTime();
            dateTime = DateTime.Now;

            //Prüfe Einstellungen
            if (radioButton1.Checked) //Keine Logs doppelt
            {
                for (int i = listeLogTries.Count-1; i >= 0; i--)
                {
                    if (LineExistsInFile(@Directory.GetCurrentDirectory() + "/Upgeloaded/logs.txt",listeLogTries[i].pfad))
                    {
                        listeLogTries.RemoveAt(i);
                    }
                }
            }
            
            while (true)
            {
                if (!backgroundWorker2.IsBusy &listeLogTries.Count>0)
                {
                    Console.WriteLine("Vorher "+listeLogTries.Count);
                    backgroundWorker2.RunWorkerAsync(listeLogTries[0]);
                    listeLogTries.RemoveAt(0);
                    Console.WriteLine("Nachher "+listeLogTries.Count);
                    //MessageBox.Show("Upload abgeschlossen");
                }
                    
                if (!backgroundWorker3.IsBusy & listeLogTries.Count > 0)
                {
                    backgroundWorker3.RunWorkerAsync(listeLogTries[0]);
                    listeLogTries.RemoveAt(0);
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
                try
                {
                    Thread.Sleep(100);
                }catch(Exception Error)
                {
                    Console.WriteLine(Error.ToString());
                }
                //Console.WriteLine("listelogtries noch so viel "+listeLogTries.Count);
                //progressBar1.Value = (int)prozentualabgeschlossen;
                //this.Update();
                if(!backgroundWorker2.IsBusy & !backgroundWorker3.IsBusy & !backgroundWorker4.IsBusy & !backgroundWorker5.IsBusy & listeLogTries.Count == 0)
                    break;
            }
            MessageBox.Show("Alle Logs wurden hochgeladen. Bitte warte aber für die Links der letzten Logs noch ab.");
            //MessageBox.Show("Start: "+dateTime+" Jetzt: "+DateTime.Now);
        }

        private void SchreibeBereitsUpgeloaded(string pfad)
        {
            #region Quelle
            //Quelle: https://docs.microsoft.com/de-de/dotnet/csharp/programming-guide/file-system/how-to-write-to-a-text-file
            #endregion

            //ErstelleVerzeichnisse();
            

            //Hänge an Datei an
            StreamWriter file = new StreamWriter(@Directory.GetCurrentDirectory()+"/Upgeloaded/logs.txt",true); //true steht für anhängen
            file.WriteLine(pfad);
            file.Close();
        }

        private void backgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Maximum = 100;
            int faktor = listeLogTriesCount-listeLogTries.Count;
            progressBar1.Value = faktor*(int)prozentualerAnteilProLog;
            Update();
            
        }

        private void backgroundWorker4_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Maximum = 100;
            int faktor = listeLogTriesCount - listeLogTries.Count;
            progressBar1.Value = faktor * (int)prozentualerAnteilProLog;
            Update();
        }

        private void backgroundWorker5_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Maximum = 100;
            int faktor = listeLogTriesCount - listeLogTries.Count;
            progressBar1.Value = faktor * (int)prozentualerAnteilProLog;
            Update();
        }

        private void backgroundWorker6_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Maximum = 100;
            int faktor = listeLogTriesCount - listeLogTries.Count;
            progressBar1.Value = faktor * (int)prozentualerAnteilProLog;
            Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SchreibeBereitsUpgeloaded(@"C:\Users\Feex\source\repos\Gw2 AutoArcUploader\Gw2 AutoArcUploader\bin\Debug\Upgeloaded");
        }
    }
}
