using Chat_backend.Entities;
using Chat_backend.Interfaces.Dtos;

namespace Chat_backend.Interfaces
{ 
    public interface IMessageRepository
    {
        abstract Task<IEnumerable<Message>> GetAllMessages();
        abstract Task<Message> GetMessageById(Guid id);
        abstract Task<Message> CreateMessage(NewMessageDto message);
        
    }
}