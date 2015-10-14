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

            DateTime currentDate = PromptForDate();
            int orderNo = PromptForOrderNo();

            OrderOperations ops = new OrderOperations();

            var order = ops.GetOrderNo(currentDate, orderNo);

            ops.RemoveOrder(currentDate, order);

            Console.ReadLine();
        }

        public DateTime PromptForDate()
        {
            Console.Write("Enter the date for your Order Date (MMDDYYYY): ");
            string input = Console.ReadLine();

           
            string format = "MMddyyyy";
            DateTime date = DateTime.ParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None);

            return date;
        }

        public int PromptForOrderNo()
        {
            Console.Write("Enter the order number: ");
            string input = Console.ReadLine();

            int OrderNo = int.Parse(input);

            return OrderNo;
        }


    }
}
