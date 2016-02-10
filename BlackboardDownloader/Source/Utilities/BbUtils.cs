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
        // Removes illegal characters from a directory string and truncates it to 30 chars max
        // Does not guarantee a legal directory name in Windows... but should work almost 100%
        public static string CleanDirectory(string directory)
        {
            char[] illegalChars = { '<', '>', ':', '"', '/', '\\', '|', '?', '*' };
            return illegalChars.Aggregate(directory, (current, c) => current.Replace(c.ToString(), string.Empty)).Truncate(30);
        }

        // Removes illegal characters from a filename
        public static string CleanFileName(string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }
    }

    // Class that adds truncate functionality to string objects.
    public static class StringExt
    {
        // Truncate string to maxLength characters
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }

    // Class that writes messages to a log file
    // Used by Scraper to log errors and other important diagnostic information
    public class Logger
    {
        private string exePath;
        private string filename;

        public Logger(string initMessage, string filename)
        {
            exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); // location of BlackboardDownloader.exe
            this.filename = filename;
            Write(initMessage);
        }

        // The full file path of the log file, including filename.
        public string FilePath
        {
            get { return exePath + "\\" + filename; }
        }

        // Writes a message to the log
        public void Write(string message)
        {
            try
            {
                using (TextWriter tw = File.AppendText(FilePath))
                {
                    // Write the date and then the message to the log file. 
                    tw.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + ": " + message);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Logging error. File not found.");
            }
        }

        // Write an exception to the log
        public void WriteException(Exception e)
        {
            Write(e.GetType() + ": " + e.Message);
        }
    }
}
