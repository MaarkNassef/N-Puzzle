using System.Diagnostics;

namespace N_Puzzle
{
    public enum Algorithm
    {
        ASTAR,
        BFS
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            #region All Tests Paths
            List<string> SampleSolveableTests = new List<string>()
            {
                @"Testcases\Sample\Sample Test\Solvable Puzzles\8 Puzzle (1).txt",
                @"Testcases\Sample\Sample Test\Solvable Puzzles\8 Puzzle (2).txt",
                @"Testcases\Sample\Sample Test\Solvable Puzzles\8 Puzzle (3).txt",
                @"Testcases\Sample\Sample Test\Solvable Puzzles\15 Puzzle - 1.txt",
                @"Testcases\Sample\Sample Test\Solvable Puzzles\24 Puzzle 1.txt",
                @"Testcases\Sample\Sample Test\Solvable Puzzles\24 Puzzle 2.txt"
            };
            List<string> SampleUnsolveableTests = new List<string>()
            {
                @"Testcases\Sample\Sample Test\Unsolvable Puzzles\8 Puzzle - Case 1.txt",
                @"Testcases\Sample\Sample Test\Unsolvable Puzzles\8 Puzzle(2) - Case 1.txt",
                @"Testcases\Sample\Sample Test\Unsolvable Puzzles\8 Puzzle(3) - Case 1.txt",
                @"Testcases\Sample\Sample Test\Unsolvable Puzzles\15 Puzzle - Case 2.txt",
                @"Testcases\Sample\Sample Test\Unsolvable Puzzles\15 Puzzle - Case 3.txt"
            };
            List<string> CompleteSolvableManhattenAndHamming = new List<string>()
            {
                @"Testcases\Complete\Complete Test\Solvable puzzles\Manhattan & Hamming\50 Puzzle.txt",
                @"Testcases\Complete\Complete Test\Solvable puzzles\Manhattan & Hamming\99 Puzzle - 1.txt",
                @"Testcases\Complete\Complete Test\Solvable puzzles\Manhattan & Hamming\99 Puzzle - 2.txt",
                @"Testcases\Complete\Complete Test\Solvable puzzles\Manhattan & Hamming\9999 Puzzle.txt"

            };
            List<string> CompleteSolvableManhattenOnly = new List<string>()
            {
                @"Testcases\Complete\Complete Test\Solvable puzzles\Manhattan Only\15 Puzzle 1.txt",
                @"Testcases\Complete\Complete Test\Solvable puzzles\Manhattan Only\15 Puzzle 3.txt",
                @"Testcases\Complete\Complete Test\Solvable puzzles\Manhattan Only\15 Puzzle 4.txt",
                @"Testcases\Complete\Complete Test\Solvable puzzles\Manhattan Only\15 Puzzle 5.txt",
            };
            List<string> CompleteUnsolvable = new List<string>()
            {
                @"Testcases\Complete\Complete Test\Unsolvable puzzles\15 Puzzle 1 - Unsolvable.txt",
                @"Testcases\Complete\Complete Test\Unsolvable puzzles\99 Puzzle - Unsolvable Case 1.txt",
                @"Testcases\Complete\Complete Test\Unsolvable puzzles\99 Puzzle - Unsolvable Case 2.txt",
                @"Testcases\Complete\Complete Test\Unsolvable puzzles\9999 Puzzle.txt"
            };
            List<string> CompleteVeryLarge = new List<string>()
            {
                @"Testcases\Complete\Complete Test\V. Large test case\TEST.txt"
            };
            #endregion

