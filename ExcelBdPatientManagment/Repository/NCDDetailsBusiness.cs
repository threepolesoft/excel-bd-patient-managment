using API.DbContexts;
using API.Repository.Interface;
using Common.Models.DbSet;
using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class NCDDetailsBusiness : INCDDetails
    {
        private readonly AppDbContext _appDbContext;

        public NCDDetailsBusiness(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<NCDModel> GetByPatientID(long PatientID)
        {


            List<NCDModel> model = new List<NCDModel>();

            var Ncd = _appDbContext.NCDDetails.Where(m => m.PatientID == PatientID).Join(
                _appDbContext.NCD,
                nd1 => nd1.NCDID,
                nc1 => nc1.ID,
                (nd1, nc1) => new NCDModel
                {
                    ID=nc1.ID,
                    Name=nc1.Name
                }).ToList();


            return Ncd;
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
