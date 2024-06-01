using API.DbContexts;
using API.Repository.Interface;
using Common.Models;
using Common.Models.DbSet;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class NCDBusiness : INCD
    {
        private readonly AppDbContext _appDbContext;

        public NCDBusiness(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<NCDModel> GetAll()
        {
            return _appDbContext.NCD.Select(m=>new NCDModel
            {
                ID = m.ID,
                Name = m.Name,
                EntryUser = m.EntryUser,
                EntryDate = m.EntryDate,
                UpdateUser = m.UpdateUser,
                UpdateDate = m.UpdateDate,
            }).ToList();
        }

        public NCDModel GetNCDByID(long ID)
        {

            return _appDbContext.NCD.Where(m => m.ID == ID).Select(m => new NCDModel
            {
                ID = m.ID,
                Name = m.Name,
                EntryUser = m.EntryUser,
                EntryDate = m.EntryDate,
                UpdateUser = m.UpdateUser,
                UpdateDate = m.UpdateDate,
            }).FirstOrDefault();

        }

        public NCDModel GetNCDByName(string Name)
        {
            return _appDbContext.NCD.Where(m => m.Name == Name).Select(m => new NCDModel
            {
                ID = m.ID,
                Name = m.Name,
                EntryUser = m.EntryUser,
                EntryDate = m.EntryDate,
                UpdateUser = m.UpdateUser,
                UpdateDate = m.UpdateDate,
            }).FirstOrDefault();
        }

        public string Save(NCDModel nCD)
        {

            string status = ActionStatus.Success;

            if (nCD.ID > 0)
            {
                if (GetNCDByName(nCD.Name) == null)
                {
                    NCDModel nCD1 = GetNCDByID(nCD.ID);

                    nCD1.Name = nCD.Name;
                    nCD1.UpdateDate = DateTime.Now;

                    _appDbContext.NCD.Update(new NCD
                    {
                        ID = nCD1.ID,
                        Name = nCD1.Name,
                        EntryUser = nCD1.EntryUser,
                        EntryDate = nCD1.EntryDate,
                        UpdateUser = nCD1.UpdateUser,
                        UpdateDate = nCD1.UpdateDate,
                    });

                    if (_appDbContext.SaveChanges() == 0)
                    {
                        status = ActionStatus.Fail;
                    };
                }
                else
                {
                    status = "This NCD already exsit";
                }
            }
            else
            {
                if (GetNCDByName(nCD.Name) == null)
                {
                    nCD.UpdateDate = null;
                    nCD.UpdateUser = null;

                    _appDbContext.NCD.Add(new NCD
                    {
                        ID = nCD.ID,
                        Name = nCD.Name,
                        EntryUser = nCD.EntryUser,
                        EntryDate = nCD.EntryDate,
                        UpdateUser = nCD.UpdateUser,
                        UpdateDate = nCD.UpdateDate,
                    });

                    if (_appDbContext.SaveChanges() == 0)
                    {
                        status = ActionStatus.Fail;
                    };

                }
                else
                {
                    status = "This NCD already exsit";
                }

            }

            return status;

        }

        public bool Delete(long ID)
        {

            NCDModel model = GetNCDByID(ID);

            if (model!=null)
            {
                _appDbContext.NCD.Remove(new NCD
                {
                    ID = model.ID,
                    Name = model.Name,
                    EntryUser = model.EntryUser,
                    EntryDate = model.EntryDate,
                    UpdateUser = model.UpdateUser,
                    UpdateDate = model.UpdateDate,
                });
            }

            if (_appDbContext.SaveChanges()==1)
            {
                return true;
            }

            return false;
        }
    }
}
