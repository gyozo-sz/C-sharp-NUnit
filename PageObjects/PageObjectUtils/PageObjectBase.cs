using BoDi;
using NUnit_practice.DataClasses;
using NUnit_practice.Hooks;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace NUnit_practice.PageObjects.Utils
{
    internal class PageObjectBase
    {
        private const string OriginalWindowKey = "OriginalWindow";
        protected readonly PageContext Context;
        private const int MaxWaitSeconds = 5;
        private readonly By AdsLocator = By.XPath("//*[contains(@class, 'google') or contains(@id, 'google')]");
        protected ScenarioContext _scenarioContext;
        protected IObjectContainer _container;

        public PageObjectBase(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            Context = objectContainer.Resolve<PageContext>("Context");
            _scenarioContext = scenarioContext;
            _container = objectContainer;
        }

        public bool IsHomePage()
        {
            return Context.Driver.Url == ScenarioHooks.DefaultPage;
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

        public IWebElement WaitForElementLoad(By locator, int maxWaitSeconds = MaxWaitSeconds)
        {
            var wait = new WebDriverWait(Context.Driver, TimeSpan.FromSeconds(maxWaitSeconds));
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

        public void HideAds()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Context.Driver;
            IReadOnlyCollection<IWebElement> elements = Context.Driver.FindElements(AdsLocator);
            while (elements.Count != 0)
            {
                IWebElement element = elements.Last();
                js.ExecuteScript(@"var el = arguments[0];
                    el.parentNode.removeChild(el)", element);
                elements = Context.Driver.FindElements(AdsLocator);
            }
        }

        private void VerticalScrollWindowBy(int scrollAmount)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Context.Driver;
            js.ExecuteScript($"window.scrollBy(0,{scrollAmount})");
        }

        public static ClickType ParseClickType(string clickType)
        {
            return (clickType) switch
            {
                "left" => ClickType.LeftClick,
                "double" => ClickType.DoubleLeftClick,
                "right" => ClickType.RightClick,
                _ => throw new ArgumentException($"Unknown click type: {clickType}"),
            };
        }

        public void Click(IWebElement? element, ClickType clickType)
        {
            switch (clickType)
            {
                case ClickType.LeftClick:
                    element!.Click();
                    break;
                case ClickType.DoubleLeftClick:
                    Context.Actions.DoubleClick(element).Perform();
                    break;
                case ClickType.RightClick:
                    Context.Actions.ContextClick(element).Perform();
                    break;
                default:
                    throw new ArgumentException($"Unknown click type: {Enum.GetName(typeof(ClickType), clickType)}");
            }
        }

        public ClickType Click(IWebElement? element, string clickTypeString)
        {
            ClickType clickType = ParseClickType(clickTypeString);
            Click(element, clickType);
            return clickType;
        }

        public IWebElement? GetButtonByText(string buttonText)
        {
            By buttonLocator = By.XPath($"//button[text()='{buttonText}']");
            return WaitForVisibility(GetElement(buttonLocator));
        }

        public void ClickButtonByText(string buttonText)
        {
            ClickElement(GetButtonByText(buttonText));
        }

        public void ClickElement(IWebElement? element,
            ClickType clickType = ClickType.LeftClick,
            int maxWaitSeconds = MaxWaitSeconds)
        {
            element = WaitForVisibility(element, maxWaitSeconds);
            MoveToElement(element);
            try
            {
                Click(element, clickType);
            }
            catch (ElementClickInterceptedException)
            {
                int scrollAmount = 300;
                VerticalScrollWindowBy(scrollAmount);
                Click(element, clickType);
            }
        }

        public void DoubleClickElement(IWebElement? element, int maxWaitSeconds = MaxWaitSeconds)
        {
            ClickElement(element, ClickType.DoubleLeftClick, maxWaitSeconds);
        }

        public void RightClickElement(IWebElement? element, int maxWaitSeconds = MaxWaitSeconds)
        {
            ClickElement(element, ClickType.RightClick, maxWaitSeconds);
        }

        public void ClickElement(By locator, int maxWaitSeconds = MaxWaitSeconds)
        {
            ClickElement(WaitForVisibility(GetElement(locator, maxWaitSeconds)));
        }

        public void WriteToField(IWebElement? field, string text, int maxWaitSeconds = MaxWaitSeconds)
        {
            WaitForVisibility(field, maxWaitSeconds).SendKeys(text);
        }

        public string GetElementText(IWebElement? field, int maxWaitSeconds = MaxWaitSeconds)
        {
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


        public void SwitchToNewTabOrWindow(string originalWindowKey)
        {
            _scenarioContext[OriginalWindowKey] = originalWindowKey;
            string newWindowHandle = Context.Driver.WindowHandles.SkipWhile(handle => handle == originalWindowKey).First();
            Context.Driver.SwitchTo().Window(newWindowHandle);
        }


        public void SwitchToOriginalWindow()
        {
            if (!_scenarioContext.ContainsKey(OriginalWindowKey)
                || _scenarioContext[OriginalWindowKey].ToString() == Context.Driver.CurrentWindowHandle)
            {
                // Could throw exception, but no need in this case
                return;
            }
            string originalWindowKey = _scenarioContext[OriginalWindowKey].ToString();
            string newWindowHandle = Context.Driver.WindowHandles.SkipWhile(handle => handle != originalWindowKey).First();
            Context.Driver.SwitchTo().Window(newWindowHandle);
        }

        public string GetWindowHandle()
        {
            return Context.Driver.CurrentWindowHandle;
        }
    }
}