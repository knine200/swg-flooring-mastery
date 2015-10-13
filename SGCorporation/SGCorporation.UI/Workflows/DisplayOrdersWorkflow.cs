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
    public class DisplayOrdersWorkflow
    {
        private Order _currentOrder;

        public void Execute()
        {
            DateTime userDate = GetDateFromUser();

            DisplayOrderInformation(userDate);
        }

        public DateTime GetDateFromUser()
        {

            Console.Clear();
            Console.Write("Enter the date for your order (MMDDYYYY): ");
            string input = Console.ReadLine();

            DateTime date = Convert.ToDateTime(input).Date;

            return date;
        }

        public void DisplayOrderInformation(DateTime userDate)
        {
            OrderOperations ops = new OrderOperations();
            Response response = ops.GetOrder(userDate);

            if (response.Success)
            {
                _currentOrder = response.OrderSlip.Order;
                PrintOrderInformation(response.OrderSlip);

            }
        }

        public void PrintOrderInformation(OrderSlip OrderSlip)
        {
            Console.Clear();
            Console.WriteLine("Order Information");
            Console.WriteLine("-------------------------");
            Console.WriteLine("Order Number: {0}", OrderSlip.Order.OrderNumber);
            Console.WriteLine("Customer Name: {0}", OrderSlip.Order.CustomerName);
        }
    }
}
