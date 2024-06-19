using NUnit_practice.PageObjects;
using NUnit_practice.PageObjects.Utils;

namespace NUnit_practice.Steps
{
    [Binding]
    public class NavigationStepDefinitions
    {
        private readonly NavigationPage _navigationPage;
        private readonly HomePage _homePage;

        public NavigationStepDefinitions(ScenarioContext scenarioContext)
        {
            _homePage = new HomePage(scenarioContext);
            _navigationPage = new NavigationPage(scenarioContext);
        }

        [Given(@"I navigated to the (.*) section in the (.*) category")]
        [When(@"I navigate to the (.*) section in the (.*) category")]
        public void GivenINavigatedToASectionInCategory(string sectionTitle, string categoryTitle)
        {
            if (_homePage.IsHomePage())
            {
                _homePage.SelectMenuCard(categoryTitle);
            }
            _navigationPage.NavigateToSectionInCategory(categoryTitle, sectionTitle);
        }


        [Given(@"I am on the Text Box section")]
        public void GivenIAmOnTheTextBoxPage()
        {
            GivenINavigatedToASectionInCategory("Text Box", "Elements");
        }
    }
}
