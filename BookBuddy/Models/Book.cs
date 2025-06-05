using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookBuddy.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public string Genre { get; set; }

        public List<Loan> Loans { get; set; }
        public List<BookBookTag> BookBookTags { get; set; }
    }
}
