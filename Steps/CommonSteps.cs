namespace NUnit_practice.Steps
{
    [Binding]
    public class CommonSteps
    {
        private readonly ScenarioContext _scenarioContext;
        public CommonSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Then(@"The page body contains title text '([^']*)'")]
        public void ThenThePageBodyContainsTitleText(string expectedHeaderText)
        {
            if (_scenarioContext["Context"] as PageContext is PageContext pageContext)
            {
                IWebElement headerText = pageContext.Driver.FindElement(By.TagName("h1"));
                Assert.That(headerText.Text, Is.EqualTo(expectedHeaderText));
            }
            else
            {
                throw new ArgumentException("Page Context is not initialized");
            }
        }

    }
}
