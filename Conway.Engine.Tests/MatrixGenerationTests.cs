namespace Conway.Engine.Tests
{
    using AutoFixture;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Diagnostics;

    [TestFixture]
    public class MatrixGenerationTests
    {
        [Test]
        public void TestFrameGeneration()
        {
            var matrix = new Matrix(10,10);
            var sut = new FrameGenerator(matrix);

            // init matrix with glider
            matrix.Init(new []
            {
                (1, 0),
                (2, 1),
                (0, 2),
                (1, 2),
                (2, 2)
            });

            sut.ApplyNextFrame();

            matrix.GetLivingCells().Should().BeEquivalentTo(
                new[]
                {
                    (0, 1),
                    (2, 1),
                    (1, 2),
                    (2, 2),
                    (1, 3)
                });
        }

        [Test]
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
            TestContext.Out.WriteLine($"Elapsed ms for {60} frames: {s.ElapsedMilliseconds}");
        }
    }
}