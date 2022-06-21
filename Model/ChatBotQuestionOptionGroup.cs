using System;
using System.ComponentModel.DataAnnotations;

namespace SignalRAppAngular.Model
{
    public class ChatBotQuestionOptionGroup
    {
        [Key]
        public long OptionGroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
