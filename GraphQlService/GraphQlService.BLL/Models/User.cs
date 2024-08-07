﻿namespace GraphQlService.BLL.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public Guid FusionUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string City { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid SubscriptionId { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
