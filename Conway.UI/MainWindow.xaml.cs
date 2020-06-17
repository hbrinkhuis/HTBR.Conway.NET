namespace Conway.UI
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using System.Windows.Threading;

    public partial class MainWindow
    {
        private readonly ICollection<Rectangle> _cells = new List<Rectangle>();
        private readonly MainWindowViewModel _vm;

        public MainWindow()
        {
            _vm = new MainWindowViewModel();
            DataContext = _vm;
            InitializeComponent();
            DrawGrid(_vm);
            _vm.FrameChanged += FrameChanged;
        }

        private void FrameChanged(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                foreach (var cell in _cells)
                    ConwayCanvas.Children.Remove(cell);

                _cells.Clear();

                foreach (var cell in _vm.Cells)
                {
                    int x = cell.X * _vm.SquareSize;
                    int y = cell.Y * _vm.SquareSize;

                    DrawRectangle(x, y);
                }
            });
        }

        private void Canvas_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var p = e.GetPosition(ConwayCanvas);

            int x = (int) p.X / _vm.SquareSize * _vm.SquareSize;
            int y = (int) p.Y / _vm.SquareSize * _vm.SquareSize;

            DrawRectangle(x, y);

            _vm.ToggleCellCommand.Execute(p);
        }

        private void DrawRectangle(int x, int y)
        {
            var rect = new Rectangle
            {
                Stroke = Brushes.Yellow,
                StrokeThickness = 1,
                Fill = Brushes.Yellow,
                Width = _vm.SquareSize,
                Height = _vm.SquareSize
            };
            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);

            _cells.Add(rect);

            ConwayCanvas.Children.Add(rect);
        }

        private void DrawGrid(MainWindowViewModel vm)
        {
            for (int i = 0; i < vm.Rows; i++)
            {
                int y = i * vm.SquareSize;
                var line = new Line
                {
                    
                    StrokeThickness = 1,
                    Stroke = Brushes.Black,
                    X1 = 0,
                    Y1 = y,
                    X2 = vm.Columns * vm.SquareSize,
                    Y2 = y,
                };

                ConwayCanvas.Children.Add(line);
            }
            for (int j = 0; j < vm.Columns; j++)
            {
                int x = j * vm.SquareSize;
                var line = new Line
                {

                    StrokeThickness = 1,
                    Stroke = Brushes.Black,
                    X1 = x,
                    Y1 = 0,
                    X2 = x,
                    Y2 = vm.Rows * vm.SquareSize,
                };

                ConwayCanvas.Children.Add(line);
            }
        }

    }
}
