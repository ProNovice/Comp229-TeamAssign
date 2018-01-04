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
    public partial class MovieAddition : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // whenever website is refreshed or postback, it shows movie session.
            LoadMovieSession();
        }

        /// <summary>
        /// Method to link AddMovie method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddMovieBtn_Click(object sender, EventArgs e)
        {
            AddMovie();
        }

        /// <summary>
        /// Display picture with input pictureUrl
        /// via Save Session and Load Session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PreviewImageBtn_Click(object sender, EventArgs e)
        {
            SaveMovieSession();
            LoadMovieSession();
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
            movie.PublishedDate = txtPublishedDate.Text;
            movie.Duration = Convert.ToInt32(txtDuration.Text);
            movie.OfficialLink = txtOfficialLink.Text;
            movie.Description = txtDescription.Text;
            movie.PictureUrl = txtImageUrl.Text;
            Session["NewMovie"] = movie;
        }

        /// <summary>
        /// Load data from movie Session and set the value or text of HTML objects
        /// </summary>
        private void LoadMovieSession()
        {
            if (Session["NewMovie"] != null)
            {
                Movie movie = new Movie();
                movie = Session["NewMovie"] as Movie;
                txtTitle.Text = movie.Title;
                txtGenre.Text = movie.Genre;
                txtDirector.Text = movie.Director;
                txtCompany.Text = movie.Company;
                txtPublishedDate.Text = movie.PublishedDate;
                txtDuration.Text = movie.Duration.ToString();
                txtOfficialLink.Text = movie.OfficialLink;
                txtDescription.Text = movie.Description;
                txtImageUrl.Text = movie.PictureUrl;
                movieImage.Src = movie.PictureUrl;
            }
        }

        /// <summary>
        /// Add a movie data into MovieManiacDB
        /// </summary>
        private void AddMovie()
        {
            if (Page.User.Identity.Name == "Movie Maniac")
            {
                SaveMovieSession();
                if (Session["NewMovie"] != null)
                {
                    Movie movie = new Movie();
                    movie = Session["NewMovie"] as Movie;

                    movie.PostedDate = DateTime.Today.ToString("YYYY-MM-DD");

                    // using SqlConnection from Web.config
                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
                    {
                        conn.Open();

                        // insert movie
                        SqlCommand insertMovie = new SqlCommand(
                             "INSERT INTO Movie (Title, Genre, Director, Company, PublishedDate, Duration, " +
                             "OfficialLink, Description, ReviewScore, Status, PostedDate, PictureUrl) " +
                             "VALUES (@Title, @Genre, @Director, @Company, @PublishedDate, @Duration, " +
                             "@OfficialLink, @Description, @ReviewScore, @Status, @PostedDate, @PictureUrl);", conn);
                        insertMovie.Parameters.AddWithValue("@Title", movie.Title);
                        insertMovie.Parameters.AddWithValue("@Genre", movie.Genre);
                        insertMovie.Parameters.AddWithValue("@Director", movie.Director);
                        insertMovie.Parameters.AddWithValue("@Company", movie.Company);
                        insertMovie.Parameters.AddWithValue("@PublishedDate", movie.PublishedDate);
                        insertMovie.Parameters.AddWithValue("@Duration", movie.Duration);
                        insertMovie.Parameters.AddWithValue("@OfficialLink", movie.OfficialLink);
                        insertMovie.Parameters.AddWithValue("@Description", movie.Description);
                        insertMovie.Parameters.AddWithValue("@ReviewScore", movie.ReviewScore);
                        insertMovie.Parameters.AddWithValue("@Status", movie.Status);
                        insertMovie.Parameters.AddWithValue("@PostedDate", movie.PostedDate);
                        insertMovie.Parameters.AddWithValue("@PictureUrl", movie.PictureUrl);

                        insertMovie.ExecuteNonQuery();

                        conn.Close();
                    }
                }
            }
        }
    }
}