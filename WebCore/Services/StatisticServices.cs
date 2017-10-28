using Domain.IServices;
using Domain.ViewModels;
using Infrastructure.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

namespace WebCore.Services
{
    public class StatisticServices :  IStatisticServices
    {
        private IBolServices iBolServices;
        public StatisticServices(IBolServices _iBolServices)
        {
            iBolServices = _iBolServices;         
        }
        public List<BarChartVM> GetBolOnDayBarChartData()
        {
            //var rawData = iBolServices.GetAllBol();
            //var data = rawData.Where(p => p.StatusCode == 1).GroupBy(p => p.CreatedDate.Value.ToShortDateString()).Select(g => new BarChartVM
            //{
            //    CreatedDate = g.Key,
            //    Total = g.Count(),
            //});
            //return data.ToList();
            return null;
        }

        public List<GetBolOnSpecificLocationDataVM> GetBolOnLocationData()
        {
            //var endWorkingTime = new TimeSpan(22, 31, 00);
            //var today = DateTime.Now;
            //var endWorkingDay = today.Add(endWorkingTime);
            //var previousWorkingDay = endWorkingDay.AddDays(-1);
            //var raw = iBolServices.GetAllBol();
            //var data = raw.Where(y=>y.CreatedDate >= previousWorkingDay && y.CreatedDate < endWorkingDay && y.StatusCode == 1).SelectMany(r => r.Branches).GroupBy(b => b.Name).Select(x => new GetBolOnSpecificLocationDataVM
            //{
            //    Location = x.Key,
            //    Total = x.Count()
            //}).ToList();
            //return data.ToList();
            return null;
        }

        public List<GetMerchandiseOnSpecificLocationDataVM> GetMerchandiseOnLocationData()
        {
            //var endWorkingTime = new TimeSpan(22, 31, 00);
            //var today = DateTime.Now;
            //var endWorkingDay = today.Add(endWorkingTime);
            //var previousWorkingDay = endWorkingDay.AddDays(-1);
            //var raw = iBolServices.GetAllBol();
            //var data = raw.Where(y => y.CreatedDate >= previousWorkingDay && y.CreatedDate < endWorkingDay && y.StatusCode == 4).SelectMany(r => r.Branches).GroupBy(b => b.Name).Select(x => new GetMerchandiseOnSpecificLocationDataVM
            //{
            //    Location = x.Key,
            //    Total = x.Count()
            //}).ToList();
            //return data.ToList();
            return null;
        }
    }
}
