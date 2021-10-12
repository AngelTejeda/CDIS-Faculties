using School_BLL;
using School_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace School.Faculties
{
    public partial class Faculty_u : ThemedPage, IAccess
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
                    DisableForm();
                    return;
                }

                LoadLists();
                LoadFacultyData();
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            UpdateFaculty();
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlState.Items.Clear();
            ddlCity.Items.Clear();
            UpdateStateDropDown();
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCity.Items.Clear();
            UpdateCityDropDown();
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

        private void DisableForm()
        {
            txtCode.Attributes["disabled"] = "disabled";
            txtName.Attributes["disabled"] = "disabled";
            txtFoundationDate.Attributes["disabled"] = "disabled";
            ddlUniversity.Attributes["disabled"] = "disabled";
            ddlCountry.Attributes["disabled"] = "disabled";
            ddlState.Attributes["disabled"] = "disabled";
            ddlCity.Attributes["disabled"] = "disabled";
            lbSubjects.Attributes["disabled"] = "disabled";
            btnEdit.Attributes["disabled"] = "disabled";
        }

        private void LoadLists()
        {
            LoadUniversityDropDown();
            LoadCountryDropDown();
            LoadSubjectsListBox();
        }

        private void LoadFacultyData()
        {
            Faculty dbFaculty = new FacultyBLL().GetById((int)ViewState["facultyId"]);

            // Check if Faculty ID does not exist
            if (dbFaculty == null)
            {
                DisableForm();
                return;
            }

            // Fill form fields
            txtId.Text = dbFaculty.faculty_id.ToString();
            txtCode.Text = dbFaculty.code;
            txtName.Text = dbFaculty.faculty_name;
            txtFoundationDate.Text = dbFaculty.foundation_date.ToString("yyyy'/'MM'/'dd");
            ddlUniversity.SelectedValue = dbFaculty.university_id.ToString();
            ddlCountry.SelectedValue = dbFaculty.City.State.country_id.ToString();
            LoadStateDropDown();
            ddlState.SelectedValue = dbFaculty.City.state_id.ToString();
            LoadCityDropDown();
            ddlCity.SelectedValue = dbFaculty.city_id.ToString();
            SelectSubjectsInListBox();
        }
        #endregion


        #region Methods
        private void UpdateFaculty()
        {
            List<Subject> subjects = GetSelectedSubjectsFromListBox();

            // Fill Faculty object with form fields
            Faculty modifiedFaculty = new Faculty()
            {
                code = txtCode.Text,
                faculty_name = txtName.Text,
                foundation_date = Convert.ToDateTime(txtFoundationDate.Text),
                university_id = int.Parse(ddlUniversity.SelectedValue),
                city_id = int.Parse(ddlCity.SelectedValue),
            };

            // Update the faculty and show a message if it fails.
            try
            {
                new FacultyBLL().Update((int)ViewState["facultyId"], modifiedFaculty, subjects);
            }
            catch (Exception ex) when (ex is BLLValidationException || ex is DALValidationException)
            {
                ThrowAlert("Error", ex.Message, false);

                return;
            }

            ThrowAlert("Alerta", "Facultad Modificada", true);
        }

        private void ThrowAlert(string title, string message, bool goToHome)
        {
            string script = $"alert('{ message }');";

            if (goToHome)
                script += "window.location='faculty_s.aspx';";

            ScriptManager.RegisterStartupScript(this, this.GetType(), title, script, true);
        }
        #endregion


        #region Lists & DropDowns
        private void LoadUniversityDropDown()
        {
            ddlUniversity.DataSource = new UniversityBLL().GetList().ToList();
            ddlUniversity.DataTextField = "university_name";
            ddlUniversity.DataValueField = "university_id";
            ddlUniversity.DataBind();

            ddlUniversity.Items.Insert(0, new ListItem("--- Seleccione Universidad ---", "0"));
        }

        private void LoadCountryDropDown()
        {
            ddlCountry.DataSource = new CountryBLL().GetList().ToList();
            ddlCountry.DataTextField = "country_name";
            ddlCountry.DataValueField = "country_id";
            ddlCountry.DataBind();

            ddlCountry.Items.Insert(0, new ListItem("--- Seleccione Pais ---", "0"));
        }

        private void LoadStateDropDown()
        {
            int countryId = int.Parse(ddlCountry.SelectedValue);

            ddlState.DataSource = new StateBLL().GetListByCountry(countryId).ToList();
            ddlState.DataTextField = "state_name";
            ddlState.DataValueField = "state_id";
            ddlState.DataBind();

            ddlState.Items.Insert(0, new ListItem("--- Seleccione Estado ---", "0"));
        }

        private void LoadCityDropDown()
        {
            int stateId = int.Parse(ddlState.SelectedValue);

            ddlCity.DataSource = new CityBLL().GetListByState(stateId).ToList();
            ddlCity.DataTextField = "city_name";
            ddlCity.DataValueField = "city_id";
            ddlCity.DataBind();

            ddlCity.Items.Insert(0, new ListItem("--- Seleccione Ciudad ---", "0"));
        }

        private void LoadSubjectsListBox()
        {
            lbSubjects.DataSource = new SubjectBLL().GetList().ToList();
            lbSubjects.DataTextField = "subject_name";
            lbSubjects.DataValueField = "subject_id";
            lbSubjects.DataBind();
        }

        private void UpdateStateDropDown()
        {
            if (ddlCountry.SelectedIndex != 0)
            {
                LoadStateDropDown();
                ddlState.Attributes.Remove("disabled");
            }
            else
            {
                ddlState.Items.Insert(0, new ListItem("--- Primero Seleccione un Pais ---", "0"));
                ddlState.Attributes["disabled"] = "disabled";
            }

            UpdateCityDropDown();
        }

        private void UpdateCityDropDown()
        {
            if (ddlState.SelectedIndex != 0)
            {
                LoadCityDropDown();
                ddlCity.Attributes.Remove("disabled");
            }
            else
            {
                ddlCity.Items.Insert(0, new ListItem("--- Primero Seleccione un Estado ---", "0"));
                ddlCity.Attributes["disabled"] = "disabled";
            }
        }

        private void SelectSubjectsInListBox()
        {
            List<FacultySubject> subjects = new FacultySubjectBLL().GetListByFacultyId((int)ViewState["facultyId"]).ToList();

            foreach (FacultySubject subject in subjects)
                lbSubjects.Items.FindByValue(subject.subject_id.ToString()).Selected = true;
        }

        private List<Subject> GetSelectedSubjectsFromListBox()
        {
            List<Subject> subjects = new List<Subject>();

            foreach (ListItem item in lbSubjects.Items)
            {
                if (item.Selected)
                {
                    subjects.Add(new Subject()
                    {
                        subject_id = int.Parse(item.Value)
                    });
                }
            }

            return subjects;
        }
        #endregion
    }
}