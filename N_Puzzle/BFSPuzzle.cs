using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Puzzle
{
    public class BFSPuzzle
    {
        public BFSNode Initial;
        public bool isSolvable;
        public int numOfSteps;
        public Queue<BFSNode>? queue;
        public BFSPuzzle(BFSNode Initial)
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
        public Stack<BFSNode> Solve()
        {
            Stack<BFSNode> stack = new Stack<BFSNode>();
            if (isSolvable)
            {
                queue = Initial.AllPossibleMoves();
                while (queue.Count > 0)
                {
                    BFSNode node = queue.Dequeue();
                    if (node.H == 0)
                    {
                        this.numOfSteps = node.G;
                        stack = GetRoot(node);
                        break;
                    }
                    Queue<BFSNode> subQueue = node.AllPossibleMoves();
                    while (subQueue.Count > 0)
                    {
                        BFSNode n = subQueue.Dequeue();
                        queue.Enqueue(n);
                    }
                }
            }
            return stack;
        }
        public Stack<BFSNode> GetRoot(BFSNode node)
        {
            Stack<BFSNode> stack = new Stack<BFSNode>();
            GetRootHelper(stack, node);
            return stack;
        }
        private void GetRootHelper(Stack<BFSNode> stack, BFSNode node)
        {
            if (node.Parent != null)
            {
                stack.Push(node);
                GetRootHelper(stack, node.Parent);
            }
        }
    }
}
