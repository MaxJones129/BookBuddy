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

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Name = "Alice Reader",
                Email = "alice@example.com"
            });

            modelBuilder.Entity<Book>().HasData(new Book
            {
                Id = -1,
                Title = "The Adventure Begins",
                Author = "John Storyteller",
                Genre = "Adventure"
            });

            modelBuilder.Entity<BookTag>().HasData(new BookTag
            {
                Id = -1,
                Name = "Fantasy"
            });

            modelBuilder.Entity<Loan>().HasData(new Loan
            {
                Id = 1,
                UserId = 1,
                BookId = -1,
                Status = "borrowed",
                DateBorrowed = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });

            modelBuilder.Entity<BookBookTag>().HasData(new BookBookTag
            {
                Id = -1, 
                BookId = -1,
                TagId = -1
            });
        }
    }
}
