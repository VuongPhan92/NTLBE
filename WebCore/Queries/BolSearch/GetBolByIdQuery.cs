using Data;
using Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCore.Queries
{
    public class GetBolByIdQuery:IQuery<BillOfLanding>
    {
        public int Id { get; set; }
    }
}
