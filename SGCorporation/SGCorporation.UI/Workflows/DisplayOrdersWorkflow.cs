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

            DateTime date;

            DateTime.TryParse(input, out date);

            return date.Date;
        }

        public void DisplayOrderInformation(DateTime userDate)
        {
            OrderOperations ops = new OrderOperations();

            Response response = ops.GetAllOrders(userDate);

            if (response.Success)
            {
                Console.WriteLine(response.Message);
            }
            else
            {
                Console.WriteLine(response.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

        }

    }
}
