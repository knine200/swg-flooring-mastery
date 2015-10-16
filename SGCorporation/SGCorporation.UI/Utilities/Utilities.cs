using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCorporation.UI.Utilities
{
    public static class Utilities
    {
        public static void LogFile(string ExceptionName, string EventName, string ControlName, int ErrorLineNo, string FormName)
        {
            StreamWriter log;

            if (!File.Exists(@"DataFile\log.txt"))
            {
                log = new StreamWriter(@"DataFile\log.txt");
            }
            else
            {
                log = File.AppendText(@"DataFile\log.txt");
            }

            log.WriteLine("Date Time: {0}", DateTime.Now);
            log.WriteLine("Exception Name: {0}", ExceptionName);
            log.WriteLine("Event Name: {0}", EventName);
            log.WriteLine("Control Name: {0}", ControlName);
            log.WriteLine("Error Line No.: {0}", ErrorLineNo);
            log.WriteLine("Form Name: {0}", FormName);

            log.Close();
        }

        public static int ExceptionLineNumber(this Exception e)
        {
            int lineNum = 0;

            try
            {
                lineNum = Convert.ToInt32(e.StackTrace.Substring(e.StackTrace.LastIndexOf(": line") + 5));
            }
            catch
            {
                // stack trace is not available
            }

            return lineNum;
        }

        //    try

        //    {

        //        //put here your code

        //    }

        //    catch (Exception exe)

        //    {

        //       //call LogFile method and pass argument as Exception message, event name, control name, error line number, current form name

        //        LogFile(exe.Message, e.ToString(), ((Control)sender).Name, exe.LineNumber(), this.FindForm().Name);

        //    }
    }
}
