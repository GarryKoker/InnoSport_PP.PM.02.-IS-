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
            optionsBuilder.UseNpgsql(
            "Host=aws-0-eu-west-2.pooler.supabase.com;" +
            "Port=5432;" +
            "Database=postgres;" +
            "User Id=postgres.dreqypvfjflhqexmfnwr;" +
            "Password=InnoSportDatabase555;" +
            "SSL Mode=Require;Trust Server Certificate=true;"
            );
        }

        public DbSet<InnoSport.Models.User> Users { get; set; } = null!;
    }
}
