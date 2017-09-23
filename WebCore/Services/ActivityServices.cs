using Data;
using Domain.Command;
using Domain.IServices;
using Infrastructure.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCore.Command;

namespace WebCore.Services
{
    public class ActivityServices: IService<Activity>,IActivityService
    {
        private readonly ICommandHandler<ActivityAddCommand> addActivityHandler;
        public ActivityServices(ICommandHandler<ActivityAddCommand> _addActivityHandler)
        {
            addActivityHandler = _addActivityHandler;
        }
        public void AddActivity(ActivityAddCommand command)
        {
            addActivityHandler.Handle(command);
        }
    }
}
