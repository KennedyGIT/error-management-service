using core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace infrastructure.Data
{
    public class ErrorDataContext : DbContext
    {
        public ErrorDataContext(DbContextOptions<ErrorDataContext> options): base(options) { }

        public DbSet<ErrorEntity> ErrorEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
