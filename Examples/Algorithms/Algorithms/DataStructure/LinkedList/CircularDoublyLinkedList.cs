namespace Algorithms.DataStructure.LinkedList
{
    public class CircularDoublyLinkedList<T>
    {
        public readonly Node<T> Header; 

        public CircularDoublyLinkedList(T headerValue)
        {
            Header = new Node<T>(headerValue);
            Header.Next = Header;
            Header.Previous = Header;
        }

        public Node<T> Find(Node<T> node)
        {
            var current = Header;
            while (current != node && current !=null)
            {
                current = current.Next;
            }
            return current;
        }

        public Node<T> Insert(T value, Node<T> after)
        {
            var node = Find(after);
            var newNode = new Node<T>(value) {Next = node.Next, Previous = node};

            newNode.Next.Previous = newNode;
            node.Next = newNode;

            return newNode;
        }

        public void Delete(Node<T> nodeToDelete)
        {
            var node = Find(nodeToDelete);
            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
        }
    }
}