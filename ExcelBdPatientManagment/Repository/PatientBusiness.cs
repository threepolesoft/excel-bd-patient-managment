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
        private readonly IDiseaseInformation _iseaseInformation;

        public PatientBusiness(
            AppDbContext appDbContext,
            IAllergiesDetails allergiesDetails,
            INCDDetails indcdDetails,
            IDiseaseInformation diseaseInformation
            )
        {
            _appDbContext = appDbContext;
            this._allergiesDetails = allergiesDetails;
            this._indcdDetails = indcdDetails;
            this._iseaseInformation = diseaseInformation;
        }

        public PatientsModel GetByPatientID(long PatientID)
        {
            PatientsModel patientsModel = new PatientsModel();
            Patients patients = _appDbContext.Patients.Where(m => m.ID == PatientID).ToList().FirstOrDefault();

            if (patients != null)
            {
                patientsModel = new PatientsModel
                {
                    ID = patients.ID,
                    PatientName = patients.PatientName,
                    DiseaseInformationID = patients.DiseaseInformationID,
                    EntryUser = patients.EntryUser,
                    EntryDate = patients.EntryDate,
                    UpdateUser = patients.UpdateUser,
                    UpdateDate = patients.UpdateDate,
                };
            }

            return patientsModel;

        }

        public PatientDetailsModel GetPatientDetails(long ID)
        {
            PatientDetailsModel patientDetailsModel = new PatientDetailsModel();

            PatientsModel patientsModel = GetByPatientID(ID);

            if (patientsModel != null)
            {

                var diseaseInformation = _iseaseInformation.GetDiseaseByID(patientsModel.DiseaseInformationID);
                var OthersNCDs=_indcdDetails.GetByPatientID(ID);
                var Allergies = _allergiesDetails.GetByPatientID(ID);

                patientDetailsModel = new PatientDetailsModel
                {
                    Disease = diseaseInformation != null ? diseaseInformation.Name : "",
                    Epilepsy=patientsModel.Epilepsy,
                    OthersNCDs=OthersNCDs!=null? OthersNCDs.ToList(): new List<NCDModel>(),
                    Allergies= Allergies != null? Allergies.ToList(): new List<AllergiesModel>(),
                };
            }

            return patientDetailsModel;

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
