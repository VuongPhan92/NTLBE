using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCore.Command
{
    public class BranchEmailUpdateCommand
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }
}
