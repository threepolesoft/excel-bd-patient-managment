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


        public List<AllergiesModel> GetAll()
        {
            return _appDbContext.Allergies.Select(m=>new AllergiesModel
            {
                ID = m.ID,
                Name = m.Name,
                EntryUser = m.EntryUser,
                EntryDate = m.EntryDate,
                UpdateUser = m.UpdateUser,
                UpdateDate = m.UpdateDate,  
            }).ToList();
        }

        public AllergiesModel GetAllergiesByID(long ID)
        {

            return _appDbContext.Allergies.Where(m => m.ID == ID).Select(m=>new AllergiesModel
            {
                ID = m.ID,
                Name = m.Name,
                EntryUser = m.EntryUser,
                EntryDate = m.EntryDate,
                UpdateUser = m.UpdateUser,
                UpdateDate = m.UpdateDate,
            }).FirstOrDefault();

        }

        public AllergiesModel GetAllergiesByName(string Name)
        {
            return _appDbContext.Allergies.Where(m => m.Name == Name).Select(m => new AllergiesModel
            {
                ID = m.ID,
                Name = m.Name,
                EntryUser = m.EntryUser,
                EntryDate = m.EntryDate,
                UpdateUser = m.UpdateUser,
                UpdateDate = m.UpdateDate,
            }).FirstOrDefault();
        }

        public string Save(AllergiesModel allergies)
        {

            string status = ActionStatus.Success;

            if (allergies.ID > 0)
            {
                if (GetAllergiesByName(allergies.Name) == null)
                {
                    AllergiesModel allergies1 = GetAllergiesByID(allergies.ID);

                    allergies1.Name = allergies.Name;
                    allergies1.UpdateDate = DateTime.Now;

                    _appDbContext.Allergies.Update(new Allergies
                    {
                        ID = allergies1.ID,
                        Name = allergies1.Name,
                        EntryUser = allergies1.EntryUser,
                        EntryDate = allergies1.EntryDate,
                        UpdateUser = allergies1.UpdateUser,
                        UpdateDate = allergies1.UpdateDate,
                    });

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

                    _appDbContext.Allergies.Add(new Allergies
                    {
                        ID = allergies.ID,
                        Name = allergies.Name,
                        EntryUser = allergies.EntryUser,
                        EntryDate = allergies.EntryDate,
                        UpdateUser = allergies.UpdateUser,
                        UpdateDate = allergies.UpdateDate,
                    });

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

            AllergiesModel allergiesModel = GetAllergiesByID(ID);

            if (allergiesModel!=null)
            {
                _appDbContext.Allergies.Remove(new Allergies
                {
                    ID = allergiesModel.ID,
                    Name = allergiesModel.Name,
                    EntryUser = allergiesModel.EntryUser,
                    EntryDate = allergiesModel.EntryDate,
                    UpdateUser = allergiesModel.UpdateUser,
                    UpdateDate = allergiesModel.UpdateDate,
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
