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
        public void DeadWithoutNeighbors_StillDead()
        {
            ReturnStatusOfCenterCell().Should().BeFalse();
        }

        [Test]
        public void DeadWithThreeNeighbors_Alive()
        {
            _matrix.AddCell(new Cell(0, 0));
            _matrix.AddCell(new Cell(0, 1));
            _matrix.AddCell(new Cell(0, 2));

            ReturnStatusOfCenterCell().Should().BeTrue();
        }

        [TestCaseSource(typeof(NeighborCases), nameof(NeighborCases.TooFewNeighbors))]
        public bool AliveWithTooFewNeighbours_Dies(ICollection<Cell> neighbors)
        {
            SetCells(neighbors);

            _matrix.AddCell(_centerCell);

            return ReturnStatusOfCenterCell();
        }


        [TestCaseSource(typeof(NeighborCases), nameof(NeighborCases.TooMuchNeighbors))]
        public bool AliveWithTooManyNeighbors_Dies(ICollection<Cell> neighbors)
        {
            SetCells(neighbors);

            _matrix.AddCell(_centerCell);

            return ReturnStatusOfCenterCell();
        }


        private class NeighborCases
        {
            // returns 0 neighbors, and 1 neighbor (every possibility in a 3x3 matrix)
            public static IEnumerable TooFewNeighbors
            {
                get
                {
                    // 0 neighbors case
                    yield return new TestCaseData(new List<Cell>()).Returns(false).SetArgDisplayNames("Empty");

                    // 1 neighbor case
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            var cell = new Cell(i, j);
                            yield return new TestCaseData(new List<Cell> { cell }).Returns(false).SetArgDisplayNames(cell.X.ToString(), cell.Y.ToString());
                        }
                    }
                }
            }

            public static IEnumerable TooMuchNeighbors
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