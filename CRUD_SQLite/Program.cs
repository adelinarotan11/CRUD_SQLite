using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_SQLite
{
    class Program
    {
        static void Main(string[] args)
        {
            TestDB db = new TestDB();
            int selector = 0;
            string nameofbook, question, CurrentNameAuthor, NewNameofbook, NewNameAuthor;
            int NewCountPage;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(string.Format("---------------------MENU--------------------"));
                Console.WriteLine("Select one of the following queries:");
                Console.WriteLine("1. SELECT");
                Console.WriteLine("2. INSERT");
                Console.WriteLine("3. UPDATE");
                Console.WriteLine("4. DELETE");
                Console.WriteLine("5. MORE INFORMATION");
                Console.WriteLine("6. EXIT");
                Console.WriteLine(string.Format("---------------------------------------------"));
                Console.ResetColor();
                try
                {
                    Console.Write("\nEnter ---> ");
                    selector = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEnter correct date!!!\n");
                    Console.ResetColor();
                    continue;
                }
                switch (selector)
                {
                    case 1:
                        //Console.WriteLine("case 1 SELECT");
                        db.SelectAllBook();
                        while (true)
                        {
                            Console.Write("Do you want to see more detailed information about a specific book? (yes/no) :  "); question = Console.ReadLine();
                            if (question == "yes")
                            {
                                Console.Write("\nEnter  title for the book: "); nameofbook = Console.ReadLine();
                                db.SelectСertainBook(nameofbook); break;
                            }
                            else if (question != "yes" && question != "no")
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Enter correct data! (yes/no)"); Console.ResetColor();
                            }
                            else break;

                        }
                        break;
                    case 2:
                        //Console.WriteLine("case 2  INSERT");
                        while (true)
                        {
                            Console.Write("Do you want to show data of Table <Author>? (yes/no) :  "); question = Console.ReadLine();
                            if (question == "yes")
                            {
                                db.SelectAuthor(); break;
                            }
                            else if (question != "yes" && question != "no")
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Enter correct data! (yes/no)"); Console.ResetColor();
                            }
                            else break;

                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\nEnter  title for the book: "); nameofbook = Console.ReadLine();
                        Console.Write("\nEnter the new name of the Аuthor for the book from the existing ones in the DB: "); NewNameAuthor = Console.ReadLine();
                        Console.Write("\nEnter  Page Count for the book: "); NewCountPage = int.Parse(Console.ReadLine());
                        Console.ResetColor();
                        db.Insert(nameofbook, NewNameAuthor, NewCountPage);
                        break;

                    case 3:
                        // Console.WriteLine("case 3  UPDATE");
                        while (true)
                        {
                            Console.Write("Do you want to show data of Table <Book> and <Author>? (yes/no) :  "); question = Console.ReadLine();
                            if (question == "yes")
                            {
                                db.SelectAllBook(); db.SelectAuthor(); break;
                            }
                            else if (question != "yes" && question != "no")
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Enter correct data! (yes/no)"); Console.ResetColor();
                            }
                            else break;

                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\nEnter the title of the book you want to update: "); nameofbook = Console.ReadLine();
                        Console.Write("\nEnter new title for the book: "); NewNameofbook = Console.ReadLine();
                        Console.Write("\nEnter current new name of the Аuthor :"); CurrentNameAuthor = Console.ReadLine();
                        Console.Write("\nEnter  new name of the Аuthor for the book from the existing ones in the DB: "); NewNameAuthor = Console.ReadLine();
                        Console.Write("\nEnter new Page Count for the book: "); NewCountPage = int.Parse(Console.ReadLine());
                        Console.ResetColor();
                        db.Update(nameofbook, NewNameofbook, CurrentNameAuthor, NewNameAuthor, NewCountPage);
                        break;
                    case 4:
                        while (true)
                        {
                            Console.Write("Do you want to show data of Table <Book> and <Author>? (yes/no) :  "); question = Console.ReadLine();
                            if (question == "yes")
                            {
                                db.SelectAllBook(); break;
                            }
                            else if (question != "yes" && question != "no")
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Enter correct data! (yes/no)"); Console.ResetColor();
                            }
                            else break;

                        }
                        Console.Write("\nEnter the title of the book you want to delete: ");
                        nameofbook = Console.ReadLine();
                        db.Delete(nameofbook);
                        break;
                    case 5:
                        Console.WriteLine("\n* SELECT -> выборка всех книг с выводом имен авторов. Если автора у книги нет, выводится информация только о книге.Если несколько авторов, выводятся имена всех авторов" +
                            "\n* INSERT-> вставка новой книги и указание ее автора(можно только из уже существующих авторов) " +
                            "\n* UPDATE-> обновление информации о книге " +
                            "\n* DELETE -> удаление книги\n");
                        break;
                    case 6:
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nSorry, invalid selection!\n");
                        Console.ResetColor();
                        break;
                }
            }

        }
    }
}
