using System.ComponentModel.DataAnnotations;

namespace LibrarySystemAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public List<Loan> Loans { get; set; } = new();
    }
}
