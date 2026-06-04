using System.ComponentModel.DataAnnotations;

namespace LibrarySystemAPI.DTOs.users;

public class CreateUserDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}
