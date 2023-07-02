using DotNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}
