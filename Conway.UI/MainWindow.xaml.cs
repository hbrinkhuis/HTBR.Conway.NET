namespace Conway.UI
{
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public partial class MainWindow
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel();
            InitializeComponent();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var vm = (DataContext as MainWindowViewModel);

            var rect = new Rectangle();
            rect.Stroke = new SolidColorBrush(Colors.Black);
            rect.StrokeThickness = 1;
            rect.Fill = new SolidColorBrush(Colors.Black);
            rect.Width = 200;
            rect.Height = 200;
            Canvas.SetLeft(rect, 0);
            Canvas.SetTop(rect, 0);

            ConwayCanvas.Children.Add(rect);
        }
    }
}
