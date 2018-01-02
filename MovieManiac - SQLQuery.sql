USE [MovieManiac]
--create tables
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE MovieManiac.[dbo].[Account] (
[MemberID] [int] NOT NULL, 
[UserName] [varchar](20) NOT NULL, 
[Password] [varchar](20) NOT NULL, 
[Email] [varchar](30) NOT NULL, 
[LoanedCount] [int] DEFAULT 0 NOT NULL, 
[position] [varchar](20) DEFAULT 'member' NOT NULL, 
CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED
(
	[MemberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE MovieManiac.[dbo].[RelatedMovie] (
RelatedMovieID int NOT NULL, 
MovieID int NOT NULL, 
MemberID int NOT NULL, 
Status varchar(20) NOT NULL,
CONSTRAINT [PK_RelatedMovie] PRIMARY KEY CLUSTERED
(
	[RelatedMovieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE MovieManiac.[dbo].[Movie] 
(MovieID int NOT NULL, 
Title varchar(50) NOT NULL, 
Genre varchar(50) NOT NULL, 
Director varchar(50), 
Company varchar(50), 
PublishedDate date, 
Duration int, 
OfficialLink varchar(255), 
Description varchar(1000), 
ReviewScore double precision DEFAULT 0 NOT NULL,
Status varchar(20) DEFAULT 'Owned' NOT NULL, 
PostedDate date NOT NULL, 
PictureUrl varchar(255) NOT NULL, 
CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED
(
	[MovieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE MovieManiac.[dbo].[Review] (
ReviewID int NOT NULL, 
MovieID int NOT NULL, 
MemberID int NOT NULL, 
Opinion varchar(1000), 
Score int NOT NULL,
revDate date NOT NULL, 
CONSTRAINT [PK_Review] PRIMARY KEY CLUSTERED
(
	[ReviewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
--alter table
ALTER TABLE MovieManiac.[dbo].RelatedMovie 
ADD CONSTRAINT FK_RelatedMovie_MemberID FOREIGN KEY (MemberID) REFERENCES Account(MemberID) 

ALTER TABLE MovieManiac.[dbo].Review 
ADD CONSTRAINT FK_Review_MemberID FOREIGN KEY (MemberID) REFERENCES Account(MemberID) 

ALTER TABLE MovieManiac.[dbo].RelatedMovie 
ADD CONSTRAINT FK_RelatedMovie_MovieID FOREIGN KEY (MovieID) REFERENCES Movie(MovieID) 

ALTER TABLE MovieManiac.[dbo].Review 
ADD CONSTRAINT FK_Review_MovieID FOREIGN KEY (MovieID) REFERENCES Movie(MovieID) 
--sequence
CREATE SEQUENCE seq_Account 
CREATE SEQUENCE seq_RelatedMovie 
CREATE SEQUENCE seq_Movie 
CREATE SEQUENCE seq_Review 
--insert 
INSERT INTO MovieManiac.[dbo].Account(MemberID, UserName, Password, Email, position) VALUES (1001, 'Movie Maniac', 'moviemaniac', 'moviemaniac@moviemaniac.com', 'Administrator')
INSERT INTO MovieManiac.[dbo].Account(MemberID, UserName, Password, Email) VALUES (1002, 'NyangNyang', 'nyangnyang2017', 'nyangnyang@gmail.com')
INSERT INTO MovieManiac.[dbo].Account(MemberID, UserName, Password, Email) VALUES (1003, 'Norisis', 'norisis', 'norisis@gmail.com')
INSERT INTO MovieManiac.[dbo].Account(MemberID, UserName, Password, Email) VALUES (1004, 'MovieBlind', 'movieblind', 'movieblind@gmail.com')
INSERT INTO MovieManiac.[dbo].Account(MemberID, UserName, Password, Email) VALUES (1005, 'Master Movie', 'mastermovie', 'mastermovie@gmail.com')
INSERT INTO MovieManiac.[dbo].Account(MemberID, UserName, Password, Email) VALUES (1006, 'Lunatic Man', 'lunaticman', 'lunaticman@gmail.com')
INSERT INTO MovieManiac.[dbo].Account(MemberID, UserName, Password, Email) VALUES (1007, 'Super Power Monster', 'superpowermonster', 'superpowermonster@gmail.com')
INSERT INTO MovieManiac.[dbo].Account(MemberID, UserName, Password, Email) VALUES (1008, 'FireDragon', 'firedragon', 'firedragon@gmail.com')
INSERT INTO MovieManiac.[dbo].Account(MemberID, UserName, Password, Email) VALUES (1009, 'ShrekIsMyWife', 'shrekismywife', 'shrekismywife@gmail.com')
INSERT INTO MovieManiac.[dbo].Account(MemberID, UserName, Password, Email) VALUES (1010, 'KingGeneralEmperor', 'kinggeneralemperor', 'kinggeneralemperor@gmail.com')

INSERT INTO MovieManiac.[dbo].Movie(MovieID, Title, Genre, Director, Company, PublishedDate, Duration, OfficialLink, Description, PostedDate, PictureUrl) VALUES 
(2001, 'The Martian', 'SF', 'Ridley Scott', 'Scott Free Productions', '2015-09-24', 141, 
'https://www.foxmovies.com/movies/the-martian', 
'The Martian is a 2015 science fiction film directed by Ridley Scott and starring Matt Damon. The screenplay by Drew Goddard is based on Andy Weir''s 2011 novel of the same name about an astronaut who is mistakenly presumed dead and left behind on Mars. The film depicts his struggle to survive and others'' efforts to rescue him. It also stars Jessica Chastain, Kristen Wiig, Jeff Daniels, Michael Pena, Kate Mara, Sean Bean, Sebastian Stan, Donald Glover, Aksel Hennie, and Chiwetel Ejiofor.', 
'2015-09-30',
'https://upload.wikimedia.org/wikipedia/en/c/cd/The_Martian_film_poster.jpg')


INSERT INTO MovieManiac.[dbo].Movie(MovieID, Title, Genre, Director, Company, PublishedDate, Duration, OfficialLink, Description, PostedDate, PictureUrl) VALUES 
(2002, 'The Matrix', 'SF', 'The Wachowski Brothers', 'Village Roadshow Pictures', '1999-03-31', 136, 
'https://www.warnerbros.com/matrix', 
'The Matrix is a 1999 science fiction action film written and directed by The Wachowski Brothers and starring Keanu Reeves, Laurence Fishburne, Carrie-Anne Moss, Hugo Weaving, and Joe Pantoliano. It depicts a dystopianfuture in which reality as perceived by most humans is actually a simulated reality called "the Matrix", created by sentient machines to subdue the human population, while their bodies'' heat and electrical activity are used as an energy source. Computer programmer Neo learns this truth and is drawn into a rebellion against the machines, which involves other people who have been freed from the "dream world."', 
'2016-10-15',
'https://www.warnerbros.com/sites/default/files/styles/key_art_270x400/public/matrix_keyart.jpg?itok=SrkeFYxx')



INSERT INTO Movie(MovieID, Title, Genre, Director, Company, PublishedDate, Duration, OfficialLink, Description, PostedDate, PictureUrl) VALUES 
(2003, 'Deadpool', 'Action', 'Tim Miller', 'Marvel Entertainment', '2016-02-08', 151, 
'https://www.foxmovies.com/movies/deadpool', 
'Deadpool is a 2016 American superhero film based on the Marvel Comics character of the same name, distributed by 20th Century Fox. It is the eighth installment of the X-Men film series. The film was directed by Tim Millerfrom a screenplay by Rhett Reese and Paul Wernick, and stars Ryan Reynolds in the title role alongside Morena Baccarin, Ed Skrein, T.J. Miller, Gina Carano, Leslie Uggams, Brianna Hildebrand, and Stefan Kapi?i?. In Deadpool, Wade Wilson hunts the man who gave him mutant abilities, but also a scarred physical appearance, as the antihero Deadpool.', 
'2016-09-20', 
'https://upload.wikimedia.org/wikipedia/en/thumb/4/46/Deadpool_poster.jpg/220px-Deadpool_poster.jpg')


INSERT INTO MovieManiac.[dbo].Movie(MovieID, Title, Genre, Director, Company, PublishedDate, Duration, OfficialLink, Description, PostedDate, PictureUrl) VALUES 
(2004, 'The Avengers', 'Action', 'Joss Whedon', 'Marvel Studios', '2015-09-24', 143, 
'http://marvel.com/avengers_movie/', 
'Marvel''s The Avengers (classified under the name Marvel Avengers Assemble in the United Kingdom and Ireland), or simply The Avengers, is a 2012 American superhero film based on the Marvel Comicssuperhero team of the same name, produced by Marvel Studios and distributed by Walt Disney Studios Motion Pictures.1 It is the sixth film in the Marvel Cinematic Universe. The film was written and directed by Joss Whedonand features an ensemble cast that includes Robert Downey Jr., Chris Evans, Mark Ruffalo, Chris Hemsworth, Scarlett Johansson, Jeremy Renner, Tom Hiddleston, Clark Gregg, Cobie Smulders, Stellan Skarsgard, and Samuel L. Jackson. In the film, Nick Fury, director of the peacekeeping organization S.H.I.E.L.D., recruits Iron Man, Captain America, the Hulk, and Thor to form a team that must stop Thor''s brother Loki from subjugating Earth.', 
'2015-09-30', 
'https://upload.wikimedia.org/wikipedia/en/thumb/f/f9/TheAvengers2012Poster.jpg/220px-TheAvengers2012Poster.jpg')

INSERT INTO MovieManiac.[dbo].Movie(MovieID, Title, Genre, Director, Company, PublishedDate, Duration, OfficialLink, Description, PostedDate, PictureUrl) VALUES 
(2005, 'The Cabin in the Woods', 'Horror', 'Drew Goddard', 'Mutant Enemy Productions','2011-04-24', 151, 
'http://discoverthecabininthewoods.com//', 
'The Cabin in the Woods is a 2012 American horror comedy film directed by Drew Goddard in his directorial debut, produced by Joss Whedon, and written by Whedon and Goddard. The film stars Kristen Connolly, Chris Hemsworth, Anna Hutchison, Fran Kranz, Jesse Williams, Richard Jenkins, and Bradley Whitford. The plot follows a group of college students who retreat to a remote forest cabin where they fall victim to backwoods zombies and the two technicians who manipulate the ongoing events from an underground facility.', 
'2015-09-30', 
'https://upload.wikimedia.org/wikipedia/en/thumb/8/84/The_Cabin_in_the_Woods_%282012%29_theatrical_poster.jpg/215px-The_Cabin_in_the_Woods_%282012%29_theatrical_poster.jpg')


INSERT INTO MovieManiac.[dbo].Movie(MovieID, Title, Genre, Director, Company, PublishedDate, Duration, OfficialLink, Description, PostedDate, PictureUrl) VALUES 
(2006, 'The Thing (1982 film)', 'Horror', 'John Carpenter', 'The Turman-Foster Company', '2015-09-24', 109, 
'https://en.wikipedia.org/wiki/The_Thing_(1982_film)', 
'The Thing (also known as John Carpenter''s The Thing) is a 1982 American science-fiction horror film directed by John Carpenter, written by Bill Lancaster, and starring Kurt Russell. The film''s title refers to its primary antagonist: a parasitic extraterrestrial lifeform that assimilates other organisms and in turn imitates them. The Thing infiltrates an Antarctic research station, taking the appearance of the researchers that it absorbs, and paranoia develops within the group.', 
'2015-09-30', 
'https://upload.wikimedia.org/wikipedia/en/thumb/a/a6/The_Thing_%281982%29_theatrical_poster.jpg/220px-The_Thing_%281982%29_theatrical_poster.jpg')


INSERT INTO MovieManiac.[dbo].Movie(MovieID, Title, Genre, Director, Company, PublishedDate, Duration, OfficialLink, Description, PostedDate, PictureUrl) VALUES 
(2007, '3 Idiots', 'Comedy', 'Rajkumar Hirani', 'Vidhu Vinod Chopra', '2009-12-25', 171, 
'https://en.wikipedia.org/wiki/3_Idiots', 
'3 Idiots is a 2009 Indian coming-of-age comedy-drama film, directed and written by Rajkumar Hirani, and produced by Vidhu Vinod Chopra, with screenplay by Abhijat Joshi, inspired by the novel Five Point Someone by Chetan Bhagat. The film stars Aamir Khan, R. Madhavan and Sharman Joshi in the title roles, along with Kareena Kapoor, Boman Irani and Omi Vaidya. The film is about the friendship of three students at an Indian engineering college, and is a satire about the social pressures under an Asian education system. It also incorporated real Indian inventions, from Remya Jose, Mohammad Idris, Jahangir Painter, and Sonam Wangchuk.', 
'2015-09-30', 
'https://upload.wikimedia.org/wikipedia/en/thumb/d/df/3_idiots_poster.jpg/220px-3_idiots_poster.jpg')


INSERT INTO MovieManiac.[dbo].Movie(MovieID, Title, Genre, Director, Company, PublishedDate, Duration, OfficialLink, Description, PostedDate, PictureUrl) VALUES 
(2008, 'Citizenfour', 'Documentary', 'Laura Poitras', 'HBO Films', '2015-09-24', 113, 
'https://citizenfourfilm.com/', 
'Citizenfour is a 2014 documentary film directed by Laura Poitras, concerning Edward Snowden and the NSA spying scandal. The film had its US premiere on October 10, 2014, at the New York Film Festival and its UK premiere on October 17, 2014, at the BFI London Film Festival. The film features Snowden and Glenn Greenwald, and was co-produced by Poitras, Mathilde Bonnefoy, and Dirk Wilutzky, with Steven Soderbergh and others serving as executive producers. Citizenfour received critical acclaim upon release, and was the recipient of numerous accolades, including the Academy Award for Best Documentary Feature at the 2015 Oscars.', 
'2015-09-30', 
'https://upload.wikimedia.org/wikipedia/en/3/37/Citizenfour_poster.jpg')


INSERT INTO MovieManiac.[dbo].Movie(MovieID, Title, Genre, Director, Company, PublishedDate, Duration, OfficialLink, Description, PostedDate, PictureUrl) VALUES 
(2009, 'Super Size Me', 'Documentary', 'Morgan Spurlock', 'The Con', '2004-05-07', 151, 
'https://en.wikipedia.org/wiki/Super_Size_Me', 
'Super Size Me is a 2004 American documentary film directed by and starring Morgan Spurlock, an American independent filmmaker. Spurlock''s film follows a 30-day period from February 1 to March 2, 2003, during which he ate only McDonald''s food. The film documents this lifestyle''s drastic effect on Spurlock''s physical and psychological well-being, and explores the fast food industry''s corporate influence, including how it encourages poor nutrition for its own profit.', 
'2015-11-30', 
'https://upload.wikimedia.org/wikipedia/en/thumb/6/6a/Super_Size_Me_Poster.jpg/220px-Super_Size_Me_Poster.jpg')


INSERT INTO MovieManiac.[dbo].RelatedMovie(RelatedMovieID, MovieID, MemberID, Status) VALUES (9001, 2001, 1001, 'loaned') 
INSERT INTO MovieManiac.[dbo].RelatedMovie(RelatedMovieID, MovieID, MemberID, Status) VALUES (9002, 2002, 1001, 'loaned') 
INSERT INTO MovieManiac.[dbo].RelatedMovie(RelatedMovieID, MovieID, MemberID, Status) VALUES (9003, 2003, 1002, 'loaned') 
INSERT INTO MovieManiac.[dbo].RelatedMovie(RelatedMovieID, MovieID, MemberID, Status) VALUES (9004, 2004, 1002, 'loaned') 
INSERT INTO MovieManiac.[dbo].RelatedMovie(RelatedMovieID, MovieID, MemberID, Status) VALUES (9005, 2005, 1003, 'loaned') 
INSERT INTO MovieManiac.[dbo].RelatedMovie(RelatedMovieID, MovieID, MemberID, Status) VALUES (9006, 2006, 1003, 'hidden') 
INSERT INTO MovieManiac.[dbo].RelatedMovie(RelatedMovieID, MovieID, MemberID, Status) VALUES (9007, 2003, 1004, 'hidden') 
INSERT INTO MovieManiac.[dbo].RelatedMovie(RelatedMovieID, MovieID, MemberID, Status) VALUES (9008, 2004, 1004, 'loaned') 
INSERT INTO MovieManiac.[dbo].RelatedMovie(RelatedMovieID, MovieID, MemberID, Status) VALUES (9009, 2005, 1005, 'hidden') 
INSERT INTO MovieManiac.[dbo].RelatedMovie(RelatedMovieID, MovieID, MemberID, Status) VALUES (9010, 2006, 1006, 'loaned') 
INSERT INTO MovieManiac.[dbo].RelatedMovie(RelatedMovieID, MovieID, MemberID, Status) VALUES (9011, 2001, 1007, 'hidden') 
INSERT INTO MovieManiac.[dbo].RelatedMovie(RelatedMovieID, MovieID, MemberID, Status) VALUES (9012, 2002, 1008, 'loaned') 
INSERT INTO MovieManiac.[dbo].RelatedMovie(RelatedMovieID, MovieID, MemberID, Status) VALUES (9013, 2002, 1009, 'loaned') 
INSERT INTO MovieManiac.[dbo].RelatedMovie(RelatedMovieID, MovieID, MemberID, Status) VALUES (9014, 2002, 1001, 'hidden') 
INSERT INTO MovieManiac.[dbo].RelatedMovie(RelatedMovieID, MovieID, MemberID, Status) VALUES (9015, 2009, 1001, 'loaned') 
INSERT INTO MovieManiac.[dbo].RelatedMovie(RelatedMovieID, MovieID, MemberID, Status) VALUES (9016, 2008, 1002, 'hidden') 
INSERT INTO MovieManiac.[dbo].RelatedMovie(RelatedMovieID, MovieID, MemberID, Status) VALUES (9017, 2008, 1002, 'loaned') 

INSERT INTO MovieManiac.[dbo].Review(ReviewID, MovieID, MemberID, Opinion, Score, revDate) VALUES (3001, 2009, 1010, '', 8.3, '2017-10-05')
INSERT INTO MovieManiac.[dbo].Review(ReviewID, MovieID, MemberID, Opinion, Score, revDate) VALUES (3002, 2001, 1001, 'Just for killing time', 5.2, '2017-10-03')
INSERT INTO MovieManiac.[dbo].Review(ReviewID, MovieID, MemberID, Opinion, Score, revDate) VALUES (3003, 2001, 1002, 'It was just average for me.', 4.6, '2017-10-04')
INSERT INTO MovieManiac.[dbo].Review(ReviewID, MovieID, MemberID, Opinion, Score, revDate) VALUES (3004, 2002, 1002, 'Hua!', 8.7, '2017-04-02')
INSERT INTO MovieManiac.[dbo].Review(ReviewID, MovieID, MemberID, Opinion, Score, revDate) VALUES (3005, 2002, 1001, 'Excellent!', 9.2, '2017-05-08')
INSERT INTO MovieManiac.[dbo].Review(ReviewID, MovieID, MemberID, Opinion, Score, revDate) VALUES (3006, 2003, 1003, 'Great!', 8.3, '2017-12-23')
INSERT INTO MovieManiac.[dbo].Review(ReviewID, MovieID, MemberID, Opinion, Score, revDate) VALUES (3007, 2004, 1004, 'You will miss the movie.', 10, '2017-07-13')
INSERT INTO MovieManiac.[dbo].Review(ReviewID, MovieID, MemberID, Opinion, Score, revDate) VALUES (3008, 2005, 1005, 'Incredible! It will bring you to 2 hours future!', 10,'2017-07-23')
INSERT INTO MovieManiac.[dbo].Review(ReviewID, MovieID, MemberID, Opinion, Score, revDate) VALUES (3009, 2006, 1007, 'Do I need to describe what a nice movie it is?', 10, '2017-08-13')
INSERT INTO MovieManiac.[dbo].Review(ReviewID, MovieID, MemberID, Opinion, Score, revDate) VALUES (3010, 2007, 1006, 'Shit', 0,'2017-09-25')
INSERT INTO MovieManiac.[dbo].Review(ReviewID, MovieID, MemberID, Opinion, Score, revDate) VALUES (3011, 2008, 1008, '', 7, '2017-11-30')
INSERT INTO MovieManiac.[dbo].Review(ReviewID, MovieID, MemberID, Opinion, Score, revDate) VALUES (3012, 2009, 1007, 'I watched it already 8 times.', 5,'2017-09-22')
INSERT INTO MovieManiac.[dbo].Review(ReviewID, MovieID, MemberID, Opinion, Score, revDate) VALUES (3013, 2003, 1005, 'Good for you', 8,'2017-08-18')
INSERT INTO MovieManiac.[dbo].Review(ReviewID, MovieID, MemberID, Opinion, Score, revDate) VALUES (3014, 2008, 1004, 'I recommend', 7, '2017-05-26')
INSERT INTO MovieManiac.[dbo].Review(ReviewID, MovieID, MemberID, Opinion, Score, revDate) VALUES (3015, 2007, 1003, 'Why did I watch this movie? Totally trash!', 1.8, '2017-01-12')
INSERT INTO MovieManiac.[dbo].Review(ReviewID, MovieID, MemberID, Opinion, Score, revDate) VALUES (3016, 2008, 1002, 'Wonderful!', 8.6, '2017-08-28')
INSERT INTO MovieManiac.[dbo].Review(ReviewID, MovieID, MemberID, Opinion, Score, revDate) VALUES (3017, 2009, 1001, 'Satisfying Movie', 9.8, '2017-07-18')
INSERT INTO MovieManiac.[dbo].Review(ReviewID, MovieID, MemberID, Opinion, Score, revDate) VALUES (3018, 2007, 1007, 'It enlightened me!', 9.9, '2017-07-09')
