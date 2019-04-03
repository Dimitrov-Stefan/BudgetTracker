using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data.Configurations
{
    public class FinancialOperationConfiguration : IEntityTypeConfiguration<FinancialOperation>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FinancialOperation> builder)
        {
            builder.ToTable("FinancialOperations");

            builder.Property(fo => fo.Description).HasMaxLength(500);

            builder.HasOne(fo => fo.FinancialItem).WithMany(fi => fi.FinancialOperations).HasForeignKey(fo => fo.FinancialItemId);
        }
    }
}
