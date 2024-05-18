using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using libraryWebApp.libraryDbContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("libraryDatabase");
builder.Services.AddDbContext<LibraryDbContext>(options =>
{
    if (connectionString != null) options.UseMySQL(connectionString);
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.MaxDepth = 0;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); // необходимо для генерации документации
builder.Services.AddSwaggerGen(); // добавили службу, генерирующую документацию

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // включает генерацию спецификации Swagger
    app.UseSwaggerUI(); // включает пользовательский интерфейс Swagger
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();