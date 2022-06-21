using System;

namespace SignalRAppAngular.Dtos{
    public class ChatBotMasterReadDto{

        public int BotId { get; set; }
        public string? BotName { get; set; }
        public string? BotDescription { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}