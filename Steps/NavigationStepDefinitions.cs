using BoDi;
using NUnit_practice.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace NUnit_practice.Steps
{
    [Binding]
    public class NavigationStepDefinitions
    {
        private PageContext context;
        private NavigationPage navigationPage;
        private TextBoxPage textBoxPage;
        private HomePage homePage;
        private ElementsPage elementsPage;

        public NavigationStepDefinitions(IObjectContainer objectContainer)
        {
            context = objectContainer.Resolve<PageContext>("Context");
            homePage = new HomePage(objectContainer);
            navigationPage = new NavigationPage(objectContainer);
        }

        [Given(@"I navigated to the (.*) page in the (.*) section")]
        public void GivenINavigatedToTheTextBoxPageInTheElementsSection(string pageTitle, string sectionTitle)
        {
            navigationPage.OpenTextBoxPage();
        }


        [Given(@"I am on the Text Box Page")]
        public void GivenIAmOnTheTextBoxPage()
        {
            elementsPage = homePage.SelectMenuCard("Elements");
            textBoxPage = elementsPage.NavigateToTextBoxPage();
        }

        [Then(@"I see the Email input field")]
        public void ThenISeeTheEmailInputField()
        {
            Assert.That(textBoxPage.IsEmailFieldVisible(), Is.True);
        }

    }
}
