namespace Conway.Engine.Tests
{
    using FluentAssertions;
    using NUnit.Framework;
    using System.Linq;

    [TestFixture]
    public class MatrixGenerationTests
    {
        [Test]
        public void TestFrameGeneration()
        {
            var matrix = new Matrix(10,10);
            var sut = new FrameGenerator(matrix);

            // init matrix with glider
            matrix.Init(new []
            {
                (1, 0),
                (2, 1),
                (0, 2),
                (1, 2),
                (2, 2)
            });

            sut.ApplyNextFrame();

            matrix.GetLivingCells().Should().BeEquivalentTo(
                new[]
                {
                    (0, 1),
                    (2, 1),
                    (1, 2),
                    (2, 2),
                    (1, 3)
                });
        }
    }

    public class FrameGenerator
    {
        private readonly Matrix _matrix;

        public FrameGenerator(Matrix matrix)
        {
            _matrix = matrix;
        }

        public void ApplyNextFrame()
        {
            foreach (var cell in _matrix.GetCells())
            {
                _matrix.WillBeAlive(cell.x, cell.y);
            }
        }
    }
}