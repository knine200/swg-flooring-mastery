using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGCorporation.BLL;
using SGCorporation.Models;

namespace SGCorporation.UI.Workflows
{
    public class RemoveOrderWorkflow
    {
        public void Execute()
        {
            Console.Clear();
            OrderOperations ops = new OrderOperations();
            Order orderToRemove = new Order();
            Response response1 = new Response();

            DateTime currentDate;

            do
            {
                currentDate = PromptForDate();
                response1 = ops.GetAllOrders(currentDate);
                if (response1.Success == false)
                {
                    Console.WriteLine("This date does not exist!");
                }
            } while (response1.Success == false);

            do
            {
                int orderNo = PromptForOrderNo();
                orderToRemove = ops.GetOrderNo(currentDate, orderNo);
                if (orderToRemove == null)
                {
                    Console.WriteLine("That order number does not exist. Try again.");
                    Console.WriteLine();
                }
            } while (orderToRemove == null);

            Console.WriteLine();
            Console.WriteLine("OrderNumber: {0} " +
                                  "\nCustomerName: {1} " +
                                  "\nStateName: {2}" +
                                  "\nTaxRate: {3}" +
                                  "\nProductType: {4}" +
                                  "\nArea: {5} " +
                                  "\nCostPerSquareFoot: {6:c} " +
                                  "\nLaborCostPerSquareFoot: {7:c} " +
                                  "\nMaterialCost: {8:c} " +
                                  "\nLaborCost: {9:c} " +
                                  "\nTax: {10:c} " +
                                  "\nTotal: {11:c}",
                                  orderToRemove.OrderNumber,
                                  orderToRemove.CustomerName,
                                  orderToRemove.StateName,
                                  orderToRemove.TaxRate,
                                  orderToRemove.ProductType,
                                  orderToRemove.Area,
                                  orderToRemove.CostPerSquareFoot,
                                  orderToRemove.LaborCostPerSquareFoot,
                                  orderToRemove.MaterialCost,
                                  orderToRemove.LaborCost,
                                  orderToRemove.Tax,
                                  orderToRemove.Total);

            Console.WriteLine();
            Console.Write("Are you sure you want to remove the above order? (Y/N)  ");
            string response = Console.ReadLine();

            if (response.ToUpper() == "Y")
            {
                ops.RemoveOrder(currentDate, orderToRemove);
                Console.WriteLine("The order has been removed");
            }
            else
            {
                Console.WriteLine("Remove order cancelled!");
            }

            Console.ReadLine();
        }



        public DateTime PromptForDate()
        {
            Console.Write("Enter the date for your order (MM/DD/YYYY): ");

            string input = Console.ReadLine();

            string[] format =
            {
                "M/d/yyyy", "MM/dd/yyyy",
                "MMddyyyy", "MM-dd-yyyy",
                "M-d-yyyy", "Mdyy", "MM dd yyyy", "M d yy"
            };

            DateTime date = DateTime.ParseExact(input,
                format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None);


            return date.Date;

        }


        public int PromptForOrderNo()
        {
            Console.Write("Enter the order number: ");
            string input = Console.ReadLine();

            int OrderNo;
            if (int.TryParse(input, out OrderNo))
            {
                return OrderNo;
            }
            else
            {
                Console.WriteLine("Try again");
                Console.ReadLine();
            }

            //int OrderNo = int.Parse(input);

            return OrderNo;
        }
    }
}
