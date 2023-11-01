namespace Chat_backend.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = default!; 
        public Guid SenderId { get; set; }
        public Guid ChatId { get; set; }
    }
}
