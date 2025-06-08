using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PublisherData;
using PublisherDomain;


//using (PubContext context = new PubContext())
//{
//    context.Database.EnsureCreated();
//}

//GetAuthors();
//AddAuthor();
//GetAuthors();

//AddAuthorWithBook();
//GetAuthorsWithBooks();

using PubContext _context = new();

//QueryFilters();
//AddSomeMoreAuthors();
//SkipAndTakeAuthors();
//SortAuthors();
QueryAggregate();
void QueryAggregate()
{
    //var author = _context.Authors.FirstOrDefault(a => a.LastName == "Lerman");
    var author = _context.Authors.OrderByDescending(a => a.FirstName).FirstOrDefault(a =>a.LastName == "Lerman");

    void GetAuthors()
{
    using var context = new PubContext();

    var authors = context.Authors.ToList();
    foreach (var author in authors)
    {
        Console.WriteLine(author.FirstName + " " + author.LastName);
    }

}

void SortAuthors()
{
    var authorsByLastname = _context.Authors
     .OrderBy(a => a.LastName)
     .ThenBy(a => a.FirstName)
     .ToList();

    authorsByLastname.ForEach(a => Console.WriteLine(a.LastName + " " + a.FirstName));
}
void SkipAndTakeAuthors()
{
    var groupSize = 2;
    for (int i = 0; i < 5; i++)
    {
        var authors = _context.Authors
            .Skip(i * groupSize)
            .Take(groupSize)
            .ToList();
        Console.WriteLine($"Group {i}:");
        foreach (var author in authors)
        {
            Console.WriteLine(author.FirstName + " " + author.LastName);
        }
        Console.WriteLine();
    }
}

void QueryFilters()
{
    //var authors = _context.Authors.Where(a => a.FirstName == "Josie")
    //    .ToList();

    //var firstName = "Josie";
    //var authors = _context.Authors.Where(a => a.FirstName == firstName)
    //    .ToList();

    //var authors = _context.Authors.Where(a => EF.Functions.Like(a.LastName, "L%"))
    //    .ToList();

    var filter = "L%";
    var authors = _context.Authors.Where(a => EF.Functions.Like(a.LastName, filter))
        .ToList();

}

void AddAuthorWithBook()
{
    var author = new Author
    {
        FirstName = "Julie",
        LastName = "Lerman"
    };
    author.Books.Add(new Book
    {
        Title = "Programming Entity Framework",
        PublishDate = new DateOnly(2009, 1, 1)
    });
    author.Books.Add(new Book
    {
        Title = "Programming Entity Framework 2nd Ed",
        PublishDate = new DateOnly(2010, 8, 1)
    });

    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}
void GetAuthorsWithBooks()
{
    using var context = new PubContext();

    var authors = context.Authors.Include(a => a.Books)
        .ToList();

    foreach (var author in authors)
    {
        Console.WriteLine(author.FirstName + " " + author.LastName);
        foreach (var book in author.Books)
        {
            Console.WriteLine("  " + book.Title + " (" + book.PublishDate.Year + ")");
        }
    }
}
void AddAuthor()
{
    var author = new Author
    {
        FirstName = "Julie",
        LastName = "Lerman"
    };
    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}

void AddSomeMoreAuthors()
{
    _context.Authors.Add(new Author { FirstName = "Rhoda", LastName = "Lerman" });
    _context.Authors.Add(new Author { FirstName = "Don", LastName = "Jones" });
    _context.Authors.Add(new Author { FirstName = "Jim", LastName = "Christopher" });
    _context.Authors.Add(new Author { FirstName = "Stephen", LastName = "Haunts" });

    _context.SaveChanges();
}