namespace Conway.Engine.Tests
{
    using FluentAssertions;
    using NUnit.Framework;
    using NUnit.Framework.Internal;

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

            _matrix.GetCell(0, 0).Should().BeTrue();
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
    }
}