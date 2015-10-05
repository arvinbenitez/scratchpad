namespace Algorithms.DataStructure.BinaryTree
{
    public class BinarySearchTree
    {
        public Node Root { get; set; }

        public void InOrder(Node node)
        {
            if (node != null)
            {
                InOrder(node.Left);
                node.Display();
                InOrder(node.Right);
            }
        }

        public int MinValue()
        {
            var current = Root;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current.Value;
        }
        public int MaxValue()
        {
            var current = Root;
            while (current.Right != null)
            {
                current = current.Right;
            }
            return current.Value;
        }

        public void Insert(int value)
        {
            var newNode = new Node{Value = value};
            if (Root == null)
            {
                Root = newNode;
            }
            else
            {
                var current = Root;
                while (true)
                {
                    Node parent = current;
                    if (value < current.Value)
                    {
                        current = current.Left;
                        if (current == null)
                        {
                            parent.Left = newNode;
                            break;
                        }
                    }
                    else
                    {
                        current = current.Right;
                        if (current == null)
                        {
                            parent.Right = newNode;
                            break;
                        }
                    }
                }
            }
        }
    }
}