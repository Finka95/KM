using System.Net.Http.Json;
using GraphQlService.BLL.Constants;
using GraphQlService.BLL.Interfaces;
using GraphQlService.BLL.Models;

namespace GraphQlService.BLL.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly HttpClient _httpClient;    

        public SubscriptionService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient(HttpClientConstants.Subscriptions);
        }

        public async Task<List<Subscription>> GetSubscriptionsAsync()
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress);
            var result = await response.Content.ReadFromJsonAsync<List<Subscription>>();
            return result;
        }

        public async Task<Subscription> GetSubscriptionByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/{id}");
            var result = await response.Content.ReadFromJsonAsync<Subscription>();
            return result;
        }
    }
}
