

namespace Chat_backend.Entities
{
    public class User 
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;    
        public string Password { get; set; } = default!;
        virtual public ICollection<Message> Messages { get; set; } = new List<Message>();
        virtual public ICollection<ChatUser> ChatUser { get; set; } = new List<ChatUser>();
    }
}
