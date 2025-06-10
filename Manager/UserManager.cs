using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using BounceBall.Models;

namespace BounceBall.Manager
{
    public class UserManager : IDisposable
    {
        private readonly HttpClient _httpClient;
        private bool _disposed = false;

        public User CurrentUser { get; private set; }
        public string AccessToken { get; private set; }

        public UserManager()
        {
            // Configure HttpClient properly
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:5003/"),
                Timeout = TimeSpan.FromSeconds(30)
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public HttpClient AuthenticatedClient => _httpClient;

        public async Task<bool> RegisterAsync(string username, string password, string email)
        {
            try
            {
                var user = new { username = username, email = email, password = password };
                using var response = await _httpClient.PostAsJsonAsync("api/users/register", user);

                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Registration failed: {ex.Message}");
                return false;
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine($"Registration timeout: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                var loginRequest = new { Username = username, Password = password };
                using var response = await _httpClient.PostAsJsonAsync("api/users/login", loginRequest);

                response.EnsureSuccessStatusCode();

                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (loginResponse == null || string.IsNullOrEmpty(loginResponse.AccessToken))
                {
                    Console.WriteLine("Invalid login response");
                    return false;
                }

                AccessToken = loginResponse.AccessToken;
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);

                // Get user details
                using var userResponse = await _httpClient.GetAsync("api/users/me");
                userResponse.EnsureSuccessStatusCode();

                CurrentUser = await userResponse.Content.ReadFromJsonAsync<User>();
                return CurrentUser != null;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Login failed: {ex.Message}");
                return false;
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine($"Login timeout: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error during login: {ex.Message}");
                return false;
            }
        }

        public void Logout()
        {
            CurrentUser = null;
            AccessToken = null;
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _httpClient?.Dispose();
                }
                _disposed = true;
            }
        }
    }


    // Response model for login
    public class LoginResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }
    }
}