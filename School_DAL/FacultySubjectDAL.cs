using System.Linq;

namespace School_DAL
{
    public class FacultySubjectDAL : BaseDAL
    {
        public IQueryable<FacultySubject> GetList()
        {
            return DbContext.FacultySubject;
        }

        public FacultySubject GetById(int facultySubjectId)
        {
            return GetList()
                .Where(facultySubject => facultySubject.faculty_subject_id == facultySubjectId)
                .FirstOrDefault();
        }

        public void Add(int facultyId, int subjectId)
        {
            new FacultyDAL().VerifyExistence(facultyId, out _);
            new SubjectDAL().VerifyExistence(subjectId, out _);

            FacultySubject newFacultySubject = new FacultySubject()
            {
                faculty_id = facultyId,
                subject_id = subjectId
            };

            DbContext.FacultySubject.Add(newFacultySubject);
            DbContext.SaveChanges();
        }

        public void Delete(int facultySubjectId)
        {
            VerifyExistence(facultySubjectId, out FacultySubject dbFacultySubject);

            DbContext.FacultySubject.Remove(dbFacultySubject);
            DbContext.SaveChanges();
        }

        #region Validations
        public void VerifyExistence(int subjectId, out FacultySubject dbFacultySubject)
        {
            dbFacultySubject = GetById(subjectId);

            if (dbFacultySubject == null)
                throw new DALValidationException("El ID ingresado no existe en la Base de Datos.");
        }
        #endregion
    }
}
