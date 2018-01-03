<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="MainTracking.aspx.cs" Inherits="Comp229_TeamAssign.MainTracking" %>

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
            </ol>

            <!-- Wrapper for slides -->
            <div class="carousel-inner">
                <div class="item active">
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
    <div class="container page-context" style="">
        <label class="subtitle">Movie List</label>
        <br />
        <label>Search By:</label>
          <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True"
             OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
        <asp:ListItem Text="All"></asp:ListItem>
        <asp:ListItem Text="Title"></asp:ListItem>       
        <asp:ListItem Text="Genre"></asp:ListItem>
        <asp:ListItem Text="Director"></asp:ListItem>
        </asp:DropDownList>
          <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search"
                onclick="btnSearch_Click" />
        <br />
        <hr />
        <br />
        <div class="row movieListRow">
        <div class="col-md-6">
                <table runat="server" cssclass="movieListTable">
                    <tr>
                        <td>
                            <a href="https://www.foxmovies.com/movies/the-martian">
                                <asp:Image CssClass="movieListPicture" ImageUrl="http://media.moviemanager.biz/movies/The-Martian-3D_25895_posterlarge.jpg" runat="server" />
                            </a>
                        </td>
                        <td>
                            <div class="movieListInfo" runat="server">
                                <p class="movieTitle">Martion</p>
                                <p>Genre: SF</p>
                                <p>Time: 120 min</p>
                                <p>Score: 5</p>
                                <p>Status: Owned</p>
                                <asp:Button ID="btnViewDetail" runat="server" CssClass="btn btn-warning" Text="View Detail" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>            <div class="col-md-6">
                <table runat="server" cssclass="movieListTable">
                    <tr>
                        <td>
                            <a href="https://www.foxmovies.com/movies/the-martian">
                                <asp:Image CssClass="movieListPicture" ImageUrl="http://media.moviemanager.biz/movies/The-Martian-3D_25895_posterlarge.jpg" runat="server" />
                            </a>
                        </td>
                        <td>
                            <div class="movieListInfo" runat="server">
                                <p class="movieTitle">Martion</p>
                                <p>Genre: SF</p>
                                <p>Time: 120 min</p>
                                <p>Score: 5</p>
                                <p>Status: Owned</p>
                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning" Text="View Detail" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="row movieListRow">
        <div class="col-md-6">
                <table runat="server" cssclass="movieListTable">
                    <tr>
                        <td>
                            <a href="https://www.foxmovies.com/movies/the-martian">
                                <asp:Image CssClass="movieListPicture" ImageUrl="http://media.moviemanager.biz/movies/The-Martian-3D_25895_posterlarge.jpg" runat="server" />
                            </a>
                        </td>
                        <td>
                            <div class="movieListInfo" runat="server">
                                <p class="movieTitle">Martion</p>
                                <p>Genre: SF</p>
                                <p>Time: 120 min</p>
                                <p>Score: 5</p>
                                <p>Status: Owned</p>
                                <asp:Button ID="Button2" runat="server" CssClass="btn btn-warning" Text="View Detail" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>            <div class="col-md-6">
                <table runat="server" cssclass="movieListTable">
                    <tr>
                        <td>
                            <a href="https://www.foxmovies.com/movies/the-martian">
                                <asp:Image CssClass="movieListPicture" ImageUrl="http://media.moviemanager.biz/movies/The-Martian-3D_25895_posterlarge.jpg" runat="server" />
                            </a>
                        </td>
                        <td>
                            <div class="movieListInfo" runat="server">
                                <p class="movieTitle">Martion</p>
                                <p>Genre: SF</p>
                                <p>Time: 120 min</p>
                                <p>Score: 5</p>
                                <p>Status: Owned</p>
                                <asp:Button ID="Button3" runat="server" CssClass="btn btn-warning" Text="View Detail" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="row movieListRow">
        <div class="col-md-6">
                <table runat="server" cssclass="movieListTable">
                    <tr>
                        <td>
                            <a href="https://www.foxmovies.com/movies/the-martian">
                                <asp:Image CssClass="movieListPicture" ImageUrl="http://media.moviemanager.biz/movies/The-Martian-3D_25895_posterlarge.jpg" runat="server" />
                            </a>
                        </td>
                        <td>
                            <div class="movieListInfo" runat="server">
                                <p class="movieTitle">Martion</p>
                                <p>Genre: SF</p>
                                <p>Time: 120 min</p>
                                <p>Score: 5</p>
                                <p>Status: Owned</p>
                                <asp:Button ID="Button4" runat="server" CssClass="btn btn-warning" Text="View Detail" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>            <div class="col-md-6">
                <table runat="server" cssclass="movieListTable">
                    <tr>
                        <td>
                            <a href="https://www.foxmovies.com/movies/the-martian">
                                <asp:Image CssClass="movieListPicture" ImageUrl="http://media.moviemanager.biz/movies/The-Martian-3D_25895_posterlarge.jpg" runat="server" />
                            </a>
                        </td>
                        <td>
                            <div class="movieListInfo" runat="server">
                                <p class="movieTitle">Martion</p>
                                <p>Genre: SF</p>
                                <p>Time: 120 min</p>
                                <p>Score: 5</p>
                                <p>Status: Owned</p>
                                <asp:Button ID="Button5" runat="server" CssClass="btn btn-warning" Text="View Detail" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="row movieListRow">
        <div class="col-md-6">
                <table runat="server" cssclass="movieListTable">
                    <tr>
                        <td>
                            <a href="https://www.foxmovies.com/movies/the-martian">
                                <asp:Image CssClass="movieListPicture" ImageUrl="http://media.moviemanager.biz/movies/The-Martian-3D_25895_posterlarge.jpg" runat="server" />
                            </a>
                        </td>
                        <td>
                            <div class="movieListInfo" runat="server">
                                <p class="movieTitle">Martion</p>
                                <p>Genre: SF</p>
                                <p>Time: 120 min</p>
                                <p>Score: 5</p>
                                <p>Status: Owned</p>
                                <asp:Button ID="Button6" runat="server" CssClass="btn btn-warning" Text="View Detail" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>            <div class="col-md-6">
                <table runat="server" cssclass="movieListTable">
                    <tr>
                        <td>
                            <a href="https://www.foxmovies.com/movies/the-martian">
                                <asp:Image CssClass="movieListPicture" ImageUrl="http://media.moviemanager.biz/movies/The-Martian-3D_25895_posterlarge.jpg" runat="server" />
                            </a>
                        </td>
                        <td>
                            <div class="movieListInfo" runat="server">
                                <p class="movieTitle">Martion</p>
                                <p>Genre: SF</p>
                                <p>Time: 120 min</p>
                                <p>Score: 5</p>
                                <p>Status: Owned</p>
                                <asp:Button ID="Button7" runat="server" CssClass="btn btn-warning" Text="View Detail" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:Repeater ID="loanedMovieRepeater" runat="server">
            <ItemTemplate>
                <asp:Table runat="server" ID="movieListTable" CssClass="container col-md-3">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Image ID="movieListPicture" ImageUrl='<%# Eval("PictureUrl") %>' runat="server" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <div id="movieListInfo" runat="server">
                                <p class="movieTitle"><%# Eval("Title") %></p>
                                <p><%# Eval("Genre") %></p>
                                <p><%# Eval("Duration") %></p>
                                <p><%# Eval("Director") %></p>
                                <p><%# Eval("Company") %></p>
                                <p><%# Eval("ReviewScore") %></p>
                                <p><%# Eval("Status") %></p>
                                <asp:Button ID="btnViewDetail" runat="server" CssClass="btn btn-warning" Text="View Detail" />
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </ItemTemplate>
        </asp:Repeater>
        <hr />
        <asp:Repeater ID="movieRepeater" runat="server">
            <ItemTemplate>
                <asp:Table runat="server" ID="movieListTable" CssClass="container col-md-3">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Image ID="movieListPicture" ImageUrl='<%# Eval("PictureUrl") %>' runat="server" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <div id="movieListInfo" runat="server">
                                <p class="movieTitle"><%# Eval("Title") %></p>
                                <p><%# Eval("Genre") %></p>
                                <p><%# Eval("Duration") %></p>
                                <p><%# Eval("Director") %></p>
                                <p><%# Eval("Company") %></p>
                                <p><%# Eval("ReviewScore") %></p>
                                <p><%# Eval("Status") %></p>
                                <asp:Button ID="btnViewDetail" runat="server" CssClass="btn btn-warning" Text="View Detail" />
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
