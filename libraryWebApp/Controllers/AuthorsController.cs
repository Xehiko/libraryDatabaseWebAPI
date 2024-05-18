using libraryWebApp.DTOs;
using libraryWebApp.libraryDbContext;
using libraryWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace libraryWebApp.Controllers;

// контроллер для сущности Author
[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    // контекст базы данных
    private readonly LibraryDbContext _context;

    // Конструктор для контроллера
    public AuthorsController(LibraryDbContext context)
    {
        _context = context;
    }

    // HTTP GET метод для получения всех авторов
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthorSaks()
    {
        // Асинхронное получение всех авторов из базы данных и преобразование в список
        var authorSaks = await _context.AuthorSaks
            .Include(a => a.Books).Include(a => a.PublishingHouses).ToListAsync();
        // Создаем экпляр AuthorDto и запем его поля из authorSak
        var authorsDto = authorSaks.Select(authorSak => new AuthorDto
        {
            AuthorId = authorSak.AuthorId,
            FirstName = authorSak.FirstName,
            LastName = authorSak.LastName,
            MiddleName = authorSak.MiddleName,
            BirthDate = authorSak.BirthDate,
            DeathDate = authorSak.DeathDate,
            AuthorBriefBiography = authorSak.AuthorBriefBiography,
            Books = authorSak.Books.Select(book => new BookDto
            {
                BookId = book.BookId,
                BookDepartmentId = book.BookDepartmentId,
                PublishingHouseId = book.PublishingHouseId,
                Name = book.Name,
                Price = book.Price,
                ReceiptDate = book.ReceiptDate,
                InventoryNumber = book.InventoryNumber
            }).ToList(),
            PublishingHouses = authorSak.PublishingHouses.Select(house => new PublishingHouseDto
            {
                PublishingHouseId = house.PublishingHouseId,
                Name = house.Name,
                HousePhoneNumber = house.HousePhoneNumber,
                Address = house.Address
            }).ToList()
        }).ToList();

        return authorsDto; // преобразовывается в тип по умолчанию - JsonResult
    }

    // HTTP GET метод для получения конкретного автора по id
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AuthorDto>> GetAuthorSak(int id)
    {
        // поиск автора по id
        var authorSak = await _context.AuthorSaks
            .Include(a => a.Books)
            .Include(a => a.PublishingHouses)
            .FirstOrDefaultAsync(a => a.AuthorId == id);

        // Если автор не найден, возвращаем статус 404 Not Found
        if (authorSak == null) return NotFound();

        // Создаем экпляр AuthorDto и запем его поля из authorSak
        var authorDto = new AuthorDto
        {
            AuthorId = authorSak.AuthorId,
            FirstName = authorSak.FirstName,
            LastName = authorSak.LastName,
            MiddleName = authorSak.MiddleName,
            BirthDate = authorSak.BirthDate,
            DeathDate = authorSak.DeathDate,
            AuthorBriefBiography = authorSak.AuthorBriefBiography,
            Books = authorSak.Books.Select(book => new BookDto
            {
                BookId = book.BookId,
                BookDepartmentId = book.BookDepartmentId,
                PublishingHouseId = book.PublishingHouseId,
                Name = book.Name,
                Price = book.Price,
                ReceiptDate = book.ReceiptDate,
                InventoryNumber = book.InventoryNumber
            }).ToList(),
            PublishingHouses = authorSak.PublishingHouses.Select(house => new PublishingHouseDto
            {
                PublishingHouseId = house.PublishingHouseId,
                Name = house.Name,
                HousePhoneNumber = house.HousePhoneNumber,
                Address = house.Address
            }).ToList()
        };

        // Возвращаем найденного автора
        return authorDto; // преобразовывается в тип по умолчанию - JsonResult
    }

    // HTTP PUT метод для обновления конкретного автора
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAuthorSak(int id, AuthorSak authorSak)
    {
        // Если id в URL не совпадает с id автора, возвращаем статус 400 Bad Request
        if (id != authorSak.AuthorId) return BadRequest();

        // Помечаем автора как измененного
        _context.Entry(authorSak).State = EntityState.Modified;

        try
        {
            // Асинхронное сохранение изменений в базе данных
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) // обработка параллельного доступа к данным
        {
            // Если автор не существует, возвращаем статус 404 Not Found
            if (!AuthorSakExists(id))
                return NotFound();
            throw;
        }

        // Возвращаем статус 204 No Content
        return NoContent();
    }

    // HTTP POST метод для добавления автора
    [HttpPost]
    public async Task<ActionResult<AuthorDto>> PostAuthorSak(AuthorSak authorSak)
    {
        // Добавляем нового автора в контекст
        _context.AuthorSaks.Add(authorSak);
        // Асинхронное сохранение изменений в базе данных
        await _context.SaveChangesAsync();

        // Создаем экпляр AuthorDto и запем его поля из authorSak
        var authorDto = new AuthorDto
        {
            AuthorId = authorSak.AuthorId,
            FirstName = authorSak.FirstName,
            LastName = authorSak.LastName,
            MiddleName = authorSak.MiddleName,
            BirthDate = authorSak.BirthDate,
            DeathDate = authorSak.DeathDate,
            AuthorBriefBiography = authorSak.AuthorBriefBiography,
            Books = authorSak.Books.Select(book => new BookDto
            {
                BookId = book.BookId,
                BookDepartmentId = book.BookDepartmentId,
                PublishingHouseId = book.PublishingHouseId,
                Name = book.Name,
                Price = book.Price,
                ReceiptDate = book.ReceiptDate,
                InventoryNumber = book.InventoryNumber
            }).ToList(),
            PublishingHouses = authorSak.PublishingHouses.Select(house => new PublishingHouseDto
            {
                PublishingHouseId = house.PublishingHouseId,
                Name = house.Name,
                HousePhoneNumber = house.HousePhoneNumber,
                Address = house.Address
            }).ToList()
        };
        
        // Возвращаем статус 201 Created с ссылкой на добавленного автора
        return CreatedAtAction("GetAuthorSak", new {id = authorSak.AuthorId}, authorDto);
    }

    // HTTP DELETE метод для удаления конкретного автора
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<AuthorDto>> DeleteAuthorSak(int id)
    {
        // Асинхронный поиск автора с указанным id
        var authorSak = await _context.AuthorSaks.FindAsync(id);
        // Если автор не существует, возвращаем статус 404 Not Found
        if (authorSak == null) return NotFound();

        // Удаляем автора из контекста
        _context.AuthorSaks.Remove(authorSak);
        // Асинхронное сохранение изменений в базе данных
        await _context.SaveChangesAsync();

        // Создаем экпляр AuthorDto и запем его поля из authorSak
        var authorDto = new AuthorDto
        {
            AuthorId = authorSak.AuthorId,
            FirstName = authorSak.FirstName,
            LastName = authorSak.LastName,
            MiddleName = authorSak.MiddleName,
            BirthDate = authorSak.BirthDate,
            DeathDate = authorSak.DeathDate,
            AuthorBriefBiography = authorSak.AuthorBriefBiography,
            Books = authorSak.Books.Select(book => new BookDto
            {
                BookId = book.BookId,
                BookDepartmentId = book.BookDepartmentId,
                PublishingHouseId = book.PublishingHouseId,
                Name = book.Name,
                Price = book.Price,
                ReceiptDate = book.ReceiptDate,
                InventoryNumber = book.InventoryNumber
            }).ToList(),
            PublishingHouses = authorSak.PublishingHouses.Select(house => new PublishingHouseDto
            {
                PublishingHouseId = house.PublishingHouseId,
                Name = house.Name,
                HousePhoneNumber = house.HousePhoneNumber,
                Address = house.Address
            }).ToList()
        };
        
        // Возвращаем удаленного автора
        return authorDto;
    }

    // вспомогательный метод для проверки существования автора
    private bool AuthorSakExists(int id)
    {
        // Проверяем, есть ли в контексте авторы с указанным id
        return _context.AuthorSaks.Any(a => a.AuthorId == id);
    }
}