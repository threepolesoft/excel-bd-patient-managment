using Common.Models.DbSet;

namespace API.Repository.Interface
{
    public interface IAllergies
    {
        Allergies GetAllergiesByID(long ID);
        Allergies GetAllergiesByName(string Name);
        string Save(Allergies nCD);
        List<Allergies> GetAll();
        bool Delete(long ID);
    }
}
