using DDDApi.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDApi.Infra.Data.Mapping
{
    public class MappingTodo : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.ToTable("Todo");

            builder.HasKey(x => x.Id).HasName("Id");
            builder.HasIndex(x => x.Code);
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => new { x.Code, x.UserId }).IsUnique();

            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.Code)
                .HasColumnName("Code")
                .HasColumnType("varchar(70)");

            builder.Property(x => x.Description)
                .HasColumnName("Description")
                .HasColumnType("varchar(70)");

            builder.Property(x => x.Date)
                .HasColumnName("Date")
                .HasColumnType("datetime");

            builder.Property(x => x.UserId)
                .HasColumnName("UserId");

            builder.Property(x => x.InData)
                .HasColumnName("InData")
                .HasColumnType("datetime");

            builder.Property(x => x.MoData)
                .HasColumnName("MoData")
                .HasColumnType("datetime");

            builder.HasOne(x => x.User)
                .WithMany(u => u.Todos)
                .HasForeignKey(x => x.UserId)
                .HasConstraintName("FK_TODO_USER");
        }
    }
}
