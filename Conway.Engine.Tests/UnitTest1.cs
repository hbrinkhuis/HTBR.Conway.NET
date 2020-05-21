using NUnit.Framework;

namespace Conway.Engine.Tests
{
    using FluentAssertions;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DeadWithoutNeighbours_StillDead()
        {
            var matrix = new bool[3, 3, 3];

            var result = WillBeAlive(1, 1, 1, matrix);

            result.Should().BeFalse();
        }

        private bool WillBeAlive(int x, int y, int z, bool[,,] matrix)
        {
            return false;
        }
    }
}