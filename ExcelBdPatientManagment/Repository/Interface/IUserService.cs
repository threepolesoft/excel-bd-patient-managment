using Common.Models;
using Common.Models.DbSet;

namespace API.Repository.Interface
{
    public interface IUserService
    {
        string AuthenticatedUser(string id, string pass);
        ApplicationUser UserByUserName(string UserName);
    }
}
