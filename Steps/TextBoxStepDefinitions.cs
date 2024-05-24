using System;
using TechTalk.SpecFlow;
using NUnit_practice.PageObjects;
using BoDi;

namespace NUnit_practice.Steps
{
    [Binding]
    public class TextBoxStepDefinitions
    {
        private TextBoxPage textBoxPage;

        public TextBoxStepDefinitions(IObjectContainer objectContainer)
        {
            var context = objectContainer.Resolve<PageContext>("Context");
            textBoxPage = new TextBoxPage(objectContainer);
        }

        [When(@"I enter '(.*)' into the (.*) Text field")]
        public void WhenIEnterXIntoTheYTextField(string input, string textFieldLabel)
        {
            textBoxPage.EnterName("Sherlock Holmes");
            Thread.Sleep(5000);
        }

        [When(@"Click the (.*) button")]
        public void WhenClickTheSubmitButton(string buttonLabel)
        {
            textBoxPage.ClickSubmitButton();
        }

        [Then(@"Result table appears echoing the entered data")]
        public void ThenResultTableAppearsEchoingTheEnteredData()
        {
            Assert.That(textBoxPage.OutputName.Text, Is.Not.Empty);
            
        }
    }
}
