using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGCorporation.Models;
using SGCorporation.Models.Interfaces;

namespace SGCorporation.Data.Mocks
{
    public class TaxRepoTest : ITaxRepository
    {
        public List<Tax> GetAllTax()
        {
            return new List<Tax>()
           {
               new Tax() {StateAbbreviation = "OH", StateName = "Ohio", TaxRate = 15M},
                new Tax() {StateAbbreviation = "IN", StateName = "Indiana", TaxRate = 7.5M}
           };
        }

        public Tax GetTax(string StateAbbreviation)
        {
            List<Tax> taxes = GetAllTax();
            return taxes.FirstOrDefault(t => t.StateAbbreviation == StateAbbreviation);
        }
    }
}
