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

        /// <summary>
        /// Gets or sets the OptionData.
        /// </summary>
        public List<Options> OptionData { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsAnswer.
        /// </summary>
        public bool IsAnswer { get; set; }

        /// <summary>
        /// Gets or sets the IsAuto.
        /// </summary>
        public bool? IsAuto { get; set; }

        /// <summary>
        /// Gets or sets the QuestionId.
        /// </summary>
        public int? QuestionId { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="Options" />.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ItemName.
        /// </summary>
        public string ItemName { get; set; }
    }
}
