using System.Text.Json.Serialization;

namespace Chat_backend.Interfaces.Dtos
{
    public class NewMessageDto
    {
        [JsonPropertyName("content")]
        public string Content { get; set; } = default!;
        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }
        [JsonPropertyName("chatId")]
        public Guid ChatId { get; set; }
    }
}
