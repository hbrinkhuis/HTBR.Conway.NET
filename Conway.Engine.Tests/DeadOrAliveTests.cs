namespace Conway.Engine.Tests
{
    using NUnit.Framework;
    using FluentAssertions;
    using System.Collections;
    using System.Collections.Generic;

    public class DeadOrAliveTests
    {
        private Matrix _matrix;
        private FrameGenerator _frameGen;
        private Cell _centerCell;

        [SetUp]
        public void Setup()
        {
            _matrix = new Matrix(3, 3);
            _frameGen = new FrameGenerator(_matrix);
            _centerCell = new Cell(1, 1);
        }

        private bool ReturnStatusOfCenterCell() => _frameGen.WillStayAlive(_centerCell);

        private void SetCells(IEnumerable<Cell> cells)
        {
            foreach(var cell in cells)
                _matrix.AddCell(cell);
        }

        [Test]
        public void DeadWithoutNeighbours_StillDead()
        {
            ReturnStatusOfCenterCell().Should().BeFalse();
        }

        [Test]
        public void DeadWithThreeNeighbours_Alive()
        {
            _matrix.AddCell(new Cell(0, 0));
            _matrix.AddCell(new Cell(0, 1));
            _matrix.AddCell(new Cell(0, 2));

            ReturnStatusOfCenterCell().Should().BeTrue();
        }

        [TestCaseSource(typeof(NeighbourCases), nameof(NeighbourCases.TooLittleNeighbours))]
        public bool AliveWithTooFewNeighbours_Dies(ICollection<Cell> neighbours)
        {
            SetCells(neighbours);

            _matrix.AddCell(_centerCell);

            return ReturnStatusOfCenterCell();
        }


        [TestCaseSource(typeof(NeighbourCases), nameof(NeighbourCases.TooMuchNeighbours))]
        public bool AliveWithTooManyNeighbours_Dies(ICollection<Cell> neighbours)
        {
            SetCells(neighbours);

            _matrix.AddCell(_centerCell);

            return ReturnStatusOfCenterCell();
        }


        private class NeighbourCases
        {
            public static IEnumerable TooLittleNeighbours
            {
                get
                {
                    // TODO yeah let's refactor this
                    yield return new TestCaseData(new List<Cell>()).Returns(false);
                    yield return new TestCaseData(new List<Cell> {new Cell(0, 1)}).Returns(false);
                    yield return new TestCaseData(new List<Cell> {new Cell(2, 0)}).Returns(false);
                }
            }

            public static IEnumerable TooMuchNeighbours
            {
                get
                {
                    // TODO yeah let's refactor this
                    yield return new TestCaseData(new List<Cell> { new Cell(0, 0), new Cell(1, 0), new Cell(1, 2), new Cell(2, 2)}).Returns(false);
                }
            }
        }
    }
}