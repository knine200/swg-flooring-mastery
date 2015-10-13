using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGCorporation.Data;
using SGCorporation.Models;

namespace SGCorporation.BLL
{
    public class OrderOperations
    {
        public Response GetAllOrders(DateTime OrderDate)
        {
            OrderRepository repo = new OrderRepository();
            Response response = new Response();

            List<Order> orders = repo.GetAllOrders(OrderDate);

            if (orders == null)
            {
                response.Success = false;
                response.Message = "This order does not exist";
            }
            else
            {
                response.Success = true;
                response.Message = "You got all the orders for the date";
            }

            foreach (var order in orders)
            {
                Console.WriteLine("OrderNumber: {0}, CustomerName: {1}, Area: {2}, CostPerSquareFoot: {3}, LaborCostPerSquareFoot: {4}, MaterialCost: {5}, LaborCost: {6}, Tax: {7}, Total: {8}", order.OrderNumber,  order.CustomerName, order.Area,order.CostPerSquareFoot, order.LaborCostPerSquareFoot,  order.MaterialCost, order.LaborCost, order.Tax, order.Total);
                
            }

            return response;
        }
    }
}
