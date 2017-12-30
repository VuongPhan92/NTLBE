using Data;
using Domain;
using System.Collections.Generic;

namespace WebCore.Command
{
    public class BolAddCommand
    {
           public BillOfLanding BOL { get; set; }
           public List<Branch> Branches { get; set; }
           public List<Customer> Customers { get; set; }
           public UserViewModel CurrentUser { get; set; }
    }
}
