using NUnit_practice.PageObjects.Utils;
using SeleniumExtras.PageObjects;

namespace NUnit_practice.PageObjects.WindowsCategory
{
    internal class SamplePage : CategoryPage
    {
        public SamplePage(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            PageFactory.InitElements(Context.Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "h1#sampleHeading")]
        public IWebElement? PageHeader { get; set; }

        public string GetHeaderText()
        {
            return GetElementText(PageHeader);
        }

        public static string GetExpectedHeaderText()
        {
            return "This is a sample page";
        }
    }
}
