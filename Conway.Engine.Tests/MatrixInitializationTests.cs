namespace Conway.Engine.Tests
{
    using FluentAssertions;
    using NUnit.Framework;
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
            (int, int)[] initMatrix = new[]
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
        public void Clone_IsAClone()
        {
            (int, int)[] initMatrix = new[]
            {
                (1, 2),
                (1, 0),
                (25, 4),
                (-1, 1)
            };
            _matrix.Init(initMatrix);

            var clone = _matrix.EmptyClone();
            clone.GetLivingCells().Should().BeEmpty();

            clone.SetCell(1,1);
            _matrix.IsAlive(1, 1).Should().BeFalse();
        }

        [Test]
        public void IsWithinBounds()
        {
            _matrix.IsInMatrix(-1, 0).Should().BeFalse();
            _matrix.IsInMatrix(-1, -1).Should().BeFalse();
            _matrix.IsInMatrix(2, -1).Should().BeFalse();
            _matrix.IsInMatrix(-1, 2).Should().BeFalse();
            _matrix.IsInMatrix(1, 2).Should().BeTrue();
            _matrix.IsInMatrix(2, 1).Should().BeTrue();
            _matrix.IsInMatrix(0, 0).Should().BeTrue();
            _matrix.IsInMatrix(0, 3).Should().BeFalse();
            _matrix.IsInMatrix(3, 3).Should().BeFalse();
            _matrix.IsInMatrix(3, 0).Should().BeFalse();
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

            _matrix.GetLivingCells().Should().BeEquivalentTo(new[]
            {
                (1, 0),
                (2, 1),
                (0, 2),
                (1, 2),
                (2, 2),
            });
        }
    }
}