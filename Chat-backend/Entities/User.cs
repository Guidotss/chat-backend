namespace Chat_backend.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;    
        public string Password { get; set; } = default!;
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public ICollection<Chat> Chats { get; set; } = new List<Chat>();
    }
}
