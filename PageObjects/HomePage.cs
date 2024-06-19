using NUnit_practice.PageObjects.Utils;
using SeleniumExtras.PageObjects;

namespace NUnit_practice.PageObjects
{
    internal class HomePage : PageObjectBase
    {
        public HomePage(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            PageFactory.InitElements(Context.Driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//div[contains(@class, 'card-body')]/h5[contains(text(), 'Elements')]")]
        public IWebElement? ElementsCard { get; set; }

        private static By MenuCard(string title)
        {
            return By.XPath($"//h5[contains(text(), '{title}')]//ancestor::div[contains(@class, 'top-card')]");
        }

        public CategoryPage SelectMenuCard(string title)
        {
            ClickElement(MenuCard(title));
            return new CategoryPage(_scenarioContext);
        }

    }
}
