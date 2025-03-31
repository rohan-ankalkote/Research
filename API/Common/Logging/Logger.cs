using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using API.Common.Models;

namespace API.Common.Logging
{
    public class Logger
    {
        public static string LogSource { get; set; } = "TEST API";
        public static string LogName { get; set; } = "TEST API NAME";

        public static void Configure()
        {
            if (!EventLog.SourceExists(LogSource))
                EventLog.CreateEventSource(LogSource, LogName);
        }


        public static void WriteError(Exception exception, string category = "General", int priority = 0, int eventId = 1, string title = "Rohan")
        {
            var message = BuildErrorMessage(exception);
            Write(message, EventLogEntryType.Error, category, priority, eventId, title);
        }

        public static string BuildErrorMessage(Exception exception)
        {
            var messageBuilder = new StringBuilder();

            messageBuilder.Append("Exception Source: ");
            messageBuilder.AppendLine(exception.Source);
            messageBuilder.AppendLine();

            messageBuilder.Append("Exception Message: ");
            messageBuilder.AppendLine(exception.Message);
            messageBuilder.AppendLine();

            messageBuilder.Append("Exception Target Site: ");
            messageBuilder.AppendLine(exception.TargetSite.Name);
            messageBuilder.AppendLine();

            messageBuilder.AppendLine("Stack Trace");
            messageBuilder.AppendLine(exception.StackTrace);

            return messageBuilder.ToString();
        }

        public static void Write(string message, EventLogEntryType type, string category = "General", int priority = 0, int eventId = 1, string title = "Rohan")
        {
            var logEntry = CreateLogEntry(message, category, priority, eventId, type, title);

            EventLog.WriteEntry(LogSource, logEntry, type, eventId);
        }


        private static string CreateLogEntry(string message, string category, int priority, int eventId, EventLogEntryType type, string title)
        {
            var logEntry = new LogModel()
            {
                TimeStamp = DateTime.Now,
                Message = message,
                Category = category,
                Priority = priority,
                EventId = eventId,
                Severity = type.ToString(),
                Title = title,
                Machine = Environment.MachineName,
                ApplicationDomain = AppDomain.CurrentDomain.FriendlyName,
                ProcessId = Process.GetCurrentProcess().Id,
                ProcessName = Process.GetCurrentProcess().ProcessName,
                Win32ThreadId = Thread.CurrentThread.ManagedThreadId,
                ThreadName = Thread.CurrentThread.Name
            };

            var messageBuilder = new StringBuilder();

            var properties = logEntry.GetType().GetProperties();
            foreach (var property in properties)
            {
                messageBuilder.AppendLine($"{property.Name}: {property.GetValue(logEntry)}");
            }

            return messageBuilder.ToString();
        }
    }
}