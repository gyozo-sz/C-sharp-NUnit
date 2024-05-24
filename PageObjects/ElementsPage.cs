using BoDi;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit_practice.PageObjects
{
    internal class ElementsPage : PageObjectBase
    {

        private NavigationPage navigationPage;

        public ElementsPage(IObjectContainer objectContainer) : base(objectContainer) {
            
            var context = objectContainer.Resolve<PageContext>("Context");
            navigationPage = new NavigationPage(objectContainer);
        }

        [FindsBy(How = How.XPath, Using = "//div[contains(@class, 'card-body')]/h5[contains(text(), 'Elements')]")]
        public IWebElement? ElementsCard { get; set; }

        public TextBoxPage NavigateToTextBoxPage()
        {
            return navigationPage.OpenTextBoxPage();
        }

    }
}
