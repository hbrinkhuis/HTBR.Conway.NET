namespace Conway.UI
{
    using Engine;

    public class MainWindowViewModel
    {
        private Matrix _matrix;

        public MainWindowViewModel()
        {
            _matrix = new Matrix(Rows, Columns);
        }

        public int Rows { get; } = 1000;

        public int Columns { get; } = 1000;
    }
}
