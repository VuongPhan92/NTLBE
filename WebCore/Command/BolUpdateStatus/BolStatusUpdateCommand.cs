﻿using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCore.Command
{
    public class BolStatusUpdateCommand
    {
        public int Id { get; set; }
        public UserViewModel CurrentUser { get; set; }

    }
}
