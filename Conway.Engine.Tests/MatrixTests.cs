namespace Conway.Engine.Tests
{
    using FluentAssertions;
    using NUnit.Framework;
    using System.Collections.Generic;

    public class MatrixTests
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
            _matrix.AddCell(new Cell(0, 0));

            _matrix.IsEmpty().Should().BeFalse();
        }

        [Test]
        public void SetCell_CellSetCorrectly()
        {
            _matrix.AddCell(new Cell(0, 0));

            _matrix.GetLivingCells().Should().BeEquivalentTo(new Cell(0, 0));
        }

        [Test]
        public void Init_CellsSetCorrectly()
        {
            IEnumerable<Cell> initMatrix = new [] 
            {
                new Cell(1, 2),
                new Cell(1, 0),
                new Cell(25, 4),
                new Cell(-1, 1)
            };
            _matrix.Init(initMatrix);

            _matrix.GetLivingCells().Should().BeEquivalentTo(new Cell(1,2), new Cell(1,0));
        }

        [Test]
        public void Clone_IsAClone()
        {
            IEnumerable<Cell> initMatrix = new[]
            {
                new Cell(1, 2), 
                new Cell(1, 0), 
                new Cell(25, 4),
                new Cell(-1, 1)
            };
            _matrix.Init(initMatrix);

            var clone = _matrix.EmptyClone();
            clone.GetLivingCells().Should().BeEmpty();

            clone.AddCell(new Cell(1,1));
            _matrix.GetLivingCells().Should().BeEquivalentTo(new Cell(1, 2), new Cell(1, 0));
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
            _matrix.Init(new[]
            {
                new Cell(1, 0),
                new Cell(2, 1),
                new Cell(0, 2),
                new Cell(1, 2),
                new Cell(2, 2),
            });

            _matrix.GetLivingCells().Should().BeEquivalentTo(new Cell(1, 0), new Cell(2, 1), new Cell(0, 2), new Cell(1, 2), new Cell(2, 2));
        }
    }
}