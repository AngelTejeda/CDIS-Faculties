using School_BLL;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace School.Faculties
{
    public partial class Faculty_s : ThemedPage, IAccess
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsSessionActive())
                    Response.Redirect("~/Login.aspx");

                LoadFaculties();
            }
        }

        protected void grdFaculties_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
                Response.Redirect("~/Faculty/faculty_u.aspx?id=" + e.CommandArgument);
            else if (e.CommandName == "Delete")
                Response.Redirect("~/Faculty/faculty_d.aspx?id=" + e.CommandArgument);
            else
                throw new NotImplementedException();
        }
        #endregion

        #region Methods
        public void LoadFaculties()
        {
            var faculties = new FacultyBLL()
                .GetList()
                .Select(faculty => new
                {
                    faculty.faculty_id,
                    faculty.code,
                    faculty.faculty_name,
                    faculty.foundation_date,
                    faculty.University.university_name,
                    faculty.City.State.Country.country_name,
                    faculty.City.State.state_name,
                    faculty.City.city_name
                });

            grdFaculties.DataSource = faculties.ToList();
            grdFaculties.DataBind();

            // Set <thead> tag
            if (faculties.Count() > 0)
                grdFaculties.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        public bool IsSessionActive()
        {
            return Session["User"] != null;
        }
        #endregion
    }
}