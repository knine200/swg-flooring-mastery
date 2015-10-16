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
                  
            Console.Write("Enter the date for your order (MM/DD/YYYY): ");

            string input = Console.ReadLine();

            string[] format =
            {
                "M/d/yyyy", "MM/dd/yyyy",
                "MMddyyyy", "MM-dd-yyyy",
                "M-d-yyyy", "Mdyy", "MM dd yyyy", "M d yy", "Mdyyyy"
            };

            DateTime date = DateTime.ParseExact(input,
                format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None);


            return date.Date;
        }

        public void DisplayOrderInformation(DateTime userDate)
        {
            OrderOperations ops = new OrderOperations();
            Response response = ops.GetAllOrders(userDate);

            if (response.Success)
            {
                Console.WriteLine(response.Message);
                Console.ReadLine();
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
