namespace Conway.Engine
{
    using System.Linq;

    public class FrameGenerator
    {
        private readonly Matrix _matrix;
        private const int TooLittleNeighborsToStayAlive = 1;
        private const int TooManyNeighborsToStayAlive = 4;

        public FrameGenerator(Matrix matrix)
        {
            _matrix = matrix;
        }

        public void ApplyNextFrame()
        {
            var nextFrame = _matrix.EmptyClone();
            var fertilityMatrix = new FertilityMatrix(_matrix);

            foreach (var cell in _matrix.GetLivingCells())
            {
                if(WillStayAlive(cell))
                    nextFrame.AddCell(cell);
            }

            nextFrame.AddCells(fertilityMatrix.GetSprouts());

            _matrix.Init(nextFrame.GetLivingCells());
        }

        public bool WillStayAlive(Cell cell)
        {
            int numberOfLivingNeighbors = _matrix.GetNeighbors(cell).Count();
            return numberOfLivingNeighbors > TooLittleNeighborsToStayAlive && numberOfLivingNeighbors < TooManyNeighborsToStayAlive;
        }
    }
}