using Common.Models;

namespace API.Repository.Interface
{
    public interface IDiseaseInformation
    {
        List<DiseaseInformationModel> GetAll();
        DiseaseInformationModel GetDiseaseByID(long ID);
        DiseaseInformationModel GetDiseaseByName(string Name);
        string Save(DiseaseInformationModel diseaseInformationModel);
        bool Delete(long ID);
    }
}
