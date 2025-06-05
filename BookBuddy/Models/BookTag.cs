using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookBuddy.Models
{
    public class BookTag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<BookBookTag> BookBookTags { get; set; }
    }
}
