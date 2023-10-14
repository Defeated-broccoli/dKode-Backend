using FactoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FactoryAPI.Helpers
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Item> Items { get; set; }
    }
}
