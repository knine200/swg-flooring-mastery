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
        private static string _value = ConfigurationManager.AppSettings["Option"];

        public static IOrderRepository CreateOrderRepository()
        {

            switch (_value)
            {
                //case "Test":
                // return new TestRepository();
                    
                case "Prod":
                    return new OrderRepository();
                default:
                    throw new Exception(string.Format("{0} not supported!"));
            }

        }
    }
}
