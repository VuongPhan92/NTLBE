using Data;
using Domain.Command;
using Domain.IServices;
using Infrastructure.Decorator;

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
