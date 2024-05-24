using BoDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace NUnit_practice.Steps
{
    [Binding]
    public class CommonSteps
    {
        IObjectContainer _container;
        public CommonSteps(IObjectContainer container)
        {
            _container = container;
        }

        [Then(@"The page body contains title text '([^']*)'")]
        public void ThenThePageBodyContainsTitleText(string expectedHeaderText)
        {
            IWebDriver driver = _container.Resolve<PageContext>("Context").Driver;
            // How should this logic be implemented?
            //  Separate header locators in all the pages seem bad
            //  New abstraction layer between PageObjectBase and other PageObjects seem bad also
            IWebElement headerText =  driver.FindElement(By.TagName("h1"));
            Assert.That(headerText.Text, Is.EqualTo(expectedHeaderText));
        }

    }
}
