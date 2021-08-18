using System;

namespace AlessandroBrizio.Cabinet.ScoreManagement
{
    [Serializable]
    public class ScoreEntry
    {
        public string name;
        public int score;
        public int seconds;
        public DateTime datetime;
    }
}
