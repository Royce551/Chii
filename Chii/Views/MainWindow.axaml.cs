using Avalonia;
using Avalonia.Controls;
using Chii.ViewModels;

namespace Chii.Views;

public partial class MainWindow : Window
{
    private MainViewModel? viewModel => DataContext as MainViewModel;

    public MainWindow()
    {
        InitializeComponent();
        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        viewModel?.Initialize();
    }
}
