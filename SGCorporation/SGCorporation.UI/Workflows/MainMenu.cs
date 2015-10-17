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
                Console.WriteLine("##########################################################");
                Console.WriteLine("##########################################################");
                Console.WriteLine("############### SG CORPORATION FLOORING APP ##############");
                Console.WriteLine("###############              BY             ##############");
                Console.WriteLine("###############         JIM & KOSHIN        ##############");
                Console.WriteLine("##########################################################");
                Console.WriteLine("##########################################################");
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.Write(" ");
                Console.WriteLine("                   (1) Display Orders");
                Console.Write(" ");
                Console.WriteLine("                   (2) Add an Order");
                Console.Write(" ");
                Console.WriteLine("                   (3) Edit an Order");
                Console.Write(" ");
                Console.WriteLine("                   (4) Remove an Order");
                Console.Write(" ");
                Console.WriteLine();
                Console.WriteLine(" " + "                   (Q) to Quit");
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.WriteLine("##########################################################");
                Console.WriteLine();
                Console.Write("                     Enter Choice: ");

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
                    Console.WriteLine();
                    Console.WriteLine("{0} is an invalid choice", choice);
                    Console.WriteLine("Try again");
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
