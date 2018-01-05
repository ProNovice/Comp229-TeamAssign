<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MovieAddition.aspx.cs" Inherits="Comp229_TeamAssign.MovieAddition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container page-context" style="">
        <label class="subtitle">Add Movie</label>
        <br />
        <hr />
        <br />
        <table id="tblMovieInfo" class="tblMovieInfo">
            <tr>
                <td id="tdPicture" class="tdPicture">
                    <img id="movieImage" runat="server" class="movieImage" src="https://pdfimages.wondershare.com/forms-templates/medium/movie-poster-template-3.png" />
                    <p>
                        Image url: 
                    <asp:TextBox CssClass="form-control center-block" ID="txtImageUrl" runat="server" type="text"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="newMovie" runat="server" ControlToValidate="txtImageUrl" ErrorMessage="This field cannot be empty." ForeColor="Red"></asp:RequiredFieldValidator>
                    </p>
                    <asp:Button ID="applyImageButton" runat="server" Text="Preview Image" OnClick="PreviewImageBtn_Click" CssClass="btn" />
                </td>
                <td>
                    <p>
                        <label class="lblMovieInfoIndex">Title: </label>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="lblMovieInfo" OnTextChanged="MovieInfo_Changed"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="newMovie" runat="server" ControlToValidate="txtTitle" ErrorMessage="This field cannot be empty." ForeColor="Red"></asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <label class="lblMovieInfoIndex">Genre: </label>
                        <asp:TextBox ID="txtGenre" runat="server" CssClass="lblMovieInfo" OnTextChanged="MovieInfo_Changed"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="newMovie" runat="server" ControlToValidate="txtGenre" ErrorMessage="This field cannot be empty." ForeColor="Red"></asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <label class="lblMovieInfoIndex">Director: </label>
                        <asp:TextBox ID="txtDirector" runat="server" CssClass="lblMovieInfo" OnTextChanged="MovieInfo_Changed"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="newMovie" runat="server" ControlToValidate="txtDirector" ErrorMessage="This field cannot be empty." ForeColor="Red"></asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <label class="lblMovieInfoIndex">Company: </label>
                        <asp:TextBox ID="txtCompany" runat="server" CssClass="lblMovieInfo" OnTextChanged="MovieInfo_Changed"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="newMovie" runat="server" ControlToValidate="txtCompany" ErrorMessage="This field cannot be empty." ForeColor="Red"></asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <label class="lblMovieInfoIndex">Published Date: </label>
                        <input ID="inputPublishedDate" type="date" runat="server" CssClass="lblMovieInfo" OnTextChanged="MovieInfo_Changed" />
                        <asp:RequiredFieldValidator ValidationGroup="newMovie" runat="server" ControlToValidate="inputPublishedDate" ErrorMessage="This field cannot be empty." ForeColor="Red"></asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <label class="lblMovieInfoIndex">Duration: </label>
                        <asp:TextBox ID="txtDuration" runat="server" CssClass="lblMovieInfo" OnTextChanged="MovieInfo_Changed" type="number"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="newMovie" runat="server" ControlToValidate="txtDuration" ErrorMessage="This field cannot be empty." ForeColor="Red"></asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <label class="lblMovieInfoIndex">Official Link: </label>
                        <asp:TextBox ID="txtOfficialLink" runat="server" CssClass="lblMovieInfo" OnTextChanged="MovieInfo_Changed"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="newMovie" runat="server" ControlToValidate="txtOfficialLink" ErrorMessage="This field cannot be empty." ForeColor="Red"></asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <label class="lblMovieInfoIndex">Status: </label>
                        <asp:TextBox ID="txtMovieStatus" runat="server" CssClass="lblMovieInfo" OnTextChanged="MovieInfo_Changed"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="newMovie" runat="server" ControlToValidate="txtMovieStatus" ErrorMessage="This field cannot be empty." ForeColor="Red"></asp:RequiredFieldValidator>
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox placeholder="Add some description here..." TextMode="MultiLine" CssClass="textArea float-right" Rows="5" ID="txtDescription" runat="server" type="text"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="newMovie" runat="server" ControlToValidate="txtDescription" ErrorMessage="This field cannot be empty." ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button runat="server" ID="btnAdd" Text="Add Movie" OnClick="AddMovieBtn_Click" CssClass="btn btn-warning float-right" ValidationGroup="newMovie" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
