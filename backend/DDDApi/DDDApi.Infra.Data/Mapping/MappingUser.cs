using DDDApi.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDApi.Infra.Data.Mapping
{
    public class MappingUser : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id).HasName("Id");
            builder.HasIndex(x => new { x.Email, x.Password });
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("varchar(70)");

            builder.Property(x => x.Email)
                .HasColumnName("Email")
                .HasColumnType("varchar(70)");

            builder.Property(x => x.Password)
                .HasColumnName("Password")
                .HasColumnType("varchar(70)");

            builder.Property(x => x.InData)
                .HasColumnName("InData")
                .HasColumnType("datetime");

            builder.Property(x => x.MoData)
                .HasColumnName("MoData")
                .HasColumnType("datetime");
        }
    }
}
