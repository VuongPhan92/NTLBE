using Domain.Command;

namespace Domain.IServices
{
    public interface IActivityService
    {
        void AddActivity(ActivityAddCommand command);
    }
}
