using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;



namespace Infrastructure.Data.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<Domain.Entities.Task>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Task> builder)
        {
            builder.ToTable("Tasks");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .IsRequired();

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(t => t.Description)
                .HasColumnType("NVARCHAR(MAX)");

            builder.Property(t => t.Customer)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasMany(t => t.Times)
                .WithOne(time => time.Task)
                .HasForeignKey(time => time.TaskId);
        }
    }
}
