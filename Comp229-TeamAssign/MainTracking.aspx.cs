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
            GetRecentMovieInfo();
            lblSearchMovieList.Visible = false;
            lblLoanedMovie.Visible = false;
            loanedMovie.Visible = false;
            if (Page.User.Identity.IsAuthenticated)
            {
                GetLoanedMovieInfo();
                lblLoanedMovie.Visible = true;
                loanedMovie.Visible = true;
            }
        }

        /// get memberID from DB
        /// if there is no match, return "".
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
            string memberID = GetMemberID();
            if (Page.User.Identity.IsAuthenticated)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand("SELECT * FROM Movie INNER JOIN RelatedMovie ON Movie.MovieID = RelatedMovie.MovieID WHERE MemberID = @MemberID AND RelatedMovie.Status != 'hidden';", conn);
                    comm.Parameters.AddWithValue("@MemberID", memberID);
                    SqlDataReader reader = comm.ExecuteReader();
                    movieRepeater.DataSource = reader;
                    movieRepeater.DataBind();
                    conn.Close();
                }

            }
        }

        protected void GetRecentMovieInfo()
        {
            // using SqlConnection from Web.config
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
            {
                conn.Open();
                string data = "2015-09-24";
                string query = "SELECT MovieID, Title, Genre, Duration, ReviewScore, Status, PictureUrl FROM Movie WHERE PublishedDate like'" + data + "%'";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                recentMovies.DataSource = ds;
                recentMovies.DataBind();
                conn.Close();
            }
            string memberID = GetMemberID();
            if (Page.User.Identity.IsAuthenticated)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand("SELECT * FROM Movie INNER JOIN RelatedMovie ON Movie.MovieID = RelatedMovie.MovieID WHERE MemberID = @MemberID AND RelatedMovie.Status != 'hidden' AND PublishedDate = '2015-09-24';", conn);
                    comm.Parameters.AddWithValue("@MemberID", memberID);
                    SqlDataReader reader = comm.ExecuteReader();
                    movieRepeater.DataSource = reader;
                    movieRepeater.DataBind();
                    conn.Close();
                }
            }
        }

        protected void GetLoanedMovieInfo()
        {
            // using SqlConnection from Web.config
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
            {
                string memberID = GetMemberID();
                conn.Open();
                SqlCommand getLoanedMovie = new SqlCommand("SELECT * FROM Movie INNER JOIN RelatedMovie ON Movie.MovieID = RelatedMovie.MovieID WHERE MemberID = @MemberID AND RelatedMovie.Status = 'loaned';",conn);
                getLoanedMovie.Parameters.AddWithValue("@MemberID", memberID);
                SqlDataReader reader = getLoanedMovie.ExecuteReader();
                loanedMovie.DataSource = reader;
                loanedMovie.DataBind();
                reader.Close();
                conn.Close();
            }
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
        protected void recentMovie_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            if (e.CommandName == "MoreDetail")
            {
                Session["MovieID"] = e.CommandArgument.ToString();
                Response.Redirect("MovieDetail.aspx");
            }
        }
        protected void loanedMovie_ItemCommand(object sender, DataListCommandEventArgs e)
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
            if (Page.User.Identity.IsAuthenticated)
            {
                lblSearchMovieList.Visible = true;
                lblMovieList.Visible = false;
                movieRepeater.Visible = false;
                lblRecentMovies.Visible = false;
                recentMovies.Visible = false;
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
            else
            {
                Response.Write("<script>alert('Please Login!');</script>");
            }
        }

    }
}