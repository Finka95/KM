using Microsoft.AspNetCore.Mvc.Testing;

namespace Tinder.IntegrationTests
{
    internal class BaseTestClass : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        protected readonly HttpClient _httpClient;

        internal BaseTestClass(CustomWebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
    }
}
