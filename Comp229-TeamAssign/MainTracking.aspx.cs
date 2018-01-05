using System;
using System.Collections.Generic;
using System.Data;
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
            lblSearchMovieList.Visible = false;
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

        protected void getLoanedCount()
        {

        }

        protected void MovieList_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            if (e.CommandName == "MoreDetail")
            {
                Session["MovieID"] = e.CommandArgument.ToString();
                Response.Redirect("MovieDetail.aspx");
            }
        }
        
        protected void movieList_ItemCommand(object sender, DataListCommandEventArgs e)
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
            lblSearchMovieList.Visible = true;
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
            {
                conn.Open();
                if (ddlSearchBy.SelectedItem.Text == "Title")
                {
                    string data = txtSearch.Text;
                    string query = "SELECT MovieID, Title, Genre, Duration, ReviewScore, Status, PictureUrl FROM Movie WHERE Title like'" + data + "%'";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    MovieList.DataSource = ds;
                    MovieList.DataBind();
                }
                else if (ddlSearchBy.SelectedItem.Text == "Genre")
                {
                    string data = txtSearch.Text;
                    string query = "SELECT MovieID, Title, Genre, Duration, ReviewScore, Status, PictureUrl FROM Movie WHERE Genre like'" + data + "%'";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    MovieList.DataSource = ds;
                    MovieList.DataBind();
                }
                conn.Close();
            }
        }
    }
}
