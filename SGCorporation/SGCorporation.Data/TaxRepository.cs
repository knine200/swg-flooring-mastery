using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGCorporation.Models;

namespace SGCorporation.Data
{
    public class TaxRepository
    {
        private const string _filePath = @"DataFiles\Taxes.txt";

        public List<Tax> GetAllTax()
        {
            List<Tax> taxes = new List<Tax>();

            string[] reader = File.ReadAllLines(_filePath);

            for (int i = 1; i < reader.Length; i++)
            {
                string[] columns = reader[i].Split(',');

                Tax tax = new Tax();

                tax.StateAbbreviation = columns[0];
                tax.StateName = columns[1];
                tax.TaxRate = decimal.Parse(columns[2]);

                taxes.Add(tax);
            }

            return taxes;
        }

        public Tax GetTax(string StateAbbreviation)
        {
            List<Tax> taxes = GetAllTax();
            return taxes.FirstOrDefault(t => t.StateAbbreviation == StateAbbreviation);
        }
    }
}
