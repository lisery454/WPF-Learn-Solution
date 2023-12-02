using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Fasetto_Word;

public class BasePage<TVm> : Page where TVm : BaseViewModel, new()
{
    private TVm _viewModel;
    public PageAnimation pageLoadAnimation { get; set; } = PageAnimation.SlideAndFadeInFromRight;
    public PageAnimation pageUnloadAnimation { get; set; } = PageAnimation.SlideAndFadeOutTLeft;
    public float SlideSeconds { get; set; } = 0.8f;

    public TVm ViewModel
    {
        get => _viewModel;
        set
        {
            if (_viewModel == value) return;
            _viewModel = value;
            DataContext = _viewModel;
        }
    }

    public BasePage()
    {
        if (pageLoadAnimation != PageAnimation.None)
            Visibility = Visibility.Collapsed;
        Loaded += OnLoaded;
        _viewModel = new TVm();
        DataContext = _viewModel;
    }

    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        await AnimateIn();
    }

    private async Task AnimateIn()
    {
        switch (pageLoadAnimation)
        {
            case PageAnimation.None:
                return;
            case PageAnimation.SlideAndFadeInFromRight:
                await this.SlideAndFadeInFromRight(SlideSeconds);
                break;
            case PageAnimation.SlideAndFadeOutTLeft:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}