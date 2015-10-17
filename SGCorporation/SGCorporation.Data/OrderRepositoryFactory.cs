using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using SGCorporation.Models.Interfaces;

namespace SGCorporation.Data
{
    public class OrderRepositoryFactory
    {
        public IOrderRepository CreateOrderRepository()
        {

            switch (ConfigurationManager.AppSettings["mode"].ToLower())
            {
                case "test":
                    return new OrderRepoTest();
                case "prod":
                    return new OrderRepository();
                default:
                    throw new NotSupportedException();

            }
        }
    }
}
