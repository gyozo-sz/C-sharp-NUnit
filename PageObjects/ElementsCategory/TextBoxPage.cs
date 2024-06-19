using NUnit_practice.DataClasses;
using NUnit_practice.PageObjects.Utils;
using SeleniumExtras.PageObjects;

namespace NUnit_practice.PageObjects.Elements
{
    internal class TextBoxPage : CategoryPage
    {
        public TextBoxPage(ScenarioContext scenarioContext) 
            : base(scenarioContext)
        {
            PageFactory.InitElements(Context.Driver, this);
        }

        // Input Fields
        [FindsBy(How = How.CssSelector, Using = "label#userName-label")]
        public IWebElement? NameTextFieldLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input#userName")]
        public IWebElement? NameTextField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "label#userEmail-label")]
        public IWebElement? EmailTextFieldLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input#userEmail")]
        public IWebElement? EmailTextField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "label#currentAddress-label")]
        public IWebElement? CurrentAddressFieldLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "textarea#currentAddress")]
        public IWebElement? CurrentAddressTextField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "label#permanentAddress-label")]
        public IWebElement? PermanentAddressTextFieldLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "textarea#permanentAddress")]
        public IWebElement? PermanentAddressTextField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button#submit")]
        public IWebElement? SubmitButton { get; set; }

        // Output Text elements
        [FindsBy(How = How.CssSelector, Using = "div#output p#name")]
        public IWebElement? OutputName { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div#output p#email")]
        public IWebElement? OutputEmail { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div#output p#currentAddress")]
        public IWebElement? OutputCurrentAddress { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div#output p#permanentAddress")]
        public IWebElement? OutputPermanentAddress { get; set; }

        public void EnterName(string fullName)
        {
            WriteToField(NameTextField, fullName);
        }

        public void EnterEmail(string email)
        {
            WriteToField(EmailTextField, email);
        }

        public void EnterCurrentAddress(string currentAddress)
        {
            WriteToField(CurrentAddressTextField, currentAddress);
        }

        public void EnterPermanentAddress(string permanentAddress)
        {
            WriteToField(PermanentAddressTextField, permanentAddress);
        }

        public void ClickSubmitButton()
        {
            ClickElement(SubmitButton);
        }

        public bool IsEmailFieldVisible()
        {
            if (EmailTextField != null)
            {
                return EmailTextField.Displayed;
            }
            return false;
        }

        public void EnterDataInTextFields(TextBoxData data)
        {
            EnterName(data.FullName);
            EnterEmail(data.Email);
            EnterCurrentAddress(data.CurrentAddress);
            EnterPermanentAddress(data.PermanentAddress);
        }

        private string ParseOutputTableRow(string outputTableRow)
        {
            return outputTableRow.Substring(outputTableRow.IndexOf(":") + 1);
        }

        public TextBoxData GetOutputTableContents()
        {
            TextBoxData data = new()
            {
                FullName = ParseOutputTableRow(OutputName!.Text),
                Email = ParseOutputTableRow(OutputEmail!.Text),
                CurrentAddress = ParseOutputTableRow(OutputCurrentAddress!.Text),
                PermanentAddress = ParseOutputTableRow(OutputPermanentAddress!.Text)
            };
            return data;
        }
    }
}
