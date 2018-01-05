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
                string memberID = GetMemberID();
                int loanedCount = GetLoanedCount(memberID);

                // swithch visibility upto the login status
                ulLogin.Visible = false;
                ulLogout.Visible = true;


                lblUsername.InnerText = "Welcome, " + username + ". Loaned count: " + loanedCount;

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

        /// <summary>
        /// Return LoanedCount of the member
        /// </summary>
        /// <param name="memberID"></param>
        private int GetLoanedCount(string memberID)
        {
            int count = 0;

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
            {
                conn.Open();

                // get a Count of all loaned movies of the user
                SqlCommand getLoanedCount = new SqlCommand(
                    "SELECT COUNT(*) FROM RelatedMovie WHERE MemberID = @MemberID AND Status = 'loaned';", conn);
                getLoanedCount.Parameters.AddWithValue("@MemberID", memberID);
                count = (int)getLoanedCount.ExecuteScalar();

                conn.Close();
            }

            return count;
        }

        /// <summary>
        /// get memberID from DB
        /// if there is no match, return "".
        /// </summary>
        /// <returns></returns>
        private string GetMemberID()
        {
            string memberID = "";

            if (Page.User.Identity.IsAuthenticated)
            {
                string username = Page.User.Identity.Name;

                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
                {
                    conn.Open();

                    SqlCommand getMemberID = new SqlCommand(
                        "SELECT MemberID FROM Account WHERE UserName = @UserName;", conn);
                    getMemberID.Parameters.AddWithValue("@UserName", username);
                    memberID = getMemberID.ExecuteScalar().ToString();

                    conn.Close();
                }

            }
            return memberID;
        }
    }
}