using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCorporation.Models
{
    public class Order
    {
        public int OrderNumber { get; set; }

        public string CustomerName { get; set; }
        public Tax StateName { get; set; }
        public Tax TaxRate { get; set; }
        public Product ProductType { get; set; }
        public decimal Area { get; set; }

        public Product CostPerSquareFoot { get; set; }
        public Product LaborCostPerSquareFoot { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal LaborCost { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
    }
}
