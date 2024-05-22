using Common.Models;

namespace API.Repository.Interface
{
    public interface IUserService
    {
        string AuthenticatedUser(string id, string pass);

        User User(string Id);
    }
}
