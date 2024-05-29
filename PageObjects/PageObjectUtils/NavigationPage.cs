using BoDi;
using SeleniumExtras.PageObjects;

namespace NUnit_practice.PageObjects.Utils
{
    internal class NavigationPage : PageObjectBase
    {
        public NavigationPage(IObjectContainer objectContainer, ScenarioContext scenarioContext) : base(objectContainer, scenarioContext)
        {
            var context = objectContainer.Resolve<PageContext>("Context");
            PageFactory.InitElements(context.Driver, this);
        }

        public static By NavigationCategoryByText(string text) => By.XPath($"//div[contains(@class, 'header-text') and contains(text(), '{text}')]");
        public static By NavigationSectionByText(string text) => By.XPath($"//ul[@class='menu-list']/li/span[contains(text(), '{text}')]");
        public static By NavigationSectionListContainerByText(string text) => By.XPath($"//div[contains(@class, 'header-text') and contains(text(), '{text}')]//ancestor::div[contains(@class, 'element-group')]//div[contains(@class, 'element-list')]");

        public IWebElement? ElementsMenuSection
        {
            get
            {
                return GetElement(NavigationCategoryByText("Elements"));
            }
        }
        public IWebElement? TextBoxMenuItem
        {
            get
            {
                return GetElement(NavigationSectionByText("Text Box"));
            }
        }

        public IWebElement? SectionMenuItem(string sectionTitle)
        {
            return GetElement(NavigationSectionByText(sectionTitle));
        }

        public IWebElement? CategoryMenuItem(string categoryTitle)
        {
            return GetElement(NavigationCategoryByText(categoryTitle));
        }

        public bool IsNavigationCategoryExpanded(string categoryTitle)
        {
            IWebElement? sectionListElement = GetNotVisibleElement(NavigationSectionListContainerByText(categoryTitle));
            if (sectionListElement == null)
            {
                return false;
            }
            return sectionListElement.GetAttribute("class").Contains("show");
        }

        public void OpenTextBoxPage()
        {
            ExpandCategoryMenu("Elements");
            ClickElement(TextBoxMenuItem);
        }

        public NavigationPage ExpandCategoryMenu(string categoryTitle)
        {
            if (!IsNavigationCategoryExpanded(categoryTitle))
            {
                ClickElement(CategoryMenuItem(categoryTitle));
            }
            return this;
        }

        public NavigationPage NavigateToSectionInCategory(string categoryTitle, string sectionTitle)
        {
            ExpandCategoryMenu(categoryTitle);
            Console.WriteLine(categoryTitle);
            Console.WriteLine(sectionTitle);
            ClickElement(SectionMenuItem(sectionTitle));
            return this;
        }
    }
}
