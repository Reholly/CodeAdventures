namespace Shared.DTO;

public class UserModel
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string? Patronymic { get; set; }
}