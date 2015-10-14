using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCorporation.UI.Workflows
{
    public class EditOrderWorkflow
    {
        public void Execute()
        {
            DisplayOrdersWorkflow dow = new DisplayOrdersWorkflow();
            dow.Execute();


            
        }

      
    }
}
