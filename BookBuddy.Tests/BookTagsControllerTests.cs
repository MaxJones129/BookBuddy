using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookBuddy.Controllers;
using BookBuddy.Data;
using BookBuddy.Models;
using System.Threading.Tasks;

namespace BookBuddy.Tests
{
    public class BookTagsControllerTests
    {
        private readonly BookTagsController _controller;
        private readonly BookBuddyDb _context;

        public BookTagsControllerTests()
        {
            var options = new DbContextOptionsBuilder<BookBuddyDb>()
                .UseInMemoryDatabase(databaseName: "TagsTestDb")
                .Options;

            _context = new BookBuddyDb(options);
            _controller = new BookTagsController(_context);
        }

        [Fact]
        public async Task CreateBookTag_ReturnsCreatedTag()
        {
            var tag = new BookTag
            {
                Name = "Bestseller",
                BookBookTags = new List<BookBookTag>()
            };

            var result = await _controller.CreateBookTag(tag);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task UpdateBookTag_ReturnsNoContent()
        {
            var tag = new BookTag
            {
                Name = "Bestseller",
                BookBookTags = new List<BookBookTag>()
            };

            _context.BookTags.Add(tag);
            await _context.SaveChangesAsync();

            tag.Name = "Updated";
            var result = await _controller.UpdateBookTag(tag.Id, tag);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetBookTagById_ReturnsTag()
        {
            var tag = new BookTag { Name = "Adventure" };
            _context.BookTags.Add(tag);
            await _context.SaveChangesAsync();

            var result = await _controller.GetBookTagById(tag.Id);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnTag = Assert.IsType<BookTag>(okResult.Value);

            Assert.Equal(tag.Id, returnTag.Id);
        }

        [Fact]
        public async Task DeleteBookTag_ReturnsNoContent()
        {
            var tag = new BookTag { Name = "Delete" };
            _context.BookTags.Add(tag);
            await _context.SaveChangesAsync();

            var result = await _controller.DeleteBookTag(tag.Id);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
