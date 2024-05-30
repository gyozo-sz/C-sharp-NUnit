using NUnit.Framework.Constraints;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit_practice
{
    public class PageContext
    {
        public PageContext()
        {
            Environment.SetEnvironmentVariable("browser", "chrome");
            string? browserEnv = Environment.GetEnvironmentVariable("browser");
            switch (browserEnv)
            {
                case "chrome":
                    Driver = new ChromeDriver();
                    break;
                case "firefox":
                    var driverService = FirefoxDriverService.CreateDefaultService(@"C:\Users\gyozo.szabo\Desktop\WebDrivers", "geckodriver.exe");
                    driverService.FirefoxBinaryPath = @"C:\Users\gyozo.szabo\AppData\Local\Mozilla Firefox\firefox.exe";
                    Driver = new FirefoxDriver(driverService);
                    break;
                case "edge":
                    Driver = new EdgeDriver();
                    break;
                default:
                    Console.WriteLine($"Unknown browser specified: {browserEnv ?? "null"}. Defaulting to Chrome");
                    Driver = new ChromeDriver();
                    break;
            }
            
            Actions = new Actions(Driver);
        }

        public IWebDriver Driver { get; private set; }
        public Actions Actions { get; private set; }
    }
}