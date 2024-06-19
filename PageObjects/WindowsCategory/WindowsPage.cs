using NUnit_practice.PageObjects.Utils;
using SeleniumExtras.PageObjects;

namespace NUnit_practice.PageObjects.WindowsCategory
{
    internal class WindowsPage : CategoryPage
    {
        public WindowsPage(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            PageFactory.InitElements(Context.Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "button#tabButton")]
        public IWebElement? NewTabButton;

        [FindsBy(How = How.CssSelector, Using = "button#windowButton")]
        public IWebElement? NewWindowButton;
    }
}
