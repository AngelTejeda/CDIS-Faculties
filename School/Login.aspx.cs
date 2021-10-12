using School_BLL;
using School_DAL;
using System;
using System.Web.UI;

namespace School
{
    public partial class Login : Page
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            CheckLogin();

            if (Session["User"] != null)
                Response.Redirect("~/Faculty/faculty_s.aspx");
        }
        #endregion

        #region Methods
        public void CheckLogin()
        {
            string username = txtUser.Text;
            string password = txtPassword.Text;

            User dbUser;
            try
            {
                dbUser = new UserBLL().Login(username, password);
            }
            catch (BLLValidationException ex)
            {
                ThrowAlert("Error", ex.Message);
                return;
            }

            Session["User"] = dbUser;
        }

        private void ThrowAlert(string title, string message)
        {
            string script = $"alert('{ message }');";

            ScriptManager.RegisterStartupScript(this, this.GetType(), title, script, true);
        }
        #endregion
    }
}