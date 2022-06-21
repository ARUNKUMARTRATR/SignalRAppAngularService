using SignalRAppAngular.Model;
using Microsoft.EntityFrameworkCore;
using SignalRAppAngular.Models;

namespace SignalRAppAngular.Data{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<ChatBotMaster> ChatBotMasters=> Set<ChatBotMaster>();
        public DbSet<ChatBotCollect> ChatBotCollectInfo => Set<ChatBotCollect>();
        public DbSet<ChatBotCollectQuestion> ChatBotCollectQuestionInfo => Set<ChatBotCollectQuestion>();
        public DbSet<ChatBotConversationDetail> ChatBotConversationDetailInfo => Set<ChatBotConversationDetail>();
        public DbSet<ChatBotConversationHeader> ChatBotConversationHeaderInfo => Set<ChatBotConversationHeader>();
        public DbSet<ChatBotConversation> ChatBotConversationInfo => Set<ChatBotConversation>();
        public DbSet<ChatBotQuestionOptionGroup> ChatBotQuestionOptionGroupInfo => Set<ChatBotQuestionOptionGroup>();
        public DbSet<ChatBotQuestionOptionGroupCode> ChatBotQuestionOptionGroupCodeInfo => Set<ChatBotQuestionOptionGroupCode>();

    }

}