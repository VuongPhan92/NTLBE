using Domain.IServices;
using Domain.ViewModels;
using Infrastructure.Decorator;
using Infrastructure.Queries;
using SimpleInjector;
using System;
using WebCore.Services;

namespace IOC
{
    public class BootStrapper
    {
        private readonly Container container;

        public BootStrapper(Container _container)
        {
            container = _container;
        }

        public void Boot()
        {
            container.Register(typeof(IEmployeeServices), typeof(EmployeeServices));
            container.Register(typeof(IStatisticServices), typeof(StatisticServices));
            container.Register(typeof(IAccountServices), typeof(AccountServices));
            container.Register(typeof(IDeliveryTypeServices), typeof(DeliveryTypeServices));
            container.Register(typeof(IBolServices), typeof(BolServices));
            container.Register(typeof(IBranchServices), typeof(BranchServices));
            container.Register(typeof(ICustomerServices), typeof(CustomerServices));
            container.Register(typeof(IStatusServices), typeof(StatusServices));
            container.Register(typeof(IMerchandiseTypeServices), typeof(MerchandiseTypeServices));
            container.Register(typeof(IActivityService), typeof(ActivityServices));
            container.Register(typeof(IService<>), AppDomain.CurrentDomain.GetAssemblies());
            container.Register(typeof(IQueryHandler<,>), AppDomain.CurrentDomain.GetAssemblies());
            container.Register(typeof(ICommandHandler<>), AppDomain.CurrentDomain.GetAssemblies());
            container.Register(typeof(ComponentVM),typeof(ComponentVM));
            
        }
    }
}