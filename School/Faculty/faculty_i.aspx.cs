using School_BLL;
using School_DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace School.Faculties
{
    public partial class Faculty_i : ThemedPage, IAccess
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            SetTheadTag();

            if (!IsPostBack)
            {
                if (!IsSessionActive())
                    Response.Redirect("~/Login.aspx");

                LoadViewStateTable();
                LoadLists();
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddFaculty();
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
        private void SetTheadTag()
        {
            if (ViewState["registeredFaculties"] != null)
            {
                DataTable registeredFaculties = (DataTable)ViewState["registeredFaculties"];

                // Set <thead> tag
                if (registeredFaculties.Rows.Count > 0)
                    grdFaculties.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        public bool IsSessionActive()
        {
            return Session["User"] != null;
        }

        private void LoadViewStateTable()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("code");
            dataTable.Columns.Add("faculty_name");

            ViewState["registeredFaculties"] = dataTable;
        }

        private void LoadLists()
        {
            LoadUniversityDropDown();
            LoadCountryDropDown();
            LoadSubjectsListBox();

            UpdateStateDropDown();
        }
        #endregion


        #region Methods
        private void AddFaculty()
        {
            List<Subject> subjects = GetSelectedSubjectsFromListBox();

            // Fill Faculty object with form fields
            Faculty newFaculty = new Faculty()
            {
                code = txtCode.Text,
                faculty_name = txtName.Text,
                foundation_date = Convert.ToDateTime(txtFoundationDate.Text),
                university_id = int.Parse(ddlUniversity.SelectedValue),
                city_id = int.Parse(ddlCity.SelectedValue),
            };

            // Add the new faculty and show a message if it fails.
            try
            {
                new FacultyBLL().Add(newFaculty, subjects);
            }
            catch (Exception ex) when (ex is BLLValidationException || ex is DALValidationException)
            {
                ThrowAlert("Error", ex.Message, false);

                return;
            }

            ClearFields();
            AddEntryToViewStateTable(newFaculty);
            ThrowAlert("Alerta", "Facultad Añadidad", false);
        }

        private void AddEntryToViewStateTable(Faculty newFaculty)
        {
            DataTable dtFacultades = (DataTable)ViewState["registeredFaculties"];
            dtFacultades.Rows.Add(newFaculty.code, newFaculty.faculty_name);

            grdFaculties.DataSource = dtFacultades;
            grdFaculties.DataBind();

            grdFaculties.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        private void ClearFields()
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtFoundationDate.Text = "";
            ddlUniversity.SelectedValue = "0";
            ddlCountry.SelectedValue = "0";

            ddlState.Items.Clear();
            ddlCity.Items.Clear();
            lbSubjects.ClearSelection();

            UpdateStateDropDown();
        }

        private void ThrowAlert(string title, string message, bool goToHome)
        {
            string script = $"alert('{ message }');";

            if (goToHome)
                script += "window.location='faculty_s.aspx';";

            ScriptManager.RegisterStartupScript(this, this.GetType(), title, script, true);
        }
        #endregion


        #region Lists & Dropdowns
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
            ddlState.DataSource = new StateBLL().GetListByCountry(1).ToList();
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