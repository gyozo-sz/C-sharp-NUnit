using BoDi;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit_practice.PageObjects
{
    internal class HomePage : PageObjectBase
    {
        IObjectContainer _container;

        public HomePage(IObjectContainer objectContainer) : base(objectContainer) {
            _container = objectContainer;
            var context = objectContainer.Resolve<PageContext>("Context");
            PageFactory.InitElements(context.Driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//div[contains(@class, 'card-body')]/h5[contains(text(), 'Elements')]")]
        public IWebElement? ElementsCard { get; set; }

        private By MenuCard(string title)
        {
            return By.XPath($"//h5[contains(text(), '{title}')]//ancestor::div[contains(@class, 'top-card')]");
        }

        public ElementsPage SelectMenuCard(string title)
        {
            ClickElement(MenuCard(title));
            return new ElementsPage(_container);
        }

    }
}
