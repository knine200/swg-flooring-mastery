using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGCorporation.UI.Workflows;
using System.Configuration;

namespace SGCorporation.UI
{
    public class Program
    {
        static void Main(string[] args)
        {

            MainMenu main = new MainMenu();
            main.Execute();

        }
    }
}
