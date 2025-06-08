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
void QueryAggregate()
{
    //var author = _context.Authors.FirstOrDefault(a => a.LastName == "Lerman");
    var author = _context.Authors.OrderByDescending(a => a.FirstName).FirstOrDefault(a => a.LastName == "Lerman");

    void GetAuthors()
    {
        using var context = new PubContext();

        var authors = context.Authors.ToList();
        foreach (var author in authors)
        {
            Console.WriteLine(author.FirstName + " " + author.LastName);
        }

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

//MODULO 4
//InsertAuthor();
void InsertAuthor()
{
    var author = new Author
    {
        FirstName = "Frank",
        LastName = "Herbert"
    };   
    _context.Authors.Add(author);
    _context.SaveChanges();
}

//RetrieveAndUpdateAuthor();
void RetrieveAndUpdateAuthor()
{
    var author = _context.Authors
        .FirstOrDefault(a => a.FirstName == "Julie" && a.LastName == "Lerman");
    if (author != null)
    {
        author.FirstName = "Lherman";
        _context.SaveChanges();
    }
}

//RetrieveAndUpdateMultipleAuthors();
void RetrieveAndUpdateMultipleAuthors()
{
    var LermanAuthors = _context.Authors
        .Where(a => a.FirstName == "Lherman")
        .ToList();
    foreach (var la in LermanAuthors)
    {
        la.FirstName = "Julie";
    }
    Console.WriteLine($"Before: \r\n{_context.ChangeTracker.DebugView.ShortView}");
    _context.ChangeTracker.DetectChanges();
    Console.WriteLine($"After: \r\n{_context.ChangeTracker.DebugView.ShortView}");
    _context.SaveChanges();
}

//VariousOperations();
void VariousOperations()
{
    var author = _context.Authors.Find(1);
    author.FirstName = "Diana Ines";
    var newAuthor = new Author { LastName = "Appleman", FirstName = "Dan" };
    _context.Authors.Add(newAuthor);
    _context.SaveChanges();
}

//CoordinatedRetrieveAndUpdateAuthor();
void CoordinatedRetrieveAndUpdateAuthor()
{
    var author = FindThatAuthor(3);
    if(author?.FirstName == "Julie")
    {
        author.FirstName = "Julia";
        SaveThatAuthor(author);
    }
}
Author FindThatAuthor(int authorId)
{
    using var shortLivedContext = new PubContext();
    return shortLivedContext.Authors.Find(authorId);
}
void SaveThatAuthor(Author author)
{
    using var anotherShortLivedContext = new PubContext();
    anotherShortLivedContext.Authors.Update(author);
    anotherShortLivedContext.SaveChanges();
}

//DeleteAnAuthor();
void DeleteAnAuthor()
{
    var author = _context.Authors.Find(5);
    if (author != null)
    {
        _context.Authors.Remove(author);
        _context.SaveChanges();
    }
}

//InsertMultpleAuthorsBetter();
void InsertMultpleAuthorsBetter()
{
    var authors = new List<Author>
    {
        new Author { FirstName = "Frank", LastName = "Herbert" },
        new Author { FirstName = "Isaac", LastName = "Asimov" },
        new Author { FirstName = "Arthur C.", LastName = "Clarke" },
        new Author { FirstName = "Ray", LastName = "Bradbury" },
        new Author { FirstName = "Ruth", LastName = "Ozeki" },
        new Author { FirstName = "Sofia", LastName = "Segovia" },
        new Author { FirstName = "Ursula K.", LastName = "LeGuin" },
        new Author { FirstName = "Hugh", LastName = "Howey" },
        new Author { FirstName = "Isabelle", LastName = "Allende" }
    };
    _context.Authors.AddRange(authors);
    _context.SaveChanges();
}
void InsertMultipleAuthors()
{
    _context.Authors.AddRange(
        new Author { FirstName = "Frank", LastName = "Herbert" },
        new Author { FirstName = "Isaac", LastName = "Asimov" },
        new Author { FirstName = "Arthur C.", LastName = "Clarke" },
        new Author { FirstName = "Ray", LastName = "Bradbury" },
        new Author { FirstName = "Ruth", LastName = "Ozeki" },
        new Author { FirstName = "Sofia", LastName = "Segovia" },
        new Author { FirstName = "Ursula K.", LastName = "LeGuin" },
        new Author { FirstName = "Hugh", LastName = "Howey" },
        new Author { FirstName = "Isabelle", LastName = "Allende" }
    );
    _context.SaveChanges();
}
void InsertMultipleAuthorsPassedIn(List<Author> listOfAuthors)
{
    _context.Authors.AddRange(listOfAuthors);
    _context.SaveChanges();
}

//ExecuteDelete();
void ExecuteDelete()
{
    var deletedId = 11;
    _context.Authors.Where(a => a.Id == deletedId)
        .ExecuteDelete();
}

//ExecuteDeleteWithH();
void ExecuteDeleteWithH()
{
    var count = _context.Authors.Where(a => a.LastName.StartsWith("H"))
        .ExecuteDelete();
}

//ExecuteUpdate();
void ExecuteUpdate()
{
    var tenYearsAgo = DateOnly.FromDateTime(DateTime.Now.AddYears(-10));
    //change price of book older than 10 years to $1.50
    var oldBookPrice = 1.50m;
    _context.Books
        .Where(b => b.PublishDate < tenYearsAgo)
        .ExecuteUpdate(setters => setters.SetProperty(b => b.BasePrice, oldBookPrice));

    //change all last names to lower case
    _context.Authors
        .ExecuteUpdate(setters => setters.SetProperty(a => a.LastName, a => a.LastName.ToLower()));
    //change all last names back to title case
    _context.Authors
        .ExecuteUpdate(setters => setters.SetProperty(
            a => a.LastName, 
            a => a.LastName.Substring(0,1).ToUpper() + a.LastName.Substring(1).ToLower()));
}
