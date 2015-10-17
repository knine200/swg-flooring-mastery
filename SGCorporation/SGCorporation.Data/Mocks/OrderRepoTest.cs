using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGCorporation.Models;
using SGCorporation.Models.Interfaces;

namespace SGCorporation.Data
{
    public class OrderRepoTest : IOrderRepository

    {
        private static List<Order> _order = new List<Order>();

        public OrderRepoTest()
        {
            _order.AddRange(new List<Order>()
            {
                new Order
                {
                    OrderNumber = 1, CustomerName = "PAUL", Tax = 400.00M, Area = 100.00M, ProductType = "STEEL", StateName = "OH", TaxRate = 1.00M, LaborCostPerSquareFoot = 2.00M, CostPerSquareFoot = 2.00M
                , MaterialCost = 200M, LaborCost = 200M, Total = 800.00M
                }
            });
        }

        public List<Order> GetAllOrders(DateTime date)
        {
            return _order;
        }

        public void Add(Order newOrder)
        {
            newOrder.OrderNumber = (_order.Any()) ? _order.Max(c => c.OrderNumber) + 1 : 1;

            _order.Add(newOrder);
        }

        public void Delete(int OrderNumber)
        {
            _order.RemoveAll(c => c.OrderNumber == OrderNumber);
        }

        public void EditOrder(DateTime date, Order order)
        {
            Delete(order.OrderNumber);
            _order.Add(order);
        }

        public Order GetByNo(int OrderNumber)
        {
            return _order.FirstOrDefault(c => c.OrderNumber == OrderNumber);
        }

        public int WriteNewLine(Order Order)
        {
            return 0;
        }

        public Order GetOrder(DateTime date, int number)
        {
            return _order.FirstOrDefault(c => c.OrderNumber == number);
        }

        public void RemoveOrder(DateTime OrderDate, Order OrderToUpdate)
        {

        }
    }
}
