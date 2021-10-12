namespace School_DAL
{
    public class BaseDAL
    {
        protected SchoolEntities DbContext;

        protected BaseDAL()
        {
            DbContext = new SchoolEntities();
        }
    }
}
