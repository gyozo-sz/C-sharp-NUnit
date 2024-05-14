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

        [Test]
        public void IdenticalStringsTest()
        {
            string str1 = "What a beautiful world!";
            string str2 = "What a beautiful world!";
            Assert.That(str1, Is.EqualTo(str2), $"String {str1} is not identical to string {str2}");
            Assert.AreEqual(str1, str2, $"String {str1} is not identical to string {str2}");
        }

        [Test]
        public void IdenticalStringListsTest() {
            string[] fruits1 = { "apple", "orange", "banana", "mango" };
            string[] fruits2 = { "apple", "orange", "banana", "mango" };
            Assert.That(fruits1, Is.EqualTo(fruits2), $"String list {fruits1} is not identical to string list {fruits2}");
            CollectionAssert.AreEqual(fruits1, fruits2, $"String list {fruits1} is not identical to string list {fruits2}");
        }

        [Test]
        public void StringListContainsElementTest()
        {
            string[] fruits = { "apple", "orange", "banana", "mango" };
            string fruit = "banana";
            Assert.That(fruits, Does.Contain(fruit), $"String list {fruits} does not contain fruit {fruit}");
            Assert.Contains(fruit, fruits, $"String list {fruits} does not contain fruit {fruit}");
        }

        [Test]
        public void IntegerGreaterThanTest() {
            int a = 10, b = 5;
            Assert.That(a, Is.GreaterThan(b), $"Integer {a} is not greater than {b}");
            Assert.Greater(a, b, $"Integer {a} is not greater than {b}");
        }

        [Test, Pairwise]
        public void SomeArithmeticTest(
             [Values(-4.3 ,-1, 0, 3.5, 15)] double a,
             [Values(-8, -3.3, 0, 1, 4.5)] double b,
             [Values(-12.9, -2, 0, 14, 23.1)] double c)
        {
            Assert.That(Math.Abs(a + b + c), Is.LessThanOrEqualTo(Math.Abs(a) + Math.Abs(b) + Math.Abs(c)), 
                $"The absolute value of the sum is greater than the sum of absolute values for the following integers: {a}, {b}, {c}");
            Assert.LessOrEqual(Math.Abs(a + b + c), Math.Abs(a) + Math.Abs(b) + Math.Abs(c),
                $"The absolute value of the sum is greater than the sum of absolute values for the following integers: {a}, {b}, {c}");
        }

        [Test]
        public void DefaultFloatingPointToleranceTestFromFixture()
        {
            float a = 2.3f;
            float b = 2.34f;
            Assert.That(a, Is.EqualTo(b));
            Assert.That(a, Is.Not.EqualTo(b + 1));
        }

        [Test]
        [DefaultFloatingPointTolerance(2)]
        public void DefaultFloatingPointToleranceTest()
        {
            float a = 2.3f;
            float b = 3.82f;
            Assert.That(a, Is.EqualTo(b));
            Assert.That(a, Is.Not.EqualTo(b + 1));
        }

        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("Test case finished.");
        }
    }
}