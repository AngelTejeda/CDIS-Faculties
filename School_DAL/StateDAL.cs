using System.Data;
using System.Linq;

namespace School_DAL
{
    public class StateDAL : BaseDAL
    {
        public IQueryable<State> GetListByCountry(int countryId)
        {
            return DbContext.State
                .Where(state => state.country_id == countryId);
        }
    }
}
