using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data.Configurations
{
    public class TimeConfiguration : IEntityTypeConfiguration<Time>
    {
        public void Configure(EntityTypeBuilder<Time> builder)
        {
            builder.ToTable("Times");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .IsRequired();

            builder.Property(t => t.TaskId)
                .IsRequired();

            builder.Property(t => t.Description)
                .HasColumnType("NVARCHAR(MAX)");

            builder.Property(t => t.BeginDate)
                .IsRequired();

            builder.Property(t => t.EndDate)
                .IsRequired(false);
        }
    }
}
