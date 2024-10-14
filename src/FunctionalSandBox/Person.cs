namespace FunctionalSandBox;

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public List<string> MiddleNames { get; set; } = [];
    public string LastName { get; set; } = string.Empty;
}