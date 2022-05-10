namespace N_Puzzle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[,] initial = {{ 0, 1 ,2},
                              { 5 ,6 ,3},
                              { 4, 7 ,8} };
            Node node = new(initial);
            Print(node.Matrix);
            Console.WriteLine("=============================================");
            Console.WriteLine("Hamming = " + node.H);
            Console.WriteLine("=============================================");
            PriorityQueue<Node,int> queue = node.AllPossibleMoves();
            while (queue.Count>0)
            {
                Node n = queue.Dequeue();
                Print(n.Matrix);
                Console.WriteLine("=============================================");
                Console.WriteLine("Hamming = " + n.H);
                Console.WriteLine("=============================================");
            }
        }
        public static void Print(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i,j] == 0)
                    {
                        Console.Write("\t");
                        continue;
                    }
                    Console.Write(matrix[i,j]+"\t");
                }
                Console.WriteLine();
            }
        }
    }
}