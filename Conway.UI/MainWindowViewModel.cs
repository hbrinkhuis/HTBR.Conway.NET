namespace Conway.UI
{
    using Engine;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;

    public class MainWindowViewModel : ViewModelBase
    {
        private readonly Matrix _matrix;
        private readonly FrameGenerator _frameGenerator;

        public MainWindowViewModel()
        {
            _matrix = new Matrix(Rows, Columns);
            _frameGenerator = new FrameGenerator(_matrix);
        }

        /// <summary>
        /// Occurs when the frame has changed, prompting a redraw
        /// </summary>
        public EventHandler FrameChanged;

        private CancellationTokenSource _taskCancellation;
        private int _generation;

        public RelayCommand<Point> ToggleCellCommand => new RelayCommand<Point>(ToggleCell);

        public RelayCommand StartLifeCommand => new RelayCommand(StartLife);

        public RelayCommand ResetCommand => new RelayCommand(ResetMatrix);

        public int Rows { get; } = 1000;

        public int Columns { get; } = 1000;

        public int SquareSize { get; } = 25;

        public int Generation
        {
            get => _generation;
            private set
            {
                _generation = value;
                RaisePropertyChanged(nameof(Generation));
            }
        }

        public int NumberOfCells => _matrix.GetLivingCells().Count();

        public ICollection<Cell> Cells => _matrix.GetLivingCells().ToList();

        private Cell ConvertToCell(Point p)
        {
            int x = (int)p.X / SquareSize;
            int y = (int)p.Y / SquareSize;
            return new Cell(x, y);
        }

        private void ToggleCell(Point p)
        {
            var cell = ConvertToCell(p);
            if(_matrix.ContainsCell(cell))
                _matrix.RemoveCell(cell);
            else
                _matrix.AddCell(cell);
            
            OnFrameChanged();
        }

        private void StartLife()
        {
            _taskCancellation = new CancellationTokenSource();
            var startLife = new Task(GenerateFrames, _taskCancellation.Token);

            startLife.Start();
            
        }

        private void GenerateFrames()
        {
            while (_taskCancellation.IsCancellationRequested == false)
            {
                _frameGenerator.ApplyNextFrame();
                OnFrameChanged();
                Generation++;
                Thread.Sleep(100);
            }
        }

        private void ResetMatrix()
        {
            _matrix.Init(new List<Cell>());
            OnFrameChanged();
            _taskCancellation.Cancel();
            Generation = 0;
        }

        private void OnFrameChanged()
        {
            RaisePropertyChanged(nameof(NumberOfCells));
            FrameChanged?.Invoke(this, new EventArgs());
        }
    }
}
