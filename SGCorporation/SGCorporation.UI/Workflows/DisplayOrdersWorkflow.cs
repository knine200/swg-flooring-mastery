using System;
using System.Collections.Generic;
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
            
            
        }

        public Response GetDateFromUser()
        {
            
               Console.Clear();
                Console.Write("Enter the date for your order (MMDDYYYY): ");
                string input = Console.ReadLine();

            var date = DateTime.Parse(input);

            var ops = new OrderOperations();
            var dateResult = ops.GetOrder(date);


            return dateResult;

        }

    }
}
