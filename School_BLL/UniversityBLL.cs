using School_DAL;
using System.Linq;

namespace School_BLL
{
    public class UniversityBLL
    {
        public IQueryable<University> GetList()
        {
            return new UniversityDAL().GetList();
        }
    }
}
