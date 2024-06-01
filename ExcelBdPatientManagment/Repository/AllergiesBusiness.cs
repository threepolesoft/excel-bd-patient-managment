using API.DbContexts;
using API.Repository.Interface;
using Common.Models.DbSet;
using Common.Models;
using System.Xml.Linq;

namespace API.Repository
{
    public class AllergiesBusiness: IAllergies
    {
        private readonly AppDbContext _appDbContext;

        public AllergiesBusiness(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public List<Allergies> GetAll()
        {
            return _appDbContext.Allergies.ToList();
        }

        public Allergies GetAllergiesByID(long ID)
        {

            return _appDbContext.Allergies.Where(m => m.ID == ID).FirstOrDefault();

        }

        public Allergies GetAllergiesByName(string Name)
        {
            return _appDbContext.Allergies.Where(m => m.Name == Name).FirstOrDefault();
        }

        public string Save(Allergies allergies)
        {

            string status = ActionStatus.Success;

            if (allergies.ID > 0)
            {
                if (GetAllergiesByName(allergies.Name) == null)
                {
                    Allergies allergies1 = GetAllergiesByID(allergies.ID);

                    allergies1.Name = allergies.Name;
                    allergies1.UpdateDate = DateTime.Now;

                    _appDbContext.Update(allergies1);

                    if (_appDbContext.SaveChanges() == 0)
                    {
                        status = ActionStatus.Fail;
                    };
                }
                else
                {
                    status = "This Allergies already exsit";
                }
            }
            else
            {
                if (GetAllergiesByName(allergies.Name) == null)
                {
                    allergies.UpdateDate = null;
                    allergies.UpdateUser = null;

                    _appDbContext.Add(allergies);

                    if (_appDbContext.SaveChanges() == 0)
                    {
                        status = ActionStatus.Fail;
                    };

                }
                else
                {
                    status = "This Allergies already exsit";
                }

            }

            return status;

        }

        public bool Delete(long ID)
        {
            _appDbContext.Allergies.Remove(GetAllergiesByID(ID));

            if (_appDbContext.SaveChanges() == 1)
            {
                return true;
            }

            return false;
        }
    }
}
