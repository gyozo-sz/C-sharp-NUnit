namespace NUnit_practice.Hooks
{
    using BoDi;
    using NUnit_practice.PageObjects;
    using System.Collections.Generic;
    using System.Linq;
    using TechTalk.SpecFlow;

    // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
    [Binding]
    public class ScenarioHooks
    {
        private readonly IObjectContainer _container;
        private const string DefaultPage = "https://demoqa.com/";

        public ScenarioHooks(IObjectContainer objectContainer)
        {
            _container = objectContainer;
            //_container.RegisterInstanceAs(_driver, "Driver");
            _container.RegisterInstanceAs(new PageContext(), "Context");
            //_container.RegisterInstanceAs<IWebDriver>(_driver, "Driver");
        }

        [BeforeScenario(Order = 1)]
        public void NavigateToPage()
        {
            PageContext context = _container.Resolve<PageContext>("Context");
            context.Driver.Navigate().GoToUrl(DefaultPage);
            context.Driver.Manage().Window.Maximize();
        }

        [AfterScenario]
        public void CloseContext()
        {
            PageContext context = _container.Resolve<PageContext>("Context");
            context.Driver.Quit();
        }
    }
}
