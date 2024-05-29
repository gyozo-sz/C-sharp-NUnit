using BoDi;
using NUnit_practice.PageObjects.WindowsCategory;

namespace NUnit_practice.Steps
{
    [Binding]
    internal class WindowsStepDefinitions
    {
        private readonly SamplePage _samplePage;
        private readonly WindowsPage _windowsPage;

        public WindowsStepDefinitions(IObjectContainer container, ScenarioContext scenarioContext)
        {
            _windowsPage = new(container, scenarioContext);
            _samplePage = new(container, scenarioContext);
        }

        [When(@"I click the (.*) button")]
        public void WhenIClickNewButton(string buttonLabel)
        {
            _windowsPage.ClickButtonByText(buttonLabel);
            _windowsPage.SwitchToNewTabOrWindow(_windowsPage.GetWindowHandle());
        }

        [Then(@"Sample page is opened in new window")]
        public void ThenSamplePageIsOpenedInNewWindow()
        {
            Assert.That(_samplePage.GetHeaderText(), Is.EqualTo(SamplePage.GetExpectedHeaderText()));
        }

    }
}
