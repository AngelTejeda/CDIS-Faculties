using System.Linq;

namespace School_DAL
{
    public class CityDAL : BaseDAL
    {
        public IQueryable<City> GetList()
        {
            return DbContext.City;
        }
    }
}
