using Data;
using Infrastructure.Queries;
using System.Collections.Generic;

namespace WebCore.Queries
{
    public class BolGetAllQuery : IQuery<IEnumerable<BillOfLanding>>
    {
        public string FilterString { get; set; }
    }
}
