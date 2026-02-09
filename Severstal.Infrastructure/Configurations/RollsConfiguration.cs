using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Severstal.Domain.Entities;

namespace Severstal.Infrastructure.Configurations
{
    internal sealed class RollsConfiguration : IEntityTypeConfiguration<Roll>
    {
        public void Configure(EntityTypeBuilder<Roll> builder)
        {
            builder.ToTable("rolls");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(r => r.Length)
           .HasColumnName("length")
           .IsRequired();

            builder.Property(r => r.Weight)
                .HasColumnName("weight")
                .IsRequired();

            builder.Property(r => r.AddedDate)
                .HasColumnName("added_date")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(r => r.RemovedDate)
                .HasColumnName("removed_date")
                .HasColumnType("date");



            builder.HasIndex(r => r.AddedDate);
            builder.HasIndex(r => r.RemovedDate);
            builder.HasIndex(r => r.Length);
            builder.HasIndex(r => r.Weight);
            builder.HasIndex(r => new { r.AddedDate, r.RemovedDate });

        }
    }
}
