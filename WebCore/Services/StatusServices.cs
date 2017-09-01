using Data;
using Domain.IServices;
using Infrastructure.Decorator;
using Infrastructure.Queries;
using System.Collections.Generic;
using WebCore.Queries;

namespace WebCore.Services
{
    public class StatusServices : IService<Status>, IStatusServices
    {
        private readonly IQueryHandler<StatusCodeGetAllQuery, IEnumerable<Status>> getAllStatusCodeHandler;

        public StatusServices(
            IQueryHandler<StatusCodeGetAllQuery, IEnumerable<Status>> _getAllStatusCodeHandler)
        {
            getAllStatusCodeHandler = _getAllStatusCodeHandler; 
        }
       
        public IEnumerable<Status> GetAllStatusCode()
        {
            return getAllStatusCodeHandler.Handle(new StatusCodeGetAllQuery { });
        }

    }
}
