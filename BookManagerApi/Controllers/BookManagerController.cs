using Microsoft.AspNetCore.Mvc;
using BookManagerApi.Models;
using BookManagerApi.Services;
using System.Net;

namespace BookManagerApi.Controllers
{
    [Route("api/v1/book")]
    [ApiController]
    public class BookManagerController : ControllerBase
    {
        private readonly IBookManagementService _bookManagementService;

        public BookManagerController(IBookManagementService bookManagementService)
        {
            _bookManagementService = bookManagementService;
        }

        // GET: api/v1/book
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return _bookManagementService.GetAllBooks();
        }

        // GET: api/v1/book/5
        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(long id)
        {
            var book = _bookManagementService.FindBookById(id);
            if (book != null)
            {
                return book;
            }

            return Result(HttpStatusCode.NotFound, $"Book id: {id} does not exist in database");
        }

        // PUT: api/v1/book/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult UpdateBookById(long id, Book book)
        {
            _bookManagementService.UpdateBookById(id, book);
            return NoContent();
        }

        // POST: api/v1/book
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Book> AddBook(Book book)
        {
            _bookManagementService.Create(book);

            if (_bookManagementService.BookExists(book.Id))
            {
                return Result(HttpStatusCode.NotFound, $"Book id: {book.Id} already exists");
            }

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        // DELETE: api/v1/book/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpDelete("{id}")]
        public ActionResult<Book> DeleteBookById(long id, Book book)
        {
            var deletedbook =_bookManagementService.Remove(id, book);

            return deletedbook;
        }

        public static ActionResult Result(HttpStatusCode statusCode, string reason) => new ContentResult
        {
            StatusCode = (int)statusCode,
            Content = $"Status Code: {(int)statusCode} {statusCode}: {reason}",
            ContentType = "text/plain",
        };
    }
}
