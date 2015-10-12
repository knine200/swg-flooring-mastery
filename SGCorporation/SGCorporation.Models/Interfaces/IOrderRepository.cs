﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCorporation.Models.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetOrderInformation(DateTime OrderDate);
    }
}
