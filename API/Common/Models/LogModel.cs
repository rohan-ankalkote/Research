using System;

namespace API.Common.Models
{
    public class LogModel
    {
        public DateTime TimeStamp { get; set; }
        public string Message { get; set; }
        public string Category { get; set; }
        public int Priority { get; set; }
        public int EventId { get; set; }
        public string Severity { get; set; }
        public string Title { get; set; }
        public string Machine { get; set; }
        public string ApplicationDomain { get; set; }
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public int Win32ThreadId { get; set; }
        public string ThreadName { get; set; }
    }
}