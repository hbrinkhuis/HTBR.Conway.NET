namespace Conway.Engine.Tests
{
    using FluentAssertions;
    using NUnit.Framework;
    using NUnit.Framework.Internal;
    using System.Linq;

    public class MatrixInitializationTests
    {
        private Matrix _matrix;

        [SetUp]
        public void Setup()
        {
            _matrix = new Matrix(3, 3);
        }

        [Test]
        public void NewMatrix_IsEmpty()
        {
            _matrix.IsEmpty().Should().BeTrue();
        }

        [Test]
        public void SetCell_NoLongerEmpty()
        {
            _matrix.SetCell(0, 0);

            _matrix.IsEmpty().Should().BeFalse();
        }

        [Test]
        public void SetCell_CellSetCorrectly()
        {
            _matrix.SetCell(0, 0);

            _matrix.IsAlive(0, 0).Should().BeTrue();
        }

        [Test]
        public void Init_CellsSetCorrectly()
        {
            var initMatrix = new[]
            {
                (1, 2),
                (1, 0),
                (25, 4),
                (-1, 1)
            };
            _matrix.Init(initMatrix);

            _matrix.IsAlive(1, 2).Should().BeTrue();
            _matrix.IsAlive(1, 0).Should().BeTrue();
        }

        [Test]
        public void IsWithinBounds()
        {
            _matrix.IsWithinBounds(-1, 0).Should().BeFalse();
            _matrix.IsWithinBounds(-1, -1).Should().BeFalse();
            _matrix.IsWithinBounds(2, -1).Should().BeFalse();
            _matrix.IsWithinBounds(-1, 2).Should().BeFalse();
            _matrix.IsWithinBounds(1, 2).Should().BeTrue();
            _matrix.IsWithinBounds(2, 1).Should().BeTrue();
            _matrix.IsWithinBounds(0, 0).Should().BeTrue();
            _matrix.IsWithinBounds(0, 3).Should().BeFalse();
            _matrix.IsWithinBounds(3, 3).Should().BeFalse();
            _matrix.IsWithinBounds(3, 0).Should().BeFalse();
        }


        [Test]
        public void GetAllLivingCells_ShouldReturnGlider()
        {
            // we'll use a glider for this
            _matrix = new Matrix(10, 10);

            _matrix.SetCell(1, 0);

            _matrix.SetCell(2, 1);

            _matrix.SetCell(0, 2);
            _matrix.SetCell(1, 2);
            _matrix.SetCell(2, 2);

            _matrix.GetCells().Where(c => c.alive).Should().BeEquivalentTo(new[]
            {
                (1, 0, true),
                (2, 1, true),
                (0, 2, true),
                (1, 2, true),
                (2, 2, true),
            });
        }
    }
}