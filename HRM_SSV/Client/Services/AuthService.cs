using Blazored.LocalStorage;
using Domain.Modals.Response;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Client.Services
{
    public interface IAuthService
    {
        Task<Response> Login(string userName, string password);
        Task Logout();
    }
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public AuthService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<Response> Login(string userName, string password)
        {
            var result = await _httpClient.GetFromJsonAsync<Response>($"/api/Authenticate/Login?username={userName}&password={password}");
            if (!result.IsSuccess)
                return result;
            await _localStorage.SetItemAsync("authToken", result.Token);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(userName);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
            return result;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("user");
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
