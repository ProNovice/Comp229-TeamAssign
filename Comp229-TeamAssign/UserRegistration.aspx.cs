using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comp229_TeamAssign
{
    public partial class UserRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // this page is for unregistered users only. redirect Main page to logged in users
            if (CheckLoginStatus())
            {
                Response.Redirect("MainTracking.aspx");
            }
        }

        /// <summary>
        /// Action of Registration form submit action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void submitBtn_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string confirmPW = txtConfirmPassword.Text;
            string responseMessage = "";
            string responseOpener = "<script>alert('";
            string responseCloser = "');</script>";
            string notSamePW = "Passwords are not matched. ";
            string usernameOverlaped = "The username already exists. Please choose another name. ";
            // condition to register user
            bool condition = true;

            // check username duplication
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
            {
                conn.Open();
                SqlCommand checkNameDuplication = new SqlCommand(
                    "SELECT COUNT(*) AS Count FROM Account WHERE UserName = @UserName", conn);
                checkNameDuplication.Parameters.AddWithValue("@UserName", username);
                int existing;
                existing = (int)checkNameDuplication.ExecuteScalar();
                conn.Close();

                if (existing != 0)
                {
                    condition = false;
                    responseMessage += usernameOverlaped;
                }

            }
            // when passwords are not matched
            if (password != confirmPW)
            {
                condition = false;
                responseMessage += notSamePW;                // add error message at the response text
            }

            // check that registration form is valid, then if it is, insert new user data in DB
            if (condition)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
                {
                    conn.Open();

                    int index;
                    // get index
                    SqlCommand getLastIndex = new SqlCommand(
                        "SELECT MAX(MemberID) FROM Account;", conn);
                    index = (int)getLastIndex.ExecuteScalar() + 1;

                    //insert new user
                    SqlCommand addUser = new SqlCommand("INSERT INTO Account (MemberID, UserName, Password, Email, LoanedCount, Position) " +
                                       "VALUES (@MemberID, @UserName, @Password, @Email, 0, 'member');", conn);

                    addUser.Parameters.AddWithValue("@MemberID", index);
                    addUser.Parameters.AddWithValue("@UserName", username);
                    addUser.Parameters.AddWithValue("@Password", password);
                    addUser.Parameters.AddWithValue("@Email", email);

                    addUser.ExecuteNonQuery();
                    Response.Write("<script>alert('Congrats! After examine your information, then you will be registered soon.');</script>");

                    conn.Close();
                }
            }
            // pop up error message
            else
            {
                Response.Write(responseOpener + responseMessage + responseCloser);
            }
        }

        private bool CheckLoginStatus()
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                return true;
            }
            else
                return false;
        }
    }
}