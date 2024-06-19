using BoDi;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace NUnit_practice.Hooks
{
    public class CustomFeatureContext
    {
        public int Counter { get; set; }
    }

    // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
    [Binding]
    public class ScenarioHooks
    {
        public const string DefaultPage = "https://demoqa.com/";

        [BeforeFeature]
        public static void InitializeFeatureContext(CustomFeatureContext featureContext)
        {
            featureContext.Counter = 0;
        }

        [BeforeScenario(Order = 1)]
        public static void NavigateToPage(CustomFeatureContext featureContext, ScenarioContext scenarioContext)
        {
            PageContext pageContext = new(scenarioContext);
            scenarioContext.Add("Context", pageContext);
            pageContext.Driver.Navigate().GoToUrl(DefaultPage);
            pageContext.Driver.Manage().Window.Maximize();
            featureContext.Counter += 1;
            Console.WriteLine("Counter: {0}", featureContext.Counter);
        }

        [AfterScenario]
        public static void CloseContext(ScenarioContext scenarioContext)
        {
            if (scenarioContext["Context"] as PageContext is PageContext context)
            {
                context.Driver.Quit();
            }
        }
    }
}
