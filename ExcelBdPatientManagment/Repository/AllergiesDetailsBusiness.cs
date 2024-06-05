using API.DbContexts;
using API.Repository.Interface;
using Common.Models.DbSet;
using Common.Models;

namespace API.Repository
{
    public class AllergiesDetailsBusiness : IAllergiesDetails
    {
        private readonly AppDbContext _appDbContext;

        public AllergiesDetailsBusiness(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<AllergiesModel> GetByPatientID(long PatientID)
        {
            List<AllergiesModel> model = new List<AllergiesModel>();

            var dt = _appDbContext.AllergiesDetails.Where(m => m.PatientID == PatientID).Join(
                _appDbContext.Allergies,
                t1 => t1.AllergiesID,
                t2 => t2.ID,
                (t1, t2) => new AllergiesModel
                {
                    ID = t2.ID,
                    Name = t2.Name
                }).ToList();


            return dt;

        }

        public string Save(AllergiesDetails allergiesDetails)
        {

            string status = ActionStatus.Success;

            _appDbContext.AllergiesDetails.Update(new AllergiesDetails
            {
                AllergiesID = allergiesDetails.AllergiesID,
                PatientID = allergiesDetails.PatientID,
                EntryUser = allergiesDetails.EntryUser,
                EntryDate = allergiesDetails.EntryDate,
                UpdateUser = allergiesDetails.UpdateUser,
                UpdateDate = allergiesDetails.UpdateDate,
            });


            if (_appDbContext.SaveChanges() == 0)
            {
                status = ActionStatus.Fail;
            };

            return status;

        }
    }
}
