using System.Windows;

namespace Fasetto_Word;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new WindowViewModel(this);
    }
}