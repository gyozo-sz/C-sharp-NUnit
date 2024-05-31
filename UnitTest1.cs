using NUnit_practice.PageObjects;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System.Xml.Serialization;

namespace NUnit_practice
{
    public class LoginPageTests
    {
        private IWebDriver webDriver;
        MyAccountPage myAccountPage;
        DataConsentPage dataConsentPage;
        readonly string URL = "https://practice.automationtesting.in/my-account/";
        readonly string LostPasswordURL = "https://practice.automationtesting.in/my-account/lost-password/";

        readonly string validEmail = "some@email.com";
        readonly string validPassword = ")3jf8BdjL2a$smm";

        const string LostPasswordText = "Lost your password?";
        const string LoginButtonText = "Login";
        const string RememberMeLabelText = "Remember me";
        const string RegisterButtonText = "Register";

        [SetUp]
        public void Setup()
        {
            var chromeOptions = new ChromeOptions() { PageLoadStrategy = PageLoadStrategy.None };
            webDriver = new ChromeDriver(chromeOptions);
            PageContext context = new(webDriver);

            myAccountPage = new MyAccountPage(context);
            dataConsentPage = new DataConsentPage(context);

            webDriver.Navigate().GoToUrl(URL);
            CloseGDPRPopup();
        }

        private void CloseGDPRPopup()
        {
            dataConsentPage.RejectDataUse();
        }

        [Test]
        public void RememberMeTest()
        {
            bool? isRememberMeActive = myAccountPage.IsRememberMeChecked();
            myAccountPage.ClickRememberMeCheckbox();
            Assert.That(myAccountPage.IsRememberMeChecked(), Is.EqualTo(!isRememberMeActive));
            myAccountPage.ClickRememberMeCheckbox();
            Assert.That(myAccountPage.IsRememberMeChecked(), Is.EqualTo(isRememberMeActive));
        }

        [Test]
        public void LostPasswordLinkTest()
        {
            myAccountPage.ClickLostPasswordLink();
            Assert.That(webDriver.Url, Is.EqualTo(LostPasswordURL));
        }

        [Test]
        public void LostPasswordLinkTextTest()
        {
            string lostPasswordText = myAccountPage.LostPasswordLinkText();
            Assert.That(lostPasswordText, Is.EqualTo(LostPasswordText));
        }

        [Test]
        public void RememberMeLabelTextTest()
        {
            string rememberMeLabelText = myAccountPage.RememberMeCheckboxLabelText();
            Assert.That(rememberMeLabelText, Is.EqualTo(RememberMeLabelText));
        }

        [Test]
        public void LoginButtonTextTest()
        {
            string loginButtonText = myAccountPage.LoginButtonText();
            Assert.That(loginButtonText, Is.EqualTo(LoginButtonText));
        }

        [Test]
        public void RegisterButtonTextTest()
        {
            string registerButtonText = myAccountPage.RegisterButtonText();
            Assert.That(registerButtonText, Is.EqualTo(RegisterButtonText));
        }

        [Test]
        public void LoginWithValidCredentials()
        {
            myAccountPage.LoginUser(validEmail, validPassword);

            string username = validEmail.Split('@')[0];
            Assert.That(myAccountPage.GetLogInGreeting(), Does.Contain(username));
        }

        [Test]
        public void LoginWithInvalidEmail()
        {
            string invalidEmail = "a" + validEmail;
            myAccountPage.LoginUser(invalidEmail, validPassword);

            Assert.That(myAccountPage.GetErrorMessageText(), Does.Contain("Error"));
            Assert.That(myAccountPage.GetErrorMessageText(), Does.Contain("email"));
        }

        [Test]
        public void LoginWithInvalidPassword()
        {
            string invalidPassword = "a" + validPassword;
            myAccountPage.LoginUser(validEmail, invalidPassword);

            Assert.That(myAccountPage.GetErrorMessageText(), Does.Contain("Error"));
            Assert.That(myAccountPage.GetErrorMessageText(), Does.Contain("password"));
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Close();
        }
    }
}