using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSport.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SectionId { get; set; }
        public int Status { get; set; } = (int)RequestStatuses.На_рассмотрении; // "На рассмотрении", "Одобрено", "Отклонено"
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
