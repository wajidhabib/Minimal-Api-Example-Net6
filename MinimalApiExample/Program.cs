using Microsoft.EntityFrameworkCore;
using MinimalApiExample;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<dbBook>(option => option.UseInMemoryDatabase("dbMinApi"));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");


app.MapPost("/books", async (Book book, dbBook dbcontext) =>
{

    dbcontext.Books.Add(book);
    await dbcontext.SaveChangesAsync();
    return Results.Created($"/books/{book.Id}", book);


});

app.MapGet("/books", async (dbBook dbcontext) =>
{

    return await dbcontext.Books.ToListAsync();


});
app.MapGet("/books/{id}", async (int id, dbBook dbcontext) =>

 {
     if (await dbcontext.Books.FindAsync(id) is Book book)
     {

         return Results.Ok(book);
     }


     return Results.NotFound(new { message = "No book found." });
 }




);
app.MapDelete("/books/{id}", async (int id, dbBook dbcontext) =>
{

    if (await dbcontext.Books.FindAsync(id) is Book book)
    {

        dbcontext.Books.Remove(book);
        await dbcontext.SaveChangesAsync();
        return Results.Ok(book);
    }


    return Results.NotFound(new { message = $"Delete faild: No book with id {id} found." });



});

app.Run();
