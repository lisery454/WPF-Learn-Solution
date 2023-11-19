using System.Windows;
using System.Windows.Input;
using Fasetto_Word.ViewModels.Base;
using Fasetto_Word.Windows;

namespace Fasetto_Word.ViewModels;

public class WindowViewModel : BaseViewModel
{
    #region Private

    private readonly Window _window;

    private int _outerMarginSize = 10;
    private int _windowRadius = 10;

    #endregion

    #region Property

    public double WindowMinimumWidth { get; set; } = 400;
    public double WindowMinimumHeight { get; set; } = 400;

    public Thickness InnerContentPadding => new(ResizeBorder);

    public int ResizeBorder { get; set; } = 6;
    public Thickness ResizeBorderThickness => new(ResizeBorder + OuterMarginSize);

    public int OuterMarginSize
    {
        get => _window.WindowState == WindowState.Maximized ? 0 : _outerMarginSize;
        set => _outerMarginSize = value;
    }

    public Thickness OuterMarginSizeThickness => new(OuterMarginSize);

    public int WindowRadius
    {
        get => _window.WindowState == WindowState.Maximized ? 0 : _windowRadius;
        set => _windowRadius = value;
    }

    public CornerRadius WindowCornerRadius => new(WindowRadius);

    public int TitleHeight { get; set; } = 42;

    public GridLength TitleHeightGridLength => new(TitleHeight + ResizeBorder);

    #endregion

    #region Command

    public ICommand MinimizeCommand { get; set; }

    public ICommand MaximizeCommand { get; set; }

    public ICommand CloseCommand { get; set; }
    public ICommand MenuCommand { get; set; }

    #endregion


    public WindowViewModel(Window window)
    {
        _window = window;

        _window.StateChanged += (_, _) =>
        {
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(WindowCornerRadius));
        };

        MinimizeCommand = new RelayCommand(() => _window.WindowState = WindowState.Minimized);
        MaximizeCommand = new RelayCommand(() =>
        {
            _window.WindowState = _window.WindowState == WindowState.Normal
                ? WindowState.Maximized
                : WindowState.Normal;
        });
        CloseCommand = new RelayCommand(() => _window.Close());
        MenuCommand = new RelayCommand(() =>
        {
            var position = Mouse.GetPosition(_window);
            var point = new Point(position.X + _window.Left, position.Y + _window.Top);
            SystemCommands.ShowSystemMenu(_window, point);
        });

        var _ = new WindowResizer(_window);
    }
}