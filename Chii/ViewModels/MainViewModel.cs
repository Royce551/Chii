using Avalonia.Controls;
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

    public MainViewModel()
    {
        BrowserContent = new AvaloniaCefBrowser();
        BrowserContent.LoadingStateChange += BrowserContent_LoadingStateChange;
        OpenDictionary(@"%E3%81%93%E3%81%AE%E8%BE%BA%E3%81%AE%E5%9B%BD%E3%81%AF%E6%95%B0%E5%AD%A6%E3%81%8C%E4%B8%80%E7%95%AA%E3%83%A4%E3%83%90%E3%81%84");

    }

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
