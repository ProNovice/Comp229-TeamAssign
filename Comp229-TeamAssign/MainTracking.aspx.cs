using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comp229_TeamAssign
{
    public partial class MainTracking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //validate username and password to login
        protected void loginBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserLogin.aspx");
        }
        //link to registration page
        protected void registerBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserRegistration.aspx");
        }
        //search for the movie
        protected void searchBtn_Click(object sender, EventArgs e)
        {

            GetMovieInfo();
        }
        //get the movie info that related to the statistic input
        protected void GetMovieInfo()
        {

        }
        //link to Movie Detail Page
        protected void movieDetailbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("MovieDetail.aspx");
        }
        //Site Security
        protected void logoutBtn_Click(object sender, EventArgs e)
        {

        }

        protected void modifyBtn_Click(object sender, EventArgs e)
        {

        }

        protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSearchBy.SelectedItem.Text == "All")
            {
                txtSearch.Text = string.Empty;
                txtSearch.Enabled = false;
            }
            else
            {
                txtSearch.Enabled = true;
                txtSearch.Text = string.Empty;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }
    }
}