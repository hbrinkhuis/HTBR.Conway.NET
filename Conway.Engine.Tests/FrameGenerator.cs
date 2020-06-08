namespace Conway.Engine.Tests
{
    using System.Linq;

    public class FrameGenerator
    {
        private readonly Matrix _matrix;
        private const int TooLittleNeighboursToStayAlive = 1;
        private const int TooManyNeighboursToStayAlive = 4;

        public FrameGenerator(Matrix matrix)
        {
            _matrix = matrix;
        }

        public void ApplyNextFrame()
        {
            var nextFrame = _matrix.EmptyClone();

            foreach (var cell in _matrix.GetLivingCells())
            {
                if(WillStayAlive(cell))
                    nextFrame.AddCell(cell);
            }

            // check areas around living cells with fertility matrix
            var fertilityMatrix = new FertilityMatrix(_matrix);
            foreach (var sprout in fertilityMatrix.GetSprouts())
            {
                nextFrame.AddCell(sprout);
            }

            _matrix.Init(nextFrame.GetLivingCells());
        }

        public bool WillStayAlive(Cell cell)
        {
            int numberOfLivingNeighbours = _matrix.GetNeighbours(cell).Count();
            return numberOfLivingNeighbours > TooLittleNeighboursToStayAlive && numberOfLivingNeighbours < TooManyNeighboursToStayAlive;
        }
    }
}