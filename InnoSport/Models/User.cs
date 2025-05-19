using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace InnoSport.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Login { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string? Otchestvo { get; set; }

        public string PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string Password { get; set; }

        public int Role { get; set; } = (int)Roles.User;
    }
}
