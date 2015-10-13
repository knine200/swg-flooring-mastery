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
            ReadSettings();
            //MainMenu main = new MainMenu();
            //main.Execute();
            Console.ReadLine();

        }

        static void ReadSettings()
        {
            var appSettings = ConfigurationManager.AppSettings;

            if (appSettings.Count == 0)
            {
                Console.WriteLine("Empty");
            }
            else
            {
                foreach (var key in appSettings.AllKeys)
                {
                    Console.WriteLine("Key: {0}, Value: {1}", key, appSettings[key]);
                }
            }
        }
    }
}
