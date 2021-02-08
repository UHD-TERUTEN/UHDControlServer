using System;

namespace UHDControlServer.Models
{
    public class FileAccessRejectLog
    {
        public int Id { get; set; }

        public int AgentId { get; set; }
        
        public DateTime DateTime { get; set; }

        public string ProgramName { get; set; }

        public string Details { get; set; }

        public bool IsAllowed { get; set; }

        public Inquiry[] Inquiries { get; set; }
    }
}
