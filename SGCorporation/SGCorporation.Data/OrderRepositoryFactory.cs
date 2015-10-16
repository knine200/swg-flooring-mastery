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
        private string _value = ConfigurationManager.AppSettings["Option"];

        public IOrderRepository CreateOrderRepository()
        {

            switch (_value)
            {
                //case "Test":
                // return new OrderRepoTest();
                    
                case "Prod":
                    return new OrderRepository();
                default:
                    throw new Exception(string.Format("{0} not supported!"));
            }

        }
    }
}
