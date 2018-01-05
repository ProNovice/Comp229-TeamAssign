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
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void submitBtn_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
            {
                //insert new user
                SqlCommand addUser = new SqlCommand("INSERT INTO Account (UserName, Password, Email) " +
                                   "VALUES (@UserName, @Password, @Email);", connection);

                addUser.Parameters.AddWithValue("@UserName", username.Text);
                addUser.Parameters.AddWithValue("@Password", password.Text);
                addUser.Parameters.AddWithValue("@Email", email.Text);
                try
                {
                    connection.Open();
                    addUser.ExecuteNonQuery();
                    Response.Write("<script>alert('Congrats! You are a member now');</script>");                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
