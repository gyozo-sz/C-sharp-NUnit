using BoDi;
using NUnit_practice.PageObjects;
using NUnit_practice.PageObjects.Utils;

namespace NUnit_practice.Steps
{
    [Binding]
    public class NavigationStepDefinitions
    {
        private readonly NavigationPage navigationPage;
        private readonly HomePage homePage;

        public NavigationStepDefinitions(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            homePage = new HomePage(objectContainer, scenarioContext);
            navigationPage = new NavigationPage(objectContainer, scenarioContext);
        }

        [Given(@"I navigated to the (.*) section in the (.*) category")]
        [When(@"I navigate to the (.*) section in the (.*) category")]
        public void GivenINavigatedToASectionInCategory(string sectionTitle, string categoryTitle)
        {
            if (homePage.IsHomePage())
            {
                homePage.SelectMenuCard(categoryTitle);
            }
            navigationPage.NavigateToSectionInCategory(categoryTitle, sectionTitle);
        }


        [Given(@"I am on the Text Box section")]
        public void GivenIAmOnTheTextBoxPage()
        {
            GivenINavigatedToASectionInCategory("Text Box", "Elements");
        }
    }
}
