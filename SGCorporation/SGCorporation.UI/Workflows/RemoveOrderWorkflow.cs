using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCorporation.UI.Workflows
{
    public class RemoveOrderWorkflow
    {
        public void Execute()
        {
            DateTime currentDate = PromptForDate();


        }

        public DateTime PromptForDate()
        {
            Console.Write("Enter the date for your Order Date (MMDDYYYY): ");
            string input = Console.ReadLine();

           
            string format = "MMddyyyy";
            DateTime date = DateTime.ParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None);

            return date;
        }


    }
}
