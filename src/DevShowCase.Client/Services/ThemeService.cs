using Microsoft.JSInterop;

namespace DevShowcase.Client.Services;

public class ThemeService(IJSRuntime js)
{
    private string _currentTheme = "theme-minimal.css";

    public async Task ApplyTheme(string? cssFileName)
    {
        if (string.IsNullOrEmpty(cssFileName))
        {
            cssFileName = "theme-minimal.css";
        }

        _currentTheme = cssFileName;
        await js.InvokeVoidAsync("document.documentElement.setAttribute", "data-theme", cssFileName);
    }

    public string GetCurrentTheme() => _currentTheme;
}
