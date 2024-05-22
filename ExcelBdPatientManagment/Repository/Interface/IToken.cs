namespace API.Repository.Interface
{
    public interface IToken
    {
        string GenerateToken(string Id);
        string GetUserIdFromToken(string token);
    }
}
