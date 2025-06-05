using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookBuddy.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public List<Loan> Loans { get; set; }
    }
}
