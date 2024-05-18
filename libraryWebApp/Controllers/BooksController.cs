using libraryWebApp.DTOs;
using libraryWebApp.libraryDbContext;
using libraryWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace libraryWebApp.Controllers;

// контроллер для сущности Author
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    // контекст базы данных
    private readonly LibraryDbContext _context;

    // Конструктор для контроллера
    public BooksController(LibraryDbContext context)
    {
        _context = context;
    }
    
    // HTTP GET метод для получения всех книг
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BooksWithAuthorsDto>>> GetBooks()
    {
        var books = await _context.BookSaks
            .Include(b => b.Authors)
            .ToListAsync();

        var booksWithAuthorsDto = books.Select(book => new BooksWithAuthorsDto()
        {
            BookId = book.BookId,
            BookDepartmentId = book.BookDepartmentId,
            PublishingHouseId = book.PublishingHouseId,
            Name = book.Name,
            Price = book.Price,
            ReceiptDate = book.ReceiptDate,
            InventoryNumber = book.InventoryNumber,
            Authors = book.Authors.Select(author => new AuthorWithoutBooksDto()
            {
                AuthorId = author.AuthorId,
                FirstName = author.FirstName,
                LastName = author.LastName,
                MiddleName = author.MiddleName,
                BirthDate = author.BirthDate,
                DeathDate = author.DeathDate,
                AuthorBriefBiography = author.AuthorBriefBiography
            }).ToList()
        }).ToList();

        return booksWithAuthorsDto;
    }
}