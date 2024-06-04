using API.DbContexts;
using API.Repository.Interface;
using Common.Models.DbSet;
using Common.Models;

namespace API.Repository
{
    public class PatientBusiness : IPatient
    {
        private readonly AppDbContext _appDbContext;
        private readonly IAllergiesDetails _allergiesDetails;
        private readonly INCDDetails _indcdDetails;

        public PatientBusiness(
            AppDbContext appDbContext,
            IAllergiesDetails allergiesDetails,
            INCDDetails indcdDetails)
        {
            _appDbContext = appDbContext;
            this._allergiesDetails = allergiesDetails;
            this._indcdDetails = indcdDetails;
        }

        public long GetID()
        {
            return _appDbContext.Patients.Count() + 1;
        }


        public List<PatientsModel> GetAll()
        {

            return _appDbContext.Patients.Select(m => new PatientsModel
            {
                ID = m.ID,
                PatientName = m.PatientName,
                DiseaseInformationID = m.DiseaseInformationID,
                EntryUser = m.EntryUser,
                EntryDate = m.EntryDate,
                UpdateUser = m.UpdateUser,
                UpdateDate = m.UpdateDate,
            }).ToList();
        }

        public string Save(PatientsModel patientsModel)
        {

            string status = ActionStatus.Success;

            Patients patient = new Patients
            {
                PatientName = patientsModel.PatientName,
                DiseaseInformationID = patientsModel.DiseaseInformationID,
                EntryUser = patientsModel.EntryUser,
                EntryDate = patientsModel.EntryDate,
            };

           _appDbContext.Patients.Add(patient);
            if (_appDbContext.SaveChanges() == 0)
            {
                status = ActionStatus.Fail;
            };

            foreach (var item in patientsModel.OthersNCDs)
            {

                _indcdDetails.Save(new NCDDetailsModel
                {
                    PatientID = patient.ID,
                    NCDID = item,
                    EntryUser = patientsModel.EntryUser,
                    EntryDate = patientsModel.EntryDate,
                });

                _allergiesDetails.Save(new AllergiesDetails
                {
                    PatientID = patient.ID,
                    AllergiesID = item,
                    EntryUser = patientsModel.EntryUser,
                    EntryDate = patientsModel.EntryDate,
                });

            }

            

            return status;

        }
    }
}
