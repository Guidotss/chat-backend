using Chat_backend.Adapters.Errors;
using Chat_backend.Entities;
using Chat_backend.Frameworks___Drivers.Database;
using Chat_backend.Interfaces;
using Chat_backend.Interfaces.Dtos;
using Microsoft.EntityFrameworkCore;
namespace Chat_backend.Adapters.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserRepository _userRepository;
        private readonly MessageRespository _messageRespository;
        internal readonly DbSet<Chat> dbSet; 
        


        public ChatRepository(ApplicationDbContext context)
        {
            _context = context; 
            dbSet = context.Set<Chat>();
            _messageRespository = new MessageRespository(context);
            _userRepository = new UserRepository(context);            
        }

        public async Task AddUserToChat(Guid chatId, Guid userId)
        {
            var chat = await dbSet.FindAsync(chatId);
            var user = await _userRepository.GetUserById(userId);
            
            if (chat == null || user == null)
            {
                _ = new HttpError("Chat or user not found", 404);
            }

            var chatUser = new ChatUser
            {
                ChatId = chatId,
                UserId = userId
            };

            chat.ChatUsers.Add(chatUser);
            await _context.SaveChangesAsync();
        }

        public async Task<Chat> CreateChat(NewChatDto chat)
        {
            var newChat = new Chat
            {
                Name = chat.Name,
                Messages = new List<Message>(),
            }; 
            await dbSet.AddAsync(newChat);
            await _context.SaveChangesAsync();
            return newChat; 
        }

        public async void DeleteChat(Guid id)
        {
            var chat = await dbSet.FindAsync(id);
            dbSet.Remove(chat);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Chat>> GetAllChats() => dbSet.ToList();
        public async Task<Chat> GetChatById(Guid id)
        {
            IQueryable<Chat> query = dbSet;
            query = query.Include("Messages");
            query = query.Include("ChatUsers"); 
            
            var chat = query.FirstOrDefault(x => x.Id == id);
            if (chat == null)
            {
                _ = new HttpError("Chat not found", 404);
            }

            return chat; 
            
        }

        public async Task RemoveUserFromChat(Guid chatId, Guid userId)
        {
            var chat = await dbSet.FindAsync(chatId);
            var user = await _userRepository.GetUserById(userId);

            if (chat == null || user == null)
            {
                _ = new HttpError("Chat or user not found", 404);
            }
            
            
            _context.ChatUser.Remove(new ChatUser { ChatId = chatId, UserId = userId });
            _context.SaveChanges(); 
        }

        public async Task<Chat> UpdateChat(UpdateChatDto chat)
        {
            var chatFromDb = dbSet.Find(chat.Id);
            if (chatFromDb == null)
            {
                _ = new HttpError("Chat not found", 404);
            }

            if (chat.Name != null)
            {
                chatFromDb.Name = chat.Name;
            }

            dbSet.Update(chatFromDb);
            _context.SaveChanges();
            return chatFromDb;
        }
    }
}
