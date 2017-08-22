using Data;
using System.Collections.Generic;

namespace Domain.IServices
{
    public interface IStatusServices
    {
        IEnumerable<Status> GetAllStatusCode();
    }
}
