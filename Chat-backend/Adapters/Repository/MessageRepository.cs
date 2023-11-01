using Chat_backend.Entities;
using Chat_backend.Frameworks___Drivers.Database;
using Chat_backend.Interfaces;
using Chat_backend.Interfaces.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Chat_backend.Adapters.Repository 
{
    public class MessageRespository : IMessageRepository
    {
        private readonly ApplicationDbContext _context;
        internal readonly DbSet<Message> dbSet;
        public MessageRespository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<Message>();
        }
        public async Task<Message> CreateMessage(NewMessageDto message)
        {
            var newMessage = new Message
            {
                ChatId = message.ChatId,
                SenderId = message.SenderId,    
                Content = message.Content,
            };
            await dbSet.AddAsync(newMessage);
            await _context.SaveChangesAsync();
            return newMessage;
        }

        public async Task<IEnumerable<Message>> GetAllMessages() => await dbSet.ToListAsync();

        public Task<Message> GetMessageById(Guid id) => dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }
}