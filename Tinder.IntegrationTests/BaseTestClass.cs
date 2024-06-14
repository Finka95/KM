using Microsoft.AspNetCore.Mvc.Testing;

namespace Tinder.IntegrationTests
{
    public class BaseTestClass : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        protected readonly HttpClient _httpClient;

        public BaseTestClass(CustomWebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
    }
}
