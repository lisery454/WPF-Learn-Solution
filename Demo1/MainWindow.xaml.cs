using System.Windows;
using Demo1.Directory.ViewModels;


namespace Demo1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new DirectoryStructureViewModel();
        }
    }
}