using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackboardDownloader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace BlackboardDownloader.Tests
{
    [TestClass]
    public class ScraperTests
    {
        [TestMethod]
        public void LoginCorrectInfoTest()
        {
            Scraper scraper = new Scraper();

            bool loginSuccess = scraper.Login("D14127051", GetLoginPassword());

            Assert.IsTrue(loginSuccess, "Login() returned false");
            Assert.IsTrue(scraper.initialized, "Scraper not initialized after login");
        }

        [TestMethod]
        public void LoginWrongInfoTest()
        {
            Scraper scraper = new Scraper();

            bool loginSuccess = scraper.Login("D14127051", "wrongpassword");

            Assert.IsFalse(loginSuccess, "Login() returned true");
        }

        [TestMethod]
        public void PopulateModulesTest()
        {
            Scraper scraper = new Scraper();
            scraper.Login("D14127051", GetLoginPassword());
            List<string> expectedModuleNames = new List<string>()
            {
                "CIDT265-2012-14: Course Information DT265",
                "CMPU4065-A: Enterprise Application Development",
                "CMPU4066-B: Object Oriented Software Development 2",
                "CMPU4067_R: Programming for Mobile and Smart Devices",
                "WKPLINT: Work Placement Internship"
            };

            //Act
            scraper.PopulateModules();
            List<string> moduleNames = scraper.GetModuleNames();

            // Assert

            Assert.IsTrue(moduleNames.Count > 0, "No modules found");   // Some modules were found
            Assert.IsTrue(moduleNames.All(expectedModuleNames.Contains), "Not all expected modules found"); // All expected modules are found
        }

        // Retrieves correct password from a secure file (ie. one not on GitHub)
        private string GetLoginPassword()
        {
            string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\password.txt";
            string password = File.ReadAllLines(filePath)[0];
            return password;
        }
    }
}