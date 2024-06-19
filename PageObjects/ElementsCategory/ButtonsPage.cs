using NUnit_practice.DataClasses;
using NUnit_practice.PageObjects.Utils;
using SeleniumExtras.PageObjects;


namespace NUnit_practice.PageObjects.Elements
{
    internal class ButtonsPage : CategoryPage
    {
        public ButtonsPage(ScenarioContext scenarioContext) : base(scenarioContext)
        {
            PageFactory.InitElements(Context.Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "button#doubleClickBtn")]
        public IWebElement? DoubleClickMeButton;

        [FindsBy(How = How.CssSelector, Using = "button#rightClickBtn")]
        public IWebElement? RightClickMeButton;

        [FindsBy(How = How.XPath, Using = "//button[text()='Click Me']")]
        public IWebElement? ClickMeButton;

        [FindsBy(How = How.CssSelector, Using = "p#doubleClickMessage")]
        public IWebElement? DoubleClickMessage;

        [FindsBy(How = How.CssSelector, Using = "p#rightClickMessage")]
        public IWebElement? RightClickMessage;

        [FindsBy(How = How.CssSelector, Using = "p#dynamicClickMessage")]
        public IWebElement? LeftClickMessage;

        public string GetRightClickMeButtonMessage()
        {
            return WaitForVisibility(RightClickMessage)!.Text;
        }
        public string GetDoubleClickMeButtonMessage()
        {
            return WaitForVisibility(DoubleClickMessage)!.Text;
        }

        public string GetClickMeButtonMessage()
        {
            return WaitForVisibility(LeftClickMessage)!.Text;
        }

        public static string GetExpectedButtonMessage(ClickType clickType)
        {
            string expectedMessage = "You have done a ";
            return clickType switch
            {
                ClickType.LeftClick => expectedMessage + "dynamic click",
                ClickType.RightClick => expectedMessage + "right click",
                ClickType.DoubleLeftClick => expectedMessage + "double click",
                _ => throw new ArgumentException($"Unknown click type: {clickType}"),
            };
        }

        public string GetActualButtonMessage(ClickType clickType)
        {
            return clickType switch
            {
                ClickType.LeftClick => GetClickMeButtonMessage(),
                ClickType.RightClick => GetRightClickMeButtonMessage(),
                ClickType.DoubleLeftClick => GetDoubleClickMeButtonMessage(),
                _ => throw new ArgumentException($"Unknown click type: {clickType}"),
            };
        }
    }
}
