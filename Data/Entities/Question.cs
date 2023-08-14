namespace Data.Entities;

public class Question
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public string Answer { get; set; } = null!;
}