<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MovieDetail.aspx.cs" Inherits="Comp229_TeamAssign.Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container page-context" style="">
        <label id="lblMovieTitle" runat="server" class="subtitle">Movie Title</label>
        <br />
        <hr />
        <br />
        <table id="tblMovieInfo" runat="server" class="tblMovieInfo">
            <tr>
                <td id="tdPicture" class="tdPicture">
                    <img id="movieImage" runat="server" class="movieImage" src="https://pdfimages.wondershare.com/forms-templates/medium/movie-poster-template-3.png" />
                </td>
                <td>
                    <p>
                        <label class="lblMovieInfoIndex">Title: </label>
                        <asp:Label ID="lblTitle" runat="server" CssClass="lblMovieInfo"></asp:Label>
                    </p>
                    <p>
                        <label class="lblMovieInfoIndex">Genre: </label>
                        <asp:Label ID="lblGenre" runat="server" CssClass="lblMovieInfo"></asp:Label>
                    </p>
                    <p>
                        <label class="lblMovieInfoIndex">Director: </label>
                        <asp:Label ID="lblDirector" runat="server" CssClass="lblMovieInfo"></asp:Label>
                    </p>
                    <p>
                        <label class="lblMovieInfoIndex">Company: </label>
                        <asp:Label ID="lblCompany" runat="server" CssClass="lblMovieInfo"></asp:Label>
                    </p>
                    <p>
                        <label class="lblMovieInfoIndex">Published Date: </label>
                        <asp:Label ID="lblPublishedDate" runat="server" CssClass="lblMovieInfo"></asp:Label>
                    </p>
                    <p>
                        <label class="lblMovieInfoIndex">Duration: </label>
                        <asp:Label ID="lblDuration" runat="server" CssClass="lblMovieInfo"></asp:Label>
                    </p>
                    <p>
                        <label class="lblMovieInfoIndex">Status: </label>
                        <asp:Label ID="lblStatus" runat="server" CssClass="lblMovieInfo"></asp:Label>
                    </p>
                    <p>
                        <label class="lblMovieInfoIndex">Review Score: </label>
                        <asp:Label ID="lblReviewScore" runat="server" CssClass="lblMovieInfo"></asp:Label>
                    </p>
                    <a id="aOfficialSite" runat="server" href="temporal" class="btn btn-warning">Go to Official Website</a>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <p id="txtDescription" class="txtDescription" runat="server">
                        The Martian is a 2015 science fiction film directed by Ridley Scott and starring Matt Damon. The screenplay by Drew Goddard is based on Andy Weir‘‘s 2011 novel of the same name about an astronaut who is mistakenly presumed dead and left behind on Mars. The film depicts his struggle to survive and others’‘ efforts to rescue him. It also stars Jessica Chastain, Kristen Wiig, Jeff Daniels, Michael Pena, Kate Mara, Sean Bean, Sebastian Stan, Donald Glover, Aksel Hennie, and Chiwetel Ejiofor.
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button runat="server" ID="btnHide" Text="Hide" CssClass="btn btn-warning float-right" OnClick="HideBtn_Click" />
                    <asp:Button runat="server" ID="btnUnhide" Text="Unhide" Visible="false" CssClass="btn btn-warning float-right" OnClick="UnhideBtn_Click" />
                    <asp:Button runat="server" ID="btnRent" Text="Rent" CssClass="btn btn-warning float-right" OnClick="RentBtn_Click" />
                    <asp:Button runat="server" ID="btnReturn" Text="Return" Visible="false" CssClass="btn btn-warning float-right" OnClick="RentBtn_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <label id="lblMovieStatusFeedback" runat="server" class="float-right"></label>
                </td>
            </tr>
        </table>
        <label class="subtitle">Review</label>
        <asp:Repeater runat="server" ID="ReviewRepeater">
            <ItemTemplate>
                <table id="tblReview" class="tblReview tblMovieInfo">
                    <tr>
                        <td class="col-md-3">
                            <asp:Label runat="server" ID="lblReviewWriter" Text='<%# Eval("UserName") %>'></asp:Label></td>
                        <td class="col-md-3">
                            <asp:Label runat="server" ID="lblReviewDate" Text='<%# Eval("revDate") %>'></asp:Label></td>
                        <td class="col-md-3">
                            <label>Score: </label>
                            <asp:Label runat="server" ID="lblReviewScore" Text='<%# Eval("Score") %>'></asp:Label>
                            <asp:TextBox runat="server" ID="txtEditReviewScore" Text='<%# Eval("Score") %>' type="number" Visible="false"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="editReviewScoreValidator" ControlToValidate="txtEditReviewTest" ValidationGroup="editReview" ErrorMessage="This field cannot be empty" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td class="col-md-3">
                            <asp:Button ID="btnEdit" OnClick="EditReviewBtn_Click" CommandName="editReview" CommandArgument='<%# Eval("ReviewID") %>' runat="server" CssClass="btn btn-warning float-right" ValidationGroup="editReview" Text="Edit" />
                            <asp:Button ID="btnCancel" OnClick="CancelEditingReviewBtn_Click" CommandName="cancelReview" CommandArgument='<%# Eval("ReviewID") %>' runat="server" CssClass="btn btn-warning float-right" Text="Cancel" Visible="false" />
                            <asp:Button ID="btnConfirm" OnClick="ConfirmEditingReviewBtn_Click" CommandName="confirmReview" CommandArgument='<%# Eval("ReviewID") %>' runat="server" CssClass="btn btn-warning float-right" ValidationGroup="editReview" Text="Confirm" Visible="false" />
                            <asp:Button ID="btnDelete" OnClick="DeleteReviewBtn_Click" CommandName="deleteReview" CommandArgument='<%# Eval("ReviewID") %>' runat="server" CssClass="btn btn-warning float-right" Text="Delete" />
                            <asp:Label ID="lblEditingReviewFeedback" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3">
                            <p id="txtReview" class="txtReview" runat="server"><%# Eval("Opinion") %></p>
                            <asp:TextBox runat="server" ID="txtEditReviewText" ValidationGroup="editReview" Visible="false" Text='<%# Eval("Opinion") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="editReviewTextValidator" ControlToValidate="txtEditReviewText" ValidationGroup="editReview" ErrorMessage="This field cannot be empty" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
        <table id="tblWritingReview" runat="server" class="tblReview tblMovieInfo">
            <tr>
                <td class="col-md-3">
                    <asp:Label runat="server" ID="lblWritingReviewWriter">User Name</asp:Label></td>
                <td class="col-md-3">
                    <asp:Label runat="server" ID="lblWritingReviewScore" Text="Score: "></asp:Label><asp:TextBox ID="txtReviewScore" runat="server" type="number" CssClass="txtReviewScore" Text="10"></asp:TextBox></td>
                <td class="col-md-3">
                    <asp:Button ID="btnWrite" runat="server" CssClass="btn btn-warning float-right" OnClick="WriteReviewBtn_Click" ValidationGroup="writingReview" Text="Write" />
                    <asp:Label ID="lblWritingReviewFeedback" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="2">
                    <asp:TextBox TextMode="MultiLine" runat="server" ID="txtReviewComment" Columns="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="txtReviewCommentValidator" runat="server" ValidationGroup="writingReview" ControlToValidate="txtReviewComment" ErrorMessage="This field cannot be empty" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
