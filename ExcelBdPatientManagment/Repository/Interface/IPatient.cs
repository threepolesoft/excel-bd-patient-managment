using Common.Models;

namespace API.Repository.Interface
{
    public interface IPatient
    {
        PatientDetailsModel GetPatientDetails(long ID);
        List<PatientsModel> GetAll();
        string Save(PatientsModel patientsModel);

    }
}
