namespace N_Puzzle
{
    public class BFSNode
    {
        public BFSNode? Parent;
        public int[,] Matrix;
        public int G;
        public int H;
        public Type type;
        public Tuple<int, int> ZeroPlace;
        public BFSNode(int[,] matrix, BFSNode? parent = null, int G = 0, Type type = Type.HAMMING)
        {
            this.Matrix = matrix;
            this.type = type;
            this.Parent = parent;
            this.ZeroPlace = new Tuple<int, int>(matrix.GetLength(0), matrix.GetLength(1)); //Garbage.
            this.G = G;
            if (type.Equals(Type.HAMMING))
            {
                this.H = findHamming();
            }
            else if (type.Equals(Type.MANHATTEN))
            {
                this.H = findManhatten();
            }
        }
        private int findManhatten()
        {
            H = 0;
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    if (Matrix[i, j] == 0)
                    {
                        this.ZeroPlace = new Tuple<int, int>(i, j);
                        continue;
                    }
                    Tuple<int, int> placeInGoal = findPlaceInGoal(Matrix[i, j]);
                    H += Math.Abs(i - placeInGoal.Item1) + Math.Abs(j - placeInGoal.Item2);
                }
            }
            return H;
        }
        private int findHamming()
        {
            H = 0;
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    if (Matrix[i, j] == 0)
                    {
                        this.ZeroPlace = new Tuple<int, int>(i, j);
                        continue;
                    }
                    Tuple<int, int> placeInGoal = findPlaceInGoal(Matrix[i, j]);
                    if (!placeInGoal.Equals(new Tuple<int, int>(i, j)))
                    {
                        H++;
                    }
                }
            }
            return H;
        }
        private Tuple<int, int> findPlaceInGoal(int num)
        {
            if (num == 0)
            {
                return new Tuple<int, int>(Matrix.GetLength(0) - 1, Matrix.GetLength(1) - 1);
            }
            else
            {
                return new Tuple<int, int>((num - 1) / Matrix.GetLength(1), (num - 1) % Matrix.GetLength(1));
            }
        }
        public Queue<BFSNode> AllPossibleMoves()
        {
            Queue<BFSNode> result = new Queue<BFSNode>();
            //Move Up
            BFSNode? move = MoveUp();
            if (move != null)
            {
                result.Enqueue(move);
            }
            //Move Down
            move = MoveDown();
            if (move != null)
            {
                result.Enqueue(move);
            }
            //Move Left
            move = MoveLeft();
            if (move != null)
            {
                result.Enqueue(move);
            }
            //Move Right
            move = MoveRight();
            if (move != null)
            {
                result.Enqueue(move);
            }
            return result;
        }
        public BFSNode? MoveUp()
        {
            Tuple<int, int> newPoint = new Tuple<int, int>(ZeroPlace.Item1 - 1, ZeroPlace.Item2);
            if (newPoint.Item1 >= 0 && (Parent == null || (Parent != null && !Parent.ZeroPlace.Equals(newPoint))))
            {
                int[,] change = new int[Matrix.GetLength(0), Matrix.GetLength(1)];
                for (int i = 0; i < Matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < Matrix.GetLength(1); j++)
                    {
                        change[i, j] = Matrix[i, j];
                    }
                }
                change[ZeroPlace.Item1, ZeroPlace.Item2] = change[newPoint.Item1, newPoint.Item2];
                change[newPoint.Item1, newPoint.Item2] = 0;
                return new BFSNode(change, this, G + 1, this.type);
            }
            return null;
        }
        public BFSNode? MoveDown()
        {
            Tuple<int, int> newPoint = new Tuple<int, int>(ZeroPlace.Item1 + 1, ZeroPlace.Item2);
            if (newPoint.Item1 < Matrix.GetLength(0) && (Parent == null || (Parent != null && !Parent.ZeroPlace.Equals(newPoint))))
            {
                int[,] change = new int[Matrix.GetLength(0), Matrix.GetLength(1)];
                for (int i = 0; i < Matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < Matrix.GetLength(1); j++)
                    {
                        change[i, j] = Matrix[i, j];
                    }
                }
                change[ZeroPlace.Item1, ZeroPlace.Item2] = change[newPoint.Item1, newPoint.Item2];
                change[newPoint.Item1, newPoint.Item2] = 0;
                return new BFSNode(change, this, G + 1, this.type);
            }
            return null;
        }
        public BFSNode? MoveLeft()
        {
            Tuple<int, int> newPoint = new Tuple<int, int>(ZeroPlace.Item1, ZeroPlace.Item2 - 1);
            if (newPoint.Item2 >= 0 && (Parent == null || (Parent != null && !Parent.ZeroPlace.Equals(newPoint))))
            {
                int[,] change = new int[Matrix.GetLength(0), Matrix.GetLength(1)];
                for (int i = 0; i < Matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < Matrix.GetLength(1); j++)
                    {
                        change[i, j] = Matrix[i, j];
                    }
                }
                change[ZeroPlace.Item1, ZeroPlace.Item2] = change[newPoint.Item1, newPoint.Item2];
                change[newPoint.Item1, newPoint.Item2] = 0;
                return new BFSNode(change, this, G + 1, this.type);
            }
            return null;
        }
        public BFSNode? MoveRight()
        {
            Tuple<int, int> newPoint = new Tuple<int, int>(ZeroPlace.Item1, ZeroPlace.Item2 + 1);
            if (newPoint.Item2 < Matrix.GetLength(1) && (Parent == null || (Parent != null && !Parent.ZeroPlace.Equals(newPoint))))
            {
                int[,] change = new int[Matrix.GetLength(0), Matrix.GetLength(1)];
                for (int i = 0; i < Matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < Matrix.GetLength(1); j++)
                    {
                        change[i, j] = Matrix[i, j];
                    }
                }
                change[ZeroPlace.Item1, ZeroPlace.Item2] = change[newPoint.Item1, newPoint.Item2];
                change[newPoint.Item1, newPoint.Item2] = 0;
                return new BFSNode(change, this, G + 1, this.type);
            }
            return null;
        }
    }
}
