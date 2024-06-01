using Common.Models;
using Common.Models.DbSet;

namespace API.Repository.Interface
{
    public interface INCD
    {
        NCDModel GetNCDByID(long ID);
        NCDModel GetNCDByName(string Name);
        string Save(NCDModel nCD);
        List<NCDModel> GetAll();
        bool Delete(long ID);
    }
}
