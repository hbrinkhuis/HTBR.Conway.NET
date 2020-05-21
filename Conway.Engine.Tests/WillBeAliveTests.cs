using NUnit.Framework;

namespace Conway.Engine.Tests
{
    using FluentAssertions;
    using System.Linq;

    public class WillBeAliveTests
    {
        private bool[,,] _matrix;

        private const int MinimumNumberOfNeighboursToBecomeAlive = 9;

        [SetUp]
        public void Setup()
        {
            _matrix = new bool[3, 3, 3];
        }

        private bool ReturnStatusOfCenterCell() => WillBeAlive(1, 1, 1, _matrix);

        [Test]
        public void DeadWithoutNeighbours_StillDead()
        {
            ReturnStatusOfCenterCell().Should().BeFalse();
        }

        [Test]
        public void DeadWithNineNeighbourse_Alive()
        {
            _matrix[0, 0, 0] = true;
            _matrix[0, 0, 1] = true;
            _matrix[0, 0, 2] = true;
            _matrix[1, 0, 0] = true;
            _matrix[1, 0, 1] = true;
            _matrix[1, 0, 2] = true;
            _matrix[2, 0, 0] = true;
            _matrix[2, 0, 1] = true;
            _matrix[2, 0, 2] = true;

            ReturnStatusOfCenterCell().Should().BeTrue();
        }

        private bool WillBeAlive(int x, int y, int z, bool[,,] matrix)
        {
            return NumberOfNeighbours(x, y, z, _matrix) >= MinimumNumberOfNeighboursToBecomeAlive;
        }

        private int NumberOfNeighbours(int x, int y, int z, bool[,,] matrix)
        {
            int numberOfNeighbours = 0;
            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    for (int k = z - 1; k < z + 2; k++)
                    {
                        if (matrix[i, j, k])
                            numberOfNeighbours++;
                    }
                }
            }

            return numberOfNeighbours;
        }
    }
}