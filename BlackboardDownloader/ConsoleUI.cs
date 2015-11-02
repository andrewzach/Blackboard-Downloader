using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackboardDownloader
{
    public class ConsoleUI
    {
        public static Scraper scraper;
        public static void Main(string[] args)
        {
            scraper = new Scraper();
            Console.WriteLine("Welcome to the Webcourses Downloader\n");

            //Login
            while(!Login())
            {
                Console.WriteLine("Invalid login. Please try again.\n");
            }
            Console.WriteLine("Login successful!\n");

            //Populate Content
            Console.WriteLine("Populating content data from webcourses. Please wait...");
            scraper.PopulateAllData();
            Console.WriteLine("Content population complete\n");

            //Choose output directory
            Console.Write("Enter desired output directory: ");
            string output = Console.ReadLine();
            scraper.OutputDirectory = output;

            //Download module files
            string again = "Y";
            while (again.ToUpper() == "Y")
            {
                DownloadModules();
                Console.Write("Again? [Y/N]: ");
                again = Console.ReadLine();
            }
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
            return scraper.Login(username, password);
        }

        public static void DownloadModules()
        {
            List<string> modules = scraper.GetModuleNames();
            Console.WriteLine("Enter the number of the module you'd like to download content for.");
            Console.WriteLine("Your Modules");
            Console.WriteLine("---------------------------------");
            DisplayModules(modules);
            Console.Write(">>");
            int choice = GetChoice(modules);
            scraper.DownloadModuleFiles(modules[choice]);
        }
        public static int GetChoice(List<string> modules)
        {
            int choice = Int32.Parse(Console.ReadLine());
            while (choice <= 0 || choice > modules.Count)
            {
                Console.WriteLine("Invalid option. Enter the number of your choice");
                choice = Int32.Parse(Console.ReadLine());
            }
            return choice-1;     
        }
        public static void DisplayModules(List<string> modules)
        {
            for (int i = 0; i < modules.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + modules[i]);
            }
        }
    }
}
