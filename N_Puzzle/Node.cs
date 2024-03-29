﻿namespace N_Puzzle
{
    public enum Type
    {
        HAMMING,
        MANHATTEN
    }
    public class Node
    {
        public Node? Parent;
        public int[,] Matrix;
        public int G;
        public int H;
        public int F;
        public Type type;
        public Tuple<int, int> ZeroPlace;
        public char? PreferredStep;
        public Node(int[,] matrix, Node? parent = null, int G = 0, char PreferredStep = '\0', Type type = Type.HAMMING)
        {
            this.Matrix = matrix;
            this.type = type;
            this.Parent = parent;
            this.ZeroPlace = new Tuple<int, int>(matrix.GetLength(0), matrix.GetLength(1)); //Garbage.
            this.PreferredStep = PreferredStep;
            this.G = G;
            if (type.Equals(Type.HAMMING))
            {
                this.H = findHamming();
            }
            else if (type.Equals(Type.MANHATTEN))
            {
                this.H = findManhatten();
            }
            this.F = G + H;
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
        public PriorityQueue<Node, int> AllPossibleMoves()
        {
            PriorityQueue<Node, int> result = new PriorityQueue<Node, int>();
            //Move Up
            Node? move = MoveUp();
            if (move != null)
            {
                result.Enqueue(move, move.F);
            }
            //Move Down
            move = MoveDown();
            if (move != null)
            {
                result.Enqueue(move, move.F);
            }
            //Move Left
            move = MoveLeft();
            if (move != null)
            {
                result.Enqueue(move, move.F);
            }
            //Move Right
            move = MoveRight();
            if (move != null)
            {
                result.Enqueue(move, move.F);
            }
            return result;
        }
        public Node? MoveUp()
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
                return new Node(change, this, G + 1, 'U', this.type);
            }
            return null;
        }
        public Node? MoveDown()
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
                return new Node(change, this, G + 1, 'D', this.type);
            }
            return null;
        }
        public Node? MoveLeft()
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
                return new Node(change, this, G + 1, 'L', this.type);
            }
            return null;
        }
        public Node? MoveRight()
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
                return new Node(change, this, G + 1, 'R', this.type);
            }
            return null;
        }
    }
}
