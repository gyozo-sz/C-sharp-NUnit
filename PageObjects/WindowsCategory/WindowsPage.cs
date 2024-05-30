using BoDi;
using NUnit_practice.PageObjects.Utils;
using SeleniumExtras.PageObjects;

namespace NUnit_practice.PageObjects.WindowsCategory
{
    internal class WindowsPage : CategoryPage
    {
        public WindowsPage(IObjectContainer objectContainer, ScenarioContext scenarioContext) : base(objectContainer, scenarioContext)
        {
            var context = objectContainer.Resolve<PageContext>("Context");
            PageFactory.InitElements(context.Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "button#tabButton")]
        public IWebElement? NewTabButton;

        [FindsBy(How = How.CssSelector, Using = "button#windowButton")]
        public IWebElement? NewWindowButton;

        public SamplePage ClickNewTabButton()
        {
            ClickElement(NewTabButton);
            SwitchToNewTabOrWindow(GetWindowHandle());
            return new(_container, _scenarioContext);
        }
    }
}
