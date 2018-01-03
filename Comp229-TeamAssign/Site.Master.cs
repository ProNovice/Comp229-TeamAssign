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
        }

        /// <summary>
        /// Action when LoginBtn is clicked
        /// Check entered username and password if they are matched with Account data in the MovieManiac DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void LoginBtn_Click(object sender, EventArgs e)
        //{
        //    string username = txtUsername.Text;
        //    string inputPassword = txtPassword.Text;
        //    string password = "";
        //    string position = "";

        //    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
        //    {
        //        conn.Open();

        //        // get password of the account
        //        SqlCommand getUserInfo = new SqlCommand(
        //             "SELECT Password, Position FROM Account WHERE UserName = @username;", conn);
        //        getUserInfo.Parameters.AddWithValue("@username", username);
        //        SqlDataReader dr = getUserInfo.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            password = dr["Password"].ToString();
        //            position = dr["Position"].ToString();
        //        }
        //        dr.Close();

        //        conn.Close();
        //    }

        //    if (inputPassword == password)
        //    {
        //        LoginComplete(username, position);
        //        lblLoginFeedback.InnerText = "";
        //    }
        //    else
        //    {
        //        lblLoginFeedback.InnerText = "Username or Password is not matched!";
        //    }
        //}

        /// <summary>
        /// Action when LogoutBtn is clicked while the status is logged in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <summary>
        /// Complete user login
        /// </summary>
        /// <param name="username"></param>
        private void LoginComplete(string username, string position)
        {
            Session["UserName"] = username;
            Session["Login"] = true;
            Session["Position"] = position;
        }

        /// <summary>
        /// Logout the account which is currently logged in
        /// </summary>
        private void Logout()
        {
            if (Session["UserName"] != null)
            {
                string username = Session["UserName"].ToString();
            }

            // remove all session to logout
            Session.RemoveAll();
        }

        /// <summary>
        /// Set visibility upto login status and user position
        /// </summary>
        private void SwitchLoginButtonVisibility()
        {
            // swithch visibility upto the login status
            if (Session["Login"] != null && Session["UserName"] != null && Session["Position"] != null)
            {
                string username = (string)Session["UserName"];
                string position = (string)Session["Position"];
                // check whether Login status is true
                if ((bool)Session["Login"])
                {
                    ulLogin.Visible = false;
                    ulLogout.Visible = true;
                    lblUsername.InnerText = username;

                    // adjust visibility up to user position
                    switch (position)
                    {
                        case "Administrator":
                            btnGoToMovieAddition.Visible = true;
                            break;
                        default:
                            break;
                    }
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