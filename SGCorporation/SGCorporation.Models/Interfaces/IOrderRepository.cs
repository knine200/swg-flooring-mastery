using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCorporation.Models.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders(DateTime OrderDate);

        int WriteNewLine(Order Order);

        void EditOrder(DateTime date, Order order);

        Order GetOrder(DateTime date, int number);

        void RemoveOrder(DateTime OrderDate, Order OrderToUpdate);
    }
}
