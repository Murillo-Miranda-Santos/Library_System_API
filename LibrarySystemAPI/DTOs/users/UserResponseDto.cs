using System.ComponentModel.DataAnnotations;

namespace LibrarySystemAPI.DTOs.users;

public class UserResponseDto
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}
