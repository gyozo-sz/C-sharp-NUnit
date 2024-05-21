using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace NUnit_practice
{
    public class SeleniumWebDriverTest
    {
        private IWebDriver webDriver;
        private readonly Dictionary<string, By> locatorMap = new();

        public SeleniumWebDriverTest() {
            locatorMap = new Dictionary<string, By>()
            {
                { "GDRP_do_not_consent_button",   By.CssSelector("button.fc-cta-do-not-consent") },
                { "search_bar",                   By.CssSelector("form#searchform input") },
                { "search_result_product_cards",  By.XPath("//article[contains(@class, 'product')]")},
                { "search_result_product_title",  By.XPath("//article[contains(@class, 'product')]//h2")},
                { "search_result_product_link",   By.XPath("//article[contains(@class, 'product')]//a")},
                { "on_sale_sticker",              By.CssSelector("div.product > span.onsale")},
                { "product_card_title",           By.CssSelector("h1.product_title")},
                { "product_card_price_span",      By.CssSelector("p.price span.amount")},
                { "search_page_title",            By.CssSelector("h1.page-title")},
                { "add_to_cart_button",           By.CssSelector("form.cart button")},
                { "navbar_cart_button",           By.CssSelector("nav a[class*=cart]")},
                { "currency_symbol_span",         By.CssSelector("span[class*=currencySymbol]") },
                { "update_basket_button",         By.CssSelector("input[name=update_cart]")}
            };
        }

        static private By ProductCardLocatorByProductName(string productName) => By.XPath($"//a[contains(text(), '{productName}')]");
        static private By RelatedProductLocatorByProductName(string relatedProductName) =>
            By.XPath($"//div[contains(@class, 'related products')]//*[contains(text(), '{relatedProductName}')]");
        static private By CartProductNameByIndex(uint cartIndex) => By.XPath($"//tr[@class='cart_item'][{cartIndex}]//td[@class='product-name']");
        static private By CartProductQuantityByIndex(uint cartIndex) => By.XPath($"//tr[@class='cart_item'][{cartIndex}]//td[@class='product-quantity']//input");
        static private By CartProductPriceByIndex(uint cartIndex) => By.XPath($"//tr[@class='cart_item'][{cartIndex}]//td[@class='product-price']/span");
        static private By CartProductSubtotalByIndex(uint cartIndex) => By.XPath($"//tr[@class='cart_item'][{cartIndex}]//td[@class='product-subtotal']");

        private Actions _getActions;
        private Actions GetActions
        {
            get
            {
                _getActions ??= new(webDriver);
                return _getActions;
            }
        }

        [SetUp]
        public void Setup()
        {
            var chromeOptions = new ChromeOptions() { PageLoadStrategy = PageLoadStrategy.None };
            webDriver = new ChromeDriver(chromeOptions);

            webDriver.Navigate().GoToUrl("https://practice.automationtesting.in/shop/");
            CloseGDPRPopup();
        }

        private IWebElement FindElementAndWait(By locator, int maxWaitInSec = 2)
        {
            WebDriverWait wait = new(webDriver, TimeSpan.FromSeconds(maxWaitInSec));
            return wait.Until(e => e.FindElement(locator));
        }

        private IReadOnlyList<IWebElement> FindElementsAndWait(By locator, int maxWaitInSec = 2)
        {
            WebDriverWait wait = new(webDriver, TimeSpan.FromSeconds(maxWaitInSec));
            return wait.Until(e => e.FindElements(locator));
        }

        private static Func<IWebDriver, IWebElement?> TextToBeDifferentInElement(By locator, string text)
        {
            return (driver) =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    var elementValue = element.Text;
                    if (!elementValue.Contains(text))
                    {
                        return element;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        private IWebElement? WaitUntilElementIsChanged(string originalValue, By locator, int maxWaitInSec = 2)
        {
            WebDriverWait wait = new(webDriver, TimeSpan.FromSeconds(maxWaitInSec));
            return wait.Until(TextToBeDifferentInElement(locator, originalValue));
        }

        private void CloseGDPRPopup()
        {
            FindElementAndWait(locatorMap["GDRP_do_not_consent_button"], 5).Click();
        }

        private void SearchForPhrase(string searchPhrase)
        {
            IWebElement searchForm = FindElementAndWait(locatorMap["search_bar"]);
            searchForm.SendKeys(searchPhrase + "\n");
        }

        private void CheckThatResultsContainTheSearchPhrase(string searchPhrase)
        {
            IReadOnlyList<IWebElement> htmlProductCards = FindElementsAndWait(locatorMap["search_result_product_cards"]);

            foreach (IWebElement? htmlProductCard in htmlProductCards)
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
            IWebElement selectedProductCard = FindElementAndWait(ProductCardLocatorByProductName(productName));
            GetActions.MoveToElement(selectedProductCard).Click().Perform();
        }

        private void CheckThatSaleStickerIsDisplayed()
        {
            Assert.That(FindElementAndWait(locatorMap["on_sale_sticker"]).Displayed, Is.True);
        }

        private void CheckThatOldAndNewPricesAreDisplayed()
        {
            IReadOnlyCollection<IWebElement?> productPrices = FindElementsAndWait(locatorMap["product_card_price_span"]);
            Assert.That(productPrices, Has.Count.EqualTo(2));
        }

        private void NavigateToRelatedProduct(string relatedProductName)
        {
            IWebElement relatedProductCart = FindElementAndWait(RelatedProductLocatorByProductName(relatedProductName));
            GetActions.MoveToElement(relatedProductCart).Perform();
            relatedProductCart.Click();
        }

        private (string, string) GetProductNameAndPrice()
        {
            string titleDisplayedOnSite = FindElementAndWait(locatorMap["product_card_title"]).Text;
            string priceDisplayedOnSite = FindElementAndWait(locatorMap["product_card_price_span"]).Text;
            return (titleDisplayedOnSite, priceDisplayedOnSite);
        }

        private void AddProductToCart()
        {
            FindElementAndWait(locatorMap["add_to_cart_button"]).Click();
        }

        private void NavigateToCart()
        {
            FindElementAndWait(locatorMap["navbar_cart_button"]).Click();
        }

        private static decimal ParseValueFromPrice(string priceWithCurrencySymbol, string currencySymbol)
        {
            return Convert.ToDecimal(priceWithCurrencySymbol.Replace(currencySymbol, ""));
        }

        private void CheckThatProductInfoMatches(string productNameOnCard, string productPriceOnCard)
        {
            int productQuantity = Convert.ToInt32(FindElementAndWait(CartProductQuantityByIndex(1)).GetAttribute("value"));
            string currencySymbol = FindElementAndWait(locatorMap["currency_symbol_span"]).Text;
            decimal productPrice = ParseValueFromPrice(FindElementAndWait(CartProductPriceByIndex(1)).Text, currencySymbol);

            decimal expectedPrice = productQuantity * productPrice;
            decimal actualSubtotal = ParseValueFromPrice(FindElementAndWait(CartProductSubtotalByIndex(1)).Text, currencySymbol);
            Assert.Multiple(() =>
            {
                Assert.That(FindElementAndWait(CartProductNameByIndex(1)).Text, Is.EqualTo(productNameOnCard));
                Assert.That(FindElementAndWait(CartProductPriceByIndex(1)).Text, Is.EqualTo(productPriceOnCard));
                Assert.That(actualSubtotal, Is.EqualTo(expectedPrice));
            });
        }

        private void CheckThatTitleContainsSearchPhrase(string searchPhrase)
        {
            Assert.Multiple(() =>
            {
                Assert.That(FindElementAndWait(locatorMap["search_page_title"]).Text, Does.Contain(searchPhrase));
                Assert.That(webDriver.Title, Does.Contain(searchPhrase));
            });
        }

        private void ChangeProductQuantityInCart(int targetQuantity)
        {
            IWebElement quantityInput = FindElementAndWait(CartProductQuantityByIndex(1));
            string originalQuantity = quantityInput.GetAttribute("value");
            if (Convert.ToInt32(originalQuantity) == targetQuantity)
            {
                return;
            }

            string originalPrice = FindElementAndWait(CartProductPriceByIndex(1)).Text;

            quantityInput.Clear();
            quantityInput.SendKeys(targetQuantity.ToString());
            FindElementAndWait(locatorMap["update_basket_button"]).Click();

            WaitUntilElementIsChanged(originalPrice, CartProductSubtotalByIndex(1), 5);
            quantityInput = FindElementAndWait(CartProductQuantityByIndex(1));
            Assert.That(Convert.ToInt32(quantityInput.GetAttribute("value")), Is.EqualTo(targetQuantity));
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
            int targetQuantity = 3;
            ChangeProductQuantityInCart(targetQuantity);
            CheckThatProductInfoMatches(productNameOnCard, productPriceOnCard);
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Close();
        }
    }
}