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
    public partial class MainTracking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetMovieInfo();
        }

        protected void GetMovieInfo()
        {
            // using SqlConnection from Web.config
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("SELECT MovieID, Title, Genre, Duration, ReviewScore, Status, PictureUrl FROM Movie;", conn);

                SqlDataReader reader = comm.ExecuteReader();

                movieRepeater.DataSource = reader;
                movieRepeater.DataBind();

                conn.Close();

            }
        }
        //bind the data
        protected void movieList_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "MoreDetail")
            {
                Session["MovieID"] = e.CommandArgument.ToString();
                Response.Redirect("MovieDetail.aspx");
            }
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