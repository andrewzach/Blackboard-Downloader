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
                { '1', "Download Content" },
                { '2', "View Content" },
                { '3', "Change Output Direcctory" },
                { '4', "Check For New Content" },
                { 'Q', "Quit" }
            };
            DisplayMenu();
            char choice = GetMenuChoice();
            while (choice != 'Q')
            {
                switch(choice)
                {
                    case '1': DownloadContent(); break;
                    case '2': ViewContent(); break;
                    case '3': ChangeOutputDir(); break;
                    case '4': CheckNewContent(); break;
                }
                DisplayMenu();
                choice = GetMenuChoice();
            }
            Console.WriteLine("Goodbye");
            scraper.SaveData();
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
            
            if (scraper.LoadData())
            {
                Console.WriteLine("Loading data from previous session...\n");
            }
            else
            {
                Console.WriteLine("Populating content data from webcourses. Please wait...");
                scraper.PopulateAllData();
                Console.WriteLine("\nContent population complete\n");
            }   
            Console.WriteLine("Modules found: ");
            DisplayModules(scraper.GetModuleNames());
            Console.WriteLine();
            Console.WriteLine("Current Output Directory: " + scraper.OutputDirectory);
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
            List<string> modules = scraper.GetModuleNames();
            Console.WriteLine("\nEnter the number of the module you'd like to view content for.");
            Console.WriteLine("Your Modules");
            Console.WriteLine("---------------------------------");
            DisplayModules(modules);
            Console.WriteLine("-1. Quit");
            Console.Write(">>");
            int choice = GetModuleChoice(modules);
            if (choice == -1)
            {
                return;
            }
            ViewDirectory(scraper.GetModuleByName(modules[choice]).Content);
        }

        public static void ViewDirectory(BbContentDirectory folder)
        {
            Console.WriteLine();
            Console.WriteLine("VIEWING DIRECTORY: " + folder.Name);
            Console.WriteLine("------------------------------------------------------------------");
            for (int i = 0; i < folder.SubFolders.Count; i++)
            {
                Console.WriteLine(i + 1 + ". Directory: " + folder.SubFolders[i].Name);
            }
            for (int i = 0; i < folder.Files.Count; i++)
            {
                Console.WriteLine(i + 1 + folder.SubFolders.Count + ". File: " + folder.Files[i].Name);
            }
            Console.WriteLine("0.  Back");
            Console.WriteLine("-1. Quit");
            Console.Write(">> ");
            int choice = GetViewChoice(folder);

            if (choice == -1) { return; }
            else if (choice == 0)
            {
                if (folder.Folder != null)
                {
                    ViewDirectory(folder.Folder);
                }
                else
                {
                    ViewContent();
                }
            }
            else if (choice <= folder.SubFolders.Count)
            {
                 ViewDirectory(folder.SubFolders[choice-1]);
            }
            else
            {
                ViewFile(folder.Files[choice - folder.SubFolders.Count - 1]);
            }
        }

        public static void ViewFile(BbContentItem file)
        {
            Console.WriteLine();
            Console.WriteLine("VIEWING FILE " + file.Name);
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine(file);
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine();
            Console.Write("Download? [Y/N]: ");
            string choice = Console.ReadLine();
            if (choice.ToUpper().StartsWith("Y"))
            {
                scraper.DownloadFile(file);
            }
            ViewDirectory(file.Folder);
        }

        public static int GetViewChoice(BbContentDirectory folder)
        {
            bool parsed;
            int choice;
            parsed = Int32.TryParse(Console.ReadLine(), out choice);
            while (!parsed || choice < -1 || choice > folder.SubFolders.Count + folder.Files.Count)
            {
                Console.WriteLine("Invalid option. Enter the number of your choice");
                parsed = Int32.TryParse(Console.ReadLine(), out choice);
            }
            return choice;
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
            Console.WriteLine("-1. Quit");
            Console.Write(">>");
            int choice = GetModuleChoice(modules);
            if (choice == -1) { return; }
            else
            {
                scraper.DownloadModuleFiles(modules[choice]);
            }
        }
        public static int GetModuleChoice(List<string> modules)
        {
            bool parsed;
            int choice;
            parsed = Int32.TryParse(Console.ReadLine(), out choice);
            while ((!parsed || choice <= 0 || choice > modules.Count) && choice != -1)
            {
                Console.WriteLine("Invalid option. Enter the number of your choice");
                parsed = Int32.TryParse(Console.ReadLine(), out choice);
            }
            if (choice == -1) { return choice; }
            return choice-1;     
        }
        public static void DisplayModules(List<string> modules)
        {
            for (int i = 0; i < modules.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + modules[i]);
            }
        }

        public static void CheckNewContent()
        {
            Console.WriteLine("Populating content data from webcourses. Please wait...");
            scraper.PopulateAllData();
            Console.WriteLine("\nContent population complete\n");
        }
    }
}
