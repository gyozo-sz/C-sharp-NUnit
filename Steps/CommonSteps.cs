using BoDi;

namespace NUnit_practice.Steps
{
    [Binding]
    public class CommonSteps
    {
        private readonly IObjectContainer _container;
        public CommonSteps(IObjectContainer container)
        {
            _container = container;
        }

        [Then(@"The page body contains title text '([^']*)'")]
        public void ThenThePageBodyContainsTitleText(string expectedHeaderText)
        {
            IWebDriver driver = _container.Resolve<PageContext>("Context").Driver;
            IWebElement headerText =  driver.FindElement(By.TagName("h1"));
            Assert.That(headerText.Text, Is.EqualTo(expectedHeaderText));
        }

    }
}
