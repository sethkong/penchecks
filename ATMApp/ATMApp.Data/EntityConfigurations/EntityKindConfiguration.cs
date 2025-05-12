using ATMApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ATMApp.Data.EntityConfigurations
{
    public class EntityKindConfiguration : BaseConfiguration<EntityKind>, IEntityTypeConfiguration<EntityKind>
    {
        public void Configure(EntityTypeBuilder<EntityKind> builder)
        {
            builder.HasKey(x => x.Id).HasName("pk_entity_kinds_id");

            builder.Property(x => x.Name).HasColumnName("name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Code).HasColumnName("code")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Description).HasColumnName("description")
                .HasMaxLength(255);

            builder.Property(x => x.ParentId).HasColumnType("uuid")
              .HasColumnName("parent_id");

            builder.HasIndex(x => new { x.Name, x.Code }).IsUnique()
              .HasDatabaseName("idx_entity_kinds_name_code");

            BaseConfigure(builder);

            builder.ToTable("entity_kinds");
        }
    }
}
