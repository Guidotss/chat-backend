

namespace Chat_backend.Entities
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        virtual public ICollection<ChatUser> ChatUser { get; set; } = new List<ChatUser>();
        virtual public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
