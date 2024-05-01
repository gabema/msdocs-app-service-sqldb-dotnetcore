using Microsoft.EntityFrameworkCore;

namespace DotNetCoreSqlDb.Data
{
    public class MyDatabaseContext : DbContext
    {
        public MyDatabaseContext (DbContextOptions<MyDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<DotNetCoreSqlDb.Models.Todo> Todo { get; set; } = default!;

        public DbSet<DotNetCoreSqlDb.Models.Rating> Rating { get; set; } = default!;
    }
}
