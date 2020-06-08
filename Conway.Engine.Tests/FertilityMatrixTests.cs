namespace Conway.Engine.Tests
{
    using FluentAssertions;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class FertilityMatrixTests
    {
        private Matrix _matrix;
        private FertilityMatrix _sut;

        [SetUp]
        public void Setup()
        {
            _matrix = new Matrix(3, 3);
        }

        [Test]
        public void TestThreeNeighbours()
        {
            _matrix.AddCell(new Cell(0, 0));
            _matrix.AddCell(new Cell(0, 2));
            _matrix.AddCell(new Cell(2, 2));

            _sut = new FertilityMatrix(_matrix);


            IEnumerable<Cell> sprouts = _sut.GetSprouts();
            sprouts.Should().HaveCount(1);
        }
    }
}
