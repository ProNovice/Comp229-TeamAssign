using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comp229_TeamAssign
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SwitchLoginButtonVisibility();
        }


        /// <summary>
        /// Try user login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            // Check the web.config for the user
            if (FormsAuthentication.Authenticate(txtUsername.Text, txtPassword.Text))
            {
                FormsAuthentication.RedirectFromLoginPage(txtUsername.Text, false);
            }
        }

        /// <summary>
        /// Logout the logged in user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        /// <summary>
        /// Set visibility upto login status and user position
        /// </summary>
        private void SwitchLoginButtonVisibility()
        {
            if (Page.User != null && Page.User.Identity.IsAuthenticated)
            {
                string username = Page.User.Identity.Name;

                // swithch visibility upto the login status
                ulLogin.Visible = false;
                ulLogout.Visible = true;
                lblUsername.InnerText = "Welcome, " + username;

                if (username == "Movie Maniac")
                {
                    btnGoToMovieAddition.Visible = true;
                }
            }
            else
            {
                ulLogin.Visible = true;
                ulLogout.Visible = false;
            }
        }
    }
}