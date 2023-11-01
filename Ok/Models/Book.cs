namespace Scaffolding1.Models;

public class Book
{
    public Book(int id, string title, string? publisher = default) => 
        (Id, Title, Publisher) = (id, title, publisher);

    public int Id { get; set; }
    public string Title { get; set; }
    public string? Publisher { get; set; }
}
