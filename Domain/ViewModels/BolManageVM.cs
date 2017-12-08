﻿using Data;

namespace Domain.ViewModels
{
    public class BolManageVM
    {
       public BillOfLanding Bol { get; set; }
        public Customer Sender { get; set; }
        public Customer Receiver { get; set; }
        public string SendDate { get; set; }
        public Branch BolFrom { get; set; }
        public Branch BolTo { get; set; }
       
    }
}
