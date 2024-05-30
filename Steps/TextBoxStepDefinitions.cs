using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using BoDi;
using NUnit_practice.DataClasses;
using NUnit_practice.PageObjects.Elements;

namespace NUnit_practice.Steps
{
    [Binding]
    public class TextBoxStepDefinitions
    {
        private readonly TextBoxPage _textBoxPage;
        private TextBoxData _textBoxData;

        public TextBoxStepDefinitions(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            _textBoxPage = new(objectContainer, scenarioContext);
            _textBoxData = new();
        }

        [When(@"I enter '(.*)' into the (.*) Text field")]
        public void WhenIEnterTextIntoTheTextField(string input, string textFieldLabel)
        {
            _textBoxPage.EnterName("Sherlock Holmes");
            Thread.Sleep(5000);
        }

        // TODO: Move to Common Steps
        [When(@"Click the (.*) button")]
        public void WhenClickTheSubmitButton(string buttonLabel)
        {
            _textBoxPage.ClickSubmitButton();
        }

        [Then(@"Result table appears echoing the entered data")]
        public void ThenResultTableAppearsEchoingTheEnteredData()
        {
            Assert.That(_textBoxPage.OutputName!.Text, Is.Not.Empty);
        }

        [Then(@"I see the Email input field")]
        public void ThenISeeTheEmailInputField()
        {
            Assert.That(_textBoxPage.IsEmailFieldVisible(), Is.True);
        }

        [When(@"I enter the following data into the text fields")]
        public void WhenIEnterTheFollowingData(Table textBoxDataTable)
        {
            TextBoxData data = textBoxDataTable.CreateInstance<TextBoxData>();
            _textBoxPage.EnterDataInTextFields(data);
            _textBoxData = data;
        }

        [Then(@"Output table appears echoing the entered data")]
        public void ThenOutputTableEchoesTheEnteredData()
        {
            TextBoxData outputTableContents = _textBoxPage.GetOutputTableContents();
            Assert.That(outputTableContents, Is.EqualTo(_textBoxData));
        }
    }
}
