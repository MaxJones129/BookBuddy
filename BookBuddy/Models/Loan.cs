using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBuddy.Models
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }

        public string Status { get; set; } // "borrowed", "returned", etc.

        public DateTime DateBorrowed { get; set; }
        public DateTime? DateReturned { get; set; }

        public User User { get; set; }
        public Book Book { get; set; }
    }
}
