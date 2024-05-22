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

        [SetUp]
        public void Setup()
        {
            webDriver = new ChromeDriver();

            webDriver.Navigate().GoToUrl(URL);

            myAccountPage = new MyAccountPage();
            dataConsentPage = new DataConsentPage();
            PageFactory.InitElements(webDriver, myAccountPage);
            PageFactory.InitElements(webDriver, dataConsentPage);

            WaitAndCloseGDPRPopup();
        }

        private void WaitAndCloseGDPRPopup()
        {
            dataConsentPage.RejectDataUse();
        }

        [Test]
        public void LoginUsernameFieldTest()
        {
            string username = "some@username.com";
            myAccountPage.EnterLoginUsername(username);
            Assert.That(myAccountPage.GetLoginUsername(), Is.EqualTo(username));
        }

        [Test]
        public void LoginPasswordFieldTest()
        {
            string password = "pass#23@{a.";
            myAccountPage.EnterLoginPassword(password);
            Assert.That(myAccountPage.GetLoginPassword(), Is.EqualTo(password));
        }

        [Test]
        public void RememberMeTest()
        {
            bool isRememberMeActive = myAccountPage.IsRememberMeChecked();
            myAccountPage.ClickRememberMeCheckbox();
            Assert.That(myAccountPage.IsRememberMeChecked(), Is.EqualTo(!isRememberMeActive));
            myAccountPage.ClickRememberMeCheckbox();
            Assert.That(myAccountPage.IsRememberMeChecked(), Is.EqualTo(isRememberMeActive));
        }

        [Test]
        public void RegisterUsernameFieldTest()
        {
            string username = "some@username.com";
            myAccountPage.EnterRegisterEmail(username);
            Assert.That(myAccountPage.GetRegisterEmail(), Is.EqualTo(username));
        }

        [Test]
        public void RegisterPasswordFieldTest()
        {
            string password = "pass#23@{a.";
            myAccountPage.EnterLoginPassword(password);
            Assert.That(myAccountPage.GetLoginPassword(), Is.EqualTo(password));
        }

        [Test]
        public void LostPasswordLinkTest()
        {
            myAccountPage.ClickLostPasswordLink();
            Assert.That(webDriver.Url, Is.EqualTo(LostPasswordURL));
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