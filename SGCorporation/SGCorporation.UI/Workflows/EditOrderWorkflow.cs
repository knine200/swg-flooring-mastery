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
    public class EditOrderWorkflow
    {
        public OrderOperations ops;
        public Response response;
        private ErrorLog oErrorLog;

        public EditOrderWorkflow()
        {
            ops = new OrderOperations();
            response = new Response();
            oErrorLog = new ErrorLog();

        }

        public void Execute()
        {
            Console.Clear();
            Order order = new Order();
            DateTime currentDate;

            do
            {
                currentDate = PromptForDate();
                response = ops.GetAllOrders(currentDate);

                if (response.Success == false)
                {
                    Console.WriteLine();
                    Console.WriteLine("That date does not exist");
                    Console.WriteLine();
                }
            } while (response.Success == false);


            do
            {
                int orderNo = PromptForOrderNo();
                order = ops.GetOrderNo(currentDate, orderNo);

                if (order == null)
                {
                    Console.WriteLine();
                    Console.WriteLine("That order number does not exist");
                    Console.WriteLine();
                }
            } while (order == null);

            try
            {
                order.CustomerName = PromptToEditStrings("CustomerName", order.CustomerName);

                //order.CustomerName = PromptToEditStrings("CustomerName", order.CustomerName);
                order.StateName = PromptToEditStrings("StateName", order.StateName);
                string stateName = order.StateName;

                Tax currentStateTax = ops.ReturnTax(stateName.ToUpper());
                order.TaxRate = currentStateTax.TaxRate;

                order.ProductType = PromptToEditStrings("ProductType", order.ProductType);

                string productType = order.ProductType;
                Product currentProduct = ops.ReturnProduct(productType);

                order.CostPerSquareFoot = currentProduct.CostPerSquareFoot;

                order.LaborCostPerSquareFoot = currentProduct.LaborCostPerSquareFoot;

                order.Area = PromptToEditDecimal("Area", order.Area);

                order.MaterialCost = order.Area * order.CostPerSquareFoot;
                order.LaborCost = order.Area * order.LaborCostPerSquareFoot;
                order.Tax = (order.MaterialCost + order.LaborCost) * order.TaxRate;
                order.Total = order.MaterialCost + order.LaborCost + order.Tax;

                ops.EditOrder(currentDate, order);

                throw new Exception();
            }
            catch (Exception ex)
            {
                Console.WriteLine(oErrorLog.WriteErrorLog(ex.ToString()));
                Console.WriteLine("Invalid entry");
                Console.ReadLine();
            }



            //order.CustomerName = PromptToEditStrings("CustomerName", order.CustomerName);
            //order.StateName = PromptToEditStrings("StateName", order.StateName);
            //string stateName = order.StateName;

            //Tax currentStateTax = ops.ReturnTax(stateName.ToUpper());
            //order.TaxRate = currentStateTax.TaxRate;

            //order.ProductType = PromptToEditStrings("ProductType", order.ProductType);

            //string productType = order.ProductType;
            //Product currentProduct = ops.ReturnProduct(productType);

            //order.CostPerSquareFoot = currentProduct.CostPerSquareFoot;

            //order.LaborCostPerSquareFoot = currentProduct.LaborCostPerSquareFoot;

            //order.Area = PromptToEditDecimal("Area", order.Area);

            //order.MaterialCost = order.Area * order.CostPerSquareFoot;
            //order.LaborCost = order.Area * order.LaborCostPerSquareFoot;
            //order.Tax = (order.MaterialCost + order.LaborCost) * order.TaxRate;
            //order.Total = order.MaterialCost + order.LaborCost + order.Tax;

            //ops.EditOrder(currentDate, order);
        }

        public DateTime PromptForDate()
        {

            Console.Write("Enter the date for your order (MM/DD/YYYY): ");

            string input = Console.ReadLine();

            DateTime date;

            DateTime.TryParse(input, out date);

            return date.Date;

        }

        public int PromptForOrderNo()
        {
            Console.Write("Enter the order number: ");
            string input = Console.ReadLine();


            int OrderNo = int.Parse(input);

            return OrderNo;
        }

        public string PromptToEditStrings(string propertyName, string propertyValue)
        {
            string answer;
            do
            {
                Console.Write("Would you like to edit {0}? (Y/N) ", propertyName);
                answer = Console.ReadLine();

                if (answer.ToUpper() == "Y")
                {
                    break;
                }
                if (answer.ToUpper() == "N")
                {
                    return propertyValue;

                    
                }
            } while (answer.ToUpper() != "Y" || answer.ToUpper() != "N" || answer == null);

            switch (propertyName)
            {
                case "CustomerName":
                    string input;
                    do
                    {
                        Console.Write("Enter a new {0}: ", propertyName);
                        input = Console.ReadLine();

                        if (string.IsNullOrEmpty(input))
                        {
                            input = "1";
                        }


                        response = ops.ValidInputCheckString(input);

                        if (response.Success == false)
                        {
                            Console.WriteLine(response.Message);
                        }
                    } while (response.Success == false);

                    decimal inputAmount;

                    if (decimal.TryParse(input, out inputAmount))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid entry");
                        Console.ReadLine();
                    }

                    if (input == propertyValue || string.IsNullOrEmpty(input))
                    {
                        return propertyValue;
                    }

                    return input.ToUpper();

                case "StateName":
                    string input1;
                    do
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("(OH) Ohio     (MI) Michigan     (PA) Pennsylvania     (IN) Indiana");
                        Console.ResetColor();
                        Console.Write("Enter a new {0}: ", propertyName);
                        input1 = Console.ReadLine();

                        if (string.IsNullOrEmpty(input1) || (input1.ToUpper() != "PA" && input1.ToUpper() != "IN" && input1.ToUpper() != "MI" && input1.ToUpper() != "OH"))
                        {
                            input1 = "1";
                        }

                        response = ops.ValidInputCheckString(input1);

                        if (response.Success == false)
                        {
                            Console.WriteLine(response.Message);
                        }
                    } while (response.Success == false);

                    if (decimal.TryParse(input1, out inputAmount))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid entry");
                        Console.ReadLine();
                    }

                    if (input1 == propertyValue || string.IsNullOrEmpty(input1))
                    {
                        return propertyValue;
                    }
                    return input1.ToUpper();

                case "ProductType":
                    string input2;
                    do
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Carpet     Laminate     Tile     Wood");
                        Console.ResetColor();
                        Console.Write("Enter a new {0}: ", propertyName);
                        input2 = Console.ReadLine();

                        if (string.IsNullOrEmpty(input2) ||( input2.ToUpper() != "CARPET" && input2.ToUpper() != "TILE" && input2.ToUpper() != "WOOD" &&
                            input2.ToUpper() != "LAMINATE" ))
                        {
                            input2 = "1";
                        }

                        response = ops.ValidInputCheckString(input2);

                        if (response.Success == false)
                        { 
                            Console.WriteLine(response.Message);
                        }

                    } while (response.Success == false);

                    string input3 = ops.UppercaseFirst(input2);

                    if (decimal.TryParse(input3, out inputAmount))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid entry");
                        Console.ReadLine();
                    }

                    if (input3 == propertyValue || string.IsNullOrEmpty(input3))
                    {
                        return propertyValue;
                    }
                    return input3;

                default:
                    Console.WriteLine();
                    Console.WriteLine("Invalid entry");
                    return Console.ReadLine();

            }
        }

        public decimal PromptToEditDecimal(string propertyName, decimal propertyValue)
        {
            string answer;

            do
            {
                Console.Write("Would you like to edit {0}? (Y/N) ", propertyName);
                answer = Console.ReadLine();

                if (answer.ToUpper() == "Y")
                {
                    break;
                }
                if (answer.ToUpper() == "N")
                {
                    return propertyValue;
                }

            } while (answer.ToUpper() != "Y" || answer.ToUpper() != "N" || answer == null);

            switch (propertyName)
            {
                case "Area":

                    string input;
                    do
                    {
                        Console.Write("Enter a new {0}: ", propertyName);
                        input = Console.ReadLine();
                        response = ops.ValidInputCheckDecimal(input);

                        if (response.Success == false)
                        {
                            Console.WriteLine(response.Message);
                        }

                    } while (response.Success == false);


                    decimal inputAmount;

                    if (decimal.TryParse(input, out inputAmount))
                    {
                        if (inputAmount == propertyValue)
                        {
                            return propertyValue;
                           
                        }

                        return inputAmount;
                    }

                    //Console.WriteLine("Invalid entry");
                    //Console.ReadLine();
                    return 0;
                    

                default:
                    Console.WriteLine("Invalid entry");
                    Console.ReadLine();
                    return 0;
            }
        }
    }
}
