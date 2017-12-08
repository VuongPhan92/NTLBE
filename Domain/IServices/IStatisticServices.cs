using Domain.ViewModels;
using System.Collections.Generic;

namespace Domain.IServices
{
    public interface IStatisticServices
    {
        List<BarChartVM> GetBolOnDayBarChartData();
        List<GetBolOnSpecificLocationDataVM> GetBolOnLocationData();
        List<GetMerchandiseOnSpecificLocationDataVM> GetMerchandiseOnLocationData();
    }
}
