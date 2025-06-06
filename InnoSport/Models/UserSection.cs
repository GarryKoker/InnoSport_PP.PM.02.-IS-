using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSport.Models
{
    public class UserSection
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SectionId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
