CREATE TABLE Book (
BookId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
NameBook VARCHAR(150) NOT NULL UNIQUE,
PageCount INTEGER NOT NULL
);

CREATE TABLE Author (
AuthorId INTEGER  PRIMARY KEY AUTOINCREMENT,
FIO VARCHAR(150) UNIQUE
);

CREATE TABLE Link_Author_Book (
ID_Book INTEGER NOT NULL,
ID_Author INTEGER ,
FOREIGN KEY (ID_Book) REFERENCES Book(BookId) ON DELETE CASCADE,
FOREIGN KEY (ID_Author) REFERENCES Author(AuthorId) ON DELETE CASCADE
);
-------------------------------------------------------------------------------------------------------------------------------------------

-- Добавим несколько книг
INSERT INTO Book (NameBook, PageCount) VALUES ("Белый клык",  300);
INSERT INTO Book (NameBook, PageCount) VALUES ("Война и мир",  2780);
INSERT INTO Book (NameBook, PageCount) VALUES ("12 стульев",  680);
INSERT INTO Book (NameBook, PageCount) VALUES ("no name", 5680);

-- Затем добавим несколько авторов
INSERT INTO Author (FIO) VALUES ("Джек Лондон");
INSERT INTO Author (FIO) VALUES ("Лев Толстой");
INSERT INTO Author (FIO) VALUES ("Илья Ильф");
INSERT INTO Author (FIO) VALUES ("Евгений Петров");

-- Связываем таблицы auth и book
-- для этого наполним данными результирующую таблицу
INSERT INTO Link_Author_Book (ID_Book, ID_Author) VALUES (1, 1);
INSERT INTO Link_Author_Book (ID_Book, ID_Author) VALUES (2, 2);
INSERT INTO Link_Author_Book (ID_Book, ID_Author) VALUES (3, 3);
INSERT INTO Link_Author_Book (ID_Book, ID_Author) VALUES (3, 4);
INSERT INTO Link_Author_Book (ID_Book) VALUES (4);
------------------------------------------------------------------------------------------------------------------------------------------
delete from Link_Author_Book where Link_Author_Book.ID_Author is null

SELECT Book.NameBook,Author.FIO,Book.PageCount
FROM Book INNER JOIN Link_Author_Book ON Link_Author_Book.ID_Book=Book.BookId INNER JOIN Author ON
Link_Author_Book.ID_Author=Author.AuthorId



UPDATE Book SET NameBook = "Test" WHERE Book.NameBook="Белый клык";


SELECT * FROM Link_Author_Book
SELECT * FROM Book
SELECT * FROM Author

-----------select----------------
SELECT Author.FIO, Book.NameBook
FROM Author 
INNER JOIN Link_Author_Book ON Author.AuthorId = Link_Author_Book.ID_Author 
INNER JOIN book ON Link_Author_Book.ID_Book = Book.BookId
WHERE Link_Author_Book.ID_Book IN (
    SELECT Book.BookId
    FROM Book 
    INNER JOIN Link_Author_Book ON Book.BookId = Link_Author_Book.ID_Book 
    INNER JOIN Author ON Link_Author_Book.ID_Author = Author.AuthorId
    GROUP BY Link_Author_Book.ID_Book
    HAVING COUNT(Link_Author_Book.ID_Author) > 1
)
ORDER BY Link_Author_Book.ID_Book
------------------------------------------



--4. Найти авторов одной книги !!!!!!!!!
CREATE FUNCTION AllAuthorOfBook(@idbook integer)
RETURNS TABLE
AS RETURN 
(
SELECT Author.FIO
FROM [TContract_detail] inner join  TProvider
on TProvider.ID=[TContract_detail].ProviderID
WHERE TContract_detail.Date_delivery_detail<>@data 
GROUP BY TProvider.Name_prov,TContract_detail.ProviderID
);

SELECT * FROM AllAuthorOfBook(3) 
DROP FUNCTION dbo.Delivery_Details
-------------------------------------------------------------------














---------PROCEDURE-------------------
CREATE PROCEDURE UpdateDataAboutBook
@NameBook VARCHAR(150) ,
@NameAuthor VARCHAR(150) 
AS
BEGIN
BEGIN TRY
DECLARE @id_book integer 
DECLARE @id_auth integer 
--DECLARE @x2 integer

set @id_book=(SELECT Book.BookId FROM Book WHERE Book.NameBook="12 стульев" LIMIT 1)
set @id_auth=(SELECT TOP 1 Author.AuthorId FROM Author WHERE Author.FIO=@NameAuthor)
set @ListAuthorsOfBook =(SELECT Link_Author_Book.ID_Author FROM Link_Author_Book WHERE Link_Author_Book.ID_Book=@id_book)
--set @x2=(SELECT TOP 1 TWeapons.ID FROM TWeapons WHERE TWeapons.TypeWeapons=@Type_Thing)
--IF(@ListAuthorsOfBook.Count()>0) 
UPDATE Book SET NameBook = @NameBook  WHERE Book.ID_Book=@id_book;
--IF(@id_auth is not null) 
UPDATE Link_Author_Book SET ID_Author =1  WHERE Link_Author_Book.ID_Author=2 and Link_Author_Book.ID_Book=1;

END TRY
BEGIN CATCH
	PRINT 'ERROR';
END CATCH
END
--------------------
SELECT Link_Author_Book.ID_Author FROM Link_Author_Book WHERE Link_Author_Book.ID_Book=3


