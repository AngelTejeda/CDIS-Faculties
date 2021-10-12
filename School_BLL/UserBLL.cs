using School_DAL;
using System.Linq;

namespace School_BLL
{
    public class UserBLL
    {
        public User Login(string username, string password)
        {
            new UserDAL().VerifyExistence(username, out User dbUser);

            if (dbUser.password != password)
                throw new BLLValidationException("Contraseña Inválida.");

            return dbUser;
        }
    }
}
