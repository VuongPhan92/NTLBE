using Data;
using System.Collections.Generic;

namespace Domain.IServices
{
    public interface IDeliveryTypeServices
    {
        IEnumerable<DeliveryType> GetAllDeliveryType();
    }
}
