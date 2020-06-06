namespace Conway.Engine.Tests
{
    using System.Collections.Generic;
 
    public class Matrix
    {
        private bool[,] _matrix;

        public Matrix(int rows, int columns)
        {
            _matrix = new bool[rows, columns];
        }

        public void Init(IEnumerable<(int x, int y)> initMatrix)
        {
            Clear();
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
            }
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

        private void Clear()
        {
            _matrix = new bool[_matrix.GetLength(0), _matrix.GetLength(1)];
        }

        public Matrix EmptyClone()
        {
            return new Matrix(_matrix.GetLength(0), _matrix.GetLength(1));
        }
    }
}