﻿namespace Conway.UI
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel();
            InitializeComponent();
        }
    }
}
