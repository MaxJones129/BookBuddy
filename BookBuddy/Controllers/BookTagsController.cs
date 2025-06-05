using Microsoft.AspNetCore.Mvc;
using BookBuddy.Models;
using BookBuddy.Data;
using Microsoft.EntityFrameworkCore;

namespace BookBuddy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookTagsController : ControllerBase
    {
        private readonly BookBuddyDb _context;

        public BookTagsController(BookBuddyDb context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<BookTag>> CreateBookTag(BookTag tag)
        {
            _context.BookTags.Add(tag);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBookTagById), new { id = tag.Id }, tag);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookTag>>> GetAllBookTags()
        {
            return await _context.BookTags.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookTag>> GetBookTagById(int id)
        {
            var tag = await _context.BookTags.FindAsync(id);
            return tag == null ? NotFound() : Ok(tag);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookTag(int id, BookTag updatedTag)
        {
            if (id != updatedTag.Id) return BadRequest();
            _context.Entry(updatedTag).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookTag(int id)
        {
            var tag = await _context.BookTags.FindAsync(id);
            if (tag == null) return NotFound();
            _context.BookTags.Remove(tag);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
