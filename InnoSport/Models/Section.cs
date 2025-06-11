using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSport.Models
{
    public class Section
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public int? TrainerId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
