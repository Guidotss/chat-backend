

namespace Chat_backend.Entities
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        virtual public ICollection<Message> Messages { get; set; } = new List<Message>();
        virtual public ICollection<ChatUser> ChatUsers { get; set; } = new List<ChatUser>();
    }
}
