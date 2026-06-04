using System.ComponentModel.DataAnnotations;

namespace LibrarySystemAPI.DTOs.books;

public class CreateBookDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;
}
