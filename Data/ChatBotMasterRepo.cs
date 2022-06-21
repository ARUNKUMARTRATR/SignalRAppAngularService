using SignalRAppAngular.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SignalRAppAngular.Data
{
    class ChatBotMasterRepo : IChatbotMasterRepo
    {
        private AppDbContext _dbcontext;

        public ChatBotMasterRepo(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            
        }
        public async Task CreateChatBot(ChatBotMaster botObj)
        {
            if(botObj==null){
                throw new ArgumentNullException(nameof(ChatBotMaster));
            }
             await _dbcontext.AddAsync(botObj);
           
        }

        public void DeleteChatBot(ChatBotMaster botObj)
        {
             if(botObj==null){
                throw new ArgumentNullException(nameof(ChatBotMaster));
            }
            _dbcontext.ChatBotMasters.Remove(botObj);
        }

        public async Task<IEnumerable<ChatBotMaster>> GetAllChatBots()
        {
            return await _dbcontext.ChatBotMasters.ToListAsync();
        }

        public async Task<ChatBotMaster?> GetChatBotById(int id)
        {
            return await _dbcontext.ChatBotMasters.FirstOrDefaultAsync(it=>it.BotId==id);
        }

        public async Task SaveChanges()
        {
            await _dbcontext.SaveChangesAsync();
        }
    }
}