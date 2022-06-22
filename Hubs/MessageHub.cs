namespace SignalRAppAngular.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using Newtonsoft.Json.Linq;
    using SignalRAppAngular.Data;
    using SignalRAppAngular.Dtos;
    using SignalRAppAngular.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="MessageHub" />.
    /// </summary>
    public class MessageHub : Hub
    {
        /// <summary>
        /// The SendToAllAsync.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        private readonly AppDbContext _dbContext;
        public MessageHub(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task SendToAllAsync(string message)
        {
            await Clients.All.SendAsync("RecieveMsg", message);
        }

        /// <summary>
        /// The SendToCallerAsync.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task SendToCallerAsync(ChatbotMessageReceiver message)
        {
            ChatBotConversation userIdentifier = new ChatBotConversation();
            //ChatBotConversationDetail conDetails = new ChatBotConversationDetail();
            var question = "";
            if (message.QuestionId == null || message.QuestionId == 0)
            {
                userIdentifier.BotId = 1;
                userIdentifier.ChannelName = "SignalRBot";
                userIdentifier.ChannelSID = message.ConnectionId;
                userIdentifier.CreatedBy = "user";
                userIdentifier.CreatedOn = DateTime.Now;
                _dbContext.ChatBotConversationInfo.Add(userIdentifier);
                _dbContext.SaveChanges();
            }

            var channelIdentiifier = _dbContext.ChatBotConversationInfo.FirstOrDefault(x => x.BotId == 1 && x.ChannelSID == message.ConnectionId).ConversationId;
            ChatBotConversationHeader convHeader = new ChatBotConversationHeader()
            {
                ConversationId = channelIdentiifier,
                ResponseJSON = JsonSerializer.Serialize(message),
            };
            _dbContext.ChatBotConversationHeaderInfo.Add(convHeader);
            _dbContext.SaveChanges();
            var convHeaderId = _dbContext.ChatBotConversationHeaderInfo.FirstOrDefault(x => x.ConversationId == channelIdentiifier).ConversationHeaderId;
            /*var questionDetails = _dbContext.ChatBotCollectQuestionInfo.FirstOrDefault(x => x.QuestionId == message.QuestionId)*/;
            var questionDetails = _dbContext.ChatBotCollectQuestionInfo.FirstOrDefault(x => x.ParentQuestionId == message.QuestionId);
            question = questionDetails.QuestionName;
            

            ChatBotConversationDetail conDetails = new ChatBotConversationDetail()
            {
                ConversationHeaderId = convHeaderId,
                QuestionName = question,
                Answer = message.Message
            };
            _dbContext.ChatBotConversationDetailInfo.Add(conDetails);
            _dbContext.SaveChanges();

            List<ChatBotCollectQuestion> chatbotQuestion = new List<ChatBotCollectQuestion>();
            chatbotQuestion = _dbContext.ChatBotCollectQuestionInfo.Where(x => x.BotId == 1).ToList();
            ChatbotMessageReceiver returnMessage = new ChatbotMessageReceiver();
            if (message.QuestionId == null || message.QuestionId == 0)
            {
                returnMessage.AnswerType = chatbotQuestion.FirstOrDefault(x => x.ParentQuestionId == 0).QuestionType;
                returnMessage.Message = chatbotQuestion.FirstOrDefault(x => x.ParentQuestionId == 0).Question;
                returnMessage.QuestionId = chatbotQuestion.FirstOrDefault(x => x.ParentQuestionId == 0).QuestionId;
            }
            //else if (message.QuestionId == 5)
            //{
            //}
            else
            {
                var childQuestions = _dbContext.ChatBotCollectQuestionInfo.Where(x => x.ParentQuestionId == message.QuestionId).ToList();
                var childQuestionId = 0;
                if(childQuestions.Count > 1)
                {
                    childQuestionId = (int)_dbContext.ChatBotCollectQuestionInfo.FirstOrDefault(x => x.BotId == 1 && x.ParentQuestionId == message.QuestionId && x.DecisionIdentifier == message.Message.ToLower()).QuestionId;
                }
                else
                {
                    childQuestionId = (int)_dbContext.ChatBotCollectQuestionInfo.FirstOrDefault(x => x.BotId == 1 && x.ParentQuestionId == message.QuestionId).QuestionId;
                }
                returnMessage.AnswerType = chatbotQuestion.FirstOrDefault(x => x.QuestionId == childQuestionId).QuestionType;
                //returnMessage.AnswerType = "options";
                returnMessage.Message = chatbotQuestion.FirstOrDefault(x => x.QuestionId == childQuestionId).Question;
                returnMessage.QuestionId = chatbotQuestion.FirstOrDefault(x => x.QuestionId == childQuestionId).QuestionId;
            }
            returnMessage.MessageUserType = 0;
            returnMessage.ConnectionId = message.ConnectionId;
            returnMessage.IsAnswer = false;
            returnMessage.IsAuto = false;
            returnMessage.DateTime = DateTime.Now;

            await Clients.Caller.SendAsync("RecieveMsg", returnMessage);
        }

        /// <summary>
        /// The OnConnectedAsync.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("onUserConnected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// The OnDisconnectedAsync.
        /// </summary>
        /// <param name="exception">The exception<see cref="Exception"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("onUserDisconnected", Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
