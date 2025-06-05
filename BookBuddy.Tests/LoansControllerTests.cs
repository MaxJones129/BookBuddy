using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookBuddy.Controllers;
using BookBuddy.Data;
using BookBuddy.Models;
using System;
using System.Threading.Tasks;

namespace BookBuddy.Tests
{
    public class LoansControllerTests
    {
        private readonly LoansController _controller;
        private readonly BookBuddyDb _context;

        public LoansControllerTests()
        {
            var options = new DbContextOptionsBuilder<BookBuddyDb>()
                .UseInMemoryDatabase(databaseName: "LoansTestDb")
                .Options;

            _context = new BookBuddyDb(options);
            _controller = new LoansController(_context);

            // Seed User and Book for Foreign Keys
            var user = new User
            {
                Name = "Loan User",
                Email = "loanuser@example.com"
            };
            var book = new Book
            {
                Title = "Loaned Book",
                Author = "Author Name",
                Genre = "Adventure"
            };
            _context.Users.Add(user);
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetLoanById_ReturnsLoan()
        {
            var user = new User { Name = "Loaner", Email = "loaner@example.com" };
            var book = new Book { Title = "Loaned Book", Author = "Author", Genre = "Action" };
            _context.Users.Add(user);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            var loan = new Loan
            {
                UserId = user.Id,
                BookId = book.Id,
                Status = "borrowed",
                DateBorrowed = DateTime.UtcNow
            };
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            var result = await _controller.GetLoanById(loan.Id);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnLoan = Assert.IsType<Loan>(okResult.Value);

            Assert.Equal(loan.Id, returnLoan.Id);
        }

        [Fact]
        public async Task CreateLoan_ReturnsCreatedLoan()
        {
            // Arrange: Create and add a user
            var user = new User
            {
                Name = "Loan User",
                Email = "loanuser@example.com"
            };
            _context.Users.Add(user);

            // Create and add a book
            var book = new Book
            {
                Title = "Loaned Book",
                Author = "Author Name",
                Genre = "Adventure"
            };
            _context.Books.Add(book);

            await _context.SaveChangesAsync(); // Save both to get IDs

            // Act: Create a loan using the saved user and book
            var loan = new Loan
            {
                UserId = user.Id,
                BookId = book.Id,
                Status = "borrowed",
                DateBorrowed = DateTime.UtcNow,
                DateReturned = null
            };

            var result = await _controller.CreateLoan(loan);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<Loan>(createdResult.Value);
            Assert.Equal("borrowed", returnValue.Status);
        }


        [Fact]
        public async Task UpdateLoan_ReturnsNoContent()
        {
            var user = new User
            {
                Name = "Loan User",
                Email = "loanuser@example.com"
            };
            _context.Users.Add(user);

            var book = new Book
            {
                Title = "Loaned Book",
                Author = "Author Name",
                Genre = "Adventure"
            };
            _context.Books.Add(book);

            await _context.SaveChangesAsync();

            var loan = new Loan
            {
                UserId = user.Id,
                BookId = book.Id,
                Status = "borrowed",
                DateBorrowed = DateTime.UtcNow,
                DateReturned = null
            };

            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            loan.Status = "returned";
            loan.DateReturned = DateTime.UtcNow;

            var result = await _controller.UpdateLoan(loan.Id, loan);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteLoan_ReturnsNoContent()
        {
            var user = new User
            {
                Name = "Loan User",
                Email = "loanuser@example.com"
            };
            _context.Users.Add(user);

            var book = new Book
            {
                Title = "Loaned Book",
                Author = "Author Name",
                Genre = "Adventure"
            };
            _context.Books.Add(book);

            await _context.SaveChangesAsync();

            var loan = new Loan
            {
                UserId = user.Id,
                BookId = book.Id,
                Status = "borrowed",
                DateBorrowed = DateTime.UtcNow,
                DateReturned = null
            };

            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            var result = await _controller.DeleteLoan(loan.Id);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
