using core.Entities;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data
{
    public class ErrorDataContext : DbContext
    {
        public ErrorDataContext(DbContextOptions<ErrorDataContext> options): base(options) { }

        public DbSet<ErrorEntity> ErrorEntities { get; set; }
    }
}
