using NUnit_practice.DataClasses;
using NUnit_practice.PageObjects.Elements;

namespace NUnit_practice.Steps
{
    [Binding]
    public class ButtonsStepDefinitions
    {
        private readonly ButtonsPage _buttonsPage;
        private readonly List<ClickType> _performedClicks;

        public ButtonsStepDefinitions(ScenarioContext scenarioContext)
        {
            _buttonsPage = new ButtonsPage(scenarioContext);
            _performedClicks = new();
        }

        [When(@"I (left|double|right) click the (.*) Button")]
        public void WhenIClickButton(string clickType, string buttonText)
        {
            _buttonsPage.HideAds();
            IWebElement? button = _buttonsPage.GetButtonByText(buttonText);
            ClickType performedClick = _buttonsPage.Click(button, clickType);
            _performedClicks.Add(performedClick);
        }

        [Then(@"Messages? with text You have done a click (?:is|are) displayed")]
        public void ThenMessageWithTextYouHaveDoneAClickIsDisplayed()
        {
            foreach (ClickType clickType in _performedClicks)
            {
                string expectedMessage = ButtonsPage.GetExpectedButtonMessage(clickType);
                Assert.That(_buttonsPage.GetActualButtonMessage(clickType), Is.EqualTo(expectedMessage));
            }
            
            
        }
    }
}
