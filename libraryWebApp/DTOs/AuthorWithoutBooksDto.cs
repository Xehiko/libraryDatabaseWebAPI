namespace libraryWebApp.DTOs;

public class AuthorWithoutBooksDto
{
    public int AuthorId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public DateTime? BirthDate { get; set; }
    public DateTime? DeathDate { get; set; }
    public string? AuthorBriefBiography { get; set; }
}