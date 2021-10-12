using School_DAL;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace School_BLL
{
    public class FacultySubjectBLL
    {
        // Return all entries where faculty_id matches to the argument given.
        public IQueryable<FacultySubject> GetListByFacultyId(int facultyId)
        {
            return new FacultySubjectDAL()
                .GetList()
                .Where(facultySubject => facultySubject.faculty_id == facultyId);
        }

        // Add a single entry with the given values.
        public void Add(int facultyId, int subjectId)
        {
            new FacultySubjectDAL().Add(facultyId, subjectId);
        }

        // Add an entry for each subject in the list. All the entries will have the same faculty_id value.
        public void AddMany(int facultyId, List<Subject> subjects)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                foreach (Subject subject in subjects)
                    Add(facultyId, subject.subject_id);

                ts.Complete();
            }
        }

        // Modify the subjects of asociated to a specific Faculty.
        public void Update(int facultyId, List<Subject> subjects)
        {
            List<FacultySubject> registeredSubjects = GetListByFacultyId(facultyId).ToList();

            using (TransactionScope ts = new TransactionScope())
            {
                DeleteMany(registeredSubjects);
                AddMany(facultyId, subjects);

                ts.Complete();
            }
        }

        // Delete a single entry of the table.
        public void Delete(int facultySubjectId)
        {
            new FacultySubjectDAL().Delete(facultySubjectId);
        }

        // Delete all the entries contained in the list.
        public void DeleteMany(List<FacultySubject> facultySubjects)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                foreach (FacultySubject facultySubject in facultySubjects)
                    Delete(facultySubject.faculty_subject_id);

                ts.Complete();
            }
        }
    }
}
