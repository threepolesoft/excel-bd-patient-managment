using Common.Models.DbSet;

namespace API.Repository.Interface
{
    public interface INCD
    {
        NCD GetNCDByID(long ID);
        NCD GetNCDByName(string Name);
        string Save(NCD nCD);
        List<NCD> GetAll();
        bool Delete(long ID);
    }
}
