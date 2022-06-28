using BookManagerApi.Models;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace BookManagerApi.Services
{
	public class BookManagementService : IBookManagementService
	{
        private readonly BookContext _context;

        public BookManagementService(BookContext context)
        {
            _context = context;
        }


        public List<Book> GetAllBooks()
        {
            var books = _context.Books.ToList();
            return books;
        }

        public Book Create(Book book)
        {
            _context.Add(book);
            _context.SaveChanges();
            return book;
        }

        public Book Remove(long id, Book book)
        {
            _context.Remove(book);
            _context.SaveChanges();
            return book;
        }

        public Book UpdateBookById(long id, Book book)
        {
            var existingBookFound = FindBookById(id);

            existingBookFound.Title = book.Title;
            existingBookFound.Description = book.Description;
            existingBookFound.Author = book.Author;
            existingBookFound.Genre = book.Genre;

            _context.SaveChanges();
            return existingBookFound;
        }

        public Book FindBookById(long id)
        {
            var book = _context.Books.Find(id);
            return book!;
        }

        public bool BookExists(long id)
        {
            return _context.Books!.Any(b => b.Id == id);
        }
    }
}

