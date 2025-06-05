using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookBuddy.Controllers;
using BookBuddy.Data;
using BookBuddy.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookBuddy.Tests
{
    public class BooksControllerTests
    {
        private readonly BooksController _controller;
        private readonly BookBuddyDb _context;

        public BooksControllerTests()
        {
            var options = new DbContextOptionsBuilder<BookBuddyDb>()
                .UseInMemoryDatabase(databaseName: "BooksTestDb")
                .Options;

            _context = new BookBuddyDb(options);
            _controller = new BooksController(_context);
        }

        [Fact]
        public async Task CreateBook_ReturnsCreatedBook()
        {
            var book = new Book
            {
                Title = "Test Book",
                Author = "Test Author",
                Genre = "Fantasy",
                Loans = new List<Loan>(),
                BookBookTags = new List<BookBookTag>()
            };
            var result = await _controller.CreateBook(book);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task UpdateBook_ReturnsNoContent()
        {
            var book = new Book
            {
                Title = "Test Book",
                Author = "Test Author",
                Genre = "Fantasy",
                Loans = new List<Loan>(),
                BookBookTags = new List<BookBookTag>()
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            book.Title = "Updated";
            var result = await _controller.UpdateBook(book.Id, book);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetBookById_ReturnsBook()
        {
            var book = new Book { Title = "Sample Book", Author = "Author", Genre = "Mystery" };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            var result = await _controller.GetBookById(book.Id);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnBook = Assert.IsType<Book>(okResult.Value);

            Assert.Equal(book.Id, returnBook.Id);
        }
-
        [Fact]
        public async Task DeleteBook_ReturnsNoContent()
        {
            var book = new Book
            {
                Title = "Test Book",
                Author = "Test Author",
                Genre = "Fantasy",
                Loans = new List<Loan>(),
                BookBookTags = new List<BookBookTag>()
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            var result = await _controller.DeleteBook(book.Id);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
