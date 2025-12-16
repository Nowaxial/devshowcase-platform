using Blazored.LocalStorage;
using DevShowcase.Shared.DTOs.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
namespace DevShowcase.Client.Services;

public class AuthService : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        if (string.IsNullOrWhiteSpace(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
    }

    public async Task<AuthResponseDto> Register(RegisterDto registerModel)
    {
        var result = await _httpClient.PostAsJsonAsync("api/auth/register", registerModel);

        if (!result.IsSuccessStatusCode)
        {
            return new AuthResponseDto { Success = false, Errors = new List<string> { "Registration failed" } };
        }
        return await result.Content.ReadFromJsonAsync<AuthResponseDto>()
               ?? new AuthResponseDto { Success = false };
    }
    public async Task<AuthResponseDto> Login(LoginDto loginModel)
    {
        var result = await _httpClient.PostAsJsonAsync("api/auth/login", loginModel);

        if (!result.IsSuccessStatusCode)
        {
            // Hantera fel här snyggare i verkligheten
            return new AuthResponseDto { Success = false, Errors = new List<string>{ "Login failed" } };
        }

        var content = await result.Content.ReadFromJsonAsync<AuthResponseDto>();
        await _localStorage.SetItemAsync("authToken", content!.Token);
        await _localStorage.SetItemAsync("refreshToken", content.RefreshToken);
        NotifyUserAuthentication(content.Token); // Anropa direkt
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", content.Token);
        return content;
    }
    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        await _localStorage.RemoveItemAsync("refreshToken");
        NotifyUserLogout(); // Anropa direkt
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }
    public void NotifyUserAuthentication(string token)
    {
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));
        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        NotifyAuthenticationStateChanged(authState);
    }
    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
        NotifyAuthenticationStateChanged(authState);
    }
    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
    }
    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}