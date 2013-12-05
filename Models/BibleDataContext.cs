using Bible;
using System.Data.Entity;
using System.Linq;

namespace WorkApplications.Models
{
    public class BibleDataContext : DbContext, IBibleDataSource
    {
        public DbSet<Translation> Translations { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Verse> Verses { get; set; }

        public BibleDataContext() : base("DefaultConnection")
        {
        }

        IQueryable<Translation> IBibleDataSource.Translations
        {
            get { return Translations; }
        }

        IQueryable<Book> IBibleDataSource.Books
        {
            get { return Books; }
        }

            IQueryable<Verse> IBibleDataSource.Verses
        {
            get { return Verses; }
        }
    }
}