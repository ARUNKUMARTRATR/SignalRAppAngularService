using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAppAngular.Model
{
    public class ChatBotConversation
    {
        [Key]
        public long ConversationId { get; set; }
        public int BotId { get; set; }
        public string UserIdentifier { get; set; }
        public string ChannelName { get; set; }
        public string ChannelSID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
