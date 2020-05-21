using NUnit.Framework;

namespace Conway.Engine.Tests
{
    using FluentAssertions;

    public class WillBeAliveTests
    {
        private Matrix _matrix;

        [SetUp]
        public void Setup()
        {
            _matrix = new Matrix();
        }

        private bool ReturnStatusOfCenterCell() => _matrix.WillBeAlive(1, 1, 1);

        private void SetCenterCell()
        {
            _matrix.SetCell(1, 1, 1);
        }

        [Test]
        public void DeadWithoutNeighbours_StillDead()
        {
            ReturnStatusOfCenterCell().Should().BeFalse();
        }

        [Test]
        public void DeadWithNineNeighbours_Alive()
        {
            _matrix.SetCell(0, 0, 0);
            _matrix.SetCell(0, 0, 1);
            _matrix.SetCell(0, 0, 2);
            _matrix.SetCell(1, 0, 0);
            _matrix.SetCell(1, 0, 1);
            _matrix.SetCell(1, 0, 2);
            _matrix.SetCell(2, 0, 0);
            _matrix.SetCell(2, 0, 1);
            _matrix.SetCell(2, 0, 2);

            ReturnStatusOfCenterCell().Should().BeTrue();
        }

        [Test]
        public void AliveWithNoNeighbours_Dies()
        {
            SetCenterCell();

            ReturnStatusOfCenterCell().Should().BeFalse();
        }
    }
}