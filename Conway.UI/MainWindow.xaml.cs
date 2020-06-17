namespace Conway.UI
{
    using System.ComponentModel;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public partial class MainWindow
    {
        public MainWindow()
        {
            var vm = new MainWindowViewModel();
            DataContext = vm;
            InitializeComponent();
            DrawGrid(vm);
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var vm = (DataContext as MainWindowViewModel);

            var rect = new Rectangle
            {
                Stroke = Brushes.Yellow,
                StrokeThickness = 1,
                Fill = Brushes.Yellow,
                Width = vm.SquareSize,
                Height = vm.SquareSize
            };
            Canvas.SetLeft(rect, 0);
            Canvas.SetTop(rect, 0);

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
                Canvas.SetLeft(line, 0);
                Canvas.SetTop(line, 0);

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
                Canvas.SetLeft(line, 0);
                Canvas.SetTop(line, 0);

                ConwayCanvas.Children.Add(line);
            }
        }
    }
}
