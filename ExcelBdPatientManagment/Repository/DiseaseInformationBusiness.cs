using API.DbContexts;
using API.Repository.Interface;
using Common.Models.DbSet;
using Common.Models;

namespace API.Repository
{
    public class DiseaseInformationBusiness : IDiseaseInformation
    {
        private readonly AppDbContext _appDbContext;


        public DiseaseInformationBusiness(
            AppDbContext appDbContext
            )
        {
            _appDbContext = appDbContext;
        }

        public List<DiseaseInformationModel> GetAll()
        {
            return _appDbContext.DiseaseInformation.Select(m => new DiseaseInformationModel
            {
                ID = m.ID,
                Name = m.Name,
                EntryUser = m.EntryUser,
                EntryDate = m.EntryDate,
                UpdateUser = m.UpdateUser,
                UpdateDate = m.UpdateDate,
            }).ToList();
        }

        public DiseaseInformationModel GetDiseaseByID(long ID)
        {

            return _appDbContext.DiseaseInformation.Where(m => m.ID == ID).Select(m => new DiseaseInformationModel
            {
                ID = m.ID,
                Name = m.Name,
                EntryUser = m.EntryUser,
                EntryDate = m.EntryDate,
                UpdateUser = m.UpdateUser,
                UpdateDate = m.UpdateDate,
            }).FirstOrDefault();

        }

        public DiseaseInformationModel GetDiseaseByName(string Name)
        {
            return _appDbContext.DiseaseInformation.Where(m => m.Name == Name).Select(m => new DiseaseInformationModel
            {
                ID = m.ID,
                Name = m.Name,
                EntryUser = m.EntryUser,
                EntryDate = m.EntryDate,
                UpdateUser = m.UpdateUser,
                UpdateDate = m.UpdateDate,
            }).FirstOrDefault();
        }

        public string Save(DiseaseInformationModel diseaseInformationModel)
        {

            string status = ActionStatus.Success;

            if (diseaseInformationModel.ID > 0)
            {
                if (GetDiseaseByName(diseaseInformationModel.Name) == null)
                {
                    DiseaseInformationModel diseaseInformationModel1 = GetDiseaseByID(diseaseInformationModel.ID);

                    diseaseInformationModel1.Name = diseaseInformationModel.Name;
                    diseaseInformationModel1.UpdateDate = DateTime.Now;

                    _appDbContext.DiseaseInformation.Update(new DiseaseInformation
                    {
                        ID = diseaseInformationModel1.ID,
                        Name = diseaseInformationModel1.Name,
                        EntryUser = diseaseInformationModel1.EntryUser,
                        EntryDate = diseaseInformationModel1.EntryDate,
                        UpdateUser = diseaseInformationModel1.UpdateUser,
                        UpdateDate = diseaseInformationModel1.UpdateDate,
                    });

                    if (_appDbContext.SaveChanges() == 0)
                    {
                        status = ActionStatus.Fail;
                    };
                }
                else
                {
                    status = "This Disease already exsit";
                }
            }
            else
            {
                if (GetDiseaseByName(diseaseInformationModel.Name) == null)
                {
                    diseaseInformationModel.UpdateDate = null;
                    diseaseInformationModel.UpdateUser = null;

                    _appDbContext.DiseaseInformation.Add(new DiseaseInformation
                    {
                        ID = diseaseInformationModel.ID,
                        Name = diseaseInformationModel.Name,
                        EntryUser = diseaseInformationModel.EntryUser,
                        EntryDate = diseaseInformationModel.EntryDate,
                        UpdateUser = diseaseInformationModel.UpdateUser,
                        UpdateDate = diseaseInformationModel.UpdateDate,
                    });

                    if (_appDbContext.SaveChanges() == 0)
                    {
                        status = ActionStatus.Fail;
                    };

                }
                else
                {
                    status = "This Disease already exsit";
                }

            }

            return status;

        }

        public bool Delete(long ID)
        {

            DiseaseInformationModel diseaseInformationModel = GetDiseaseByID(ID);

            if (diseaseInformationModel != null)
            {
                _appDbContext.DiseaseInformation.Remove(new DiseaseInformation
                {
                    ID = diseaseInformationModel.ID,
                    Name = diseaseInformationModel.Name,
                    EntryUser = diseaseInformationModel.EntryUser,
                    EntryDate = diseaseInformationModel.EntryDate,
                    UpdateUser = diseaseInformationModel.UpdateUser,
                    UpdateDate = diseaseInformationModel.UpdateDate,
                });
            }



            if (_appDbContext.SaveChanges() == 1)
            {
                return true;
            }

            return false;
        }
    }
}
