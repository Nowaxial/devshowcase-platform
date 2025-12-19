using DevShowcase.Shared.DTOs.Public;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DevShowcase.Client.Services;

public class PortfolioStateService
{
    private PortfolioDto? _currentPortfolio;
    private string? _currentUsername;
    private bool _isLoading;

    public PortfolioDto? CurrentPortfolio
    {
        get => _currentPortfolio;
        private set
        {
            _currentPortfolio = value;
            NotifyStateChanged();
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        private set
        {
            _isLoading = value;
            NotifyStateChanged();
        }
    }

    public event Action? OnChange;

    public async Task LoadPortfolioOnceAsync(string username, ApiService apiService)
    {
        if (_currentUsername == username && _currentPortfolio != null) return;

        IsLoading = true;
        _currentUsername = username;
        try
        {
            CurrentPortfolio = await apiService.GetPortfolioAsync(username);
        }
        catch (Exception)
        {
            CurrentPortfolio = null;
        }
        finally
        {
            IsLoading = false;
        }
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
