using NUnit_practice.PageObjects;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace NUnit_practice.PageObjects
{
    internal class DataConsentPage : PageObjectBase
    {
        public DataConsentPage(PageContext context) : base(context) {
            PageFactory.InitElements(context.Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "div.fc-footer-buttons-container button.fc-cta-do-not-consent")]
        public IWebElement? DoNotConsentButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div.fc-footer-buttons-container button.fc-cta-consent")]
        public IWebElement? ConsentButton { get; set; }

        public DataConsentPage RejectDataUse()
        {
            ClickElement(DoNotConsentButton);
            return this;
        }
    }
}
