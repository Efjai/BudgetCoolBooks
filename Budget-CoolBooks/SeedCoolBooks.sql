--Fyll på här så att vi får en bra SQL-seed att fylla på med mock-data



-- TA BORT DESCRIPTION PÅ GENRES NÄR DEN ÄR BORTTAGEN I MODELS!!!

-- FIXA BOOLEAN VÄRDE PÅ "ISDELETED" I BOOKS!!!

--SEED AUTHORS
Insert into Authors(Firstname, Lastname, Created)
values ('William', 'Faulkner', GetDate());
Insert into Authors(Firstname, Lastname, Created)
values ('Mark', 'Twain', GetDate());
Insert into Authors(Firstname, Lastname, Created)
values ('Lewis', 'Carroll', GetDate());
Insert into Authors(Firstname, Lastname, Created)
values ('Toni', 'Morrison', GetDate());
Insert into Authors(Firstname, Lastname, Created)
values ('Charles', 'Dickens', GetDate());
Insert into Authors(Firstname, Lastname, Created)
values ('Mary', 'Shelley', GetDate());
Insert into Authors(Firstname, Lastname, Created)
values ('Georg', 'Eliot', GetDate());
Insert into Authors(Firstname, Lastname, Created)
values ('Virginia', 'Woolf', GetDate());
Insert into Authors(Firstname, Lastname, Created)
values ('George', 'Orwell', GetDate());
Insert into Authors(Firstname, Lastname, Created)
values ('Jane', 'Austen', GetDate());
Insert into Authors(Firstname, Lastname, Created)
values ('J.D', 'Salinger', GetDate());
Insert into Authors(Firstname, Lastname, Created)
values ('F. Scott', 'Fitzgerald', GetDate());
Insert into Authors(Firstname, Lastname, Created)
values ('H.G.', 'Wells', GetDate());
Insert into Authors(Firstname, Lastname, Created)
values ('J.R.R.', 'Tolkien', GetDate());
Insert into Authors(Firstname, Lastname, Created)
values ('Chinua', 'Achebe', GetDate());
Insert into Authors(Firstname, Lastname, Created)
values ('Harper', 'Lee', GetDate());
Insert into Authors(Firstname, Lastname, Created)
values ('James', 'Joyce', GetDate());


--SEED GENRES
Insert into Genres(Name, Description, Created)
values ('Roman','', GetDate());

Insert into Genres(Name, Description, Created)
values ('Fantasy','', GetDate());

Insert into Genres(Name, Description, Created)
values ('Horror','', GetDate());

Insert into Genres(Name, Description, Created)
values ('Science Fiction','', GetDate());

Insert into Genres(Name, Description, Created)
values ('Thriller','', GetDate());

Insert into Genres(Name, Description, Created)
values ('Biography','', GetDate());

Insert into Genres(Name, Description, Created)
values ('Childrens','', GetDate());



--SEED BOOKS

Insert into Books(Title, Description, ISBN, Imagepath, IsDeleted, Created,
AuthorId, userId, GenreId)
values ('Absalom, Absalom!', 'Absalom, Absalom! is a novel by the American author William Faulkner, first published in 1936. Taking place before, during, and after the American Civil War, it is a story about three families of the American South, with a focus on the life of Thomas Sutpen.', '9789173370073', '/images/Absalom-Absalom.jpg', 'FALSE', GetDate(), 1, null, 1);

Insert into Books(Title, Description, ISBN, Imagepath, IsDeleted, Created,
AuthorId, userId, GenreId)
values ('Adventures of Huckleberry Finn', 'Mark Twains classic The Adventures of Huckleberry Finn (1884) is told from the point of view of Huck Finn, a barely literate teen who fakes his own death to escape his abusive, drunken father. He encounters a runaway slave named Jim, and the two embark on a raft journey down the Mississippi River.' , '9781910619872', '/images/Adventures-of-Huckleberry-Finn.jpg', 'FALSE', GetDate(), 2, null, 1);

Insert into Books(Title, Description, ISBN, Imagepath, IsDeleted, Created,
AuthorId, userId, GenreId)
values ('Alices Adventures in Wonderland', 'Alices Adventures in Wonderland by Lewis Carroll is a story about Alice who falls down a rabbit hole and lands into a fantasy world that is full of weird, wonderful people and animals. It is classic childrens book that is also popular with adults.', '9781447279990', '/images/Alices-Adventures-in-Wonderland.jpg', 'FALSE', GetDate(), 3, null, 7);

