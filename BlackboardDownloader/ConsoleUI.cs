using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackboardDownloader
{
    public class ConsoleUI
    {
        public static Scraper bbdl;
        public static void Main(string[] args)
        {
            bbdl = new Scraper();
            Console.WriteLine("Welcome to the Webcourses Downloader\n");
            while(!Login())
            {
                Console.WriteLine("Invalid login. Please try again.\n");
            }
            Console.WriteLine("Login successful!\n");
            bbdl.PopulateModules();
            DisplayModules();
            Console.ReadLine();
        }

        public static bool Login()
        {
            string username, password;
            Console.WriteLine("Please enter your Webcourses login information");
            Console.Write("Username: ");
            username = Console.ReadLine();
            Console.Write("Password: ");
            password = Console.ReadLine();
            return bbdl.Login(username, password);
        }

        public static void DisplayModules()
        {
            Console.WriteLine("Your Modules");
            Console.WriteLine("---------------------------------");
            List<string> modules = bbdl.GetModuleNames();
            for (int i = 0; i < modules.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + modules[i]);
            }
        }
    }
}
