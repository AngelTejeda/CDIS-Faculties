using School_BLL;
using School_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace School.Faculties
{
    public partial class Faculty_d : ThemedPage, IAccess
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsSessionActive())
                    Response.Redirect("~/Login.aspx");

                if (!GetFacultyIdFromQuery())
                {
                    btnDelete.Attributes["disabled"] = "disabled";
                    return;
                }

                LoadSubjectsListBox();
                LoadFacultyData();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteFaculty();
        }
        #endregion


        #region Startup
        public bool IsSessionActive()
        {
            return Session["User"] != null;
        }

        private bool GetFacultyIdFromQuery()
        {
            if (int.TryParse(Request.QueryString["id"], out int facultyId))
                ViewState["facultyId"] = facultyId;

            return ViewState["facultyId"] != null;
        }

        public void LoadFacultyData()
        {
            Faculty dbFaculty = new FacultyBLL().GetById((int)ViewState["facultyId"]);

            // Check if Faculty ID exists
            if (dbFaculty == null)
            {
                btnDelete.Attributes["disabled"] = "disabled";
                return;
            }

            // Fill labels
            lblId.Text = dbFaculty.faculty_id.ToString();
            lblCode.Text = dbFaculty.code;
            lblName.Text = dbFaculty.faculty_name;
            lblFoundationDate.Text = dbFaculty.foundation_date.ToString("yyyy'/'MMMM'/'dd");
            lblUniversity.Text = dbFaculty.University.university_name;
            lblCountry.Text = dbFaculty.City.State.Country.country_name;
            lblState.Text = dbFaculty.City.State.state_name;
            lblCity.Text = dbFaculty.City.city_name;
            SelectSubjectsInListBox();
        }
        #endregion


        #region Methods
        public void DeleteFaculty()
        {
            new FacultyBLL().Delete((int)ViewState["facultyId"]);
            ThrowAlert("Alerta", "Facultad Eliminada.", true);
        }

        private void ThrowAlert(string title, string message, bool goToHome)
        {
            string script = $"alert('{ message }');";

            if (goToHome)
                script += "window.location='faculty_s.aspx';";

            ScriptManager.RegisterStartupScript(this, this.GetType(), title, script, true);
        }

        private void LoadSubjectsListBox()
        {
            lbSubjects.DataSource = new SubjectBLL().GetList().ToList();
            lbSubjects.DataTextField = "subject_name";
            lbSubjects.DataValueField = "subject_id";
            lbSubjects.DataBind();
        }

        private void SelectSubjectsInListBox()
        {
            List<FacultySubject> subjects = new FacultySubjectBLL().GetListByFacultyId((int)ViewState["facultyId"]).ToList();

            foreach (FacultySubject subject in subjects)
                lbSubjects.Items.FindByValue(subject.subject_id.ToString()).Selected = true;
        }
        #endregion
    }
}