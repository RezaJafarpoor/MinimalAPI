using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Entities;
using MinimalAPI.Persistence;
using MinimalAPI.Repositories;
using MinimalAPI.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(option =>
    option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging());
builder.Services.AddScoped<IBookRepository, BookRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapPost("books", async (Book book, IBookRepository bookRepository) =>
{
    var created = await bookRepository.CreateAsync(book);
    if (!created)
    {
        return Results.BadRequest(new
        {
            errorMessage ="A book with this isbn exist"
        });
    }

    return Results.Created($"/books/{book.Isbn}", book);

});
app.MapDelete("delete", async (string isbn, IBookRepository bookRepository) =>
{
    var created = await bookRepository.DeleteByIsbnAsync(isbn);
    if (!created)
    {
        return Results.BadRequest(new
        {
            errorMessage ="A book with this isbn exist"
        });
    }

    return Results.NoContent();


});


app.Run();


