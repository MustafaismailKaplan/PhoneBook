using Microsoft.EntityFrameworkCore;
using PhoneBook.Api.Data.Entities;

namespace PhoneBook.Api.Data
{
    public class PhoneBookContext : DbContext
    {
        public PhoneBookContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<ContactDetail> ContactDetail { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
