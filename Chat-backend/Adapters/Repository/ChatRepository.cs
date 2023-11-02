﻿using Chat_backend.Entities;
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
        public Task AddUserToChat(Guid chatId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Chat> CreateChat(NewChatDto chat)
        {
            var newChat = new Chat
            {
                Name = chat.Name,
                Users = new List<User>(),
                Messages = new List<Message>()
            };

            await dbSet.AddAsync(newChat);
            await _context.SaveChangesAsync();
            return newChat; 
            
        }

        public Task<Message> CreateMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public void DeleteChat(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Chat>> GetAllChats()
        {
            throw new NotImplementedException();
        }

        public Task<Chat> GetChatById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Message>> GetMessagesFromChat(Guid chatId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsersFromChat(Guid chatId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveUserFromChat(Guid chatId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Chat> UpdateChat(Chat chat)
        {
            throw new NotImplementedException();
        }
    }
}
