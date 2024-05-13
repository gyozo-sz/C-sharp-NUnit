namespace NUnit_practice
{
    public class AssertionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IdenticalStringsTest()
        {
            string str1 = "What a beautiful world!";
            string str2 = "What a beautiful world!";
            Assert.That(str1, Is.EqualTo(str2), $"String {str1} is not identical to string {str2}");
        }

        [Test]
        public void IdenticalStringListsTest() {
            string[] fruits1 = { "apple", "orange", "banana", "mango" };
            string[] fruits2 = { "apple", "orange", "banana", "mango" };
            Assert.That(fruits1, Is.EqualTo(fruits2), $"String list {fruits1} is not identical to string list {fruits2}");
        }

        [Test]
        public void StringListContainsElementTest()
        {
            string[] fruits = { "apple", "orange", "banana", "mango" };
            string fruit = "banana";
            Assert.That(fruits, Does.Contain(fruit), $"String list {fruits} does not contain fruit {fruit}");
        }

        [Test]
        public void IntegerGreaterThanTest() {
            int a = 10, b = 5;
            Assert.That(a, Is.GreaterThan(b), $"Integer {a} is not greater than {b}");
        }
    }
}