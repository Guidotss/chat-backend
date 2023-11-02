namespace Chat_backend.Interfaces.Dtos
{
    public class NewUserDto
    {
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;

    }
}
