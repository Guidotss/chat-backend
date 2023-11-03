namespace Chat_backend.Interfaces.Dtos
{
    public class AddUserToChatDto
    {
        public Guid UserId { get; set; }
        public Guid ChatId { get; set; }
    }
}
