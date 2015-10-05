using Algorithms.DataStructure;
using Algorithms.DataStructure.LinkedList;
using NUnit.Framework;

namespace Algorithms.Tests
{
    [TestFixture]
    public class LinkedListTest
    {
        [Test]
        public void When_Inserting_Node_Should_Add_In_Correct_Location()
        {
            var linkedList = new CircularDoublyLinkedList<string>("Header");
            Node<string> currentNode = null;

            for (var i = 0; i < 100; i++)
            {
                currentNode = linkedList.Insert(string.Format("Value {0}", i), currentNode ?? linkedList.Header);
            }

            Assert.That(linkedList.Header.Next.Value, Is.EqualTo("Value 0"));
            Assert.That(linkedList.Header.Previous.Value, Is.EqualTo("Value 99"));

            Assert.That(linkedList.Header.Previous.Previous.Value, Is.EqualTo("Value 98"));
            Assert.That(linkedList.Header.Next.Next.Value, Is.EqualTo("Value 1"));

            var count = 0;
            var current = linkedList.Header.Next;
            do
            {
                count++;
                current = current.Next;
            } while (current != linkedList.Header);
            Assert.That(count, Is.EqualTo(100));

        }
    }
}