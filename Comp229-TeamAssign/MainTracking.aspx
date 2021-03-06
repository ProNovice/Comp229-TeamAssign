﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MainTracking.aspx.cs" Inherits="Comp229_TeamAssign.MainTracking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="banner">
        <!-- Carousel -->
        <!-- source: https://www.w3schools.com/bootstrap/bootstrap_carousel.asp -->
        <div id="myCarousel" class="carousel slide" data-ride="carousel">
            <!-- Indicators -->
            <ol class="carousel-indicators">
                <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                <li data-target="#myCarousel" data-slide-to="1"></li>
                <li data-target="#myCarousel" data-slide-to="2"></li>
                <li data-target="#myCarousel" data-slide-to="3"></li>
            </ol>

            <!-- Wrapper for slides -->
            <div class="carousel-inner">
                <div class="item active">
                    <img src="../Assets/MMBanner.jpg" style="width: 100%;" />
                </div>
                <div class="item">
                    <img src="http://images6.fanpop.com/image/photos/40500000/Justice-League-2017-Poster-You-Can-t-Save-the-World-Alone-justice-league-movie-40583604-1500-500.jpg" style="width: 100%;" />
                </div>
                <div class="item">
                    <img src="https://www.bleedingcool.com/wp-content/uploads/2017/09/1500x500.jpg" style="width: 100%;" />
                </div>
                <div class="item">
                    <img src="https://allwallpapers.info/wp-content/uploads/2016/05/47260-captain-america-the-winter-soldier-poster-1920x1080-movie-wallpaper-1500x500.jpeg" style="width: 100%;" />
                </div>
            </div>

            <!-- Left and Right controls -->
            <a class="left carousel-control" href="#myCarousel" data-slide="prev">
                <span class="glyphicon glyphicon-chevron-left"></span>
                <span class="sr-only">Previous</span>
            </a>
            <a class="right carousel-control" href="#myCarousel" data-slide="next">
                <span class="glyphicon glyphicon-chevron-right"></span>
                <span class="sr-only">Next</span>
            </a>
        </div>
    </div>
    <br />

    <div class="container page-context" style="">
        <br />
        <label style="font-size: large">Search By:</label>
        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" Height="30px"
            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
            <asp:ListItem Text="Title"></asp:ListItem>
            <asp:ListItem Text="Genre"></asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox Height="30px" ID="txtSearch" runat="server" placeholder="Insert data here"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search"
            OnClick="btnSearch_Click" CssClass="btn btn-primary" ValidationGroup="InsertValidation" />
        <asp:RequiredFieldValidator ID="txtSearchRequiredVal" runat="server" ErrorMessage="Cannot Be Empty" ValidationGroup="InsertValidation" ControlToValidate="txtSearch" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <hr />
        <br />
    </div>
    <%--Display Search Results--%>
    <div class="container">
        <asp:Label ID="lblSearchMovieList" runat="server" class="subtitle">Search Results:</asp:Label>

        <asp:DataList runat="server" ID="MovieList" RepeatColumns="2" RepeatLayout="Table" OnItemCommand="MovieList_ItemCommand">
            <ItemTemplate>
                <table class="table">
                    <tr>
                        <th colspan="2">
                            <p class="movieTitle">
                                <asp:LinkButton ID="movieTitle" runat="server"
                                    Text=' <%#Eval("Title")%>'
                                    CommandName="MoreDetail"
                                    CommandArgument='<%#Eval("MovieID")%>' />
                            </p>
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <asp:Image ID="movieListPicture" ImageUrl='<%# Eval("PictureUrl") %>' Width="200px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td><b>Genre:</b></td>
                        <td>
                            <%# Eval("Genre") %>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Length:</b></td>
                        <td><%# Eval("Duration") %></td>
                    </tr>
                    <tr>
                        <td><b>Review Score:</b></td>
                        <td><%# Eval("ReviewScore") %></td>
                    </tr>
                    <tr>
                        <td><b>Status: </b></td>
                        <td><%# Eval("Status") %></td>
                    </tr>
                </table>
            </ItemTemplate>

        </asp:DataList>
        <hr />
        <%--Display Loaned Movie List--%>
        <asp:Label ID="lblLoanedMovie" runat="server" class="subtitle">Loaned Movies:</asp:Label>
        <asp:DataList ID="loanedMovie" BackColor="black" ForeColor="White" runat="server" RepeatColumns="4" RepeatLayout="Table" OnItemCommand="loanedMovie_ItemCommand">
            <ItemTemplate>
                <table class="table">
                    <tr>
                        <th colspan="2">
                            <p class="movieTitle">
                                <asp:LinkButton ID="movieTitle" runat="server"
                                    Text=' <%#Eval("Title")%>'
                                    CommandName="MoreDetail"
                                    CommandArgument='<%#Eval("MovieID")%>' />
                            </p>
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <asp:Image ID="movieListPicture" ImageUrl='<%# Eval("PictureUrl") %>' Width="200px" Height="350px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td><b>Genre:</b></td>
                        <td>
                            <%# Eval("Genre") %>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Length:</b></td>
                        <td><%# Eval("Duration") %></td>
                    </tr>
                    <tr>
                        <td><b>Review Score:</b></td>
                        <td><%# Eval("ReviewScore") %></td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
        <hr />
        <%--Display Recent Movies List--%>
        <asp:Label ID="lblRecentMovies" runat="server" class="subtitle">Recent Movies:</asp:Label>
        <asp:DataList ID="recentMovies" BackColor="black" ForeColor="White" runat="server" RepeatColumns="4" RepeatLayout="Table" OnItemCommand="recentMovie_ItemCommand">
            <ItemTemplate>
                <table class="table">
                    <tr>
                        <th colspan="2">
                            <p class="movieTitle">
                                <asp:LinkButton ID="movieTitle" runat="server"
                                    Text=' <%#Eval("Title")%>'
                                    CommandName="MoreDetail"
                                    CommandArgument='<%#Eval("MovieID")%>' />
                            </p>
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <asp:Image ID="movieListPicture" ImageUrl='<%# Eval("PictureUrl") %>' Width="200px" Height="350px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td><b>Genre:</b></td>
                        <td>
                            <%# Eval("Genre") %>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Length:</b></td>
                        <td><%# Eval("Duration") %></td>
                    </tr>
                    <tr>
                        <td><b>Review Score:</b></td>
                        <td><%# Eval("ReviewScore") %></td>
                    </tr>
                    <tr>
                        <td><b>Status:</b></td>
                        <td><%# Eval("Status") %></td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
        <hr />
        <%-- Display Movie List--%>
        <asp:Label ID="lblMovieList" runat="server" class="subtitle">Movie List</asp:Label>
        <asp:DataList ID="movieRepeater" runat="server" RepeatColumns="4" RepeatLayout="Table" OnItemCommand="movieList_ItemCommand">
            <ItemTemplate>
                <table class="table">
                    <tr>
                        <th colspan="2">
                            <p class="movieTitle">
                                <asp:LinkButton ID="movieTitle" runat="server"
                                    Text=' <%#Eval("Title")%>'
                                    CommandName="MoreDetail"
                                    CommandArgument='<%#Eval("MovieID")%>' />
                            </p>
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <asp:Image ID="movieListPicture" ImageUrl='<%# Eval("PictureUrl") %>' Width="200px" Height="350px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td><b>Genre:</b></td>
                        <td>
                            <%# Eval("Genre") %>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Length:</b></td>
                        <td><%# Eval("Duration") %></td>
                    </tr>
                    <tr>
                        <td><b>Review Score:</b></td>
                        <td><%# Eval("ReviewScore") %></td>
                    </tr>
                    <tr>
                        <td><b>Status:</b></td>
                        <td><%# Eval("Status") %></td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
    </div>
</asp:Content>
