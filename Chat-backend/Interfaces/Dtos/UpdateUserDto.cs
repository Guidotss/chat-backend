namespace Chat_backend.Interfaces.Dtos
{
    public class UpdateUserDto
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; } = default!;
        public string? Email { get; set; } = default!;
        public string? Password { get; set; } = default!;
    }
}
