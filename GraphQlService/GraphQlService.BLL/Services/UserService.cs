using GraphQlService.BLL.Interfaces;
using GraphQlService.BLL.Models;
using System.Net.Http.Json;
using GraphQlService.BLL.Constants;

namespace GraphQlService.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient(HttpClientConstants.Tinder);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress);
            var result = await response.Content.ReadFromJsonAsync<List<User>>();
            return result;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/{id}");
            var result = await response.Content.ReadFromJsonAsync<User>();
            return result;
        }
    }
}
