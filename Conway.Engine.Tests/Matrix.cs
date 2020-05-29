namespace Conway.Engine.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;

    public class Matrix
    {
        private const int MinimumNumberOfNeighboursToBecomeAlive = 3;
        private const int TooLittleNeighboursToStayAlive = 1;
        private const int TooManyNeighboursToStayAlive = 4;
        private bool[,] _matrix;

        public Matrix(uint rows, uint columns)
        {
            _matrix = new bool[rows, columns];
        }

        public bool WillBeAlive(int x, int y)
        {
            int numberOfNeighbours = NumberOfNeighbours(x, y);
            if (IsAlive(x, y))
            {
                return numberOfNeighbours > TooLittleNeighboursToStayAlive && numberOfNeighbours < TooManyNeighboursToStayAlive;
            }

            return numberOfNeighbours >= MinimumNumberOfNeighboursToBecomeAlive;
        }

        public bool IsAlive(int x, int y)
        {
            return _matrix[x, y];
        }

        private int NumberOfNeighbours(int x, int y)
        {
            int numberOfNeighbours = 0;
            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    if (_matrix[i, j] && !(i == x && j == y))
                        numberOfNeighbours++;
                }
            }

            return numberOfNeighbours;
        }

        public bool IsEmpty()
        {

            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    if (_matrix[i, j])
                            return false;
                }
            }

            return true;
        }

        public void SetCell(int x, int y)
        {
            _matrix[x, y] = true;
        }

        public bool GetCell(int x, int y)
        {
            return _matrix[x, y];
        }

        public void Init(IEnumerable<(int x, int y)> initMatrix)
        {
            foreach ((int x, int y) in initMatrix)
            {
                if(IsWithinBounds(x, y))
                    SetCell(x, y);
            }
        }

        public bool IsWithinBounds(int x, int y)
        {
            return x >= 0 && y >= 0 && x < _matrix.GetLength(0) && y < _matrix.GetLength(1);
        }
    }
}