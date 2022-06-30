﻿using System;
using BookManagerApi.Models;

namespace BookManagerApi.Services
{
	public interface IBookManagementService
	{
        List<Book> GetAllBooks();
        Book Create(Book book);
        Book UpdateBookById(long id, Book book);
        // Book Remove(long id, Book book);
        Book Delete(long id);
        Book FindBookById(long id);
        bool BookExists(long id);
    }
}
