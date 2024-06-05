using Common.Models;
using Common.Models.DbSet;

namespace API.Repository.Interface
{
    public interface IAllergiesDetails
    {
        List<AllergiesModel> GetByPatientID(long PatientID);
        string Save(AllergiesDetails allergiesDetails);
    }
}
