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