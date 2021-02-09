using System.ComponentModel.DataAnnotations;

namespace UHDControlServer.Models
{
    public class Inquiry
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Log { get; set; }

        public string Details { get; set; }
    }
}
