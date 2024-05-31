﻿using OpenQA.Selenium.DevTools.V122.FedCm;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace NUnit_practice.PageObjects
{
    internal class PageObjectBase
    {
        protected readonly PageContext Context;
        private const int MaxWaitSeconds = 5;

        public PageObjectBase(PageContext context)
        {
            Context = context;
        }

        public static Func<IWebDriver, IWebElement> ElementIsVisible(IWebElement? element)
        {
            return (driver) =>
            {
                try
                {
                    if (element != null && element.Displayed)
                    {
                        return element;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        public IWebElement WaitForVisibility(IWebElement? element, int maxWaitSeconds = MaxWaitSeconds)
        {
            var wait = new WebDriverWait(Context.Driver, TimeSpan.FromSeconds(maxWaitSeconds));
            return wait.Until(ElementIsVisible(element));
        }

        public void ClickElement(IWebElement? element, int maxWaitSeconds = MaxWaitSeconds)
        {
            WaitForVisibility(element, maxWaitSeconds).Click();
        }

        public void WriteToField(IWebElement? field, string text, int maxWaitSeconds = MaxWaitSeconds)
        {
            WaitForVisibility(field, maxWaitSeconds).SendKeys(text);
        }

        public string GetElementText(IWebElement? field, int maxWaitSeconds = MaxWaitSeconds) {
            return WaitForVisibility(field, maxWaitSeconds).Text;
        }

        public string GetElementAttribute(IWebElement? field, string attribute, int maxWaitSeconds = MaxWaitSeconds)
        {
            return WaitForVisibility(field, maxWaitSeconds).GetAttribute(attribute);
        }

        public bool IsElementSelected(IWebElement? element, int maxWaitSeconds = MaxWaitSeconds)
        {
            return WaitForVisibility(element, maxWaitSeconds).Selected;
        }
    }
}
