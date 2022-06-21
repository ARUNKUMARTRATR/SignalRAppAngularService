using System;
using System.ComponentModel.DataAnnotations;

namespace SignalRAppAngular.Models{
    public class ChatBotMaster{
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