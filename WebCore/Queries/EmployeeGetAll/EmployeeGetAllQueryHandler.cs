using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCore.Queries;

namespace WebCore.Queries
{
    public class EmployeeGetAllQueryHandler : IQueryHandler<EmployeeGetAllQuery, IEnumerable<Employee>>
    {
        public IEnumerable<Employee> Handle(EmployeeGetAllQuery query)
        {
            var uow = new UnitOfWork<EF>();
            var result = uow.Repository<Employee>().GetAll().Where(p => !p.DeletedDate.HasValue);
            uow.Dispose();
            return result;
        }
    }
}
