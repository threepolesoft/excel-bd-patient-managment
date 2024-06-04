using Common.Models;

namespace API.Repository.Interface
{
    public interface IPatient
    {
        long GetID();
        List<PatientsModel> GetAll();
        string Save(PatientsModel patientsModel);

    }
}
