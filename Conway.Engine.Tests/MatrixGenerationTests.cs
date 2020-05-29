namespace Conway.Engine.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class MatrixGenerationTests
    {
        [Test]
        public void GetAllLivingCells_ShouldReturnGlider()
        {
            // we'll use a glider for this
            var sut = new Matrix(10, 10);

            sut.SetCell(1,0);

            sut.SetCell(2,1);
            
            sut.SetCell(0,2);
            sut.SetCell(1,2);
            sut.SetCell(2,2);
        }
    }
}