using System.Collections.Generic;
using Models.Entities.Identity;
using Models.Enums;

namespace Models.Entities
{
    public class FinancialItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public FinancialItemType Type { get; set; }

        public bool IsActive { get; set; }

        public IList<FinancialOperation> FinancialOperations { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
