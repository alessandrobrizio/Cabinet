using UnityEngine;
using AlessandroBrizio.Cabinet.Core.Samples;

namespace AlessandroBrizio.Cabinet.Logging.Samples
{
    public class LoggingDemo : DatabaseDemo<LogsDatabaseTable>
    {
        public void AddLog()
        {
            table.AddLog();
        }

        public void GetLogCount()
        {
            Debug.Log(table.GetLogCount());
        }
    }
}
