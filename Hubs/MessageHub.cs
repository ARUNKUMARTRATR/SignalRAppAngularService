namespace SignalRAppAngular.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using SignalRAppAngular.Data;
    using SignalRAppAngular.Dtos;
    using SignalRAppAngular.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
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
        public async Task SendToCallerAsync(ChatbotMessageReceiverDto message)
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
            var questionDetails = new ChatBotCollectQuestion();
            if (message.QuestionId == null || message.QuestionId == 0)
            {
                questionDetails = _dbContext.ChatBotCollectQuestionInfo.FirstOrDefault(x => x.QuestionId == 1);
            }
            else
            {
                questionDetails = _dbContext.ChatBotCollectQuestionInfo.FirstOrDefault(x => x.QuestionId == message.QuestionId);
            }
            question = questionDetails.QuestionName;
            

            ChatBotConversationDetail conDetails = new ChatBotConversationDetail()
            {
                ConversationHeaderId = convHeaderId,
                QuestionName = question,
                Answer = message.UserData
            };
            _dbContext.ChatBotConversationDetailInfo.Add(conDetails);
            _dbContext.SaveChanges();

            var optionGroupId = 0;
            List<ChatBotCollectQuestion> chatbotQuestion = new List<ChatBotCollectQuestion>();
            chatbotQuestion = _dbContext.ChatBotCollectQuestionInfo.Where(x => x.BotId == 1).ToList();
            ChatbotMessageReceiverDto returnMessage = new ChatbotMessageReceiverDto();
            if (message.QuestionId == null || message.QuestionId == 0)
            {
                returnMessage.AnswerType = chatbotQuestion.FirstOrDefault(x => x.ParentQuestionId == 0).QuestionType;
                returnMessage.Message = chatbotQuestion.FirstOrDefault(x => x.ParentQuestionId == 0).Question;
                returnMessage.QuestionId = chatbotQuestion.FirstOrDefault(x => x.ParentQuestionId == 0).QuestionId;
                optionGroupId = (int)chatbotQuestion.FirstOrDefault(x => x.ParentQuestionId == 0).OptionGroupId;
            }
            else if (question == "yes_policy_number")
            {
                var insuredName = _dbContext.ChatBotConversationDetailInfo.FirstOrDefault(x => x.ConversationHeaderId == convHeaderId && x.QuestionName == "yes_policy_number").Answer;
                var contactNumber = _dbContext.ChatBotConversationDetailInfo.FirstOrDefault(x => x.ConversationHeaderId == convHeaderId && x.QuestionName == "phone_number").Answer;
                var decisionIdentifier = getClaimExistsStatus(insuredName, contactNumber);
                var childQuestions = _dbContext.ChatBotCollectQuestionInfo.Where(x => x.ParentQuestionId == message.QuestionId).ToList();
                var childQuestionId = 0;
                if (childQuestions.Count > 1)
                {
                    childQuestionId = (int)_dbContext.ChatBotCollectQuestionInfo.FirstOrDefault(x => x.BotId == 1 && x.ParentQuestionId == message.QuestionId && x.DecisionIdentifier == decisionIdentifier).QuestionId;
                }
                else
                {
                    childQuestionId = (int)_dbContext.ChatBotCollectQuestionInfo.FirstOrDefault(x => x.BotId == 1 && x.ParentQuestionId == message.QuestionId).QuestionId;
                }
                optionGroupId = (int)chatbotQuestion.FirstOrDefault(x => x.QuestionId == childQuestionId).OptionGroupId;
                returnMessage.AnswerType = chatbotQuestion.FirstOrDefault(x => x.QuestionId == childQuestionId).QuestionType;
                //returnMessage.AnswerType = "options";
                returnMessage.Message = chatbotQuestion.FirstOrDefault(x => x.QuestionId == childQuestionId).Question;
                returnMessage.QuestionId = chatbotQuestion.FirstOrDefault(x => x.QuestionId == childQuestionId).QuestionId;
            }
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
                optionGroupId = (int)chatbotQuestion.FirstOrDefault(x => x.QuestionId == childQuestionId).OptionGroupId;
                returnMessage.AnswerType = chatbotQuestion.FirstOrDefault(x => x.QuestionId == childQuestionId).QuestionType;
                //returnMessage.AnswerType = "options";
                returnMessage.Message = chatbotQuestion.FirstOrDefault(x => x.QuestionId == childQuestionId).Question;
                returnMessage.QuestionId = chatbotQuestion.FirstOrDefault(x => x.QuestionId == childQuestionId).QuestionId;
            }
            var optionList = _dbContext.ChatBotQuestionOptionGroupCodeInfo.Where(x => x.OptionGroupId == optionGroupId).Select(x => new Options
            {
                Id = (int)x.OptionCodeId,
                ItemName = x.Code
            }).ToList();
            returnMessage.OptionData = optionList;
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

        private string getClaimExistsStatus(string insuredName, string phoneNumber)
        {
            HttpClient httpClient = new HttpClient();
            var urlString = "https://measuringapp.experionglobal.dev/ChatBot/checkPhoneAndInsuredName?insuredName=" + insuredName + "&phoneNumber=" + phoneNumber;
            Uri u = new Uri(urlString);
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = u,
            };
            //var req = httpClient.SendAsync(request);
            var task = Task.Run(() => httpClient.SendAsync(request));
            task.Wait();
            var response = task.Result.StatusCode;
            if(response.ToString() == "OK")
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }
    }
}
