using System.Linq;

namespace School_DAL
{
    public class UniversityDAL : BaseDAL
    {
        public IQueryable<University> GetList()
        {
            return DbContext.University;
        }
    }
}
