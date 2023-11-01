namespace Chat_backend.Entities
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
