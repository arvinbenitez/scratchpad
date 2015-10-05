using System;

namespace Algorithms.DataStructure.BinaryTree
{
    public class Node
    {
        public int Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }

        public void Display()
        {
            Console.WriteLine(Value);
        }
    }
}
