using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCorporation.UI.Workflows
{
    public class MainMenu
    {
        public void Execute()
        {
            string input;

            do
            {
                Console.Clear();
                Console.WriteLine("WELCOME TO SG CORP!");
                Console.WriteLine("---------------------");
                Console.WriteLine("1. Display Orders");
                Console.WriteLine("2. Add an Order");
                Console.WriteLine("3. Edit an Order");
                Console.WriteLine("4. Remove an Order");
                Console.WriteLine();
                Console.WriteLine("(Q) to Quit");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Enter Choice: ");

                input = Console.ReadLine();

                if (input.ToUpper() != "Q")
                {
                    ProcessChoice(input);
                }


            } while (input.ToUpper() != "Q");

        }

        public void ProcessChoice(string choice)
        {
            switch (choice)
            {
                case "1":
                    DisplayOrdersWorkflow dow = new DisplayOrdersWorkflow();
                    dow.Execute();
                    break;
                case "2":
                    AddOrderWorkflow aow = new AddOrderWorkflow();
                    aow.Execute();
                    break;
                case "3":
                    EditOrderWorkflow eow = new EditOrderWorkflow();
                    eow.Execute();
                    break;
                case "4":
                    RemoveOrderWorkflow rwf = new RemoveOrderWorkflow();
                    rwf.Execute();
                    break;
                default:
                    Console.WriteLine("{0} is an invalid entry!", choice);
                    Console.WriteLine("Press Enter to continue....");
                    Console.ReadLine();
                    break;

            }




        }
    }
}
