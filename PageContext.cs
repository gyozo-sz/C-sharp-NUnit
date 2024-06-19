using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace NUnit_practice
{
    public class PageContext
    {
        const string DriversDirectoryRelativePath = "Drivers/";

        public PageContext(ScenarioContext scenarioContext)
        {
            scenarioContext.TryGetValue("Browser", out var browserEnv);
            Console.WriteLine(browserEnv);
            switch (browserEnv)
            {
                case "Chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    Driver = new ChromeDriver();
                    break;
                case "Firefox":
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    Driver = new FirefoxDriver();
                    break;
                case "Edge":
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    Driver = new EdgeDriver();
                    break;
                default:
                    Console.WriteLine($"Unknown browser specified: {browserEnv ?? "null"}. Defaulting to Chrome");
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    Driver = new ChromeDriver();
                    break;
            }
            Actions = new Actions(Driver);
        }

        public IWebDriver Driver { get; private set; }
        public Actions Actions { get; private set; }
    }
}