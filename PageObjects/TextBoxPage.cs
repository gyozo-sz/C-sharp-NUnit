using BoDi;
using SeleniumExtras.PageObjects;

namespace NUnit_practice.PageObjects
{
    internal class TextBoxPage : PageObjectBase
    {
        public TextBoxPage(IObjectContainer objectContainer) : base(objectContainer) {
            var context = objectContainer.Resolve<PageContext>("Context");
            PageFactory.InitElements(context.Driver, this);
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

        [FindsBy(How = How.CssSelector, Using = "input#currentAddress")]
        public IWebElement? CurrentAddressTextField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "label#permanentAddress-label")]
        public IWebElement? PermanentAddressTextFieldLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input#permanentAddress")]
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
    }   
}
