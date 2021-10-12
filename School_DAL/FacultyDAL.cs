using System.Data;
using System.Linq;

namespace School_DAL
{
    public class FacultyDAL : BaseDAL
    {
        public IQueryable<Faculty> GetList()
        {
            return DbContext.Faculty;
        }

        public Faculty GetById(int facultyId)
        {
            return GetList()
                .Where(faculty => faculty.faculty_id == facultyId)
                .FirstOrDefault();
        }

        public void Add(Faculty newFaculty)
        {
            DbContext.Faculty.Add(newFaculty);
            DbContext.SaveChanges();
        }

        public void Update(int facultyId, Faculty modifiedFaculty)
        {
            VerifyExistence(facultyId, out Faculty dbFaculty);

            // Update the faculty values.
            dbFaculty.code = modifiedFaculty.code;
            dbFaculty.faculty_name = modifiedFaculty.faculty_name;
            dbFaculty.foundation_date = modifiedFaculty.foundation_date;
            dbFaculty.university_id = modifiedFaculty.university_id;
            dbFaculty.city_id = modifiedFaculty.city_id;

            DbContext.SaveChanges();
        }

        public void Delete(int facultyId)
        {
            VerifyExistence(facultyId, out Faculty dbFaculty);

            DbContext.Faculty.Remove(dbFaculty);
            DbContext.SaveChanges();
        }

        #region Validations
        public void VerifyExistence(int facultyId, out Faculty dbFaculty)
        {
            dbFaculty = GetById(facultyId);

            if (dbFaculty == null)
                throw new DALValidationException("El ID de la Facultad no existe en la Base de Datos.");
        }
        #endregion
    }
}
