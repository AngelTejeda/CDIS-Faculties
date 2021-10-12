using School_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace School_BLL
{
    public class FacultyBLL
    {
        public IQueryable<Faculty> GetList()
        {
            return new FacultyDAL().GetList();
        }

        public Faculty GetById(int facultyId)
        {
            return new FacultyDAL().GetById(facultyId);
        }

        public Faculty GetByCode(string code)
        {
            return new FacultyDAL()
                .GetList()
                .Where(faculty => faculty.code == code)
                .FirstOrDefault();
        }

        public void Add(Faculty newFaculty)
        {
            ValidateFoundationDate(newFaculty.foundation_date);
            ValidateUniqueCode(newFaculty.code);

            new FacultyDAL().Add(newFaculty);
        }

        // Add a single entry to "Faculty" table and an entry to "FacultySubjects" for each item in the subjects list.
        public void Add(Faculty newFaculty, List<Subject> subjects)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                Add(newFaculty);
                new FacultySubjectBLL().AddMany(newFaculty.faculty_id, subjects);

                ts.Complete();
            }
        }

        public void Update(int facultyId, Faculty modifiedFaculty)
        {
            ValidateFoundationDate(modifiedFaculty.foundation_date);
            ValidateUniqueCode(modifiedFaculty.code, facultyId);

            new FacultyDAL().Update(facultyId, modifiedFaculty);
        }

        // Modify both the Faculty and the subjects asociated.
        public void Update(int facultyId, Faculty modifiedFaculty, List<Subject> subjects)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                Update(facultyId, modifiedFaculty);
                new FacultySubjectBLL().Update(facultyId, subjects);

                ts.Complete();
            }
        }

        public void Delete(int facultyId)
        {
            List<FacultySubject> facultySubjects = new FacultySubjectBLL().GetListByFacultyId(facultyId).ToList();

            using (TransactionScope ts = new TransactionScope())
            {
                new FacultySubjectBLL().DeleteMany(facultySubjects);
                new FacultyDAL().Delete(facultyId);

                ts.Complete();
            }
        }

        #region Validations
        private void ValidateFoundationDate(DateTime foundationDate)
        {
            if (foundationDate.Year < 1900)
                throw new BLLValidationException("Fecha no permitida. Introduzca una fecha mayor a 1900.");

            if (foundationDate.Year > 2010)
                throw new BLLValidationException("Fecha no permitida. Introduzca una fecha menor a 2010.");
        }

        private void ValidateUniqueCode(string code)
        {
            Faculty faculty = GetByCode(code);
            if (faculty != null)
                throw new BLLValidationException("El código de la facultad ya existe. Ingrese un código diferente.");
        }

        private void ValidateUniqueCode(string code, int facultyId)
        {
            Faculty faculty = GetByCode(code);
            if (faculty != null && faculty.faculty_id != facultyId)
                throw new BLLValidationException("El código de la facultad ya existe. Ingrese un código diferente.");
        }
        #endregion
    }
}
