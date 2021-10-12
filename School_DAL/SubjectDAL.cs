using System.Linq;

namespace School_DAL
{
    public class SubjectDAL : BaseDAL
    {
        public IQueryable<Subject> GetList()
        {
            return DbContext.Subject;
        }

        public Subject GetById(int subjectId)
        {
            return GetList()
                .Where(subject => subject.subject_id == subjectId)
                .FirstOrDefault();
        }

        #region Validations
        public void VerifyExistence(int subjectId, out Subject dbSubject)
        {
            dbSubject = GetById(subjectId);

            if (dbSubject == null)
                throw new DALValidationException("El ID de la Materia no existe en la Base de Datos.");
        }
        #endregion
    }
}
