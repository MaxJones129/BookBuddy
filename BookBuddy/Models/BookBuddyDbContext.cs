using Microsoft.EntityFrameworkCore;
using BookBuddy.Models;


namespace BookBuddy.Data
{
    public class BookBuddyDb : DbContext
    {
        public BookBuddyDb(DbContextOptions<BookBuddyDb> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<BookTag> BookTags { get; set; }
        public DbSet<BookBookTag> BookBookTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookBookTag>()
                .HasOne(bbt => bbt.Book)
                .WithMany(b => b.BookBookTags)
                .HasForeignKey(bbt => bbt.BookId);

            modelBuilder.Entity<BookBookTag>()
                .HasOne(bbt => bbt.BookTag)
                .WithMany(bt => bt.BookBookTags)
                .HasForeignKey(bbt => bbt.TagId);
        }
    }
}
