namespace Conway.Engine.Tests
{
    using FluentAssertions;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Diagnostics;

    [TestFixture]
    public class FrameGeneratorTests
    {
        [Test]
        public void TestFrameGeneration()
        {
            var matrix = new Matrix(10,10);
            var sut = new FrameGenerator(matrix);

            // init matrix with glider
            matrix.Init(new List<Cell>
            {
                new Cell(1, 0),
                new Cell(2, 1),
                new Cell(0, 2),
                new Cell(1, 2),
                new Cell(2, 2)
            });

            sut.ApplyNextFrame();

            matrix.GetLivingCells().Should().BeEquivalentTo(new Cell(0, 1), new Cell(2, 1), new Cell(1, 2), new Cell(2, 2), new Cell(1, 3));
        }

        [Test]
        [Repeat(5)]
        public void PerformanceTest_EmptyFrame()
        {
            var matrix = new Matrix(1000, 1000);

            var sut = new FrameGenerator(matrix);
            Stopwatch s = new Stopwatch();
            s.Start();
            for (int i = 0; i < 60; i++)
            {
                sut.ApplyNextFrame();
            }
            s.Stop();
            TestContext.Out.WriteLine($"Elapsed ms for {60} empty frames: {s.ElapsedMilliseconds}");
        }
    }
}