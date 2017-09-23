using Domain.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IServices
{
    public interface IActivityService
    {
        void AddActivity(ActivityAddCommand command);
    }
}
