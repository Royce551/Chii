using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Chii.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;
using Xilium.CefGlue.Avalonia;

namespace Chii.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    public AvaloniaCefBrowser BrowserContent { get; private set; }

    [ObservableProperty]
    private bool alwaysOnTop;

    private IPlatformHandler platform;

    public MainViewModel()
    {
        BrowserContent = new AvaloniaCefBrowser();
        BrowserContent.LoadingStateChange += BrowserContent_LoadingStateChange;
        BrowserContent.Address = "https://jisho.org";
    }

    public void Initialize()
    {
        if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            platform = new WindowsPlatformHandler(desktop.MainWindow);

        platform.Initialize();
        platform.ClipboardChanged += Platform_ClipboardChanged;   
    }

    private void Platform_ClipboardChanged(object? sender, System.EventArgs e) => Search();

    public async Task Search()
    {
        var query = await TopLevel.GetTopLevel(BrowserContent)?.Clipboard?.GetTextAsync();
        OpenDictionary(query);
    }

    private void BrowserContent_LoadingStateChange(object sender, Xilium.CefGlue.Common.Events.LoadingStateChangeEventArgs e)
    {
        BrowserContent.ZoomLevel = -3;
    }

    public void OpenDictionary(string query)
    {
        BrowserContent.Address = $"https://jisho.org/search/{query}";
        
    }
}
