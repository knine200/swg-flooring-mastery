using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCorporation.Models
{
    public class OrderSlip
    {
        public DateTime OrderDate { get; set; }
        public Order Order { get; set; }
    }
}
