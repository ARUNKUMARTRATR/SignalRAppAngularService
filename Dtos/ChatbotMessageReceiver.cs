using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAppAngular.Dtos
{
    public class ChatbotMessageReceiver
    {
        public string ConnectionId { get; set; }
        public int MessageUserType { get; set; }
        public int MessageType { get; set; }
        public string AnswerType { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsAnswer { get; set; }
        public bool? IsAuto { get; set; }
        public int QuestionId { get; set; }
    }
}
