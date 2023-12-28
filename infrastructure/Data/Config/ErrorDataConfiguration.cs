using core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.Data.Config
{
    public class ErrorDataConfiguration : IEntityTypeConfiguration<ErrorEntity>
    {
        public void Configure(EntityTypeBuilder<ErrorEntity> builder)
        {
            builder.Property(e => e.Id).IsRequired();
            builder.Property(e => e.ErrorTitle).HasMaxLength(100);
            builder.Property(e => e.ErrorDescription).HasMaxLength(int.MaxValue);
            builder.Property(e => e.ErrorCode).HasMaxLength(10);
            builder.Property(e => e.CreatedAt).IsRequired();
        }
    }
}
