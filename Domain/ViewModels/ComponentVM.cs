using Data;
using System.Collections.Generic;

namespace Domain.ViewModels
{
    public class ComponentVM
    {
        public IEnumerable<DeliveryType> DeliveryType { get; set; }
        public string CurrentTimeStamp { get; set; }
        public IEnumerable<Branch> Branch { get; set; }
        public IEnumerable<MerchandiseType> Type { get; set; }      
    } 
}