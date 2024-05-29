using BoDi;

namespace NUnit_practice.PageObjects.Utils
{
    internal class CategoryPage : NavigationPage
    {
        public CategoryPage(IObjectContainer objectContainer, ScenarioContext scenarioContext) : base(objectContainer, scenarioContext)
        {

        }
    }
}
