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

        public List<NCD> GetAll()
        {
            return _appDbContext.NCD.ToList();
        }

        public NCD GetNCDByID(long ID)
        {

            return _appDbContext.NCD.Where(m => m.ID == ID).FirstOrDefault();

        }

        public NCD GetNCDByName(string Name)
        {
            return _appDbContext.NCD.Where(m => m.Name == Name).FirstOrDefault();
        }

        public string Save(NCD nCD)
        {

            string status = ActionStatus.Success;

            if (nCD.ID > 0)
            {
                if (GetNCDByName(nCD.Name) == null)
                {
                    NCD nCD1 = GetNCDByID(nCD.ID);

                    nCD1.Name = nCD.Name;
                    nCD1.UpdateDate = DateTime.Now;

                    _appDbContext.Update(nCD1);

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

                    _appDbContext.Add(nCD);

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
            _appDbContext.NCD.Remove(GetNCDByID(ID));

            if (_appDbContext.SaveChanges()==1)
            {
                return true;
            }

            return false;
        }
    }
}
