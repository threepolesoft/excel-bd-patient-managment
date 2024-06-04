using Common.Models.DbSet;

namespace API.Repository.Interface
{
    public interface IAllergiesDetails
    {
        string Save(AllergiesDetails allergiesDetails);
    }
}
