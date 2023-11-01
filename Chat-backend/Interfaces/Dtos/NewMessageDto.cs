namespace Chat_backend.Interfaces.Dtos
{
    public class NewMessageDto
    {
        public string Content { get; set; } = default!; 
        public Guid SenderId { get; set; }
        public Guid ChatId { get; set; }
    }
}
