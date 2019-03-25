using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data.Configurations
{
    public class FinancialOperationConfiguration : IEntityTypeConfiguration<FinancialOperation>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FinancialOperation> builder)
        {
            builder.ToTable("FinancialOperation");

            builder.Property(fo => fo.Description).HasMaxLength(500);
        }
    }
}
