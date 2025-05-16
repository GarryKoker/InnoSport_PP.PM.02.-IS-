using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InnoSport.Data
{
    public class AppDBContext: DbContext
    {
        public AppDBContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("postgresql://neondb_owner:npg_fOi73drXAFCm@ep-black-mud-a9oiu3p2-pooler.gwc.azure.neon.tech/neondb?sslmode=require");
        }

        public DbSet<InnoSport.Models.Users> Users { get; set; }
    }
}
