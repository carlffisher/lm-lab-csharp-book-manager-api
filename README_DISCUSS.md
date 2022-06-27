📘 Discussion Task

Explore the code and make notes on the following features and how it is being implemented in the code. We'd like you to note down what classes and methods are used and how the objects interact.

The features are:
- Get All Books
- Add a Book
- Update a Book



GetAllBooks() is a method of the class BookManagementService.
It is called as part of a test in BookManagerControllerTests to obtain a list books that mock the expected return from a test databse when the latter is asked to return a list of all books it contains.
Thus, a test can compare the mock return list and the test database return list to confirm they match, which means the test is successful.
BookManagement service class obtains this book list via a 'context', also created by BookManagementService, which is the interface that provides methods for an app to access data and operations it may require.


AddBook() is a method of the class BookManagerController. The latter class descends from class ControllerBase, whick, I think, is implemented by AspNetCore?
In this App, AddBook() is used by BookManagerControlTests to populate a test database. It takes as an argument type Book, which contains all the details of a book to be added to the test database.
To create a new book, the method calls Create, which belongs to the context created by the BookManagementService.
On returning the result of the Create, AddBook returns an Action Result, which supplies the added books name, id, and the Book struct.


UpdateBookById() is a method also belonging to class BookManagerController. It takes as arguments a book id and a struct containing the datails of the book to be updated.
The method, in turn, calls Update, which belongs to the class BookManagementService, passing in the latter two arguments. 







📘 Task 1: Implement the following User Story with tests.

`User Story: As a user, I want to use the Book Manager API to delete a book using its ID`


📘 Extension Task: Oh no! 😭 We've only covered the happy paths in the solution, can you figure out a way
to add in exception handling to the project? 

- Clue 1: What if someone wants to add a book with an ID for a book that already exists? How do we handle this gracefully?

- Clue 2: What if someone wants to find a book by an ID that doesn't yet exist? 
  How can we improve the API by handling errors gracefully and show a helpful message to the client?
