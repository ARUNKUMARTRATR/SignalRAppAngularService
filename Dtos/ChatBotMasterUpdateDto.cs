using System;
using System.ComponentModel.DataAnnotations;

namespace SignalRAppAngular.Dtos{
    public class ChatBotMasterUpdateDto{
        [Key]
        public int BotId { get; set; }
        [Required]
        public string? BotName { get; set; }
        public string? BotDescription { get; set; }
         [Required]
        public string? CreatedBy { get; set; }
         [Required]
        public DateTime CreatedOn { get; set; }
    }
}