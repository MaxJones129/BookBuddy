using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBuddy.Models
{
    public class BookBookTag
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }

        [ForeignKey("BookTag")]
        public int TagId { get; set; }

        public Book Book { get; set; }
        public BookTag BookTag { get; set; }
    }
}
