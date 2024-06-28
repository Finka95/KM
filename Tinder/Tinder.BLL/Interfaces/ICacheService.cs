namespace Tinder.BLL.Interfaces
{
    public interface ICacheService
    {
        public Task SetAsync<T>(string key, T value, CancellationToken cancellationToken);
        public Task<T> GetAsync<T>(string key, CancellationToken cancellationToken);
        public Task RemoveAsync(string key, CancellationToken cancellationToken);
    }
}
