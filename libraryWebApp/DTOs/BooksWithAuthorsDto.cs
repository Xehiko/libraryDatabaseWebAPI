namespace libraryWebApp.DTOs;

public class BooksWithAuthorsDto
{
    public int BookId { get; set; }
    public int BookDepartmentId { get; set; }
    public int PublishingHouseId { get; set; }
    public string Name { get; set; } = null!;
    public int Price { get; set; }
    public DateTime ReceiptDate { get; set; }
    public int InventoryNumber { get; set; }
    public List<AuthorWithoutBooksDto> Authors { get; set; } = null!;
}