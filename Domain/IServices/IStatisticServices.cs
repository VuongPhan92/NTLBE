using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IServices
{
    public interface IStatisticServices
    {
        List<BarChartVM> GetBolBarChartData();
    }
}
