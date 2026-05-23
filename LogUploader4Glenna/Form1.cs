using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace LogUploader4Glenna
{
    public partial class Form1 : Form
    {
        private readonly object _logFileLock = new object();
        private HashSet<string> _bereitsHochgeladen;

        public Form1()
        {
            InitializeComponent();

            string environmentOrdner = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            txtBoxLogOrdner.Text = environmentOrdner + "\\Guild Wars 2\\addons\\arcdps\\arcdps.cbtlogs\\";
            txtBoxZeitraum.Text = "";

            ErstelleVerzeichnisse();
            combBoxContent.SelectedIndex = 0;

            toolTipUploadLastLog.SetToolTip(btnUploadLastLog,
                "Lädt ausschließlich den letzten gefundenen PvE-Log hoch — unabhängig davon, ob er ein Erfolg war.");
            toolTipUploadLastLog.SetToolTip(btnUploadHoch,
                "Lädt für jeden Boss im Zeitraum den ersten erfolgreichen Kill-Log hoch und zählt die Versuche.");
            toolTipUploadLastLog.SetToolTip(txtBoxZeitraum,
                "Anzahl Tage zurück. Leer lassen für 'seit letztem Wochen-Reset (Montag 14:00 MESZ)'.");
        }

        #region Zeitraum & Cutoff

        /// <summary>Liefert den letzten Wochen-Reset (Montag 14:00 MESZ/MEZ) als UTC-DateTime.</summary>
        private static DateTime GetLastWeeklyReset()
        {
            TimeZoneInfo zone;
            try
            {
                zone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            }
            catch
            {
                zone = TimeZoneInfo.Local;
            }

            DateTime nowLocal = TimeZoneInfo.ConvertTime(DateTime.UtcNow, zone);
            int daysSinceMonday = ((int)nowLocal.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            DateTime reset = nowLocal.Date.AddDays(-daysSinceMonday).AddHours(14);
            if (nowLocal < reset)
            {
                reset = reset.AddDays(-7);
            }
            return TimeZoneInfo.ConvertTimeToUtc(reset, zone);
        }

        private DateTime ComputeCutoff()
        {
            string input = (txtBoxZeitraum.Text ?? "").Trim();
            if (string.IsNullOrEmpty(input) || input == "0")
            {
                return GetLastWeeklyReset();
            }
            if (int.TryParse(input, out int days))
            {
                return DateTime.UtcNow.AddDays(-days).Date;
            }
            return GetLastWeeklyReset();
        }

        #endregion

        #region Datei- & Pfad-Helfer

        private void ErstelleVerzeichnisse()
        {
            string dir = Path.Combine(Directory.GetCurrentDirectory(), "Upgeloaded");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string file = Path.Combine(dir, "logs.txt");
            if (!File.Exists(file))
            {
                File.Create(file).Close();
            }
        }

        private string LogsTxtPfad()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "Upgeloaded", "logs.txt");
        }

        private HashSet<string> LadeBereitsHochgeladen()
        {
            try
            {
                return new HashSet<string>(File.ReadAllLines(LogsTxtPfad()));
            }
            catch
            {
                return new HashSet<string>();
            }
        }

        private void MarkiereHochgeladen(string pfad)
        {
            lock (_logFileLock)
            {
                File.AppendAllText(LogsTxtPfad(), pfad + Environment.NewLine);
                _bereitsHochgeladen?.Add(pfad);
            }
        }

        /// <summary>Bestimmt den Gruppen-Key für den Boss (abhängig von Ordnerstruktur-Auswahl).</summary>
        private string GetBossKey(FileInfo fi, bool unterordnerCharakter)
        {
            if (unterordnerCharakter)
            {
                // Struktur: bossOrdner\charakterOrdner\file.evtc
                string parent = Path.GetDirectoryName(fi.DirectoryName);
                return parent ?? fi.DirectoryName;
            }
            return fi.DirectoryName;
        }

        private static string BackSlashEntfernen(string s)
        {
            return s.Replace("\\", "");
        }

        private static string SlashUmkehren(string s)
        {
            return s.Replace("\\", "/");
        }

        #endregion

        #region Upload-API

        /// <summary>Lädt eine Datei zu dps.report hoch und liefert das deserialisierte Report-Objekt.</summary>
        private ReportAusgabe UploadLogParsed(string filepfad)
        {
            string normalisiert = SlashUmkehren(filepfad);

            var client = new RestClient("https://dps.report/uploadContent?json=1&generator=ei");
            var request = new RestRequest();
            request.AddFile("file", normalisiert);
            var response = client.Post(request);
            string content = response.Content;

            if (string.IsNullOrEmpty(content))
            {
                return null;
            }

            try
            {
                return JsonConvert.DeserializeObject<ReportAusgabe>(content);
            }
            catch (JsonException ex)
            {
                // Loggen statt stillschweigend verwerfen
                File.AppendAllText("upload_error.log", DateTime.UtcNow + " JsonError: " + ex.Message + Environment.NewLine + content + Environment.NewLine);
                return null;
            }
        }

        /// <summary>Upload mit Retry-Logik (bis zu N Versuche).</summary>
        private ReportAusgabe UploadLogParsedMitRetry(string filepfad, int maxVersuche = 3)
        {
            for (int versuch = 1; versuch <= maxVersuche; versuch++)
            {
                try
                {
                    var result = UploadLogParsed(filepfad);
                    if (result != null && !string.IsNullOrEmpty(result.permalink))
                    {
                        return result;
                    }
                }
                catch
                {
                    // ignore, retry
                }
                Thread.Sleep(500 * versuch);
            }
            return null;
        }

        /// <summary>Legacy-Wrapper für btnUploadLastLog — gibt nur die URL zurück.</summary>
        private string UploadLog(string filepfad)
        {
            var r = UploadLogParsedMitRetry(filepfad);
            if (r == null) return "";
            if (r.encounter != null && (r.encounter.boss ?? "").Contains("Kitty Golem"))
            {
                return "";
            }
            return r.permalink ?? "";
        }

        #endregion

        #region Haupt-Upload-Pipeline

        private async void btnUploadHoch_Click(object sender, EventArgs e)
        {
            btnUploadHoch.Enabled = false;
            btnUploadLastLog.Enabled = false;
            txtBoxUploadedLogs.Clear();
            progressBarUpload.Value = 0;
            label4.Text = "0%";

            try
            {
                await UploadAllAsync();
                MessageBox.Show("Alle Logs wurden verarbeitet.", "Fertig",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Upload: " + ex.Message, "Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnUploadHoch.Enabled = true;
                btnUploadLastLog.Enabled = true;
            }
        }

        private async Task UploadAllAsync()
        {
            string logOrdner = txtBoxLogOrdner.Text;
            if (string.IsNullOrWhiteSpace(logOrdner) || !Directory.Exists(logOrdner))
            {
                MessageBox.Show("Log-Ordner existiert nicht: " + logOrdner);
                return;
            }

            DateTime cutoff = ComputeCutoff();
            long minSizeBytes = 100L * 1024L;
            if (int.TryParse(txtBoxMaxLogSize.Text, out int kb))
            {
                minSizeBytes = kb * 1024L;
            }
            bool pveOnly = combBoxContent.SelectedIndex == 0;
            bool avoidDup = radioButton1.Checked;
            bool unterordnerCharakter = radioButton4.Checked;

            _bereitsHochgeladen = LadeBereitsHochgeladen();

            // Sammle alle Log-Dateien
            var alleLogs = new List<FileInfo>();
            try
            {
                alleLogs.AddRange(Directory.GetFiles(logOrdner, "*.zevtc", SearchOption.AllDirectories).Select(p => new FileInfo(p)));
                alleLogs.AddRange(Directory.GetFiles(logOrdner, "*.evtc", SearchOption.AllDirectories).Select(p => new FileInfo(p)));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Lesen des Log-Ordners: " + ex.Message);
                return;
            }

            var qualifiziert = alleLogs
                .Where(fi => fi.Length > minSizeBytes)
                .Where(fi => fi.CreationTime.ToUniversalTime() >= cutoff)
                .Where(fi => pveOnly
                    ? !fi.DirectoryName.Contains("WvW")
                    : fi.DirectoryName.Contains("WvW"))
                .Where(fi => !avoidDup || !_bereitsHochgeladen.Contains(fi.FullName))
                .ToList();

            if (qualifiziert.Count == 0)
            {
                AppendLine("(Keine Logs im Zeitraum gefunden.)");
                return;
            }

            var gruppen = qualifiziert
                .GroupBy(fi => GetBossKey(fi, unterordnerCharakter))
                .ToList();

            int gesamt = gruppen.Count;
            int fertig = 0;
            var semaphore = new SemaphoreSlim(4, 4);

            var tasks = gruppen.Select(async gruppe =>
            {
                await semaphore.WaitAsync();
                try
                {
                    await Task.Run(() => VerarbeiteBossGruppe(gruppe));
                }
                finally
                {
                    semaphore.Release();
                    int aktuell = Interlocked.Increment(ref fertig);
                    UpdateProgress(aktuell, gesamt);
                }
            });

            await Task.WhenAll(tasks);
        }

        private void VerarbeiteBossGruppe(IGrouping<string, FileInfo> gruppe)
        {
            string bossKey = gruppe.Key;
            string bossOrdnerName = Path.GetFileName(bossKey);
            var sortiert = gruppe.OrderByDescending(fi => fi.CreationTime).ToList();
            int versuche = sortiert.Count;

            string erfolgsUrl = null;
            string ersteWipeUrl = null;

            foreach (var fi in sortiert)
            {
                var report = UploadLogParsedMitRetry(fi.FullName);
                if (report == null)
                {
                    continue;
                }

                MarkiereHochgeladen(fi.FullName);

                if (ersteWipeUrl == null && !string.IsNullOrEmpty(report.permalink))
                {
                    ersteWipeUrl = report.permalink;
                }

                if (report.encounter != null && report.encounter.success)
                {
                    erfolgsUrl = report.permalink;
                    break;
                }
            }

            string zeile = erfolgsUrl != null
                ? erfolgsUrl + " " + versuche
                : "WIPE " + (ersteWipeUrl ?? bossOrdnerName) + " " + versuche;

            AppendLine(zeile);
        }

        #endregion

        #region UI-Thread-sichere Helfer

        private void AppendLine(string text)
        {
            if (txtBoxUploadedLogs.InvokeRequired)
            {
                txtBoxUploadedLogs.Invoke((MethodInvoker)(() => txtBoxUploadedLogs.AppendText(text + Environment.NewLine)));
            }
            else
            {
                txtBoxUploadedLogs.AppendText(text + Environment.NewLine);
            }
        }

        private void UpdateProgress(int fertig, int gesamt)
        {
            int prozent = gesamt > 0 ? (int)Math.Round(100.0 * fertig / gesamt) : 100;
            if (progressBarUpload.InvokeRequired)
            {
                progressBarUpload.Invoke((MethodInvoker)(() =>
                {
                    progressBarUpload.Value = Math.Min(100, Math.Max(0, prozent));
                    label4.Text = prozent + "%";
                }));
            }
            else
            {
                progressBarUpload.Value = Math.Min(100, Math.Max(0, prozent));
                label4.Text = prozent + "%";
            }
        }

        #endregion

        #region "Lade letzten Log hoch" — Sekundär-Aktion

        private void btnUploadLastLog_Click(object sender, EventArgs e)
        {
            string logOrdner = txtBoxLogOrdner.Text;
            if (string.IsNullOrWhiteSpace(logOrdner) || !Directory.Exists(logOrdner))
            {
                MessageBox.Show("Log-Ordner existiert nicht: " + logOrdner);
                return;
            }

            long minSizeBytes = 100L * 1024L;
            if (int.TryParse(txtBoxMaxLogSize.Text, out int kb))
            {
                minSizeBytes = kb * 1024L;
            }
            DateTime cutoff = ComputeCutoff();

            var alleLogs = new List<FileInfo>();
            try
            {
                alleLogs.AddRange(Directory.GetFiles(logOrdner, "*.zevtc", SearchOption.AllDirectories).Select(p => new FileInfo(p)));
                alleLogs.AddRange(Directory.GetFiles(logOrdner, "*.evtc", SearchOption.AllDirectories).Select(p => new FileInfo(p)));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler: " + ex.Message);
                return;
            }

            var passend = alleLogs
                .Where(fi => !fi.DirectoryName.Contains("WvW"))
                .Where(fi => fi.Length > minSizeBytes)
                .Where(fi => fi.CreationTime.ToUniversalTime() >= cutoff)
                .OrderByDescending(fi => fi.CreationTime)
                .ToList();

            if (passend.Count == 0)
            {
                MessageBox.Show("Kein PvE-Log im Zeitraum gefunden.");
                return;
            }

            string url = UploadLog(passend[0].FullName);
            if (!string.IsNullOrEmpty(url))
            {
                AppendLine(url);
                MessageBox.Show("Der letzte Log wurde hochgeladen (ohne Versuchszahl).");
            }
            else
            {
                MessageBox.Show("Upload fehlgeschlagen.");
            }
        }

        #endregion

        #region Eingabe-Validierung

        private void txtBoxZeitraum_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtBoxMaxLogSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        #endregion

        #region Update-Check (Menü)

        private void nachUpdatesPrüfenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("Version.xml");
                string aktuelleVersion = doc.DocumentElement.FirstChild.InnerXml;

                string neueVersion = HoleNeueVersion();

                if (neueVersion != aktuelleVersion)
                {
                    MessageBox.Show(
                        "Es gibt eine neue Version.\nNeu: " + neueVersion + "\nAktuell: " + aktuelleVersion,
                        "Update verfügbar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Du hast schon die aktuellste Version.",
                        "Kein Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update-Check fehlgeschlagen: " + ex.Message);
            }
        }

        private string HoleNeueVersion()
        {
            string remoteUri = "https://github.com/Borovrel/LogUploader4Glenna/archive/master.zip";
            string zipPath = "Uploader4Glenna.zip";
            string extractPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploader4GlennaExtract");

            using (WebClient wc = new WebClient())
            {
                wc.DownloadFile(new Uri(remoteUri), zipPath);
            }

            if (Directory.Exists(extractPath))
            {
                Directory.Delete(extractPath, true);
            }
            ZipFile.ExtractToDirectory(zipPath, extractPath);

            XmlDocument doc = new XmlDocument();
            doc.Load(Path.Combine(extractPath, "LogUploader4Glenna-master", "LogUploader4Glenna", "Version.xml"));
            string neueVersion = doc.DocumentElement.FirstChild.InnerXml;

            try
            {
                File.Delete(zipPath);
                Directory.Delete(extractPath, true);
            }
            catch
            {
                // ignore cleanup errors
            }

            return neueVersion;
        }

        #endregion

        #region Form-Lifecycle

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Top = Properties.Settings.Default.WindowsPosY;
                Left = Properties.Settings.Default.WindowsPosX;
            }
            catch
            {
                // ignore — defaults bleiben
            }

            try
            {
                if (Properties.Settings.Default.Ordnerstruktur == 0)
                {
                    radioButton3.Checked = true;
                }
                else if (Properties.Settings.Default.Ordnerstruktur == 1)
                {
                    radioButton4.Checked = true;
                }
            }
            catch
            {
                // ignore
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Properties.Settings.Default.WindowsPosX = Left;
                Properties.Settings.Default.WindowsPosY = Top;
                Properties.Settings.Default.Ordnerstruktur = radioButton4.Checked ? 1 : 0;
                Properties.Settings.Default.Save();
            }
            catch
            {
                // ignore — Settings können fehlschlagen, das ist nicht kritisch
            }
        }

        #endregion
    }
}
