using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAppAngular.Model
{
    public class ChatBotConversationHeader
    {
        [Key]
        public long ConversationHeaderId { get; set; }
        public long ConversationId { get; set; }
        public string DecisionIdentifier { get; set; }
        public string ResponseJSON { get; set; }
        public int CollectId { get; set; }
    }
}
