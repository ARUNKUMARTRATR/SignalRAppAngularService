namespace SignalRAppAngular.Dtos
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ChatbotMessageReceiverDto" />.
    /// </summary>
    public class ChatbotMessageReceiverDto
    {
        /// <summary>
        /// Gets or sets the ConnectionId.
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// Gets or sets the MessageUserType.
        /// </summary>
        public int MessageUserType { get; set; }

        /// <summary>
        /// Gets or sets the MessageType.
        /// </summary>
        public int MessageType { get; set; }

        /// <summary>
        /// Gets or sets the AnswerType.
        /// </summary>
        public string AnswerType { get; set; }

        /// <summary>
        /// Gets or sets the Message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the DateTime.
        /// </summary>
        public DateTime DateTime { get; set; }
        public List<Options> OptionData { get; set; }
        public bool IsAnswer { get; set; }

        /// <summary>
        /// Gets or sets the IsAuto.
        /// </summary>
        public bool? IsAuto { get; set; }
        public long? QuestionId { get; set; }
    }
    public class Options
    {
        public int Id { get; set; }
        public int ItemName { get; set; }
    }
}
