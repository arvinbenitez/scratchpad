using Algorithms.Sorting;
using NUnit.Framework;

namespace Algorithms.Tests
{
    [TestFixture]
    public class RadixSortTest
    {
        [Test]
        public void When_Sorting_Two_Digits_Should_Sort()
        {
            var ints = new[] {99, 75, 64, 87, 88};
            var sorter = new RadixSort();
            sorter.Sort(ints);
            Assert.That(ints[0], Is.EqualTo(64));
            Assert.That(ints[1], Is.EqualTo(75));
            Assert.That(ints[2], Is.EqualTo(87));
            Assert.That(ints[3], Is.EqualTo(88));
            Assert.That(ints[4], Is.EqualTo(99));
        }

        [Test]
        public void When_Sorting_Mixed_Digits_Should_Sort()
        {
            var ints = new[] {990, 75, 64, 870, 88};
            var sorter = new RadixSort();
            sorter.Sort(ints);
            Assert.That(ints[0], Is.EqualTo(64));
            Assert.That(ints[1], Is.EqualTo(75));
            Assert.That(ints[2], Is.EqualTo(88));
            Assert.That(ints[3], Is.EqualTo(870));
            Assert.That(ints[4], Is.EqualTo(990));
        }

        [Test]
        public void When_Sorting_Large_Digits_Should_Sort()
        {
            var ints = new[] { 2147242505, 1999919477, 19361, 870, 88 };
            var sorter = new RadixSort();
            sorter.Sort(ints);
            Assert.That(ints[0], Is.EqualTo(88));
            Assert.That(ints[1], Is.EqualTo(870));
            Assert.That(ints[2], Is.EqualTo(19361));
            Assert.That(ints[3], Is.EqualTo(1999919477));
            Assert.That(ints[4], Is.EqualTo(2147242505));
        }
    }
}
