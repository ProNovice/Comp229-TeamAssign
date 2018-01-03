using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
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
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MovieManiac;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            //insert new user
            SqlCommand addUser = new SqlCommand("INSERT INTO Account (UserName, Password, Email) " +
                               "VALUES (@UserName, @Password, @Email);", connection);

            addUser.Parameters.Add("@UserName", System.Data.SqlDbType.VarChar);
            addUser.Parameters["@UserName"].Value = username.Text;

            addUser.Parameters.Add("@Password", System.Data.SqlDbType.VarChar);
            addUser.Parameters["@Password"].Value = password.Text;

            addUser.Parameters.Add("@Email", System.Data.SqlDbType.Date);
            addUser.Parameters["@Email"].Value = email.Text;
            addUser.ExecuteNonQuery();

            try
            {
                connection.Open();
                addUser.ExecuteNonQuery();
                dbErrorMessage.Text = "Congratulations! You are a member now!";
            }
            catch (SqlException error)
            {
                dbErrorMessage.Text += error.Message;
            }
            finally
            {
                connection.Close();
            }
        }
    }
    }
