using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGCorporation.BLL;
using SGCorporation.Models;

namespace SGCorporation.UI.Workflows
{
    public class AddOrderWorkflow
    {

        public void Execute()
        {
            Console.Clear();

            OpenOrder();
        }
        public void OpenOrder()
        {
            OrderOperations ops = new OrderOperations();
            Response response = ops.CreateOrder();

            if (response.Success)
            {
                Console.WriteLine();
                Console.WriteLine("New order created!");
                Console.WriteLine("Order number:                  {0}", response.CreateOrderInfo.OrderNumber);
                Console.WriteLine("Customer Name:                  {0}", response.CreateOrderInfo.CustomerName);
                Console.WriteLine("State Abbreviation:             {0}", response.CreateOrderInfo.StateName.ToUpper());
                Console.WriteLine("TaxRate:                        {0}", response.CreateOrderInfo.TaxRate);
                Console.WriteLine("Product Type:                   {0}", response.CreateOrderInfo.ProductType);
                Console.WriteLine("Area:                           {0}", response.CreateOrderInfo.Area);
                Console.WriteLine("Cost Per Square Foot:          {0:c}", response.CreateOrderInfo.CostPerSquareFoot);
                Console.WriteLine("Labor Cost Per Square Foot:    {0:c}", response.CreateOrderInfo.LaborCostPerSquareFoot);
                Console.WriteLine("Material Cost:                 {0:c}", response.CreateOrderInfo.MaterialCost);
                Console.WriteLine("Labor Cost:                    {0:c}", response.CreateOrderInfo.LaborCost);
                Console.WriteLine("Tax:                           {0:c}", response.CreateOrderInfo.Tax);
                Console.WriteLine("Total:                         {0:c}", response.CreateOrderInfo.Total);
                Console.WriteLine();
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }

        }
    }
}
