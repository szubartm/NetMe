using Microsoft.EntityFrameworkCore;
using NetMe.API.Data.Models;

namespace NetMe.API.Data
{
    internal sealed class PostgreSQLDbContext : DbContext
    {
        public PostgreSQLDbContext(DbContextOptions<PostgreSQLDbContext> options) : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; }


    }
}
