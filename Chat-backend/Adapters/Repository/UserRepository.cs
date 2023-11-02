using Chat_backend.Entities;
using Chat_backend.Frameworks___Drivers.Database;
using Chat_backend.Interfaces;
using Chat_backend.Interfaces.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace Chat_backend.Adapters.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        internal readonly DbSet<User> dbSet;

        private string HashPassword(string password)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            return hash;
        }
        public UserRepository(ApplicationDbContext context)
        {
            _context = context; 
            dbSet = context.Set<User>();
        }

        public async Task<User> CreateUser(NewUserDto user)
        {
            var newUser = new User
            {
                Username = user.UserName,
                Email = user.Email,
                Password = HashPassword(user.Password),
                Chats = new List<Chat>(),
                Messages = new List<Message>()
            };

            await dbSet.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return newUser; 
            
        }

        public async void DeleteUser(Guid id)
        {
            var user =  await GetUserById(id);
            dbSet.Remove(user); 
        }

        public async Task<IEnumerable<User>> GetAllUsers() => await dbSet.ToListAsync();


        public async Task<User> GetUserById(Guid id)
        {
            var user = await dbSet.FindAsync(id) ?? throw new Exception("User not found");
            return user;
        }

        public async Task<User> UpdateUser(UpdateUserDto user)
        {
            var userFromDb = await GetUserById(user.Id);
            if (user.Password != null)
            {
                userFromDb.Password = HashPassword(user.Password);
            }
            if(user.UserName != null)
            {
                userFromDb.Username = user.UserName;
            }

            if (user.Email != null)
            {
                   userFromDb.Email = user.Email;
            }

            if (userFromDb == null) throw new Exception("User not found"); 

            dbSet.Update(userFromDb);
            return userFromDb;
        }
    }
}
