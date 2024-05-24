using BoDi;
using OpenQA.Selenium.Internal;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit_practice.PageObjects
{
    internal class NavigationPage : PageObjectBase
    {
        IObjectContainer _container;

        public NavigationPage(IObjectContainer objectContainer) : base(objectContainer)
        {
            _container = objectContainer;
            var context = objectContainer.Resolve<PageContext>("Context");
            PageFactory.InitElements(context.Driver, this);
        }

        public static By NavigationSectionByText(string text) => By.XPath($"//div[contains(@class, 'header-text') and contains(text(), '{text}')]");
        public static By NavigationPageByText(string text) => By.XPath($"//ul[@class='menu-list']/li/span[contains(text(), '{text}')]");
        public static By NavigationSectionPageListContainerByText(string text) => By.XPath($"//div[contains(@class, 'header-text') and contains(text(), '{text}')]//ancestor::div[contains(@class, 'element-group')]//div[contains(@class, 'element-list')]");

        public IWebElement? ElementsMenuSection {  get
            {
                return GetElement(NavigationSectionByText("Elements"));
            } 
        }
        public IWebElement? TextBoxMenuItem
        {
            get
            {
                return GetElement(NavigationPageByText("Text Box"));
            }
        }

        public bool IsNavigationSectionExpanded(string sectionName)
        { 
            IWebElement? pageListElement = GetNotVisibleElement(NavigationSectionPageListContainerByText(sectionName));
            if (pageListElement == null)
            {
                return false;
            }
            return pageListElement.GetAttribute("class").Contains("show");
        }

        public TextBoxPage OpenTextBoxPage()
        {
            if (!IsNavigationSectionExpanded("Elements"))
            {
                Console.WriteLine("Section elements are not listed");
                ClickElement(ElementsMenuSection);
            }
            ClickElement(TextBoxMenuItem);
            return new TextBoxPage(_container);
        }
    }
}
