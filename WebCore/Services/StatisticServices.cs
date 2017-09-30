using Domain.IServices;
using Domain.ViewModels;
using Infrastructure.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCore.Services
{
    public class StatisticServices :  IStatisticServices
    {
        private IBolServices iBolServices;
        public StatisticServices(IBolServices _iBolServices)
        {
            iBolServices = _iBolServices;
        }
        public List<BarChartVM> GetBolBarChartData()
        {
            var rawData = iBolServices.GetAllBol();
            var data = rawData.GroupBy(p => p.CreatedDate.Value.ToShortDateString()).Select(g => new BarChartVM
            {
                CreatedDate = g.Key,
                Total = g.Count(),
            });
            return data.ToList();
        }
    }
}
