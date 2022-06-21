using SignalRAppAngular.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRAppAngular.Data{
   public interface IChatbotMasterRepo
   {
    Task SaveChanges();
    Task<ChatBotMaster?> GetChatBotById(int id);
    Task <IEnumerable<ChatBotMaster>> GetAllChatBots();
    Task CreateChatBot(ChatBotMaster botObj);
    void DeleteChatBot(ChatBotMaster botObj);
   }
}