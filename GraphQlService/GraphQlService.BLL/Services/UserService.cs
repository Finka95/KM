using System.Net;
using GraphQlService.BLL.Interfaces;
using GraphQlService.BLL.Models;
using System.Net.Http.Json;
using GraphQlService.BLL.Constants;
using GraphQlService.BLL.Exceptions;
using Microsoft.AspNetCore.Http;

namespace GraphQlService.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpClientFactory factory,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = factory.CreateClient(HttpClientConstants.Tinder);
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            SetBearerToken();

            var response = await _httpClient.GetAsync(_httpClient.BaseAddress);
            var result = await response.Content.ReadFromJsonAsync<List<User>>();
            return result;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            SetBearerToken();

            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/{id}");
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                throw new NotFoundException("not found");
            }
            var result = await response.Content.ReadFromJsonAsync<User>();
            return result;
        }

        private void SetBearerToken()
        {
            var bearerToken = _httpContextAccessor
                .HttpContext
                .Request
                .Headers
                .Authorization
                .FirstOrDefault()?
                .Split(" ")
                .Last();

            _httpClient.DefaultRequestHeaders.Authorization = new System
                .Net.Http.Headers
                .AuthenticationHeaderValue("Bearer", bearerToken);
        }
    }
}
