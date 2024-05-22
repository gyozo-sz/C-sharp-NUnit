using SeleniumExtras.PageObjects;

namespace NUnit_practice.PageObjects
{
    internal class MyAccountPage
    {
        // Login Section elements
        [FindsBy(How = How.XPath, Using = "(//div[@id='content']//h2)[1]")]
        public IWebElement LoginTitle { get; set; }

        // Username elements
        [FindsBy(How = How.CssSelector, Using = "form.login label[for=username]")]
        public IWebElement LoginUsernameLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form.login input[id=username]")]
        public IWebElement LoginUsernameField { get; set; }

        // Password elements
        [FindsBy(How = How.CssSelector, Using = "form.login label[for=password]")]
        public IWebElement LoginPasswordLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form.login input[id=password]")]
        public IWebElement LoginPasswordField { get; set; }

        // Remember me elements
        [FindsBy(How = How.CssSelector, Using = "form.login label[for=rememberme]")]
        public IWebElement RememberMeLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form.login input[id=rememberme]")]
        public IWebElement RememberMeCheckbox { get; set; }

        // Others
        [FindsBy(How = How.CssSelector, Using = "form.login input[type=submit]")]
        public IWebElement LoginButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form.login p[class*=lost_password] a")]
        public IWebElement LostPasswordLink { get; set; }


        // Registering section elements
        [FindsBy(How = How.XPath, Using = "(//div[@id='content']//h2)[2]")]
        public IWebElement RegisterTitle { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form.register label[for=reg_email]")]
        public IWebElement RegisterEmailLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form.register input[id=reg_email]")]
        public IWebElement RegisterEmailField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form.register label[for=reg_password]")]
        public IWebElement RegisterPasswordLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form.register input[id=reg_password]")]
        public IWebElement RegisterPasswordField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form.register input[type=submit]")]
        public IWebElement RegisterButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div[class*=MyAccount-content] p")]
        public IWebElement WelcomeMessage { get; set; }

        [FindsBy(How = How.CssSelector, Using = "ul[class*=error] li")]
        public IWebElement ErrorMessage { get; set; }


        public string LostPasswordLinkText()
        {
            return LostPasswordLink.Text;
        }

        public string RememberMeCheckboxLabelText()
        {
            return RememberMeLabel.Text;
        }

        public string RegisterButtonText()
        {
            return RegisterButton.Text;
        }

        public MyAccountPage EnterLoginUsername(string username)
        {
            LoginUsernameField.SendKeys(username);
            return this;
        }

        public MyAccountPage EnterLoginPassword(string password)
        {
            LoginPasswordField.SendKeys(password);
            return this;
        }

        public MyAccountPage ClickLoginButton()
        {
            LoginButton.Click();
            return this;
        }

        public MyAccountPage ClickLostPasswordLink()
        {
            LostPasswordLink.Click();
            return this;
        }

        public MyAccountPage ClickRememberMeCheckbox()
        {
            RememberMeCheckbox.Click();
            return this;
        }

        public MyAccountPage EnterRegisterEmail(string email)
        {
            RegisterEmailField.SendKeys(email);
            return this;
        }

        public MyAccountPage EnterRegisterPassword(string password)
        {
            RegisterPasswordField.SendKeys(password);
            return this;
        }

        public MyAccountPage LoginUser(string Username, string password)
        {
            EnterLoginUsername(Username);
            EnterLoginPassword(password);
            ClickLoginButton();
            return this;
        }

        public string GetLogInGreeting()
        {
            return WelcomeMessage.Text;
        }

        public string GetLoginUsername()
        {
            return LoginUsernameField.GetAttribute("value");
        }

        public string GetLoginPassword()
        {
            return LoginPasswordField.GetAttribute("value");
        }

        public bool IsRememberMeChecked()
        {
            return RememberMeCheckbox.Selected;
        }

        public string GetRegisterEmail()
        {
            return RegisterEmailField.GetAttribute("value");
        }

        public string GetRegisterPassword()
        {
            return RegisterPasswordField.GetAttribute("value");
        }

        public string? GetErrorMessageText()
        {
            try
            {
                return ErrorMessage.Text;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
    }
}
