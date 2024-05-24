namespace NUnit_practice
{
    [TestFixture]
    [DefaultFloatingPointTolerance(0.1)]
    public class AssertionTests
    {
        [SetUp]
        public void Setup()
        {
            Console.WriteLine("Test case started.");
        }


        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("Test case finished.");
        }
    }
}