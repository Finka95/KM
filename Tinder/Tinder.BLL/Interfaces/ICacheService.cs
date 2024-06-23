namespace Tinder.BLL.Interfaces
{
    public interface ICacheService
    {
        public Task SetAsync<T>(string key, T value);
        public Task<T> GetAsync<T>(string key);
        public Task RemoveAsync(string key);
    }
}
