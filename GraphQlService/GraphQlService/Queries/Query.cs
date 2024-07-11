using GraphQlService.BLL.Interfaces;
using GraphQlService.ViewModels;

namespace GraphQlService.Queries
{
    public class Query
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IUserService _userService;

        public Query(IUserService userService, ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
            _userService = userService;
        }
        
        [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 3)]
        [UseFiltering]
        [UseSorting]
        public async Task<List<UserViewModel>> GetUsersAsync()
        {
            var users = await _userService.GetUsersAsync();
            var subscriptions = await _subscriptionService.GetSubscriptionsAsync();

            return users.Select(u =>
            {
                var subscription = subscriptions.Find(s => s.FusionUserId == u.FusionUserId);

                return new UserViewModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    City = u.City,
                    Description = u.Description,
                    Age = u.Age,
                    Subscription = new SubscriptionViewModel
                    {
                        Id = subscription.Id,
                        SubscriptionType = subscription.SubscriptionType,
                        Email = subscription.Email,
                    }
                };
            }).ToList();
        }

        public async Task<UserViewModel> GetUserByIdAsync(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            var subscription = await _subscriptionService.GetSubscriptionByIdAsync(user.SubscriptionId);

            return new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                City = user.City,
                Description = user.Description,
                Age = user.Age,
                Subscription = new SubscriptionViewModel
                {
                    Id = subscription.Id,
                    SubscriptionType = subscription.SubscriptionType,
                    Email = subscription.Email,
                }
            };
        }
    }
}
