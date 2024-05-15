using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Xml.Serialization;

namespace NUnit_practice
{
    public class SeleniumWebDriverTest
    {
        private IWebDriver webDriver;
        private readonly Dictionary<string, By> locatorMap = new Dictionary<string, By>();

        public SeleniumWebDriverTest() {
            locatorMap = new Dictionary<string, By>()
            {
                { "GDRP_do_not_consent_button",             By.CssSelector("button.fc-cta-do-not-consent") },
                { "search_bar",                             By.CssSelector("form#searchform input") },
                { "search_result_product_cards",            By.XPath("//article[contains(@class, 'product')]")},
                { "search_result_product_title",            By.XPath("//article[contains(@class, 'product')]//h2")},
                { "search_result_product_link",             By.XPath("//article[contains(@class, 'product')]//a")},
                { "on_sale_sticker",                        By.CssSelector("div.product > span.onsale")},
                { "product_card_title",                     By.CssSelector("h1.product_title")},
                { "product_card_price_span",                By.CssSelector("p.price span.amount")},
                { "search_page_title",                      By.CssSelector("h1.page-title")},
                {"add_to_cart_button",                      By.CssSelector("form.cart button")},
                { "navbar_cart_button",                     By.CssSelector("nav a[class*=cart]")},
                { "cart_product_name_table_element",        By.CssSelector("td.product-name")},
                { "cart_product_total_price_table_element", By.CssSelector("td.product-subtotal span.amount")}
        };
            
        }

        public By ProductCardLocatorByProductName(string productName) => By.XPath($"//a[contains(text(), '{productName}')]");
        public By RelatedProductLocatorByProductName(string relatedProductName) => 
            By.XPath($"//div[contains(@class, 'related products')]//*[contains(text(), '{relatedProductName}')]");

        [SetUp]
        public void Setup()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.PageLoadStrategy = PageLoadStrategy.None;
            webDriver = new ChromeDriver(chromeOptions);
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            webDriver.Navigate().GoToUrl("https://practice.automationtesting.in/shop/");
            WaitAndCloseGDPRPopup();
            //IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
        }

        private IWebElement FindElementWithWait(By selectedBy, uint waitTime)
        {
            WebDriverWait wait = new(webDriver, TimeSpan.FromSeconds(waitTime));
            return wait.Until(e => e.FindElement(selectedBy));
        }

        private void WaitAndCloseGDPRPopup()
        {
            webDriver.FindElement(locatorMap["GDRP_do_not_consent_button"]).Click();
        }

        private void SearchForPhrase(string searchPhrase)
        {
            IWebElement searchForm = webDriver.FindElement(locatorMap["search_bar"]);
            searchForm.SendKeys(searchPhrase + "\n");
        }

        private void CheckThatResultsContainTheSearchPhrase(string searchPhrase)
        {
            IReadOnlyList<IWebElement> htmlProductCards = webDriver.FindElements(locatorMap["search_result_product_cards"]);

            foreach (IWebElement htmlProductCard in htmlProductCards)
            {
                IReadOnlyList<IWebElement> productLinks = htmlProductCard.FindElements(locatorMap["search_result_product_link"]);
                Assert.That(productLinks, Is.Not.Empty);
                IWebElement productLink = productLinks[0];
                Assert.That(productLink.GetAttribute("href"), Does.StartWith("https://"));

                
                IWebElement productName = htmlProductCard.FindElement(locatorMap["search_result_product_title"]);
                Assert.That(productName.Text, Does.Contain(searchPhrase));
            }
        }

        private void ClickProductInSearchResult(string productName)
        {
            string selectedProductName = "Thinking in HTML";
            IWebElement selectedProductCard = webDriver.FindElement(ProductCardLocatorByProductName(selectedProductName));
            Actions actions = new(webDriver);
            actions.MoveToElement(selectedProductCard);
            actions.Click();
            actions.Perform();
        }

        private void CheckThatSaleStickerIsDisplayed()
        {
            Assert.That(webDriver.FindElements(locatorMap["on_sale_sticker"]), Is.Not.Empty);
        }

        private void CheckThatOldAndNewPricesAreDisplayed()
        {
            IReadOnlyCollection<IWebElement> productPrices = webDriver.FindElements(locatorMap["product_card_price_span"]);
            Assert.That(productPrices.Count, Is.EqualTo(2));
        }

        private void NavigateToRelatedProduct(string relatedProductName)
        {
            IWebElement relatedProductCart = webDriver.FindElement(RelatedProductLocatorByProductName(relatedProductName));
            Actions actions = new(webDriver);
            actions.MoveToElement(relatedProductCart);
            actions.Perform();
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)webDriver;
            relatedProductCart.Click();
        }

        private (string, string) GetProductNameAndPrice()
        {
            string titleDisplayedOnSite = webDriver.FindElement(locatorMap["product_card_title"]).Text;
            string priceDisplayedOnSite = webDriver.FindElement(locatorMap["product_card_price_span"]).Text;
            return (titleDisplayedOnSite, priceDisplayedOnSite);
        }

        private void AddProductToCart()
        {
            webDriver.FindElement(locatorMap["add_to_cart_button"]).Click();
        }

        private void NavigateToCart()
        {
            webDriver.FindElement(locatorMap["navbar_cart_button"]).Click();
        }

        private void CheckThatProductNameAndPriceMatch(string productNameOnCard, string productPriceOnCard)
        {
            Assert.That(webDriver.FindElement(locatorMap["cart_product_name_table_element"]).Text, Is.EqualTo(productNameOnCard));
            Assert.That(webDriver.FindElement(locatorMap["cart_product_total_price_table_element"]).Text, Is.EqualTo(productPriceOnCard));
        }

        private void CheckThatTitleContainsSearchPhrase(string searchPhrase)
        {
            Assert.That(webDriver.FindElement(locatorMap["search_page_title"]).Text, Does.Contain(searchPhrase));
            Assert.That(webDriver.Title, Does.Contain(searchPhrase));
        }

        [Test]
        public void ShoppingFlowE2E()
        {
            string searchPhrase = "HTML";
            SearchForPhrase(searchPhrase);
            CheckThatTitleContainsSearchPhrase(searchPhrase);
            CheckThatResultsContainTheSearchPhrase(searchPhrase);

            ClickProductInSearchResult("Thinking in HTML");
            CheckThatSaleStickerIsDisplayed();
            CheckThatOldAndNewPricesAreDisplayed();

            string html5WebAppDevelopmentTitle = "HTML5 WebApp Develpment";
            NavigateToRelatedProduct(html5WebAppDevelopmentTitle);

            (string productNameOnCard, string productPriceOnCard) = GetProductNameAndPrice();
            AddProductToCart();

            NavigateToCart();
            CheckThatProductNameAndPriceMatch(productNameOnCard, productPriceOnCard);
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Close();
        }
    }
}