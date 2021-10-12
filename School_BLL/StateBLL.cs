using School_DAL;
using System.Linq;

namespace School_BLL
{
    public class StateBLL
    {
        public IQueryable<State> GetListByCountry(int countryId)
        {
            return new StateDAL().GetListByCountry(countryId);
        }
    }
}
