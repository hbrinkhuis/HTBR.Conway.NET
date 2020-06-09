namespace Conway.Engine
{
    using System;
    using System.Collections.Generic;

    public class FertilityMatrix
    {
        private const int NumberOfNeighboursToBecomeAlive = 3;
        private int[,] _fertility;

        public FertilityMatrix(Matrix matrix)
        {
            _fertility = new int[matrix.Rows, matrix.Columns];
            ApplyFertilityIndex(matrix.GetLivingCells());
        }

        private void ApplyFertilityIndex(IEnumerable<Cell> cells)
        {
            foreach (var cell in cells)
            {
                int iStart = Math.Max(0, cell.X - 1);
                int jStart = Math.Max(0, cell.Y - 1);
                int iEnd = Math.Min(cell.X + 1, _fertility.GetLength(0) - 1);
                int jEnd = Math.Min(cell.Y + 1, _fertility.GetLength(1) - 1);

                for (int i = iStart; i <= iEnd; i++)
                {
                    for (int j = jStart; j <= jEnd; j++)
                    {
                        if (i == cell.X && j == cell.Y) // set cell itself to max value
                            _fertility[cell.X, cell.Y] = int.MaxValue;

                        if (_fertility[i, j] == int.MaxValue) // a living cell can't be dead but fertile ground
                            continue;

                        _fertility[i, j]++; // raise fertility index
                    }
                }
            }
        }

        public IEnumerable<Cell> GetSprouts()
        {
            for (int i = 0; i < _fertility.GetLength(0); i++)
            {
                for (int j = 0; j < _fertility.GetLength(1); j++)
                {
                    if(_fertility[i,j] == NumberOfNeighboursToBecomeAlive)
                        yield return new Cell(i,j);
                }
            }
        }
    }
}