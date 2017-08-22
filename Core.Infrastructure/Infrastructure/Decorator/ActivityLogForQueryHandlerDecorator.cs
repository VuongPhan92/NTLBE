using Infrastructure.Queries;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Decorator
{
    public class ActivityLogForQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        protected IQueryHandler<TQuery, TResult> decorator;
        public ActivityLogForQueryHandlerDecorator( IQueryHandler<TQuery, TResult> _decorator)
        {
            decorator = _decorator;
        }

        public TResult Handle(TQuery query)
        {
          
            return decorator.Handle(query);
        }
    }
}
