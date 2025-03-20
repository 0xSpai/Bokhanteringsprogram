using System;
using System.Collections.Generic;
using System.IO;

namespace BookStuff
{
    class Book
    {
        public string Name;
        public string Author;
        public bool Borrowed;

        public Book(string n, string a, bool b = false)
        {
            Name = n;
            Author = a;
            Borrowed = b;
        }
    }

    class Program
    {
        static List<Book> books = new List<Book>();
        static string fileName = "books.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Book App");

            // Load books
            try
            {
                if (File.Exists(fileName))
                {
                    string[] lines = File.ReadAllLines(fileName);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length >= 3)
                        {
                            books.Add(new Book(parts[0], parts[1], parts[2] == "yes"));
                        }
                    }
                }
                else
                {
                    // Default books
                    books.Add(new Book("Den kompletta boken om DOS 6", "John Socha"));
                    books.Add(new Book("Pascal helt enkelt", "Serafim Dahl"));
                    books.Add(new Book("Tokyo Ghoul", "Sui Ishida"));
                    books.Add(new Book("Ninja: Get Good at Gaming Guide", "Ninja"));
                    books.Add(new Book("The C++ Programming Language", "Bjarne Stroustrup"));
                    books.Add(new Book("My Little Pony: Meet The Ponies", "Hasbro"));
                    books.Add(new Book("Neural Networks and Deep Learning", " Charu C. Aggarwal"));
                    books.Add(new Book("A Clockwork Orange", "Anthony Burger"));
                    books.Add(new Book("Percy Jackson & The Olympians", "Rick Riordan"));
                    books.Add(new Book("The Art of War", "Sun Tzu"));

                    SaveBooks();
                }
            }
            catch
            {
                Console.WriteLine("Error loading books!");
            }

            bool run = true;
            while (run)
            {
                // Show menu
                Console.WriteLine("\nOPTIONS:");
                Console.WriteLine("1. Show all books");
                Console.WriteLine("2. Add book");
                Console.WriteLine("3. Find book");
                Console.WriteLine("4. Borrow book");
                Console.WriteLine("5. Return book");
                Console.WriteLine("6. Delete book");
                Console.WriteLine("7. Quit");
                Console.Write("Choose: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    // Show all books
                    ListBooks();
                }
                else if (choice == "2")
                {
                    // Add new book
                    Console.Write("Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Author: ");
                    string author = Console.ReadLine();

                    books.Add(new Book(name, author));
                    SaveBooks();
                    Console.WriteLine("Book added!");
                }
                else if (choice == "3")
                {
                    // Find book
                    Console.Write("Search: ");
                    string search = Console.ReadLine().ToLower();

                    bool found = false;
                    for (int i = 0; i < books.Count; i++)
                    {
                        if (books[i].Name.ToLower().Contains(search) ||
                            books[i].Author.ToLower().Contains(search))
                        {
                            Console.WriteLine($"{i + 1}. {books[i].Author} - {books[i].Name} {(books[i].Borrowed ? "(Borrowed)" : "")}");
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine("No matches!");
                    }
                }
                else if (choice == "4")
                {
                    // Borrow book
                    ListBooks();

                    Console.Write("Book number to borrow: ");
                    if (int.TryParse(Console.ReadLine(), out int num) && num > 0 && num <= books.Count)
                    {
                        if (books[num - 1].Borrowed)
                        {
                            Console.WriteLine("This book is already borrowed!");
                        }
                        else
                        {
                            books[num - 1].Borrowed = true;
                            SaveBooks();
                            Console.WriteLine("Book borrowed!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid number!");
                    }
                }
                else if (choice == "5")
                {
                    // Return book
                    ListBooks();

                    Console.Write("Book number to return: ");
                    if (int.TryParse(Console.ReadLine(), out int num) && num > 0 && num <= books.Count)
                    {
                        if (!books[num - 1].Borrowed)
                        {
                            Console.WriteLine("This book is not borrowed!");
                        }
                        else
                        {
                            books[num - 1].Borrowed = false;
                            SaveBooks();
                            Console.WriteLine("Book returned!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid number!");
                    }
                }
                else if (choice == "6")
                {
                    // Delete book
                    ListBooks();

                    Console.Write("Book number to delete: ");
                    if (int.TryParse(Console.ReadLine(), out int num) && num > 0 && num <= books.Count)
                    {
                        Console.Write("Are you sure? (y/n): ");
                        if (Console.ReadLine().ToLower() == "y")
                        {
                            books.RemoveAt(num - 1);
                            SaveBooks();
                            Console.WriteLine("Book deleted!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid number!");
                    }
                }
                else if (choice == "7")
                {
                    run = false;
                    Console.WriteLine("Goodbye!");
                }
                else
                {
                    Console.WriteLine("Invalid choice!");
                }
            }
        }

        static void SaveBooks()
        {
            try
            {
                List<string> lines = new List<string>();
                foreach (Book b in books)
                {
                    lines.Add($"{b.Name},{b.Author},{(b.Borrowed ? "yes" : "no")}");
                }
                File.WriteAllLines(fileName, lines);
            }
            catch
            {
                Console.WriteLine("Error saving books!");
            }
        }

        static void ListBooks()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("No books found!");
                return;
            }

            for (int i = 0; i < books.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {books[i].Author} - {books[i].Name} {(books[i].Borrowed ? "(Borrowed)" : "")}");
            }
        }
    }
}