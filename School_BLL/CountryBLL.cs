using School_DAL;
using System.Linq;

namespace School_BLL
{
    public class CountryBLL
    {
        public IQueryable<Country> GetList()
        {
            return new CountryDAL().GetList();
        }
    }
}
