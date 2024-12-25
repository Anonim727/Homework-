using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9 { }

 class Program
    
{
        static void Main(string[] args)
        { }
            
    public class Author
        {
            public string Name { get; set; }
        }
    }
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public Book(int id, string title, Author author)
        {
            Id = id;
            Title = title;
            Author = author;
        }
        public static explicit operator string(Book book)
        {
            return book.Title;
        }
    }
    public static explicit operator int(Book book)
    {
        return book.Id;
    }
    public static explicit operator string(Book book)
    {
        return book.Title;
    }
    public static implicit operator Author(Book book)
    {
        return book.Author.Name;
    }

} 

    

