using BoDi;
using NUnit_practice.DataClasses;
using NUnit_practice.PageObjects.Elements;

namespace NUnit_practice.Steps
{
    [Binding]
    public class ButtonsStepDefinitions
    {
        private readonly ButtonsPage buttonsPage;
        private readonly List<ClickType> performedClicks;

        public ButtonsStepDefinitions(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            buttonsPage = new ButtonsPage(objectContainer, scenarioContext);
            performedClicks = new();
        }

        [When(@"I (left|double|right) click the (.*) Button")]
        public ClickType WhenIClickButton(string clickType, string buttonText)
        {
            buttonsPage.HideAds();
            IWebElement? button = buttonsPage.GetButtonByText(buttonText);
            ClickType performedClick;
            switch (clickType)
            {
                case "left":
                    buttonsPage.ClickElement(button);
                    performedClick = ClickType.LeftClick;
                    break;
                case "double":
                    buttonsPage.ClickElement(button, ClickType.DoubleLeftClick);
                    performedClick = ClickType.DoubleLeftClick;
                    break;
                case "right":
                    buttonsPage.ClickElement(button, ClickType.RightClick);
                    performedClick = ClickType.RightClick;
                    break;
                default:
                    throw new ArgumentException($"Unknown click type: {clickType}");
            }
            performedClicks.Add(performedClick);
            return performedClick;
        }

        [Then(@"Messages? with text You have done a click (?:is|are) displayed")]
        public void ThenMessageWithTextYouHaveDoneAClickIsDisplayed()
        {
            foreach (ClickType clickType in performedClicks)
            {
                string expectedMessage = ButtonsPage.GetExpectedButtonMessage(clickType);
                Assert.That(buttonsPage.GetActualButtonMessage(clickType), Is.EqualTo(expectedMessage));
            }
            
            
        }
    }
}
