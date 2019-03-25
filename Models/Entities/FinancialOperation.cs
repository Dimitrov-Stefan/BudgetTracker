using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public class FinancialOperation
    {
        public int Id { get; set; }

        public int FinancialItemId { get; set; }

        public FinancialItem FinancialItem { get; set; }

        public double Amount { get; set; }

        public string Description { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
