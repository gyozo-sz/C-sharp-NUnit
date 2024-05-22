using SeleniumExtras.PageObjects;

namespace NUnit_practice.PageObjects
{
    internal class MyAccountPage : PageObjectBase
    {
        public MyAccountPage(PageContext context) : base(context) {
            PageFactory.InitElements(context.Driver, this);
        }

        // Login Section elements
        [FindsBy(How = How.XPath, Using = "(//div[@id='content']//h2)[1]")]
        public IWebElement? LoginTitle { get; set; }

        // Username elements
        [FindsBy(How = How.CssSelector, Using = "form.login label[for=username]")]
        public IWebElement? LoginUsernameLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form.login input[id=username]")]
        public IWebElement? LoginUsernameField { get; set; }

        // Password elements
        [FindsBy(How = How.CssSelector, Using = "form.login label[for=password]")]
        public IWebElement? LoginPasswordLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form.login input[id=password]")]
        public IWebElement? LoginPasswordField { get; set; }

        // Remember me elements
        [FindsBy(How = How.CssSelector, Using = "form.login label[for=rememberme]")]
        public IWebElement? RememberMeLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form.login input[id=rememberme]")]
        public IWebElement? RememberMeCheckbox { get; set; }

        // Others
        [FindsBy(How = How.CssSelector, Using = "form.login input[type=submit]")]
        public IWebElement? LoginButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form.login p[class*=lost_password] a")]
        public IWebElement? LostPasswordLink { get; set; }


        // Registering section elements
        [FindsBy(How = How.XPath, Using = "(//div[@id='content']//h2)[2]")]
        public IWebElement? RegisterTitle { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form.register label[for=reg_email]")]
        public IWebElement? RegisterEmailLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form.register input[id=reg_email]")]
        public IWebElement? RegisterEmailField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form.register label[for=reg_password]")]
        public IWebElement? RegisterPasswordLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form.register input[id=reg_password]")]
        public IWebElement? RegisterPasswordField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form.register input[type=submit]")]
        public IWebElement? RegisterButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div[class*=MyAccount-content] p")]
        public IWebElement? WelcomeMessage { get; set; }

        [FindsBy(How = How.CssSelector, Using = "ul[class*=error] li")]
        public IWebElement? ErrorMessage { get; set; }


        public string? LostPasswordLinkText()
        {
            return GetElementText(LostPasswordLink);
        }

        public string? RememberMeCheckboxLabelText()
        {
            return GetElementText(RememberMeLabel);
        }

        public string? RegisterButtonText()
        {
            return GetElementText(RegisterButton);
        }

        public MyAccountPage EnterLoginUsername(string username)
        {
            WriteToField(LoginUsernameField, username);
            return this;
        }

        public MyAccountPage EnterLoginPassword(string password)
        {
            WriteToField(LoginPasswordField, password);
            return this;
        }

        public MyAccountPage ClickLoginButton()
        {
            ClickElement(LoginButton);
            return this;
        }

        public MyAccountPage ClickLostPasswordLink()
        {
            ClickElement(LostPasswordLink);
            return this;
        }

        public MyAccountPage ClickRememberMeCheckbox()
        {
            ClickElement(RememberMeCheckbox);
            return this;
        }

        public MyAccountPage EnterRegisterEmail(string email)
        {
            WriteToField(RegisterEmailField, email);
            return this;
        }

        public MyAccountPage EnterRegisterPassword(string password)
        {
            WriteToField(RegisterPasswordField, password);
            return this;
        }

        public MyAccountPage LoginUser(string Username, string password)
        {
            EnterLoginUsername(Username);
            EnterLoginPassword(password);
            ClickLoginButton();
            return this;
        }

        public string? GetLogInGreeting()
        {
            return GetElementText(WelcomeMessage);
        }

        public string? GetLoginUsername()
        {
            return GetElementAttribute(LoginUsernameField, "value");
        }

        public string? GetLoginPassword()
        {
            return GetElementAttribute(LoginPasswordField, "value");
        }

        public bool? IsRememberMeChecked()
        {
            return IsElementSelected(RememberMeCheckbox);
        }

        public string? GetRegisterEmail()
        {
            return GetElementAttribute(RegisterEmailField, "value");
        }

        public string? GetRegisterPassword()
        {
            return GetElementAttribute(RegisterPasswordField, "value");

        }

        public string? GetErrorMessageText()
        {
            try
            {
                return GetElementText(ErrorMessage);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
    }
}