Insert into Books(Title, Description, ISBN, Imagepath, IsDeleted, Created,
AuthorId, userId, GenreId)
values ('Beloved', 'The work examines the destructive legacy of slavery as it chronicles the life of a Black woman named Sethe, from her pre-Civil War days as a slave in Kentucky to her time in Cincinnati, Ohio, in 1873. Although Sethe lives there as a free woman, she is held prisoner by memories of the trauma of her life as a slave.', '9780099760115', '/images/Beloved.jpg', 'FALSE', GetDate(), 4, null, 1);

Insert into Books(Title, Description, ISBN, Imagepath, IsDeleted, Created,
AuthorId, userId, GenreId)
values ('David Copperfield', 'The most autobiographical of Dickens works, David Copperfield tells the moving story of Davids journey from birth to maturity -- a journey that inextricably links his life with some of Dickens most extraordinary and colorful families.', '9129656273', '/images/David-Copperfield.jpg', 'FALSE', GetDate(), 5, null, 1);

Insert into Books(Title, Description, ISBN, Imagepath, IsDeleted, Created,
AuthorId, userId, GenreId)
values ('Frankenstein', 'Victor Frankenstein tells Walton his story—a happy childhood, an unhealthy obsession with alchemy, and his engagement to his cousin Elizabeth. Victor enrolls at the University of Ingolstadt, where he discovers the secret of life and builds a creature from dead bodies.', '9780141198965', '/images/Frankenstein.jpg', 'FALSE', GetDate(), 6, null, 3);

Insert into Books(Title, Description, ISBN, Imagepath, IsDeleted, Created,
AuthorId, userId, GenreId)
values ('Great Expectations', 'Great Expectations follows the childhood and young adult years of Pip a blacksmiths apprentice in a country village. He suddenly comes into a large fortune (his great expectations) from a mysterious benefactor and moves to London where he enters high society.', '9788171674589', '/images/Great-Expectations.jpg', 'FALSE', GetDate(), 5, null, 1);

Insert into Books(Title, Description, ISBN, Imagepath, IsDeleted, Created,
AuthorId, userId, GenreId)
values ('Middlemarch', 'Set in Middlemarch, a fictional English Midland town, in 1829 to 1832, it follows distinct, intersecting stories with many characters. Issues include the status of women, the nature of marriage, idealism, self-interest, religion, hypocrisy, political reform, and education.', '9780143107729', '/images/Middlemarch.jpg', 'FALSE', GetDate(), 7, null, 1);

Insert into Books(Title, Description, ISBN, Imagepath, IsDeleted, Created,
AuthorId, userId, GenreId)
values ('Mrs Dalloway', 'Mrs. Dalloway, through its depiction of Clarissa and Septimus, who can be seen as foils for each other, and of the political atmosphere in Britain during the 1920s, explores the fragmented yet fluid nature of time and the interconnectedness of perception and reality across individuals and social spheres.', '9789174999396', '/images/Mrs-Dalloway.jpg', 'FALSE', GetDate(), 8, null, 1);

Insert into Books(Title, Description, ISBN, Imagepath, IsDeleted, Created,
AuthorId, userId, GenreId)
values ('1984 (Nineteen Eighty-Four)', '1984 is a dystopian novella by George Orwell published in 1949, which follows the life of Winston Smith, a low ranking member of the Party, who is frustrated by the omnipresent eyes of the party, and its ominous ruler Big Brother. Big Brother controls every aspect of peoples lives.', '9780241453513', '/images/Nineteen-Eighty-four.jpg', 'FALSE', GetDate(), 9, null, 4);

Insert into Books(Title, Description, ISBN, Imagepath, IsDeleted, Created,
AuthorId, userId, GenreId)
values ('Pride and Prejudice', 'Pride and Prejudice follows the turbulent relationship between Elizabeth Bennet, the daughter of a country gentleman, and Fitzwilliam Darcy, a rich aristocratic landowner. They must overcome the titular sins of pride and prejudice in order to fall in love and marry.', '9780141439518', '/images/pride-and-prejudice.jpg', 'FALSE', GetDate(), 10, null, 1);

Insert into Books(Title, Description, ISBN, Imagepath, IsDeleted, Created,
AuthorId, userId, GenreId)
values ('The Catcher in the Rye', 'The novel details two days in the life of 16-year-old Holden Caulfield after he has been expelled from prep school. Confused and disillusioned, Holden searches for truth and rails against the “phoniness” of the adult world. He ends up exhausted and emotionally unstable. The events are related after the fact.', '9780316769174', '/images/The-catcher-in-the-rye.jpg', 'FALSE', GetDate(), 11, null, 1);

