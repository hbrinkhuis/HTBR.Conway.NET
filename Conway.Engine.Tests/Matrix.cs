namespace Conway.Engine.Tests
{
    public class Matrix
    {
        private const int MinimumNumberOfNeighboursToBecomeAlive = 9;
        private bool[,,] _matrix;

        public Matrix()
        {
            _matrix = new bool[3, 3, 3];
        }

        public bool WillBeAlive(int x, int y, int z)
        {
            return NumberOfNeighbours(x, y, z) >= MinimumNumberOfNeighboursToBecomeAlive;
        }

        private int NumberOfNeighbours(int x, int y, int z)
        {
            int numberOfNeighbours = 0;
            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    for (int k = z - 1; k < z + 2; k++)
                    {
                        if (_matrix[i, j, k])
                            numberOfNeighbours++;
                    }
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
                    for (int k = 0; k < _matrix.GetLength(2); k++)
                    {
                        if (_matrix[i, j, k])
                            return false;
                    }
                }
            }

            return true;
        }

        public void SetCell(int x, int y, int z)
        {
            _matrix[x, y, z] = true;
        }
    }
}