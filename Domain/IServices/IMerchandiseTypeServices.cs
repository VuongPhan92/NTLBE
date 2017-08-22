using Data;
using System.Collections.Generic;

namespace Domain.IServices
{
    public interface IMerchandiseTypeServices
    {
        IEnumerable<MerchandiseType> GetAllMerchandiseType();
    }
}