namespace Conway.Engine.Tests
{
    public class FrameGenerator
    {
        private readonly Matrix _matrix;
        private const int NumberOfNeighboursToBecomeAlive = 3;
        private const int TooLittleNeighboursToStayAlive = 1;
        private const int TooManyNeighboursToStayAlive = 4;

        public FrameGenerator(Matrix matrix)
        {
            _matrix = matrix;
        }

        public void ApplyNextFrame()
        {
            var nextFrame = _matrix.EmptyClone();

            // check living cells
            foreach ((int x, int y, bool _) in _matrix.GetCells())
            {
                if(WillBeAlive(x, y))
                    nextFrame.SetCell(x, y);
            }
            _matrix.Init(nextFrame.GetLivingCells());
        }

        public bool WillBeAlive(int x, int y)
        {
            int numberOfLivingNeighbours = NumberOfLivingNeighbours(x, y);
            if (_matrix.IsAlive(x, y))
            {
                return numberOfLivingNeighbours > TooLittleNeighboursToStayAlive && numberOfLivingNeighbours < TooManyNeighboursToStayAlive;
            }

            return numberOfLivingNeighbours == NumberOfNeighboursToBecomeAlive;
        }

        private int NumberOfLivingNeighbours(int x, int y)
        {
            int numberOfNeighbours = 0;
            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    if (_matrix.IsInMatrix(i, j) && _matrix.IsAlive(i, j) && !(i == x && j == y))
                        numberOfNeighbours++;
                }
            }

            return numberOfNeighbours;
        }
    }
}