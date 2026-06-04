using System.ComponentModel.DataAnnotations;

namespace LibrarySystemAPI.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        public bool IsLoaned { get; set; }
        public List<Loan> Loans { get; set; } = new();
    }
}
