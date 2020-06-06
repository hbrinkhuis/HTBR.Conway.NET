namespace Conway.Engine.Tests
{
    using System.Collections.Generic;
 
    public class Matrix
    {
        private const int NumberOfNeighboursToBecomeAlive = 3;
        private const int TooLittleNeighboursToStayAlive = 1;
        private const int TooManyNeighboursToStayAlive = 4;
        private bool[,] _matrix;

        public Matrix(int rows, int columns)
        {
            _matrix = new bool[rows, columns];
        }

        public void Init(IEnumerable<(int x, int y)> initMatrix)
        {
            foreach ((int x, int y) in initMatrix)
            {
                if (IsInMatrix(x, y))
                    SetCell(x, y);
            }
        }

        public void SetCell(int x, int y)
        {
            _matrix[x, y] = true;
        }

        public IEnumerable<(int x, int y, bool alive)> GetCells()
        {
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    bool isAlive = IsAlive(i, j);
                    yield return (i, j, isAlive);
                }
            }
        }

        public IEnumerable<(int x, int y)> GetLivingCells()
        {
            foreach (var cell in GetCells())
            {
                if (cell.alive)
                    yield return (cell.x, cell.y);
            };
        }

        public bool WillBeAlive(int x, int y)
        {
            int numberOfLivingNeighbours = NumberOfLivingNeighbours(x, y);
            if (IsAlive(x, y))
            {
                return numberOfLivingNeighbours > TooLittleNeighboursToStayAlive && numberOfLivingNeighbours < TooManyNeighboursToStayAlive;
            }

            return numberOfLivingNeighbours == NumberOfNeighboursToBecomeAlive;
        }

        public bool IsAlive(int x, int y)
        {
            return IsInMatrix(x, y) && _matrix[x, y];
        }

        public bool IsEmpty()
        {

            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    if (IsInMatrix(i,j) && _matrix[i, j])
                            return false;
                }
            }

            return true;
        }

        public bool IsInMatrix(int x, int y)
        {
            return x >= 0 && y >= 0 && x < _matrix.GetLength(0) && y < _matrix.GetLength(1);
        }

        private int NumberOfLivingNeighbours(int x, int y)
        {
            int numberOfNeighbours = 0;
            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    if (IsInMatrix(i, j) && _matrix[i, j] && !(i == x && j == y))
                        numberOfNeighbours++;
                }
            }

            return numberOfNeighbours;
        }

        public Matrix Clone()
        {
            var clone = new Matrix(_matrix.GetLength(0), _matrix.GetLength(1));
            clone.Init(GetLivingCells());

            return clone;
        }
    }
}