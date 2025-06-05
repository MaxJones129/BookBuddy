using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookBuddy.Controllers;
using BookBuddy.Data;
using BookBuddy.Models;
using System.Threading.Tasks;

namespace BookBuddy.Tests
{
    public class UsersControllerTests
    {
        private readonly UsersController _controller;
        private readonly BookBuddyDb _context;

        public UsersControllerTests()
        {
            var options = new DbContextOptionsBuilder<BookBuddyDb>()
                .UseInMemoryDatabase(databaseName: "UsersTestDb")
                .Options;

            _context = new BookBuddyDb(options);
            _controller = new UsersController(_context);
        }

        [Fact]
        public async Task GetUserById_ReturnsUser()
        {
            var user = new User { Name = "Test User", Email = "test@example.com" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var result = await _controller.GetUserById(user.Id);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnUser = Assert.IsType<User>(okResult.Value);

            Assert.Equal(user.Id, returnUser.Id);
        }

       [Fact]
        public async Task CreateUser_ReturnsCreatedUser()
        {
            var user = new User { Name = "John", Email = "john@example.com" };
            var result = await _controller.CreateUser(user);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task UpdateUser_ReturnsNoContent()
        {
            var user = new User { Name = "Old", Email = "old@example.com" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            user.Name = "Updated";
            var result = await _controller.UpdateUser(user.Id, user);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ReturnsNoContent()
        {
            var user = new User { Name = "Delete", Email = "delete@example.com" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var result = await _controller.DeleteUser(user.Id);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
