namespace Conway.Engine.Tests
{
    using FluentAssertions;
    using NUnit.Framework;

    public class MatrixInitializationTests
    {
        private Matrix _matrix;

        [SetUp]
        public void Setup()
        {
            _matrix = new Matrix();
        }

        [Test]
        public void NewMatrix_IsEmpty()
        {
            _matrix.IsEmpty().Should().BeTrue();
        }

        [Test]
        public void SetCell_NoLongerEmpty()
        {
            _matrix.SetCell(0, 0, 0);

            _matrix.IsEmpty().Should().BeFalse();
        }
    }
}