Insert into Books(Title, Description, ISBN, Imagepath, IsDeleted, Created,
AuthorId, userId, GenreId)
values ('The Great Gatsby', 'Scott Fitzgeralds novel,The Great Gatsby, follows Jay Gatsby, a man who orders his life around one desire: to be reunited with Daisy Buchanan, the love he lost five years earlier. Gatsbys quest leads him from poverty to wealth, into the arms of his beloved, and eventually to death.', '9781853260414', '/images/The-greatest-Gatsby.jpg', 'FALSE', GetDate(), 12, null, 1);

Insert into Books(Title, Description, ISBN, Imagepath, IsDeleted, Created,
AuthorId, userId, GenreId)
values ('The Invisible Man', 'The story concerns the life and death of a scientist named Griffin who has gone mad. Having learned how to make himself invisible, Griffin begins to use his invisibility for nefarious purposes, including murder. When he is finally killed, his body becomes visible again.', '9780451531674', '/images/The-Invisible-Man.jpg', 'FALSE', GetDate(), 13, null, 1);

Insert into Books(Title, Description, ISBN, Imagepath, IsDeleted, Created,
AuthorId, userId, GenreId)
values ('The Lord of the Rings', 'The Lord of the Rings is the saga of a group of sometimes reluctant heroes who set forth to save their world from consummate evil. Its many worlds and creatures were drawn from Tolkiens extensive knowledge of philology and folklore.', '9780261103252', '/images/The-Lord-of-the-Rings.jpg', 'FALSE', GetDate(), 14, null, 2);

Insert into Books(Title, Description, ISBN, Imagepath, IsDeleted, Created,
AuthorId, userId, GenreId)
values ('The Sound and the Fury', 'The Sound and the Fury is a dramatic presentation of the decline of the once-aristocratic Compson family of Yoknapatawpha County, in northern Mississippi. Divided into four sections, the history is narrated by three Compson brothers — Benjamin, Quentin, and Jason — followed by a section by an omniscient narrator.', '9780679732242', '/images/The-Sound-and-the-Fury.jpg', 'FALSE', GetDate(), 1, null, 1);

Insert into Books(Title, Description, ISBN, Imagepath, IsDeleted, Created,
AuthorId, userId, GenreId)
values ('Things Fall Apart', 'The novel chronicles the life of Okonkwo, the leader of an Igbo community, from the events leading up to his banishment from the community for accidentally killing a clansman, through the seven years of his exile, to his return, and it addresses a particular problem of emergent Africa—the intrusion in the 1890s of white missionaries and colonial government into tribal Igbo society.', '9780141023380', '/images/Things-Fall-Apart.jpg', 'FALSE', GetDate(), 15, null, 1);

Insert into Books(Title, Description, ISBN, Imagepath, IsDeleted, Created,
AuthorId, userId, GenreId)
values ('To Kill a Mockingbird', 'To Kill a Mockingbird is both a young girls coming-of-age story and a darker drama about the roots and consequences of racism and prejudice, probing how good and evil can coexist within a single community or individual.', '9780446310789', '/images/to-kill-a-mocking-bird-233x300.jpg', 'FALSE', GetDate(), 16, null, 5);

Insert into Books(Title, Description, ISBN, Imagepath, IsDeleted, Created,
AuthorId, userId, GenreId)
values ('To the Lighthouse', 'To the Lighthouse is made up of three powerfully charged visions into the life of the Ramsay family, living in a summer house off the rocky coast of Scotland. Theres maternal Mrs. Ramsay, the highbrow Mr. Ramsay, their eight children, and assorted holiday guests.', '9780141183411', '/images/To-the-Lighthouse.jpg', 'FALSE', GetDate(), 8, null, 1);

Insert into Books(Title, Description, ISBN, Imagepath, IsDeleted, Created,
AuthorId, userId, GenreId)
values ('Ulysses', 'Ulysses is a poem which gives us details about the unhappiness and monotony Ulysses is going through in his old age. He is living at his home on the island of Ithaca. The summary of Ulysses will take us through the monologue which he speaks in the poem. We learn that Ulysses is not content with the way of his life.', '9789100146238', '/images/Ulysses.jpg', 'FALSE', GetDate(), 17, null, 1);


--SEED REVIEWS
INSERT INTO Reviews (Title, Text, UserId, Rating, IsDeleted, Created, AuthorId)
VALUES ('HEJ', 'Text Text bla bla', '32e59fb4-f5e3-43bc-8df4-202f9209d153', 4, 'false', GETDATE(), 1);

INSERT INTO Reviews (Title, Text, UserId, Rating, IsDeleted, Created, AuthorId)
VALUES ('HEJ', 'Text Text bla bla', '32e59fb4-f5e3-43bc-8df4-202f9209d153', 2, 0, GETDATE(), 1);


--SELECT-SHORTCUTS
Select * 
FROM REVIEWS;