namespace LogUploader4Glenna
{
    public class ReportAusgabe
    {
        public string id;
        public string permalink;
        public long uploadTime;
        public long encounterTime;
        public Encounter encounter;
    }

    public class Encounter
    {
        public string boss;
        public int bossId;
        public bool success;
        public double duration;
        public bool jsonAvailable;
    }
}