            #region Run All test cases...
            while (true)
            {
                Console.WriteLine("N-Puzzle:\n[1] Sample test cases\n[2] Complete testing\n[3] Very Large Complete testing\n[4] BFS Sample\n[5] Exit");
                Console.WriteLine("\nEnter your choice [1-2-3-4-5]: ");
#pragma warning disable CS8604 // Possible null reference argument.
                int choice = int.Parse(Console.ReadLine());
#pragma warning restore CS8604 // Possible null reference argument.
                #region Exit
                if (choice == 5)
                {
                    break;
                }
                #endregion
                switch (choice)
                {
                    case 1:
                        #region Sample Tests
                        Console.WriteLine("\n===========================");
                        Console.WriteLine("||Sample Tests -Solvable-||");
                        Console.WriteLine("===========================");
                        Console.WriteLine("Choose the way to solve\n[1]Manhatten\n[2]Hamming");
#pragma warning disable CS8604 // Possible null reference argument.
                        int MorH = int.Parse(Console.ReadLine());
#pragma warning restore CS8604 // Possible null reference argument.
                        if (MorH == 1)
                        {
                            ReadAndTest(SampleSolveableTests, Type.MANHATTEN);
                        }
                        else if (MorH == 2)
                        {
                            ReadAndTest(SampleSolveableTests, Type.HAMMING);
                        }
                        Console.WriteLine("\n=============================");
                        Console.WriteLine("||Sample Tests -Unsolvable-||");
                        Console.WriteLine("=============================");
                        ReadAndTest(SampleUnsolveableTests);
                        break;
                    #endregion
                    case 2:
                        #region Complete Tests
                        Console.WriteLine("\n==========================================");
                        Console.WriteLine("||Complete Tests -Manhatten and Hamming-||");
                        Console.WriteLine("==========================================");
                        Console.WriteLine("Choose the way to solve\n[1]Manhatten\n[2]Hamming");
#pragma warning disable CS8604 // Possible null reference argument.
                        MorH = int.Parse(Console.ReadLine());
#pragma warning restore CS8604 // Possible null reference argument.
                        if (MorH == 1)
                        {
                            ReadAndTest(CompleteSolvableManhattenAndHamming, Type.MANHATTEN);
                        }
                        else if (MorH == 2)
                        {
                            ReadAndTest(CompleteSolvableManhattenAndHamming, Type.HAMMING);
                        }
                        Console.WriteLine("\n===================================");
                        Console.WriteLine("||Complete Tests -Manhatten only-||");
                        Console.WriteLine("===================================");
                        ReadAndTest(CompleteSolvableManhattenOnly, Type.MANHATTEN);
                        Console.WriteLine("\n===============================");
                        Console.WriteLine("||Complete Tests -Unsolvable-||");
                        Console.WriteLine("===============================");
                        ReadAndTest(CompleteUnsolvable);
                        break;
                    #endregion
                    case 3:
                        #region Very Large Test
                        Console.WriteLine("\n===============================");
                        Console.WriteLine("||Complete Tests -Very Large-||");
                        Console.WriteLine("===============================");
                        ReadAndTest(CompleteVeryLarge, Type.MANHATTEN);
                        break;
                    #endregion
                    case 4:
                        #region BFS Sample
                        Console.WriteLine("\n===================================");
                        Console.WriteLine("||BFS Tests -Sample Solvable Only-||");
                        Console.WriteLine("===================================");
                        List<string> BFSSample = new List<string>(SampleSolveableTests);
                        BFSSample.RemoveAt(BFSSample.Count - 1);
                        ReadAndTest(BFSSample, Type.MANHATTEN, Algorithm.BFS);
                        Console.WriteLine("\n======================================");
                        Console.WriteLine("||BFS Tests -Sample Unsolvable Only-||");
                        Console.WriteLine("======================================");
                        ReadAndTest(SampleUnsolveableTests, Type.MANHATTEN, Algorithm.BFS);
                        break;
                        #endregion
                }
                Console.WriteLine();
            }
            #endregion
        }
        public static void Print(int[,] input)
        {
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    if (input[i, j] == 0)
                    {
                        Console.Write("\t");
                        continue;
                    }
                    Console.Write(input[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
        public static void Solve(int[,] initial, Type t, bool PrintSteps)
        {
            Node node = new(initial, type: t);
            Puzzle puzzle = new(node);
            Stack<Node> nodes = puzzle.Solve();
            if (puzzle.isSolvable)
            {
                Console.WriteLine("We can solve it in " + puzzle.numOfSteps + " steps!");
            }
            else
            {
                Console.WriteLine("It is Unsolvable!");
            }
            if (PrintSteps)
            {
                string stepsStr = string.Empty;
                while (nodes.Count > 0)
                {
                    Node nextNode = nodes.Pop();
                    stepsStr += nextNode.PreferredStep;
                    //Print(nextNode.Matrix);
                    //Console.WriteLine("---------------------------------------------------");
                    //Console.WriteLine("---------------------------------------------------");
                }
                Console.WriteLine("The steps: " + stepsStr);
            }
        }
        public static void BFSSolve(int[,] initial, Type t, bool PrintSteps)
        {
            BFSNode node = new(initial, type: t);
            BFSPuzzle puzzle = new(node);
            Stack<BFSNode> nodes = puzzle.Solve();
            if (puzzle.isSolvable)
            {
                Console.WriteLine("We can solve it in " + puzzle.numOfSteps + " steps!");
            }
            else
            {
                Console.WriteLine("It is Unsolvable!");
            }
            if (PrintSteps)
            {
                string stepsStr = string.Empty;
                while (nodes.Count > 0)
                {
                    BFSNode nextNode = nodes.Pop();
                    stepsStr += nextNode.PreferredStep;
                    //Print(nextNode.Matrix);
                    //Console.WriteLine("---------------------------------------------------");
                    //Console.WriteLine("---------------------------------------------------");
                }
                Console.WriteLine("The steps: " + stepsStr);
            }
        }
        public static void ReadAndTest(List<string> paths, Type type = Type.HAMMING, Algorithm algorithm = Algorithm.ASTAR, bool PrintSteps = false)
        {
            foreach (string test in paths)
            {
                FileStream file = new FileStream(test, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(file);
#pragma warning disable CS8604 // Possible null reference argument.
                int grid = int.Parse(sr.ReadLine());
#pragma warning restore CS8604 // Possible null reference argument.
                sr.ReadLine();
                int[,] initial = new int[grid, grid];
                for (int i = 0; i < grid; i++)
                {
                    string? text = sr.ReadLine();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    string[] numbers = text.Split(" ");
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    for (int j = 0; j < grid; j++)
                    {
                        initial[i, j] = int.Parse(numbers[j]);
                    }
                }
                sr.Close();
                file.Close();
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                if (algorithm == Algorithm.ASTAR)
                {
                    Solve(initial, type, PrintSteps);
                }
                else if (algorithm == Algorithm.BFS)
                {
                    BFSSolve(initial, type, PrintSteps);
                }
                stopwatch.Stop();
                Console.WriteLine("Time Elapsed = " + stopwatch.Elapsed.TotalSeconds + " seconds, In Milliseconds = "+stopwatch.Elapsed.TotalMilliseconds+" ms!");
            }
        }
    }
}