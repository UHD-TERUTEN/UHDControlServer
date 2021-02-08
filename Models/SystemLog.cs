using System;

namespace UHDControlServer.Models
{
    public class SystemLog
    {
        public int Id { get; set; }

        public int AgentId { get; set; }

        public DateTime DateTime { get; set; }

        public int Size { get; set; }
    }
}
