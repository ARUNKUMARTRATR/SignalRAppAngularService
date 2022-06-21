using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAppAngular.Model
{
    public class ChatBotConversationDetail
    {
        [Key]
        public long ConversationDetailId { get; set; }
        public long ConversationHeaderId { get; set; }
        public string QuestionName { get; set; }
        public string Answer { get; set; }
    }
}
