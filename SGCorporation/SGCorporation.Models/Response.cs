﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCorporation.Models
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public OrderSlip OrderSlip { get; set; }
        public CreateOrderSlip CreateOrderInfo { get; set; }
        public EditSlip EditOrderInfo { get; set; }
        public RemoveSlip RemoveOrderInfo { get; set; }
    }
}
