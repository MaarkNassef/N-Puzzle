namespace N_Puzzle
{
    public class Puzzle
    {
        public Node Initial;
        public bool isSolvable;
        public int numOfSteps;
        public PriorityQueue<Node, int>? queue;
        public Puzzle(Node Initial)
        {
            this.Initial = Initial;
            this.isSolvable = IsSolvable();
        }
        private int Inversions()
        {
            int iterator = 0;
            for (int i = 0; i < Initial.Matrix.GetLength(0) * Initial.Matrix.GetLength(1) - 1; i++)
                for (int j = i + 1; j < Initial.Matrix.GetLength(0) * Initial.Matrix.GetLength(1); j++)
                    if (Initial.Matrix[i / Initial.Matrix.GetLength(0), i % Initial.Matrix.GetLength(0)] > 0
                        && Initial.Matrix[j / Initial.Matrix.GetLength(0), j % Initial.Matrix.GetLength(0)] > 0
                        && Initial.Matrix[i / Initial.Matrix.GetLength(0), i % Initial.Matrix.GetLength(0)] > Initial.Matrix[j / Initial.Matrix.GetLength(0), j % Initial.Matrix.GetLength(0)])
                        iterator++;
            return iterator;
        }
        public bool IsSolvable()
        {
            int invCount = Inversions();
            if (Initial.Matrix.GetLength(0) % 2 == 1 && invCount % 2 == 0)
            {
                return true;
            }
            else if (Initial.Matrix.GetLength(0) % 2 == 0 && Initial.ZeroPlace.Item1 % 2 == 0 && invCount % 2 == 1)
            {
                return true;
            }
            else if (Initial.Matrix.GetLength(0) % 2 == 0 && Initial.ZeroPlace.Item1 % 2 == 1 && invCount % 2 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Stack<Node> Solve()
        {
            Stack<Node> stack = new Stack<Node>();
            if (isSolvable)
            {
                queue = Initial.AllPossibleMoves();
                while (queue.Count > 0)
                {
                    Node node = queue.Dequeue();
                    if (node.H == 0)
                    {
                        this.numOfSteps = node.G;
                        stack = GetRoot(node);
                        break;
                    }
                    PriorityQueue<Node, int> subQueue = node.AllPossibleMoves();
                    while (subQueue.Count > 0)
                    {
                        Node n = subQueue.Dequeue();
                        queue.Enqueue(n, n.F);
                    }
                }
            }
            return stack;
        }
        public Stack<Node> GetRoot(Node node)
        {
            Stack<Node> stack = new Stack<Node>();
            while (node != null)
            {
                stack.Push(node);
                node = node.Parent;
            }
            return stack;
        }
        private void GetRootHelper(Stack<Node> stack, Node node)
        {
            if (node.Parent != null)
            {
                stack.Push(node);
                GetRootHelper(stack, node.Parent);
            }
        }
    }
}
