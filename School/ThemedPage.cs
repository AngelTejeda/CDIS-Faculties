using School_DAL;
using System;
using System.Web.UI;

namespace School
{
    public class ThemedPage : Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                User dbUser = (User)Session["User"];

                string userType = dbUser.user_type;

                switch (userType)
                {
                    case "Administrator":
                        Page.Theme = "AdministratorTheme";
                        break;
                    case "Teacher":
                        Page.Theme = "TeacherTheme";
                        break;
                    default:
                        Page.Theme = "DefaultTheme";
                        break;
                }
            }
        }
    }
}