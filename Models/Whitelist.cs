using System;

namespace UHDControlServer.Models
{
    public class Whitelist
    {
        public int Id { get; set; }

        public string Version { get; set; }

        public DateTime LastUpdated { get; set; }

        public DateTime LastDistributed { get; set; }
    }
}
