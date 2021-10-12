using System.Linq;

namespace School_DAL
{
    public class UserDAL : BaseDAL
    {
        public IQueryable<User> GetList()
        {
            return DbContext.User;
        }

        #region Validations
        public void VerifyExistence(string username, out User dbUser)
        {
            dbUser = GetList()
                .Where(user => user.username == username)
                .FirstOrDefault();

            if (dbUser == null)
                throw new DALValidationException("El usuario ingresado no existe.");
        }
        #endregion
    }
}
