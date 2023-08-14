using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO;

public class RegisterModel
{
    [Required]
    [StringLength(25, MinimumLength = 3)]
    public string Username { get; set; } = null!;
    
    [Required]
    [StringLength(30, MinimumLength = 5)]
    [EmailAddress]
    public string Email { get; set; } = null!;
    
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; } = null!;
    
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Surname { get; set; } = null!;
    
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string? Patronymic { get; set; }
    
    [Required]
    [StringLength(50, MinimumLength = 8)]
    [PasswordPropertyText]
    public string Password { get; set; } = null!;
}