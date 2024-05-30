using BoDi;
using NUnit_practice.PageObjects.Utils;
using SeleniumExtras.PageObjects;

namespace NUnit_practice.PageObjects
{
    internal class HomePage : PageObjectBase
    {
        public HomePage(IObjectContainer objectContainer, ScenarioContext scenarioContext) : base(objectContainer, scenarioContext) 
        {
            var context = objectContainer.Resolve<PageContext>("Context");
            PageFactory.InitElements(context.Driver, this);
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
            return new CategoryPage(_container, _scenarioContext);
        }

    }
}
