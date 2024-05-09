﻿namespace Tinder.BLL.Models
{
    public class MessageModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public Guid ChatId { get; set; }
        public ChatModel Chat { get; set; }
        public Guid SenderId { get; set; }
        public UserModel User { get; set; }
    }
}
