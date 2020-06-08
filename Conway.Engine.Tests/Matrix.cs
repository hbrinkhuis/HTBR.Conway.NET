namespace Conway.Engine.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    public class Matrix
    {
        private readonly ICollection<Cell> _cells;

        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            int size = Rows * Columns;
            _cells = new List<Cell>(size);
        }

        public int Rows { get; } // X direction

        public int Columns { get; } // Y direction

        public void Init(IEnumerable<Cell> initMatrix)
        {
            _cells.Clear();
            foreach (var cell in initMatrix)
            {
                AddCell(cell);
            }
        }

        public void AddCell(Cell cell)
        {
            if (IsInMatrix(cell.X, cell.Y) && ContainsCell(cell) == false)
            {
                _cells.Add(cell);
            }
        }

        public void AddCells(IEnumerable<Cell> cells)
        {
            foreach (var cell in cells)
            {
                AddCell(cell);
            }
        }

        public IEnumerable<Cell> GetLivingCells()
        {
            return _cells;
        }

        public IEnumerable<Cell> GetNeighbours(Cell cell)
        {
            int minX = cell.X - 1;
            int maxX = cell.X + 1;
            int minY = cell.Y - 1;
            int maxY = cell.Y + 1;
            return _cells.Where(c => c != cell
                                     && c.X >= minX && c.X <= maxX
                                     && c.Y >= minY && c.Y <= maxY);
        }

        public bool IsEmpty()
        {
            return _cells.Any() == false;
        }

        public bool IsInMatrix(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Rows && y < Columns;
        }

        public Matrix EmptyClone()
        {
            return new Matrix(Rows, Columns);
        }

        private bool ContainsCell(Cell cell)
        {
            return _cells.Any(c => c.X == cell.X && c.Y == cell.Y);
        }
    }
}