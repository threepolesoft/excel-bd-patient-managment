using Common.Models;
using Common.Models.DbSet;

namespace API.Repository.Interface
{
    public interface IAllergies
    {
        AllergiesModel GetAllergiesByID(long ID);
        AllergiesModel GetAllergiesByName(string Name);
        string Save(AllergiesModel nCD);
        List<AllergiesModel> GetAll();
        bool Delete(long ID);
    }
}
