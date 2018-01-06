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
    public partial class MovieUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMovieInfo();
                LoadMovieSession();
            }
        }

        /// <summary>
        /// Method to link UpdateMovie method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UpdateMovieBtn_Click(object sender, EventArgs e)
        {
            UpdateMovie();
        }

        /// <summary>
        /// Display picture with input pictureUrl
        /// via Save Session and Load Session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PreviewImageBtn_Click(object sender, EventArgs e)
        {
            movieImage.Src = txtImageUrl.Text;
        }

        /// <summary>
        /// To prevent losing input movie data, Save movie session automatically
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MovieInfo_Changed(object sender, EventArgs e)
        {
            SaveMovieSession();
        }

        /// <summary>
        /// Generate Session for a new movie
        /// Prevent losing movie data in textBoxes
        /// </summary>
        private void SaveMovieSession()
        {
            Movie movie = new Movie();
            movie.Title = txtTitle.Text;
            movie.Genre = txtGenre.Text;
            movie.Director = txtDirector.Text;
            movie.Company = txtCompany.Text;
            movie.PublishedDate = inputPublishedDate.Value;
            movie.Duration = Convert.ToInt32(txtDuration.Text);
            movie.OfficialLink = txtOfficialLink.Text;
            movie.Description = txtDescription.Text;
            movie.PictureUrl = txtImageUrl.Text;
            movie.Status = txtMovieStatus.Text;
            Session["Movie"] = movie;
        }

        /// <summary>
        /// Load data from movie Session and set the value or text of HTML objects
        /// </summary>
        private void LoadMovieSession()
        {
            if (Session["Movie"] != null)
            {
                Movie movie = new Movie();
                movie = Session["Movie"] as Movie;
                txtTitle.Text = movie.Title;
                txtGenre.Text = movie.Genre;
                txtDirector.Text = movie.Director;
                txtCompany.Text = movie.Company;
                inputPublishedDate.Value = movie.PublishedDate;
                txtDuration.Text = movie.Duration.ToString();
                txtOfficialLink.Text = movie.OfficialLink;
                txtDescription.Text = movie.Description;
                txtImageUrl.Text = movie.PictureUrl;
                movieImage.Src = movie.PictureUrl;
                txtMovieStatus.Text = movie.Status;
            }
        }

        /// <summary>
        /// load movie data from database
        /// </summary>
        private void LoadMovieInfo()
        {
            if (Session["MovieID"] != null)
            {
                Movie movie = new Movie();
                string movieID = Session["MovieID"].ToString();

                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
                {
                    conn.Open();

                    SqlCommand getMovie = new SqlCommand(
                        "SELECT *, CONVERT(VARCHAR(10), PublishedDate, 120) AS Date FROM Movie WHERE MovieID = @MovieID;", conn);
                    getMovie.Parameters.AddWithValue("@MovieID", movieID);

                    SqlDataReader dr = getMovie.ExecuteReader();

                    while (dr.Read())
                    {
                        movie.MovieID = Convert.ToInt32(movieID);
                        movie.Title = dr["Title"].ToString();
                        movie.Genre = dr["Genre"].ToString();
                        movie.Director = dr["Director"].ToString();
                        movie.Company = dr["Company"].ToString();
                        movie.PublishedDate = dr["Date"].ToString();
                        movie.Duration = (int)dr["Duration"];
                        movie.OfficialLink = dr["OfficialLink"].ToString();
                        movie.Description = dr["Description"].ToString();
                        movie.PictureUrl = dr["PictureUrl"].ToString();
                        movie.PostedDate = dr["PostedDate"].ToString();
                        movie.ReviewScore = (double)dr["ReviewScore"];
                        movie.Status = dr["Status"].ToString();
                    }
                    dr.Close();

                    Session["Movie"] = movie;

                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Add a movie data into MovieManiacDB
        /// </summary>
        private void UpdateMovie()
        {
            if (Page.User.Identity.Name == "Movie Maniac" && Session["MovieID"] != null)
            {
                string movieID = Session["MovieID"].ToString();

                SaveMovieSession();

                if (Session["Movie"] != null)
                {
                    Movie movie = new Movie();
                    movie = Session["Movie"] as Movie;

                    // using SqlConnection from Web.config
                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
                    {
                        conn.Open();

                        int index;
                        // get index
                        SqlCommand getLastIndex = new SqlCommand(
                            "SELECT MAX(MovieID) FROM Movie;", conn);
                        index = (int)getLastIndex.ExecuteScalar() + 1;

                        // insert movie
                        SqlCommand insertMovie = new SqlCommand(
                             "UPDATE Movie SET Title = @Title, Genre = @Genre, Director = @Director, Company = @Company, " +
                             "PublishedDate = @PublishedDate, Duration = @Duration, OfficialLink = @OfficialLink, Description = @Description, " +
                             "ReviewScore = @ReviewScore, Status = @Status, PictureUrl = @PictureUrl " +
                             "WHERE MovieID = @MovieID;", conn);
                        insertMovie.Parameters.AddWithValue("@MovieID", movieID);
                        insertMovie.Parameters.AddWithValue("@Title", movie.Title);
                        insertMovie.Parameters.AddWithValue("@Genre", movie.Genre);
                        insertMovie.Parameters.AddWithValue("@Director", movie.Director);
                        insertMovie.Parameters.AddWithValue("@Company", movie.Company);
                        insertMovie.Parameters.AddWithValue("@PublishedDate", inputPublishedDate.Value);
                        insertMovie.Parameters.AddWithValue("@Duration", movie.Duration);
                        insertMovie.Parameters.AddWithValue("@OfficialLink", movie.OfficialLink);
                        insertMovie.Parameters.AddWithValue("@Description", movie.Description);
                        insertMovie.Parameters.AddWithValue("@ReviewScore", movie.ReviewScore);
                        insertMovie.Parameters.AddWithValue("@Status", movie.Status);
                        insertMovie.Parameters.AddWithValue("@PictureUrl", movie.PictureUrl);

                        insertMovie.ExecuteNonQuery();

                        conn.Close();

                        Response.Redirect("MovieDetail.aspx");
                    }
                }
            }
        }
    }
}
