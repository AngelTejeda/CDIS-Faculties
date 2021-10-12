using School_DAL;
using System.Linq;

namespace School_BLL
{
    public class SubjectBLL
    {
        public IQueryable<Subject> GetList()
        {
            return new SubjectDAL().GetList();
        }
    }
}
