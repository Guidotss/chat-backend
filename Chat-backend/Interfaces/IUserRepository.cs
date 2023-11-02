

using Chat_backend.Entities;
using Chat_backend.Interfaces.Dtos;

namespace Chat_backend.Interfaces 
{
    public interface IUserRepository 
    {
        abstract Task<IEnumerable<User>> GetAllUsers();
        abstract Task<User> GetUserById(Guid id);
        abstract Task<User> CreateUser(NewUserDto user);
        abstract Task<User> UpdateUser(UpdateUserDto user);
        abstract void DeleteUser(Guid id);
    }
}