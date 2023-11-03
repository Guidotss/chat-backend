namespace Chat_backend.Entities
{
    public class ChatUser
    {
        public Guid ChatsId { get; set; }
        public Chat Chat { get; set; } = default!;

        public User User { get; set; } = default!;
        public Guid UsersId { get; set; }
    }
}
