using AutoMapper;
using SignalRAppAngular.Dtos;
using SignalRAppAngular.Models;

namespace SignalRAppAngular.Profiles
{
    public class ChatBotProfile:Profile
    {
        public ChatBotProfile()
        {
            //Source --> Target
            CreateMap<ChatBotMaster,ChatBotMasterReadDto>();
            CreateMap<ChatBotMasterCreateDto,ChatBotMaster>();
            CreateMap<ChatBotMasterUpdateDto,ChatBotMaster>();
        }
        
    }
    
}