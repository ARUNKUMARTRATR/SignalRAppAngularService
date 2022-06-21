using System.ComponentModel.DataAnnotations;

namespace SignalRAppAngular.Model
{
    public class ChatBotQuestionOptionGroupCode
    {
        [Key]
        public long OptionCodeId  { get; set; }
        public long OptionGroupId { get; set; }
        public string Code { get; set; }
        public string CodeDescription { get; set; }
        
    }
}

