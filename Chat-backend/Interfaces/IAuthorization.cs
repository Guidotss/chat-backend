namespace Chat_backend.Interfaces
{
    public interface IAuthorization
    {
        public string GetToken(string email, string username, Guid id); 
        public bool VerifyToken(string token);
        public string GetEmail(string token);
    }
}
