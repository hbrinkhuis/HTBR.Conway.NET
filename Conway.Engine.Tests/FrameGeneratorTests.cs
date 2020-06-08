namespace Conway.Engine.Tests
{
    using FluentAssertions;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Diagnostics;

    [TestFixture]
    public class FrameGeneratorTests
    {
        private Matrix _matrix;
        private FrameGenerator _sut;

        [SetUp]
        public void Setup()
        {
            _matrix = new Matrix(1000, 1000); 
            _sut = new FrameGenerator(_matrix);
        }

        [Test]
        public void TestFrameGeneration()
        {
            InitGlider();
            
            _sut.ApplyNextFrame();

            _matrix.GetLivingCells().Should().BeEquivalentTo(new Cell(0, 1), new Cell(2, 1), new Cell(1, 2), new Cell(2, 2), new Cell(1, 3));
        }

        [Test]
        [Repeat(5)]
        public void PerformanceTest_EmptyFrame()
        {
            ApplyFramesAndLog(60);
            _matrix.IsEmpty().Should().BeTrue();
        }

        [Test]
        [Repeat(5)]
        public void PerformanceTest_Glider()
        {
            InitGlider();
            ApplyFramesAndLog(60);
            _matrix.GetLivingCells().Should().HaveCount(5);
            _matrix.GetLivingCells().Should().BeEquivalentTo(new List<Cell>
            {
                new Cell(16, 15),
                new Cell(17, 16),
                new Cell(0, 2),
                new Cell(1, 2),
                new Cell(2, 2)
            })
        }

        private void InitGlider()
        {
            // init matrix with glider
            _matrix.Init(new List<Cell>
            {
                new Cell(1, 0),
                new Cell(2, 1),
                new Cell(0, 2),
                new Cell(1, 2),
                new Cell(2, 2)
            });
        }

        private void ApplyFramesAndLog(int numberOfFrames)
        {
            var s = new Stopwatch();
            s.Start();
            for (int i = 0; i < numberOfFrames; i++)
            {
                _sut.ApplyNextFrame();
            }
            s.Stop();
            TestContext.Out.WriteLine($"Elapsed ms for {numberOfFrames} frames: {s.ElapsedMilliseconds}");
        }
    }
}