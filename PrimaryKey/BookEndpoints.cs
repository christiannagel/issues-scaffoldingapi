using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace PrimaryKey;

public static class BookEndpoints
{
    public static void MapBookEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Book").WithTags(nameof(Book));

        group.MapGet("/", async (BooksContext db) =>
        {
            return await db.Book.ToListAsync();
        })
        .WithName("GetAllBooks")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Book>, NotFound>> (int bookid, BooksContext db) =>
        {
            return await db.Book.AsNoTracking()
                .FirstOrDefaultAsync(model => model.BookId == bookid)
                is Book model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetBookById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int bookid, Book book, BooksContext db) =>
        {
            var affected = await db.Book
                .Where(model => model.BookId == bookid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.BookId, book.BookId)
                    .SetProperty(m => m.Title, book.Title)
                    .SetProperty(m => m.Publisher, book.Publisher)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateBook")
        .WithOpenApi();

        group.MapPost("/", async (Book book, BooksContext db) =>
        {
            db.Book.Add(book);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Book/{book.BookId}",book);
        })
        .WithName("CreateBook")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int bookid, BooksContext db) =>
        {
            var affected = await db.Book
                .Where(model => model.BookId == bookid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteBook")
        .WithOpenApi();
    }
}
