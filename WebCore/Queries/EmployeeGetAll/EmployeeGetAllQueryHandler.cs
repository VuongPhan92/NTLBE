using Data;
using Infrastructure.Queries;
using Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;

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
