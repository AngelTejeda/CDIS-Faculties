using System.Linq;

namespace School_DAL
{
    public class CountryDAL : BaseDAL
    {
        public IQueryable<Country> GetList()
        {
            return DbContext.Country;
        }
    }
}
