using Chat_backend.Entities;
using Chat_backend.Interfaces.Dtos;

namespace Chat_backend.Interfaces
{
    public interface IChatRepository
    {
        abstract Task<IEnumerable<Chat>> GetAllChats();
        abstract Task<Chat> GetChatById(Guid id);
        abstract Task<Chat> CreateChat(NewChatDto chat);
        abstract Task<Chat> UpdateChat(UpdateChatDto chat);
        abstract void DeleteChat(Guid id);
        abstract Task AddUserToChat(Guid chatId, Guid userId);
        abstract Task RemoveUserFromChat(Guid chatId, Guid userId);
    }
}
