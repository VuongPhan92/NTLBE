using Data;
using Domain.IServices;
using Domain.ViewModels;
using Infrastructure.Decorator;
using Infrastructure.Queries;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WebCore.Command;
using WebCore.Queries;

namespace WebCore.Services
{
    public class BolServices : IService<BillOfLanding>, IBolServices
    {
        private readonly IQueryHandler<BolGetAllQuery, IEnumerable<BillOfLanding>> getAllBolHandler;
        private readonly IQueryHandler<GetBolByBolCodeQuery, BillOfLanding> getBolByBolCodeHandler;
        private readonly IQueryHandler<GetBolByIdQuery, BillOfLanding> getBolByIdHandler;
        private readonly ICommandHandler<BolAddCommand> addBolHandler;
        private readonly ICustomerServices iCusomterservices;
        private readonly ICommandHandler<BolStatusUpdateCommand> bolStatusUpdateHandler;
        private readonly ICommandHandler<BolUpdateStatusByBolCodeCommand> bolStatusUpdateByBolCodeHandler;


        public BolServices(
            IQueryHandler<BolGetAllQuery, IEnumerable<BillOfLanding>> _getAllBolHandler,
            IQueryHandler<GetBolByIdQuery,BillOfLanding> _getBolByIdHandler,
            IQueryHandler<GetBolByBolCodeQuery, BillOfLanding> _getBolByBolCodeHandler,
            ICommandHandler<BolAddCommand> _addBolHandler,
            ICustomerServices _iCusomterservices   ,
            ICommandHandler<BolStatusUpdateCommand> _bolStatusUpdateHandler,
            ICommandHandler<BolUpdateStatusByBolCodeCommand> _bolStatusUpdateByBolCodeHandler
            )
        {
            getAllBolHandler = _getAllBolHandler;
            getBolByIdHandler = _getBolByIdHandler;
            addBolHandler = _addBolHandler;
            iCusomterservices = _iCusomterservices;
            bolStatusUpdateHandler = _bolStatusUpdateHandler;
            getBolByBolCodeHandler = _getBolByBolCodeHandler;
            bolStatusUpdateByBolCodeHandler = _bolStatusUpdateByBolCodeHandler;

        }

        public IEnumerable<BillOfLanding> GetAllBol(string filterDate)
        {
            return getAllBolHandler.Handle(new BolGetAllQuery { FilterString = filterDate });
        }
        public List<Customer> ValidateCustomerInfo (BillOfLanding bol, CustomerVM subCommand)
        {
            var sender = iCusomterservices.GetAllCustomer().Where(p => p.Name.Equals(subCommand.SenderName) && p.Phone.Equals(subCommand.SenderPhone));
            var receiver = iCusomterservices.GetAllCustomer().Where(p => p.Name.Equals(subCommand.ReceiverName) && p.Phone.Equals(subCommand.ReceiverPhone));
            if (sender.Count() < 1 && receiver.Count() > 0)
            {
                bol.Customers.Add(new Customer() { Name = subCommand.SenderName, Phone = subCommand.SenderPhone, CreatedDate = System.DateTime.Now });
                return new List<Customer> { receiver.FirstOrDefault() };
            }
            else if (sender.Count() > 0 && receiver.Count() < 1)
            {
                bol.Customers.Add (new Customer() { Name = subCommand.ReceiverName, Phone = subCommand.ReceiverPhone, CreatedDate = System.DateTime.Now });
                return new List<Customer> { sender.FirstOrDefault() };
            }
            else if (sender.Count() > 0 && receiver.Count() > 0)
            {
                return new List<Customer> { sender.FirstOrDefault(),receiver.FirstOrDefault() };
            }
            else
            {
                bol.Customers.Add(new Customer() {  Name = subCommand.SenderName, Phone = subCommand.SenderPhone, CreatedDate = System.DateTime.Now });
                bol.Customers.Add(new Customer() {  Name = subCommand.ReceiverName, Phone = subCommand.ReceiverPhone, CreatedDate = System.DateTime.Now });            
                return  bol.Customers.ToList();
            }
        }
        public void CreateNewBol(BolVM command,CustomerVM subCommand)
        {
            BillOfLanding bol = new BillOfLanding();
            //Many to Many relationship handle
            var branchFrom = new Branch() { Id = command.BolFromId };
            var branchTo = new Branch() { Id = command.BolToId };
            var customerList =  ValidateCustomerInfo(bol, subCommand);
            //End
            bol.Start = command.Start;
            bol.Contact = command.Contact;
            bol.AdditionalFee = command.AdditionalFee;
            bol.BolCode = command.BolCode;
            bol.CreatedDate = System.DateTime.Now;
            bol.DeliveryType = command.DeliveryType;
            bol.Prepaid = command.Prepaid;
            bol.MerchandiseType = command.MerchandiseTypeId;
            bol.Liabilities = command.Liabilities;     
            bol.ReceiveDate = System.DateTime.ParseExact(command.ReceiveDate,"dd/M/yyyy",CultureInfo.InvariantCulture);
            bol.ReceiveTime =  System.DateTime.ParseExact(command.ReceiveTime,"HH:mm:ss", CultureInfo.InvariantCulture);
            bol.SendAddress = command.SendAddress;  
            bol.SendDate = System.DateTime.ParseExact(command.SendDate,"dd/M/yyyy", CultureInfo.InvariantCulture);
            bol.StatusCode = command.StatusCode;          
            bol.Total = command.Total;
            bol.Weight = command.Weight;
            bol.Quantity = command.Quantity;
            bol.MixValue = command.MixedValue;
            bol.DeliveryPrice = command.DeliveryPrice;
            bol.SubTotal = command.SubTotal;
            bol.Discount = command.Discount;
            bol.IsGuarantee = command.IsGuarantee;
            bol.IsOnHand = command.IsOnHand;
            bol.IsAlarm = command.IsAlarm;
            bol.DeclareValue = command.DeclareValue;
            bol.Contact = command.Contact;
            bol.Start = command.Start;
            addBolHandler.Handle(new BolAddCommand { BOL = bol, Branches = new List<Branch> { branchFrom, branchTo }, Customers = customerList});                   
        }
        public void UpdateStatus(int bolId)
        {
            bolStatusUpdateHandler.Handle(new BolStatusUpdateCommand { Id = bolId });
        }
        public void UpdateStatusByBolCode(string bolCode)
        {
            bolStatusUpdateByBolCodeHandler.Handle(new BolUpdateStatusByBolCodeCommand { BolCode = bolCode });
        }

        public BillOfLanding GetBolById(int id)
        {
            return getBolByIdHandler.Handle(new GetBolByIdQuery { Id = id });
        }
        public BillOfLanding GetBolByBolCode(string bolCode)
        {
            return getBolByBolCodeHandler.Handle(new GetBolByBolCodeQuery { BolCode = bolCode });
        }
    }
}