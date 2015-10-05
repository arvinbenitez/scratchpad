namespace Algorithms.DataStructure.LinkedList
{
    public class LinkedListIterator<T>
    {
        private readonly CircularDoublyLinkedList<T> list;
        private Node<T> current;

        public LinkedListIterator(CircularDoublyLinkedList<T> list)
        {
            this.list = list;
            current = list.Header;
        }

        public Node<T> GetNext()
        {
            current = current.Next;
            return current;
        }

        public Node<T> Current
        {
            get { return current; }
        }
    }
}