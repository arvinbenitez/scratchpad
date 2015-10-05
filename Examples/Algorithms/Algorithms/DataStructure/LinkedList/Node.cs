namespace Algorithms.DataStructure.LinkedList
{
    public class Node<T>
    {
        private readonly T value;

        public Node(T value)
        {
            this.value = value;
        }


        public Node<T> Next { get; set; }
        public Node<T> Previous { get; set; }

        public T Value
        {
            get { return value; }
        }
    }
}