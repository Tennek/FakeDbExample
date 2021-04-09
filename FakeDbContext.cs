using System.Data.Entity;
using DynamicSQLSandbox.Model;

namespace DynamicSQLSandbox.FakeDb
{
    public class FakeDbContext : DbContext
    {
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
    }
}
