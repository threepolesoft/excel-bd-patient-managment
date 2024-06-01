using API.DbContexts;
using API.Repository.Interface;
using Common.Models;
using Common.Models.DbSet;
using System.Data;

namespace API.Repository
{
    public class UserServiceBusiness : IUserService
    {
        private readonly AppDbContext _appDbContext;

        Res res = new Res();

        public UserServiceBusiness(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public string AuthenticatedUser(string id, string pass)
        {
            string status = ActionStatus.Success;

            ApplicationUser user = _appDbContext.ApplicationUser.Where(m => m.UserName == id && m.Password == pass).ToList().FirstOrDefault();

            if (user != null)
            {
                status = ActionStatus.Success;
            }
            else
            {
                status = "Incorrect information.";
            }

            return status;
        }

        public ApplicationUser UserByUserName(string UserName)
        {

            ApplicationUser applicationUser = _appDbContext.ApplicationUser.Where(m => m.UserName == UserName).ToList().FirstOrDefault();

            return applicationUser;
        }
    }
}
