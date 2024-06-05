using Common.Models;

namespace API.Repository.Interface
{
    public interface INCDDetails
    {
        List<NCDModel> GetByPatientID(long PatientID);
        string Save(NCDDetailsModel nCDDetailsModel);
    }
}
