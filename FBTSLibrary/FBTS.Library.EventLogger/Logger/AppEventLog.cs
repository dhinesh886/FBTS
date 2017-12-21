using System.Diagnostics;

namespace FBTS.Library.EventLogger.Logger
{
    public class AppEventLog
    {
        private const string LogName = "Application";

        public static void Write(string product, string message)
        {
            try
            {
                if (!EventLog.SourceExists(product))
                {
                    EventLog.CreateEventSource(product, LogName);
                }

                EventLog.WriteEntry(product, message);
            }
            catch
            {
            }
        }

        public static void Write(string product, string message, int eventId)
        {
            try
            {
                if (!EventLog.SourceExists(product))
                {
                    EventLog.CreateEventSource(product, LogName);
                }

                EventLog.WriteEntry(product, message, EventLogEntryType.Error, eventId);
            }
            catch
            {
            }
        }
        public static void Write(string product, string message, int eventId, EventLogEntryType eventLogEntryType)
        {
            try
            {
                if (!EventLog.SourceExists(product))
                {
                    EventLog.CreateEventSource(product, LogName);
                }

                EventLog.WriteEntry(product, message, eventLogEntryType, eventId);
            }
            catch
            {
            }
        }
    }
}
