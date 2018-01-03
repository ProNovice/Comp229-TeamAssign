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
    public partial class Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMovieDetail();
            }
        }
        #region Related Movie
        protected void RentBtn_Click(object sender, EventArgs e)
        {
            RentMovie();
            LoadMovieDetail();
        }
        /// <summary>
        /// Action of ReturnBtn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ReturnBtn_Click(object sender, EventArgs e)
        {
            ReturnMovie();
            LoadMovieDetail();
        }
        /// <summary>
        /// Action of HideBtn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void HideBtn_Click(object sender, EventArgs e)
        {
            HideMovie();
            LoadMovieDetail();
        }
        /// <summary>
        /// Action of UnhideBtn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UnhideBtn_Click(object sender, EventArgs e)
        {
            UnhidMovie();
            LoadMovieDetail();
        }
        #endregion

        #region Related Review        
        /// <summary>
        /// Action of WriteReviewBtn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void WriteReviewBtn_Click(object sender, EventArgs e)
        {
            // only allowed when user is logged in
            // but in default, writing review form is hidden if user is not logged in.
            if (checkLoginStatus())
            {
                if (Session["MovieID"] != null)
                {
                    string movieID = Session["MovieID"].ToString();
                    string memberID = GetMemberID();
                    string opinion = txtReviewComment.Text;
                    string score = txtReviewScore.Text;
                    string date = DateTime.Today.ToString("YYYY-MM-DD");

                    // using SqlConnection from Web.config
                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
                    {
                        conn.Open();

                        // insert review
                        SqlCommand insertReview = new SqlCommand(
                         "INSERT INTO Movie (ReviewID, MovieID, MemberID, Opinion, Score, revDate) " +
                         "VALUES (NEXT VALUE FOR seq_Review, @MovieID, @MemberID, @Opinion, @Score, @revDate);", conn);
                        insertReview.Parameters.AddWithValue("@MovieID", movieID);
                        insertReview.Parameters.AddWithValue("@MemberID", memberID);
                        insertReview.Parameters.AddWithValue("@Opinion", opinion);
                        insertReview.Parameters.AddWithValue("@Score", score);
                        insertReview.Parameters.AddWithValue("@revDate", date);

                        insertReview.ExecuteNonQuery();

                        conn.Close();
                    }
                    LoadMovieDetail();
                }
            }
        }
        /// <summary>
        /// Action of EditReviewBtn
        /// Set visibility of options and fields to edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditReviewBtn_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as Button).Parent as RepeaterItem;
            (item.FindControl("lblReviewScore") as Label).Visible = false;
            (item.FindControl("txtReview") as Label).Visible = false;
            (item.FindControl("btnEdit") as Label).Visible = false;
            (item.FindControl("txtEditReviewScore") as TextBox).Visible = true;
            (item.FindControl("txtEditReviewText") as TextBox).Visible = true;
            (item.FindControl("btnCancel") as Button).Visible = true;
            (item.FindControl("btnConfirm") as Button).Visible = true;
        }

        /// <summary>
        /// Action of CancelReviewBtn
        /// Hide options and fiedls for editing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelEditingReviewBtn_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as Button).Parent as RepeaterItem;
            (item.FindControl("lblReviewScore") as Label).Visible = true;
            (item.FindControl("txtReview") as Label).Visible = true;
            (item.FindControl("btnEdit") as Label).Visible = true;
            (item.FindControl("txtEditReviewScore") as TextBox).Visible = false;
            (item.FindControl("txtEditReviewText") as TextBox).Visible = false;
            (item.FindControl("btnCancel") as Button).Visible = false;
            (item.FindControl("btnConfirm") as Button).Visible = false;
        }

        /// <summary>
        /// Update edited review into DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ConfirmEditingReviewBtn_Click(object sender, EventArgs e)
        {
            if (checkLoginStatus())
            {
                RepeaterItem item = (sender as Button).Parent as RepeaterItem;
                string opinion = (item.FindControl("txtEditReviewText") as TextBox).Text;
                string score = (item.FindControl("txtEditReviewScore") as TextBox).Text;
                string reviewID = (sender as Button).CommandArgument;
                string memberID = GetMemberID();

                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
                {
                    conn.Open();

                    // update review in DB
                    // double check with ReviewID and Current MemberID
                    SqlCommand updateReview = new SqlCommand(
                         "UPDATE Review SET Score = @Score, Opinion = @Opinion WHERE ReviewID = @ReviewID AND MemberID = @MemberID;", conn);
                    updateReview.Parameters.AddWithValue("@Score", score);
                    updateReview.Parameters.AddWithValue("@Opinion", opinion);
                    updateReview.Parameters.AddWithValue("@ReviewID", reviewID);
                    updateReview.Parameters.AddWithValue("@MemberID", memberID);
                    updateReview.ExecuteNonQuery();

                    conn.Close();
                }
            }
            LoadMovieDetail();
        }

        /// <summary>
        /// Action of DeleteReviewBtn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DeleteReviewBtn_Click(object sender, EventArgs e)
        {
            if (checkLoginStatus())
            {
                RepeaterItem item = (sender as Button).Parent as RepeaterItem;
                string opinion = (item.FindControl("txtEditReviewText") as TextBox).Text;
                string score = (item.FindControl("txtEditReviewScore") as TextBox).Text;
                string reviewID = (sender as Button).CommandArgument;
                string memberID = GetMemberID();

                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
                {
                    conn.Open();

                    // delete review in DB
                    // double check with ReviewID and Current MemberID
                    SqlCommand deleteReview = new SqlCommand(
                         "DELETE FROM Review WHERE ReviewID = @ReviewID AND MemberID = @MemberID;", conn);
                    deleteReview.Parameters.AddWithValue("@Score", score);
                    deleteReview.Parameters.AddWithValue("@Opinion", opinion);
                    deleteReview.Parameters.AddWithValue("@ReviewID", reviewID);
                    deleteReview.Parameters.AddWithValue("@MemberID", memberID);
                    deleteReview.ExecuteNonQuery();

                    conn.Close();
                }
            }
            LoadMovieDetail();
        }

        #endregion
        /// <summary>
        /// Display data of selected movie
        /// </summary>
        private void LoadMovieDetail()
        {
            if (Session["MovieID"] != null)
            {
                string movieID = Session["MovieID"].ToString();

                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
                {
                    Movie movie = new Movie();

                    conn.Open();

                    // get movie data from DB
                    SqlCommand getTitle = new SqlCommand(
                         "SELECT * FROM Movie " +
                         "WHERE MovieID = @MovieID;", conn);
                    getTitle.Parameters.AddWithValue("@MovieID", movieID);
                    movie = getTitle.ExecuteScalar() as Movie;

                    Session["Movie"] = movie;

                    // display data on page
                    lblTitle.Text = movie.Title;
                    lblGenre.Text = movie.Genre;
                    lblDirector.Text = movie.Director;
                    lblCompany.Text = movie.Company;
                    lblPublishedDate.Text = movie.PublishedDate;
                    lblDuration.Text = movie.Duration.ToString();
                    aOfficialSite.HRef = movie.OfficialLink;
                    txtDescription.InnerText = movie.Description;
                    movieImage.Src = movie.PictureUrl;

                    conn.Close();
                }
                // shows available options up to status of movie
                SetMovieStatusVisibility();
            }
            else
            {
                // if there is no value in the session, redirect MainTracking page
                Response.Redirect("MainTracking.aspx");
            }
        }
        /// <summary>
        /// Up to status of movie, it shows available options
        /// </summary>
        private void SetMovieStatusVisibility()
        {
            // whene user is logged in
            if (checkLoginStatus())
            {
                // check status of relationship between movie and user
                string crtStatus = GetMovieStatus();
                // when status is loaned
                if (crtStatus.ToLower() == "loaned")
                {
                    lblStatus.Text = "Loaned";
                    btnHide.Visible = false;
                    btnUnhide.Visible = false;
                    btnRent.Visible = false;
                    btnReturn.Visible = true;
                }
                // when status is hidden
                else if (crtStatus.ToLower() == "hidden")
                {
                    lblStatus.Text = "Hidden";
                    btnHide.Visible = false;
                    btnUnhide.Visible = true;
                    btnRent.Visible = false;
                    btnReturn.Visible = false;
                }
                // whene there is no relationship
                else
                {
                    Movie movie = new Movie();

                    if (Session["Movie"] != null)
                    {
                        movie = Session["Movie"] as Movie;

                        // show status of Movie itself
                        lblStatus.Text = movie.Status;
                    }
                    btnHide.Visible = true;
                    btnUnhide.Visible = false;
                    btnRent.Visible = true;
                    btnReturn.Visible = false;
                }
            }
            // when user is not logged in
            else
            {
                btnHide.Visible = true;
                btnUnhide.Visible = false;
                btnRent.Visible = true;
                btnReturn.Visible = false;
                // hide writing review form
                tblWritingReview.Visible = false;
            }
        }

        /// <summary>
        /// Return boolean value upto status of user login
        /// </summary>
        /// <returns></returns>
        private bool checkLoginStatus()
        {
            if (Session["Login"] != null)
            {
                if ((bool)Session["Login"])
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Change the status of movie 'loaned'
        /// </summary>
        private void RentMovie()
        {
            if (Session["MovieID"] != null && Session["UserName"] != null)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
                {
                    string movieID = Session["MovieID"].ToString();
                    string memberID = GetMemberID(); ;

                    conn.Open();

                    SqlCommand insertRelationship = new SqlCommand(
                        "INSERT INTO RelatedMovie (RelatedMovieID, MovieID, MemberID, Status) " +
                        "VALUES (NEXT VALUE FOR seq_RelatedMovie, @MovieID, @MemberID, @Status);", conn);
                    insertRelationship.Parameters.AddWithValue("@MovieID", movieID);
                    insertRelationship.Parameters.AddWithValue("@MemberID", memberID);
                    insertRelationship.Parameters.AddWithValue("@Status", "loaned");

                    conn.Close();
                }
            }
            string crtStatus = GetMovieStatus();
        }
        /// <summary>
        /// Remove the status of movie 'loaned' and delte the relationship of movie and user
        /// </summary>
        private void ReturnMovie()
        {
            if (Session["MovieID"] != null && Session["UserName"] != null)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
                {
                    string movieID = Session["MovieID"].ToString();
                    string memberID = GetMemberID(); ;

                    conn.Open();

                    SqlCommand deleteRelationship = new SqlCommand(
                        "DELETE FROM RelatedMovie WHERE MovieID = @MovieID AND MemberID = @MemberID AND Status = @Status;", conn);
                    deleteRelationship.Parameters.AddWithValue("@MovieID", movieID);
                    deleteRelationship.Parameters.AddWithValue("@MemberID", memberID);
                    deleteRelationship.Parameters.AddWithValue("@Status", "loaned");

                    conn.Close();
                }
            }
            string crtStatus = GetMovieStatus();
        }
        /// <summary>
        /// Change the status of movie 'hidden'
        /// </summary>
        private void HideMovie()
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
            {
                string movieID = Session["MovieID"].ToString();
                string memberID = GetMemberID();

                conn.Open();

                SqlCommand insertRelationship = new SqlCommand(
                    "INSERT INTO RelatedMovie (RelatedMovieID, MovieID, MemberID, Status) " +
                    "VALUES (NEXT VALUE FOR seq_RelatedMovie, @MovieID, @MemberID, @Status);", conn);
                insertRelationship.Parameters.AddWithValue("@MovieID", movieID);
                insertRelationship.Parameters.AddWithValue("@MemberID", memberID);
                insertRelationship.Parameters.AddWithValue("@Status", "hidden");

                conn.Close();
            }
            string crtStatus = GetMovieStatus();
        }
        /// <summary>
        /// Remove the status of movie 'hidden' and delte the relationship of movie and user
        /// </summary>
        private void UnhidMovie()
        {
            if (Session["MovieID"] != null && Session["UserName"] != null)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
                {
                    string movieID = Session["MovieID"].ToString();
                    string memberID = GetMemberID(); ;

                    conn.Open();

                    SqlCommand deleteRelationship = new SqlCommand(
                        "DELETE FROM RelatedMovie WHERE MovieID = @MovieID AND MemberID = @MemberID AND Status = @Status;", conn);
                    deleteRelationship.Parameters.AddWithValue("@MovieID", movieID);
                    deleteRelationship.Parameters.AddWithValue("@MemberID", memberID);
                    deleteRelationship.Parameters.AddWithValue("@Status", "hidden");

                    conn.Close();
                }
            }
            string crtStatus = GetMovieStatus();
        }

        /// <summary>
        /// Get current status of Movie
        /// (This status is about relationship status between User and Movie
        /// such as 'loaned' or 'hidden' or null.)
        /// </summary>
        /// <returns></returns>
        private string GetMovieStatus()
        {
            string status = null;
            if (Session["MovieID"] != null && Session["UserName"] != null)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
                {

                    string movieID = Session["MovieID"].ToString();
                    string memberID = GetMemberID();

                    conn.Open();
                    SqlCommand getMovieStatus = new SqlCommand(
                        "SELECT Status FROM RelatedMovie WHERE MovieID = @MovieID AND MemberID = @MemberID", conn);
                    getMovieStatus.Parameters.AddWithValue("@MovieID", movieID);
                    getMovieStatus.Parameters.AddWithValue("@MemberID", memberID);
                    status = getMovieStatus.ExecuteScalar().ToString();

                    conn.Close();
                }
            }
            return status;
        }

        /// <summary>
        /// get memberID from DB
        /// if there is no match, return null
        /// </summary>
        /// <returns></returns>
        private string GetMemberID()
        {
            string memberID = null;

            if (Session["UserName"] != null)
            {
                string username = Session["UserName"].ToString();

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