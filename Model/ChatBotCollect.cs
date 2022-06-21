using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAppAngular.Model
{
    public class ChatBotCollect
    {
        [Key]
        public int CollectId { get; set; }
        public string CollectName { get; set; }
        public string CollectDecription { get; set; }
        public int BotId { get; set; }
        public int ParentCollectId { get; set; }
        public string OnCompleteRedirectMethod { get; set; }
        public string OnCompleteRedirectURI { get; set; }
        public bool IsFinalCollect { get; set; }
        public string DecisionIdentifier { get; set; }
    }
}
