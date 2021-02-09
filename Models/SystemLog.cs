using System;
using System.ComponentModel.DataAnnotations;

namespace UHDControlServer.Models
{
    public class SystemLog
    {
        [Key]
        public int Id { get; set; }

        public int AgentId { get; set; }

        public DateTime DateTime { get; set; }

        public int Size { get; set; }
    }
}
