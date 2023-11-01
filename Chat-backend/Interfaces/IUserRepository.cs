

using Chat_backend.Entities;

namespace Chat_backend.Interfaces 
{
    public interface IUserRepository 
    {
        abstract Task<IEnumerable<User>> GetAllUsers();
        abstract Task<User> GetUserById(Guid id);
        abstract Task<User> CreateUser(User user);
        abstract Task<User> UpdateUser(User user);
        abstract void DeleteUser(Guid id);
        abstract Task<IEnumerable<Chat>> GetChatsFromUser(Guid userId);
        abstract Task<IEnumerable<Message>> GetMessagesFromUser(Guid userId);
    }
}