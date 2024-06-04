using API.DbContexts;
using API.Repository.Interface;
using Common.Models.DbSet;
using Common.Models;

namespace API.Repository
{
    public class NCDDetailsBusiness : INCDDetails
    {
        private readonly AppDbContext _appDbContext;

        public NCDDetailsBusiness(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public string Save(NCDDetailsModel nCDDetailsModel)
        {

            string status = ActionStatus.Success;

            _appDbContext.NCDDetails.Update(new NCDDetails
            {
                NCDID = nCDDetailsModel.NCDID,
                PatientID = nCDDetailsModel.PatientID,
                EntryUser = nCDDetailsModel.EntryUser,
                EntryDate = nCDDetailsModel.EntryDate,
                UpdateUser = nCDDetailsModel.UpdateUser,
                UpdateDate = nCDDetailsModel.UpdateDate,
            });



            if (_appDbContext.SaveChanges() == 0)
            {
                status = ActionStatus.Fail;
            };

            return status;

        }
    }
}
