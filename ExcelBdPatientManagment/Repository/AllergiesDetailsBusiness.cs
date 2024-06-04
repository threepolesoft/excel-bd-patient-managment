using API.DbContexts;
using API.Repository.Interface;
using Common.Models.DbSet;
using Common.Models;

namespace API.Repository
{
    public class AllergiesDetailsBusiness: IAllergiesDetails
    {
        private readonly AppDbContext _appDbContext;

        public AllergiesDetailsBusiness(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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
