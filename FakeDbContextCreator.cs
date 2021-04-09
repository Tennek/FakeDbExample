using System.Collections.Generic;
using System.Linq;
using DynamicSQLSandbox.Model;
using System.Data.Entity;
using Moq;

namespace DynamicSQLSandbox.FakeDb
{
    public static class FakeDbContextCreator
    {
        public static FakeDbContext CreateFakeDbContext()
        {
            var mockDbContext = new Mock<FakeDbContext>();


            var authors = GenerateAuthors();
            var books = GenerateBooksForAuthors(authors);

            mockDbContext.Setup(x => x.Authors).Returns(GenerateAuthorDbSet(authors));
            mockDbContext.Setup(x => x.Books).Returns(GenerateBookDbSet(books));

            return mockDbContext.Object;
        }

        private static DbSet<Author> GenerateAuthorDbSet(List<Author> authors)
        {
            var data = authors.AsQueryable();

            var mockDbSet = new Mock<DbSet<Author>>();
            mockDbSet.As<IQueryable<Author>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Author>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Author>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Author>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockDbSet.Object;
        }

        private static DbSet<Book> GenerateBookDbSet(List<Book> books)
        {
            var data = books.AsQueryable();

            var mockDbSet = new Mock<DbSet<Book>>();
            mockDbSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockDbSet.Object;
        }

        private static List<Author> GenerateAuthors()
        {
            return new List<Author>()
            {
                new Author() {Id = 1, FirstName = "Kenneth", LastName = "Van Eetvelde", Age = 30},
                new Author() {Id = 2, FirstName = "Kevin", LastName = "Van Eetvelde", Age = 25},
                new Author() {Id = 3, FirstName = "Rudy", LastName = "De Bakker", Age = 55},
                new Author() {Id = 4, FirstName = "Rudy", LastName = "VanLee", Age = 43},
                new Author() {Id = 5, FirstName = "Mariah", LastName = "Van Schlagers", Age = 78},
            };
        }

        private static List<Book> GenerateBooksForAuthors(List<Author> authors)
        {
            var bookList = new List<Book>();
            foreach (var auth in authors)
            {
                auth.Books = new List<Book>()
                {
                    new Book() {Id = auth.Id + 10, Title = "Book " + auth.Id + 10, Author = auth, AuthorId = auth.Id, NumberOfPages = auth.Id + 10},
                    new Book() {Id = auth.Id + 20, Title = "Book " + auth.Id + 20, Author = auth, AuthorId = auth.Id, NumberOfPages = auth.Id + 20},
                    new Book() {Id = auth.Id + 30, Title = "Book " + auth.Id + 30, Author = auth, AuthorId = auth.Id, NumberOfPages = auth.Id + 30},
                };
                bookList.AddRange(auth.Books);
            }
            return bookList;
        }
    }
}
