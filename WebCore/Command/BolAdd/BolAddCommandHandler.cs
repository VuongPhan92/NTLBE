using Data;
using Domain.Command;
using Domain.IServices;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using System.Data.Entity.Validation;
using System.Diagnostics;
namespace WebCore.Command
{
    public class BolAddCommandHandler : ICommandHandler<BolAddCommand>
    {
        private readonly IActivityService iActivityServices;
        public BolAddCommandHandler(IActivityService _iActivityServices)
        {
            iActivityServices = _iActivityServices;
        }
        public void Handle(BolAddCommand command)
        {
            using ( var uow = new UnitOfWork<EF>())
            {
                try
                {
                    //add
                    foreach (var branch in command.Branches)
                    {
                        uow.Repository<Branch>().Attach(branch);
                        command.BOL.Branches.Add(branch);
                    }
                    foreach (var customer in command.Customers)
                    {
                        if (customer.Id == 0)
                        {
                            uow.Repository<Customer>().Add(customer);
                            uow.SubmitChanges();
                        }
                       
                            uow.Repository<Customer>().Attach(customer);
                            command.BOL.Customers.Add(customer);
                    }

                    uow.Repository<BillOfLanding>().Add(command.BOL);
                    uow.SubmitChanges();
                    //log
                    var activity = new Activity();
                    activity.Source = "Vận Đơn";
                    activity.Source_Id = command.BOL.Id;
                    activity.Action = "Tạo";
                    activity.Current_value = command.BOL.BolCode;
                    activity.CreatedDate = command.BOL.CreatedDate;
                    activity.CreatedBy = "";
                    iActivityServices.AddActivity(new ActivityAddCommand { Activity = activity });
                    uow.SubmitChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                }
            }         
        }
    }
}