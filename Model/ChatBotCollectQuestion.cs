using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAppAngular.Model
{
    public class ChatBotCollectQuestion
    {
        [Key]
        public long QuestionId { get; set; }
        public string Question { get; set; }
        public string QuestionName { get; set; }
        public int CollectId { get; set; }
        public string QuestionType { get; set; }
        [Required]
        public long ParentQuestionId { get; set; }
        public int BotId { get; set; }
        public string DecisionIdentifier { get; set; }
        public long OptionGroupId { get; set; }
    }
}
