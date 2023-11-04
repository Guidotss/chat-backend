namespace Chat_backend.Interfaces.Dtos
{
    public class GetChatByIdResponseDto
    {
        public string Name { get; set; } = default!;
        public Guid[] UsersId { get; set; } = default!;
        public MessageDto[] Messages { get; set; } = default!;
    }
}
