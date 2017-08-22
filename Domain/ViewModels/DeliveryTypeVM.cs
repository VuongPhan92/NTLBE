using System;

namespace Domain.ViewModels
{
    public class DeliveryTypeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> Value { get; set; }   
    }
}
