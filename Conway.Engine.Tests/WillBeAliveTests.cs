using NUnit.Framework;

namespace Conway.Engine.Tests
{
    using FluentAssertions;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class WillBeAliveTests
    {
        private Matrix _matrix;

        [SetUp]
        public void Setup()
        {
            _matrix = new Matrix();
        }

        private bool ReturnStatusOfCenterCell() => _matrix.WillBeAlive(1, 1);

        private void SetCenterCell()
        {
            _matrix.SetCell(1, 1);
        }

        private void SetCells(ICollection<(int x, int y)> cellTuples)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if(cellTuples.Any(t => t.x == i && t.y == j))
                        _matrix.SetCell(i, j);
                }
            }
        }


        [Test]
        public void DeadWithoutNeighbours_StillDead()
        {
            ReturnStatusOfCenterCell().Should().BeFalse();
        }

        [Test]
        public void DeadWithThreeNeighbours_Alive()
        {
            _matrix.SetCell(0, 0);
            _matrix.SetCell(0, 1);
            _matrix.SetCell(0, 2);

            ReturnStatusOfCenterCell().Should().BeTrue();
        }

        [TestCaseSource(typeof(NeighbourCases), nameof(NeighbourCases.TooLittleNeighbours))]
        public bool AliveWithTooFewNeighbours_Dies(ICollection<(int x, int y)> neighbours)
        {
            SetCells(neighbours);

            SetCenterCell();

            return ReturnStatusOfCenterCell();
        }


        [TestCaseSource(typeof(NeighbourCases), nameof(NeighbourCases.TooMuchNeighbours))]
        public bool AliveWithTooManyNeighbours_Dies(ICollection<(int x, int y)> neighbours)
        {
            SetCells(neighbours);

            SetCenterCell();

            return ReturnStatusOfCenterCell();
        }


        private class NeighbourCases
        {
            public static IEnumerable TooLittleNeighbours
            {
                get
                {
                    // TODO yeah let's refactor this
                    yield return new TestCaseData(new List<(int x, int y)> { }).Returns(false);
                    yield return new TestCaseData(new List<(int x, int y)> {(0, 1)}).Returns(false);
                    yield return new TestCaseData(new List<(int x, int y)> {(2, 0)}).Returns(false);
                }
            }

            public static IEnumerable TooMuchNeighbours
            {
                get
                {
                    // TODO yeah let's refactor this
                    yield return new TestCaseData(new List<(int x, int y)> { (0, 0), (1, 0), (1, 2), (2, 2) }).Returns(false);
                }
            }
        }
    }
}