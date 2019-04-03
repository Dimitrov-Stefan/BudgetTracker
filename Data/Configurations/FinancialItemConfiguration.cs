using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace Data.Configurations
{
    public class FinancialItemConfiguration : IEntityTypeConfiguration<FinancialItem>
    {
        public void Configure(EntityTypeBuilder<FinancialItem> builder)
        {
            builder.ToTable("FinancialItems");

            builder.Property(fi => fi.Name).HasMaxLength(256).IsRequired();

            builder.HasOne(fi => fi.User).WithMany(u => u.FinancialItems).HasForeignKey(fi => fi.UserId);
        }
    }
}
