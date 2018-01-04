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
                LoadPage();
            }
        }

        /// <summary>
        /// Method package for loading movie detail and reviews at once
        /// </summary>
        private void LoadPage()
        {
            UpdateMovieScore();
            LoadMovieDetail();
            LoadReview();
        }

        #region Related Movie
        /// <summary>
        /// Action of Rent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            UpdateMovieScore();
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
            if (CheckLoginStatus())
            {
                if (Session["MovieID"] != null)
                {
                    string movieID = Session["MovieID"].ToString();
                    string memberID = GetMemberID();
                    string opinion = txtReviewComment.Text;
                    string score = txtReviewScore.Text;
                    var date = DateTime.Today;

                    // using SqlConnection from Web.config
                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
                    {
                        conn.Open();
                        int index;
                        // get index
                        SqlCommand getLastIndex = new SqlCommand(
                            "SELECT MAX(ReviewID) FROM Review;", conn);
                        index = (int)getLastIndex.ExecuteScalar() + 1;

                        // insert review
                        SqlCommand insertReview = new SqlCommand(
                         "INSERT INTO Review (ReviewID, MovieID, MemberID, Opinion, Score, revDate) " +
                         "VALUES (@ReviewID, @MovieID, @MemberID, @Opinion, @Score, @revDate);", conn);
                        insertReview.Parameters.AddWithValue("@ReviewID", index);
                        insertReview.Parameters.AddWithValue("@MovieID", movieID);
                        insertReview.Parameters.AddWithValue("@MemberID", memberID);
                        insertReview.Parameters.AddWithValue("@Opinion", opinion);
                        insertReview.Parameters.AddWithValue("@Score", score);
                        insertReview.Parameters.AddWithValue("@revDate", date);

                        insertReview.ExecuteNonQuery();

                        conn.Close();
                    }
                    LoadPage();
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

            if (CheckLoginStatus())
            {
                string reviewID = (sender as Button).CommandArgument;
                string writerID = GetWriterID(reviewID);
                string crtMemberID = GetMemberID();
                if (writerID == crtMemberID)
                {
                    item.FindControl("lblReviewScore").Visible = false;
                    item.FindControl("txtReview").Visible = false;
                    item.FindControl("btnEdit").Visible = false;
                    item.FindControl("txtEditReviewScore").Visible = true;
                    item.FindControl("txtEditReviewText").Visible = true;
                    item.FindControl("btnCancel").Visible = true;
                    item.FindControl("btnConfirm").Visible = true;
                }
                else
                    (item.FindControl("lblEditingReviewFeedback") as Label).Text = "You are not the review writer";
            }
            else
                (item.FindControl("lblEditingReviewFeedback") as Label).Text = "You are not logged in";
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
            item.FindControl("lblReviewScore").Visible = true;
            item.FindControl("txtReview").Visible = true;
            item.FindControl("btnEdit").Visible = true;
            item.FindControl("txtEditReviewScore").Visible = false;
            item.FindControl("txtEditReviewText").Visible = false;
            item.FindControl("btnCancel").Visible = false;
            item.FindControl("btnConfirm").Visible = false;
        }

        /// <summary>
        /// Update edited review into DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ConfirmEditingReviewBtn_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as Button).Parent as RepeaterItem;

            if (CheckLoginStatus())
            {
                string reviewID = (sender as Button).CommandArgument;
                string writerID = GetWriterID(reviewID);
                string crtMemberID = GetMemberID();
                if (writerID == crtMemberID)
                {
                    string opinion = (item.FindControl("txtEditReviewText") as TextBox).Text;
                    string score = (item.FindControl("txtEditReviewScore") as TextBox).Text;

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
                        updateReview.Parameters.AddWithValue("@MemberID", crtMemberID);
                        updateReview.ExecuteNonQuery();

                        conn.Close();
                    }

                    item.FindControl("lblReviewScore").Visible = true;
                    item.FindControl("txtReview").Visible = true;
                    item.FindControl("btnEdit").Visible = true;
                    item.FindControl("txtEditReviewScore").Visible = false;
                    item.FindControl("txtEditReviewText").Visible = false;
                    item.FindControl("btnCancel").Visible = false;
                    item.FindControl("btnConfirm").Visible = false;
                    
                    LoadPage();
                }
                else
                    (item.FindControl("lblEditingReviewFeedback") as Label).Text = "You are not the review writer";
            }
            else
                (item.FindControl("lblEditingReviewFeedback") as Label).Text = "You are not logged in";
        }

        /// <summary>
        /// Action of DeleteReviewBtn
        /// Delete Review data in DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DeleteReviewBtn_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as Button).Parent as RepeaterItem;

            if (CheckLoginStatus())
            {
                string reviewID = (sender as Button).CommandArgument;
                string writerID = GetWriterID(reviewID);
                string crtMemberID = GetMemberID();
                if (writerID == crtMemberID)
                {
                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
                    {
                        conn.Open();

                        // delete review in DB
                        // double check with ReviewID and Current MemberID
                        SqlCommand deleteReview = new SqlCommand(
                             "DELETE FROM Review WHERE ReviewID = @ReviewID", conn);
                        deleteReview.Parameters.AddWithValue("@ReviewID", reviewID);
                        deleteReview.ExecuteNonQuery();

                        conn.Close();
                    }
                    LoadPage();
                }
                else
                    (item.FindControl("lblEditingReviewFeedback") as Label).Text = "You are not the review writer";
            }
            else
                (item.FindControl("lblEditingReviewFeedback") as Label).Text = "You are not logged in";
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
                    SqlCommand getMovie = new SqlCommand(
                         "SELECT *, CONVERT(VARCHAR(10), PublishedDate, 120) AS Date, CONVERT(VARCHAR(10), PostedDate, 120) AS PostedDate FROM Movie " +
                         "WHERE MovieID = @MovieID;", conn);
                    getMovie.Parameters.AddWithValue("@MovieID", movieID);

                    SqlDataReader dr = getMovie.ExecuteReader();

                    while (dr.Read())
                    {
                        movie.MovieID = (int)dr["MovieID"];
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
                    }
                    dr.Close();

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
                    lblReviewScore.Text = movie.ReviewScore.ToString();
                    lblStatus.Text = GetMovieStatus();

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
        /// Load review data from DB and display them into Repeater
        /// </summary>
        private void LoadReview()
        {
            if (Session["MovieID"] != null)
            {
                string movieID = Session["MovieID"].ToString();

                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
                {
                    Movie movie = new Movie();

                    conn.Open();

                    // get movie data from DB
                    SqlCommand getReview = new SqlCommand(
                         "SELECT *, CONVERT(VARCHAR(10), revDate, 120) AS Date, Account.UserName AS UserName FROM Review " +
                         "INNER JOIN Account ON Review.MemberID = Account.MemberID " +
                         "WHERE MovieID = @MovieID;", conn);
                    getReview.Parameters.AddWithValue("@MovieID", movieID);

                    SqlDataReader dr = getReview.ExecuteReader();

                    ReviewRepeater.DataSource = dr;
                    ReviewRepeater.DataBind();

                    dr.Close();
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
            if (CheckLoginStatus())
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
        private bool CheckLoginStatus()
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Change the status of movie 'loaned'
        /// </summary>
        private void RentMovie()
        {
            // only when there is no relatedReview data between Movie and User
            if (Session["MovieID"] != null && Page.User.Identity.Name != null || GetMovieStatus() != "Loaned" || GetMovieStatus() != "Hidden")
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
                {
                    string movieID = Session["MovieID"].ToString();
                    string memberID = GetMemberID(); ;

                    conn.Open();
                    int index;
                    // get index
                    SqlCommand getLastIndex = new SqlCommand(
                        "SELECT MAX(RelatedMovieID) FROM RelatedMovie;", conn);
                    index = (int)getLastIndex.ExecuteScalar() + 1;

                    SqlCommand insertRelationship = new SqlCommand(
                        "INSERT INTO RelatedMovie (RelatedMovieID, MovieID, MemberID, Status) " +
                        "VALUES (@RelatedMovieID, @MovieID, @MemberID, @Status);", conn);
                    insertRelationship.Parameters.AddWithValue("@RelatedMovieID", index);
                    insertRelationship.Parameters.AddWithValue("@MovieID", movieID);
                    insertRelationship.Parameters.AddWithValue("@MemberID", memberID);
                    insertRelationship.Parameters.AddWithValue("@Status", "loaned");

                    conn.Close();
                }
            }
        }
        /// <summary>
        /// Remove the status of movie 'loaned' and delte the relationship of movie and user
        /// </summary>
        private void ReturnMovie()
        {
            // when the movie is loaned
            if (Session["MovieID"] != null && Page.User.Identity.Name != null && GetMovieStatus() != "Loaned")
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
        }
        /// <summary>
        /// Change the status of movie 'hidden'
        /// </summary>
        private void HideMovie()
        {
            // only when there is no relatedReview data between Movie and User
            if (Session["MovieID"] != null && Page.User.Identity.Name != null || GetMovieStatus() != "Loaned" || GetMovieStatus() != "Hidden")
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
                {
                    string movieID = Session["MovieID"].ToString();
                    string memberID = GetMemberID(); ;

                    conn.Open();
                    int index;
                    // get index
                    SqlCommand getLastIndex = new SqlCommand(
                        "SELECT MAX(RelatedMovieID) FROM RelatedMovie;", conn);
                    index = (int)getLastIndex.ExecuteScalar() + 1;

                    SqlCommand insertRelationship = new SqlCommand(
                        "INSERT INTO RelatedMovie (RelatedMovieID, MovieID, MemberID, Status) " +
                        "VALUES (@RelatedMovieID, @MovieID, @MemberID, @Status);", conn);
                    insertRelationship.Parameters.AddWithValue("@RelatedMovieID", index);
                    insertRelationship.Parameters.AddWithValue("@MovieID", movieID);
                    insertRelationship.Parameters.AddWithValue("@MemberID", memberID);
                    insertRelationship.Parameters.AddWithValue("@Status", "hidden");

                    conn.Close();
                }
            }
        }
        /// <summary>
        /// Remove the status of movie 'hidden' and delte the relationship of movie and user
        /// </summary>
        private void UnhidMovie()
        {
            // when the movie is hidden
            if (Session["MovieID"] != null && Page.User.Identity.Name != null && GetMovieStatus() != "Hidden")
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
        }

        /// <summary>
        /// Get current status of Movie
        /// (This status is about relationship status between User and Movie
        /// such as 'loaned' or 'hidden' or null.)
        /// </summary>
        /// <returns></returns>
        private string GetMovieStatus()
        {
            string status = "Available";
            if (Session["MovieID"] != null && Page.User.Identity.Name != null)
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
                    string relatedStatus;

                    // prevent NullReferenceException
                    try
                    {
                        relatedStatus = getMovieStatus.ExecuteScalar().ToString();
                    }
                    catch
                    {
                        relatedStatus = "";
                    }

                    if (relatedStatus == "loaned")
                        status = "Loaned";
                    else if (relatedStatus == "hidden")
                        status = "Hidden";

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

        /// <summary>
        /// Return Writer ID of input reviewID
        /// </summary>
        /// <param name="reviewID"></param>
        /// <returns></returns>
        private string GetWriterID(string reviewID)
        {
            string memberID = "";
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
            {
                conn.Open();

                SqlCommand getWriterID = new SqlCommand(
                    "SELECT MemberID FROM Review WHERE ReviewID = @ReviewID;", conn);
                getWriterID.Parameters.AddWithValue("@ReviewID", reviewID);
                memberID = getWriterID.ExecuteScalar().ToString();

                conn.Close();
            }
            return memberID;
        }

        /// <summary>
        /// Update movie score with related reviews in DB
        /// </summary>
        private void UpdateMovieScore()
        {
            if (Session["MovieID"] != null)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MovieManiacDB"].ConnectionString))
                {
                    string movieID = Session["MovieID"].ToString();
                    string memberID = GetMemberID();
                    double sum = 0;
                    double count = 1;
                    double score;

                    conn.Open();

                    // get SUM and Count of all review scores
                    SqlCommand getMovieScore = new SqlCommand(
                        "SELECT SUM(Score) AS TotalScore, COUNT(Score) AS CountScore FROM Review WHERE MemberID = @MemberID", conn);
                    getMovieScore.Parameters.AddWithValue("@MemberID", memberID);
                    SqlDataReader dr = getMovieScore.ExecuteReader();

                    while (dr.Read())
                    {
                        sum = (double)dr["TotalScore"];
                        count = (double)dr["CountScore"];
                    }
                    dr.Close();

                    score = Math.Round(sum / count, 2);

                    // update movie score in movie db
                    SqlCommand updateMovieScore = new SqlCommand(
                        "UPDATE Movie SET Score = @Score WHERE MovieID = @MovieID;", conn);
                    updateMovieScore.Parameters.AddWithValue("@MovieID", movieID);
                    updateMovieScore.Parameters.AddWithValue("@Score", score);
                    updateMovieScore.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }
    }
}