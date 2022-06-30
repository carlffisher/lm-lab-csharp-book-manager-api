using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using BookManagerApi.Controllers;
using BookManagerApi.Models;
using BookManagerApi.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace BookManagerApi.Tests;

public class BookManagerControllerTests
{
    private BookManagerController? _controller;
    private Mock<IBookManagementService>? _mockBookManagementService;

    [SetUp]
    public void Setup()
    {
        //Arrange
        _mockBookManagementService = new Mock<IBookManagementService>();
        _controller = new BookManagerController(_mockBookManagementService.Object);
    }

    [Test]
    public void GetBooks_Returns_AllBooks()
    {
        //Arange
        _mockBookManagementService!.Setup(b => b.GetAllBooks()).Returns(GetTestBooks());

        //Act
        var result = _controller!.GetBooks();

        //Assert
        result.Should().BeOfType(typeof(ActionResult<IEnumerable<Book>>));
        result.Value.Should().BeEquivalentTo(GetTestBooks());
        result.Value!.Count().Should().Be(3);
    }

    [Test]
    public void GetBookById_Returns_CorrectBook()
    {
        //Arrange
        var testBookFound = GetTestBooks().FirstOrDefault();
        _mockBookManagementService!.Setup(b => b.FindBookById(1)).Returns(testBookFound!);

               //Act
        var result = _controller!.GetBookById(1);

        //Assert
        result.Should().BeOfType(typeof(ActionResult<Book>));
        result.Value.Should().Be(testBookFound);

    }

    [Test]
    public void UpdateBookById_Updates_Correct_Book()
    {
        //Arrange
        long existingBookId = 3;
        Book existingBookFound = GetTestBooks().FirstOrDefault(b => b.Id.Equals(existingBookId))!;

        var bookUpdates = new Book() { Id = 3, Title = "Book Three", Description = "I am updating this for Book Three", Author = "Person Three", Genre = Genre.Education };

        _mockBookManagementService!.Setup(b => b.FindBookById(existingBookId)).Returns(existingBookFound!);

        //Act
        var result = _controller!.UpdateBookById(existingBookId, bookUpdates);

        //Assert
        result.Should().BeOfType(typeof(NoContentResult));
    }

    [Test]
    public void AddBook_Creates_A_Book()
    {
        //Arrange
        var newBook = new Book() { Id = 4, Title = "Book Four", Description = "This is the description for Book Four", Author = "Person Four", Genre = Genre.Education };

        _mockBookManagementService!.Setup(b => b.Create(newBook)).Returns(newBook);

        //Act
        var result = _controller!.AddBook(newBook);

        //Assert
        result.Should().BeOfType(typeof(ActionResult<Book>));
    }
   
    private static List<Book> GetTestBooks()
    {
        return new List<Book>
        {
            new Book() { Id = 1, Title = "Book One", Description = "This is the description for Book One", Author = "Person One", Genre = Genre.Education },
            new Book() { Id = 2, Title = "Book Two", Description = "This is the description for Book Two", Author = "Person Two", Genre = Genre.Fantasy },
            new Book() { Id = 3, Title = "Book Three", Description = "This is the description for Book Three", Author = "Person Three", Genre = Genre.Thriller },
        };
    }

    [Test]
    public void DeleteBookById_Deletes_Correct_Book()
    {
                //Arrange

                var newBook = new Book() { Id = 5, Title = "Book Five", Description = "This is the description for Book Five", Author = "Person Five", Genre = Genre.Education };
                _mockBookManagementService!.Setup(b => b.Create(newBook)).Returns(newBook);

                //Act
                var result = _controller!.AddBook(newBook);

                //Assert
                result.Should().BeOfType(typeof(ActionResult<Book>));

                //Arrange

                long existingBookId = 5;
                Book existingBookFound = GetTestBooks().FirstOrDefault(b => b.Id.Equals(existingBookId))!;

                var bookUpdates = new Book() { Id = 5, Title = "Book Five", Description = "This is the description for Book Five", Author = "Person Five", Genre = Genre.Education };

                //Act
                var result2 = _controller!.DeleteBookById(existingBookId);

                //Assert
                result2.Should().BeOfType(typeof(ActionResult<Book>));
    }

   [Test]
   public void GetBookById_Returns_NoBookExists()
   {
        long existingBookId = 9;
        _mockBookManagementService!.Setup(b => b.BookExists(9)).Returns(false);

        var result = BookManagerController.Result(HttpStatusCode.NotFound, $"Book id: {existingBookId} does not exist in database") as ContentResult;

        Assert.NotNull(result);
        Assert.AreEqual($"Status Code: 404 NotFound: Book id: {existingBookId} does not exist in database", result!.Content);
   }

   [Test]
   public void AddBook_Creates_A_Book_That_Exists()
   {
        long existingBookId = 1;
        _mockBookManagementService!.Setup(b => b.BookExists(existingBookId)).Returns(true);

        var result = BookManagerController.Result(HttpStatusCode.BadRequest, $"Book id: {existingBookId} exists") as ContentResult;

        Assert.NotNull(result);
        Assert.AreEqual($"Status Code: 400 BadRequest: Book id: {existingBookId} exists", result!.Content);
   }
}
