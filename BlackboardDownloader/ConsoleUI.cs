using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackboardDownloader
{
    public class ConsoleUI
    {
        public static Dictionary<char, string> menuOptions;
        public static Scraper scraper;

        public static void Main(string[] args)
        {

            DisplayWelcome();
            Initialize();
            menuOptions = new Dictionary<char, string>
            {
                { 'D', "Download Content" },
                { 'V', "View Content" },
                { 'O', "Change Output Direcctory" },
                { 'Q', "Quit" }
            };
            DisplayMenu();
            char choice = GetMenuChoice();
            while (choice != 'Q')
            {
                switch(choice)
                {
                    case 'D': DownloadContent(); break;
                    case 'V': ViewContent(); break;
                    case 'O': ChangeOutputDir(); break;
                }
                DisplayMenu();
                choice = GetMenuChoice();
            }
            Console.WriteLine("Goodbye");
            Console.ReadLine();
        }

        public static void Initialize()
        {
            scraper = new Scraper();
            //Login
            while (!Login())
            {
                Console.WriteLine("Invalid login. Please try again.\n");
            }
            Console.WriteLine("Login successful!\n");

            //Populate Content
            Console.WriteLine("Populating content data from webcourses. Please wait...");
            scraper.PopulateAllData();
            Console.WriteLine("\nContent population complete\n");
            Console.WriteLine("Modules found: ");
            DisplayModules(scraper.GetModuleNames());
            Console.WriteLine();
        }

        public static void DisplayWelcome()
        {
            Console.WriteLine("###############################################");
            Console.WriteLine("######### Blackboard Downloader v 0.1 #########");
            Console.WriteLine("############# By Andrew Zacharias #############");
            Console.WriteLine("###############################################");
            Console.WriteLine();
        }

        public static void DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("--------- MAIN MENU ---------");
            foreach (var option in menuOptions)
            {
                Console.WriteLine(option.Key + ". " + option.Value);
            }
        }

        public static char GetMenuChoice()
        {
            char choice;
            Console.Write(">> ");
            choice = Console.ReadLine().ToUpper()[0];     // Take first char of input string as selected choice
            while (!menuOptions.ContainsKey(choice))
            {
                Console.WriteLine("Invalid input. Try again.");
                Console.Write(">> ");
                choice = Console.ReadLine().ToUpper()[0];
            }
            return choice;
        }

        public static void ViewContent()
        {
            Console.WriteLine("Option still in development. Come back later...");
        }

        public static void ChangeOutputDir()
        {
            //Choose output directory
            Console.WriteLine("\nCurrent output directory: " + scraper.OutputDirectory);
            Console.Write("Enter desired output directory: ");
            string output = Console.ReadLine();     // TODO: Verify valid output directory
            scraper.OutputDirectory = output;
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

        public static void DownloadContent()
        {
            List<string> modules = scraper.GetModuleNames();
            Console.WriteLine("\nEnter the number of the module you'd like to download content for.");
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
