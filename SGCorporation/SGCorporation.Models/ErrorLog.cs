using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCorporation.Models
{
    public class ErrorLog
    {
        private static string LogDirectory = String.Empty;

        public bool WriteErrorLog(string logMessage)
        {
            bool successStatus = false;

            LogDirectory = ConfigurationManager.AppSettings["LogDirectory"].ToString();
            DateTime currentDateTime = DateTime.Now;

            string currentDateTimeString = currentDateTime.ToString();
            CheckCreateLogDirectory(LogDirectory);

            string logLine = BuildLogLine(currentDateTime, logMessage);
            LogDirectory = (LogDirectory + "Log_" + LogFileName(DateTime.Now) + ".txt");

            lock (typeof(ErrorLog))
            {
                StreamWriter m_logSWriter = null;
                try
                {
                    m_logSWriter = new StreamWriter(LogDirectory, true);
                    m_logSWriter.WriteLine(logLine);
                    successStatus = true;
                }
                catch
                {
                }
                finally
                {
                    if (m_logSWriter != null)
                    {
                        m_logSWriter.Close();
                    }
                }
            }

            return successStatus;
        }

        private static bool CheckCreateLogDirectory(string logPath)
        {
            bool loggingDirectoryExists = false;
            DirectoryInfo dirInfo = new DirectoryInfo(logPath);

            if (dirInfo.Exists)
            {
                loggingDirectoryExists = true;
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(logPath);
                    loggingDirectoryExists = true;
                }
                catch
                {
                    // Logging failure
                }

            }

            return loggingDirectoryExists;
        }

        private static string BuildLogLine(DateTime currentDateTime, string logMessage)
        {
            StringBuilder loglineStringBuilder = new StringBuilder();
            loglineStringBuilder.Append(LogFileEntryDateTime(currentDateTime));
            loglineStringBuilder.Append("\t");
            loglineStringBuilder.Append(logMessage);

            return loglineStringBuilder.ToString();
        }

        public static string LogFileEntryDateTime(DateTime currentDateTime)
        {
            return currentDateTime.ToString("dd - MM - yyyy HH: mm:ss");
        }

        private static string LogFileName(DateTime currentDateTime)
        {
            return currentDateTime.ToString("dd_MM_yyyy");
        }

        //        try
        //        {
        //            throw new Exception();
        //        }
        //            catch (Exception ex)
        //        {
        //            oErrorLog.WriteErrorLog(ex.ToString());
        //        }
    }
}

