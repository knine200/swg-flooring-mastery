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
        public Response GetOrder(OrderSlip Order)
        {
            OrderRepository repo = new OrderRepository();
            Response response = new Response();

            Order order = repo.GetOrder(Order);

            if (order == null)
            {
                response.Success = false;
                response.Message = "This order does not exist";
            }
            else
            {
                response.Success = true;
                response.OrderSlip.Order = order;
            }

            return response;
        }
    }
}
