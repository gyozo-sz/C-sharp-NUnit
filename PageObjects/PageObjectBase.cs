using BoDi;
using OpenQA.Selenium.Support.UI;
using System.Xml.Linq;

namespace NUnit_practice.PageObjects
{
    internal class PageObjectBase
    {
        protected readonly PageContext Context;
        private const int MaxWaitSeconds = 5;

        public PageObjectBase(IObjectContainer objectContainer)
        {
            Context = objectContainer.Resolve<PageContext>("Context");
        }

        public IWebElement WaitForVisibility(IWebElement? element, int maxWaitSeconds = MaxWaitSeconds)
        {
            var wait = new WebDriverWait(this.Context.Driver, TimeSpan.FromSeconds(maxWaitSeconds));
            return wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }

        public IWebElement WaitForElementLoad(By locator, int maxWaitSeconds = MaxWaitSeconds)
        {
            var wait = new WebDriverWait(this.Context.Driver, TimeSpan.FromSeconds(maxWaitSeconds));
            return wait.Until(ExpectedConditions.ElementExists(locator));
        }

        public void MoveToElement(IWebElement? element)
        {
            Context.Actions.MoveToElement(element).Perform();
        }

        public IWebElement? GetNotVisibleElement(By locator, int maxWaitSeconds = MaxWaitSeconds)
        {
            return WaitForElementLoad(locator, maxWaitSeconds);
        }

        public IWebElement? GetElement(By locator, int maxWaitSeconds = MaxWaitSeconds)
        {
            IWebElement? element = Context.Driver.FindElement(locator);
            return WaitForVisibility(element, maxWaitSeconds);
        }

        private void VerticalScrollWindowBy(int scrollAmount) {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Context.Driver;
            js.ExecuteScript($"window.scrollBy(0,{scrollAmount})");
        }

        public void ClickElement(IWebElement? element, int maxWaitSeconds = MaxWaitSeconds)
        {
            element = WaitForVisibility(element, maxWaitSeconds);
            MoveToElement(element);
            try
            {
                element.Click();
            } catch (ElementClickInterceptedException)
            {
                int scrollAmount = 300;
                VerticalScrollWindowBy(scrollAmount);
                element.Click();
            }

        }

        public void ClickElement(By locator, int maxWaitSeconds = MaxWaitSeconds)
        {
            ClickElement(WaitForVisibility(GetElement(locator)));
        }

        public void WriteToField(IWebElement? field, string text, int maxWaitSeconds = MaxWaitSeconds)
        {
            WaitForVisibility(field, maxWaitSeconds).SendKeys(text);
        }

        public string? GetElementText(IWebElement? field, int maxWaitSeconds = MaxWaitSeconds)
        {
            return WaitForVisibility(field, maxWaitSeconds).Text;
        }

        public string? GetElementAttribute(IWebElement? field, string attribute, int maxWaitSeconds = MaxWaitSeconds)
        {
            return WaitForVisibility(field, maxWaitSeconds).GetAttribute(attribute);
        }

        public bool? IsElementSelected(IWebElement? element, int maxWaitSeconds = MaxWaitSeconds)
        {
            return WaitForVisibility(element, maxWaitSeconds).Selected;
        }
    }
}