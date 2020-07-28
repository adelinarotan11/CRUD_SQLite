using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devart.Data.SQLite;


namespace CRUD_SQLite
{
    class TestDB
    {

        int _idCurrentBook, _idCurrentAuthorOfBook, _idNewAuthorOfBook;
        string rez;

        internal void SelectAllBook()
        {
            using (SQLiteConnection cn = GetConnection())
            {
                cn.Open();
                string query = "SELECT NameBook,PageCount FROM Book ";
                SQLiteCommand cmd = new SQLiteCommand(query, cn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                // Console.WriteLine("Connection open......\n");
                Console.WriteLine(string.Format("--------------------------------------------------"));
                Console.WriteLine(string.Format("NameBook\t" + "PageCount"));
                Console.WriteLine(string.Format("--------------------------------------------------"));
                int count = 1;
                while (reader.Read())
                {
                    Console.Write(count++ + ") ");

                    Console.WriteLine(string.Format(reader["NameBook"].ToString() + "\t|" + reader["PageCount"].ToString() + "\t|"));

                }
            }
            Console.WriteLine(string.Format("--------------------------------------------------"));
        }
        internal void SelectСertainBook(string TitleBook)
        {

            using (SQLiteConnection cn = GetConnection())
            {
                cn.Open();
                SQLiteCommand cmd2;
                SQLiteDataReader reader2;

                //поиск Id изменяемой книги
                string queryFindIdCurrentBook = string.Format("SELECT BookId FROM Book WHERE Book.NameBook = '{0}' LIMIT 1", TitleBook);
                SQLiteCommand cmd1 = new SQLiteCommand(queryFindIdCurrentBook, cn);
                SQLiteDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    _idCurrentBook = int.Parse(reader1["BookId"].ToString());
                }
                if (_idCurrentBook == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Book with name <{0}> not to include to DB", TitleBook);
                    Console.ResetColor();
                    return;
                }
                reader1.Close();

                string queryFindIdAuthor = string.Format("SELECT FIO FROM Book INNER JOIN Author INNER JOIN Link_Author_Book on Link_Author_Book.ID_Author = Author.AuthorId and " +
                    "Link_Author_Book.ID_Book = Book.BookId WHERE Book.NameBook= '{0}' LIMIT 1", TitleBook);
                SQLiteCommand cmd = new SQLiteCommand(queryFindIdAuthor, cn);
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    rez = reader["FIO"].ToString();

                }
                if (rez == null)
                {
                    string queryRez1 = string.Format("SELECT NameBook,PageCount FROM Book WHERE Book.NameBook= '{0}'", TitleBook);
                    cmd2 = new SQLiteCommand(queryRez1, cn);
                    reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        Console.WriteLine(string.Format("\n--------------------------------------------------"));
                        Console.WriteLine(string.Format("Title of book: " + reader2["NameBook"].ToString() + "\t| Number of pages: " + reader2["PageCount"].ToString()));

                    }
                }
                else
                {
                    string queryRez2 = string.Format("SELECT NameBook, FIO, PageCount " +
                     "FROM Book INNER JOIN Author INNER JOIN Link_Author_Book on Link_Author_Book.ID_Author = Author.AuthorId and " +
                     "Link_Author_Book.ID_Book = Book.BookId WHERE Book.NameBook = '{0}'", TitleBook);
                    cmd2 = new SQLiteCommand(queryRez2, cn);
                    reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        Console.WriteLine(string.Format("\n--------------------------------------------------"));
                        Console.WriteLine(string.Format("Title of book: " + reader2["NameBook"].ToString() + "\t| Author(s): " + reader2["FIO"].ToString() + "\t| Number of pages:" + reader2["PageCount"].ToString() + "\t|"));

                    }
                }

            }
            Console.WriteLine(string.Format("--------------------------------------------------"));
        }
        internal void SelectAuthor()
        {
            using (SQLiteConnection cn = GetConnection())
            {
                cn.Open();
                string query = "SELECT FIO FROM Author";
                SQLiteCommand cmd = new SQLiteCommand(query, cn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                // Console.WriteLine("Connection open......\n");
                Console.WriteLine(string.Format("--------------------------------------------------"));
                Console.WriteLine(string.Format("FIO"));
                Console.WriteLine(string.Format("--------------------------------------------------"));
                while (reader.Read())
                {
                    Console.Write(string.Format("|"));
                    Console.WriteLine(string.Format(reader["FIO"].ToString()));
                }
            }
            Console.WriteLine(string.Format("--------------------------------------------------"));
        }


        internal void Insert(string NameBook, string NameAuthor, int NewCountPage)
        {
            using (SQLiteConnection cn = GetConnection())
            {
                cn.Open();
                //запрос ищет Id  текущего автора
                string queryFindIdCurrentAuthor = string.Format("SELECT AuthorId FROM Author WHERE Author.FIO = '{0}' LIMIT 1", NameAuthor);
                SQLiteCommand cmd4 = new SQLiteCommand(queryFindIdCurrentAuthor, cn);
                SQLiteDataReader reader4 = cmd4.ExecuteReader();
                while (reader4.Read())
                {
                    _idCurrentAuthorOfBook = int.Parse(reader4["AuthorId"].ToString());
                }
                reader4.Close();


                //Добавление новой книги
                string queryUpdateCountPageBook = string.Format("INSERT INTO Book (NameBook, PageCount) VALUES ('{0}','{1}')", NameBook, NewCountPage);
                SQLiteCommand cmd3 = new SQLiteCommand(queryUpdateCountPageBook, cn);
                SQLiteDataReader reader3 = cmd3.ExecuteReader();
                reader3.Close();

                //поиск Id книги
                string queryFindIdCurrentBook = string.Format("SELECT BookId FROM Book WHERE Book.NameBook = '{0}' LIMIT 1", NameBook);
                SQLiteCommand cmd1 = new SQLiteCommand(queryFindIdCurrentBook, cn);
                SQLiteDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    _idCurrentBook = int.Parse(reader1["BookId"].ToString());
                }
                reader1.Close();
                //Связывание книги и автора
                string queryUpdateFromResultTableIdAuvtor = string.Format("INSERT INTO Link_Author_Book (ID_Book, ID_Author) VALUES ('{0}','{1}') ", _idCurrentBook, _idCurrentAuthorOfBook);
                SQLiteCommand cmd6 = new SQLiteCommand(queryUpdateFromResultTableIdAuvtor, cn);
                SQLiteDataReader reader6 = cmd6.ExecuteReader();
                reader6.Close();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Book with name <{0}> was successful added", NameBook);
                Console.ResetColor();
            }

        }

        internal void Update(string NameBook, string NewNameBook, string CurrentNameAuthor, string NewNameAuthor, int NewCountPage)
        {
            using (SQLiteConnection cn = GetConnection())
            {
                cn.Open();
                //поиск Id изменяемой книги
                string queryFindIdCurrentBook = string.Format("SELECT BookId FROM Book WHERE Book.NameBook = '{0}' LIMIT 1", NameBook);
                SQLiteCommand cmd1 = new SQLiteCommand(queryFindIdCurrentBook, cn);
                SQLiteDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    _idCurrentBook = int.Parse(reader1["BookId"].ToString());
                }
                if (_idCurrentBook == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Book with name <{0}> not to include to DB", NameBook);
                    Console.ResetColor();
                    return;
                }
                reader1.Close();

                //запрос ищет текущего автора для книги
                string queryFindIdCurrentAuthor = string.Format("SELECT AuthorId FROM Author WHERE Author.FIO = '{0}' LIMIT 1", CurrentNameAuthor);
                SQLiteCommand cmd4 = new SQLiteCommand(queryFindIdCurrentAuthor, cn);
                SQLiteDataReader reader4 = cmd4.ExecuteReader();
                while (reader4.Read())
                {
                    _idCurrentAuthorOfBook = int.Parse(reader4["AuthorId"].ToString());
                }
                reader4.Close();
                //запрос ищет ID нового автора в таблице всех существующих авторов
                string queryFindId_NewAuthor = string.Format("SELECT AuthorId FROM Author WHERE Author.FIO = '{0}' LIMIT 1", NewNameAuthor);
                SQLiteCommand cmd5 = new SQLiteCommand(queryFindId_NewAuthor, cn);
                SQLiteDataReader reader5 = cmd5.ExecuteReader();
                while (reader5.Read())
                {

                    _idNewAuthorOfBook = int.Parse(reader5["AuthorId"].ToString());
                }
                reader5.Close();
                if (_idNewAuthorOfBook == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Db no include this Author");
                    Console.ResetColor();
                    return;
                }
                reader1.Close();
                //обновление названия книги
                string queryUpdateNameBook = string.Format("UPDATE Book SET NameBook = '{0}' WHERE Book.NameBook = '{1}'", NewNameBook, NameBook);
                SQLiteCommand cmd2 = new SQLiteCommand(queryUpdateNameBook, cn);
                SQLiteDataReader reader2 = cmd2.ExecuteReader();
                reader2.Close();
                //обновление кол-ва страниц
                string queryUpdateCountPageBook = string.Format("UPDATE Book SET PageCount = '{0}' WHERE Book.NameBook = '{1}'", NewCountPage, NameBook);
                SQLiteCommand cmd3 = new SQLiteCommand(queryUpdateCountPageBook, cn);
                SQLiteDataReader reader3 = cmd3.ExecuteReader();
                reader3.Close();
                //Обновление автора для книги
                string queryUpdateFromResultTableIdAuvtor = string.Format("UPDATE Link_Author_Book SET ID_Author ='{0}' WHERE Link_Author_Book.ID_Author='{1}' AND Link_Author_Book.ID_Book='{2}' ", _idNewAuthorOfBook, _idCurrentAuthorOfBook, _idCurrentBook);
                SQLiteCommand cmd6 = new SQLiteCommand(queryUpdateFromResultTableIdAuvtor, cn);
                SQLiteDataReader reader6 = cmd6.ExecuteReader();
                reader6.Close();
                Console.WriteLine("Book with name <{0}> was successful update", NameBook);

            }
        }

        internal void Delete(string NameBook)
        {
            using (SQLiteConnection cn = GetConnection())
            {
                cn.Open();
                string query = string.Format("DELETE FROM Book WHERE Book.NameBook = '{0}'", NameBook);
                SQLiteCommand cmd = new SQLiteCommand(query, cn);
                //  SQLiteDataReader reader = cmd.ExecuteReader();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nBook with name <{0}> was successful delete", NameBook);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nBook with name <{0}> not to include to DB", NameBook);
                    Console.ResetColor();
                }

            }
        }



        private SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(@"Data Source=D:\TestDb.db;FailIfMissing=False;");

        }
    }
}
