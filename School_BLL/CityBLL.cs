using School_DAL;
using System.Linq;

namespace School_BLL
{
    public class CityBLL
    {
        public IQueryable<City> GetListByState(int stateId)
        {
            return new CityDAL()
                .GetList()
                .Where(city => city.state_id == stateId);
        }
    }
}
