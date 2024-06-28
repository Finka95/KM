using Tinder.BLL.Providers.Interfaces;

namespace Tinder.BLL.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
