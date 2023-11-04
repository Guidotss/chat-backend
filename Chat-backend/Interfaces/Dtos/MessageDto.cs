namespace Chat_backend.Interfaces.Dtos
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = default!; 
        public Guid UserId { get; set; }
    }
}
