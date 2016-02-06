using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BlackboardDownloader
{
    // Contains some utility functions used in BlackboardDownloader
    public static class BbUtils
    {
        public static string CleanDirectory(string directory)
        {
            char[] illegalChars = { '<', '>', ':', '"', '/', '\\', '|', '?', '*' };
            return illegalChars.Aggregate(directory, (current, c) => current.Replace(c.ToString(), string.Empty)).Truncate(30);
        }

        public static string CleanFileName(string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }
    }

    // Class that adds truncate functionality to string objects.
    public static class StringExt
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }

    public class Logger
    {
        private string exePath;
        private string filename;

        public Logger(string initMessage, string filename)
        {
            exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            this.filename = filename;
            Write(initMessage);
        }

        public void Write(string message)
        {
            try
            {
                using (TextWriter tw = File.AppendText(exePath + "\\" + filename))
                {
                    tw.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + ": " + message);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Logging error. File not found.");
            }
        }

        public string GetLogFilePath()
        {
            return exePath + "\\" + filename;
        }

        public void WriteException(Exception e)
        {
            Write(e.GetType() + ": " + e.Message);
        }
    }
}
