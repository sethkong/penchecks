using ATMApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ATMApp.Data.EntityConfigurations
{
    public class BaseConfiguration<T> where T : BaseEntity, new()
    {
        public void BaseConfigure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Id).HasColumnType("uuid")
                .HasColumnName("id")
                .HasDefaultValueSql("uuid_generate_v4()")
                .IsRequired();

            builder.Property(x => x.InactiveAt)
               .HasColumnName("inactive_at")
               .HasColumnType("timestamptz");

            builder.Property(x => x.DeletedAt)
                .HasColumnName("deleted_at")
                .HasColumnType("timestamptz");

            builder.Property(x => x.InsertedAt)
                .HasColumnName("inserted_at")
                .HasColumnType("timestamptz")
                .HasDefaultValueSql("now()");

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("timestamptz");

            builder.Property(x => x.InsertedBy).HasMaxLength(36)
                .HasColumnName("inserted_by")
                .HasDefaultValue(null);

            builder.Property(x => x.UpdatedBy).HasMaxLength(36)
                .HasColumnName("updated_by")
                .HasDefaultValue(null);
        }
    }
}
