using System;
using System.ComponentModel.DataAnnotations;

namespace UHDControlServer.Models
{
    public class FileAccessRejectLog
    {
        [Key]
        public int Id { get; set; }

        public int AgentId { get; set; }
        
        public DateTime Date { get; set; }

        public string ProgramName { get; set; }

        public string FileName { get; set; }

        public string Operation { get; set; }

        public string PlainText { get; set; }

        public bool IsAllowed { get; set; }
    }
}
