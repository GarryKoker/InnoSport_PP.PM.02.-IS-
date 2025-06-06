using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSport.Models
{
    public class Training
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public int TrainerId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Time { get; set; }
        public string Description { get; set; }
    }
}
