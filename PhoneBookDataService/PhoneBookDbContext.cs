using Microsoft.EntityFrameworkCore;
using PhoneBookModels;

namespace PhoneBookDataService
{
    public class PhoneBookDbContext : DbContext
    {
        private readonly string _dbPath;
        public DbSet<Contact> Contacts { get; set; }

        public PhoneBookDbContext(string dbPath)
        {
            _dbPath = dbPath;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_dbPath);
        }
    }
}
