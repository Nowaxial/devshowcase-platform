using MudBlazor;

namespace DevShowCase.Client.Layout
{
    public partial class MainLayout
    {
        bool _drawerOpen = true;
        bool _isDarkMode = true;
        private MudThemeProvider _mudThemeProvider = null!;
        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _isDarkMode = await _mudThemeProvider.GetSystemDarkModeAsync();
                StateHasChanged();
            }
        }
    }
}