using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SGCorporation.BLL;
using SGCorporation.Models;

namespace SGCorporation.UI.Workflows
{
    public class DisplayOrdersWorkflow
    {
        ErrorLog oErrorLog = new ErrorLog();

        public void Execute()
        {
            DateTime userDate = GetDateFromUser();

            Console.Clear();

            DisplayOrderInformation(userDate);
        }

        public DateTime GetDateFromUser()
        {
            Console.Clear();

            string input = "";
            DateTime date1;
            try
            {
                Console.Write("Enter the date for your order (MM/DD/YYYY): ");


                input = Console.ReadLine();


                DateTime.TryParse(input, out date1);

                throw new Exception();


            }
            catch (Exception ex)
            {
                oErrorLog.WriteErrorLog(ex.ToString());
                OrderOperations ops = new OrderOperations();
                WrongUserInputLog log = new WrongUserInputLog()
                {
                    ErrorTime = DateTime.Now,
                    Message = $"Display An Order -> invalid user input -> {input}"
                };
                ops.PassOnWrongUserInput(log);
            }

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
