using System;

namespace Domain.ViewModels
{
    public class MerchandiseTypeVM
    {
        public int Id { get; set; }
        public string MerchandiseType1 { get; set; }
        public Nullable<decimal> Value { get; set; }
        public string CalculationUnit { get; set; }
        public string Description { get; set; }
      
    }
}
