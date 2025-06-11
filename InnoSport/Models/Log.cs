using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSport.Models
{
    public class Log
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
        public string Details { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
