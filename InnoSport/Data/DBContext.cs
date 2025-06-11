using Microsoft.EntityFrameworkCore;

namespace InnoSport.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext() => Database.EnsureCreated();
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=../../../../InnoSportDB.db");
        }

        public DbSet<InnoSport.Models.User> Users { get; set; } = null!;
        public DbSet<InnoSport.Models.Section> Sections { get; set; } = null!;
        public DbSet<InnoSport.Models.UserSection> UserSections { get; set; } = null!;
        public DbSet<InnoSport.Models.Training> Trainings { get; set; } = null!;
        public DbSet<InnoSport.Models.Progress> Progresses { get; set; } = null!;
        public DbSet<InnoSport.Models.Notification> Notifications { get; set; } = null!;
        public DbSet<InnoSport.Models.Log> Logs { get; set; } = null!;
        public DbSet<InnoSport.Models.LeaveRequest> LeaveRequests { get; set; } = null!;
    }
}