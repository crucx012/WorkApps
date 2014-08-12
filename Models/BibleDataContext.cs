using Bible;
using System.Data.Entity;
using System.Linq;

namespace WorkApplications.Models
{
    public class BibleDataContext : DbContext, IBibleDataContext
    {
        public DbSet<Translation> Translations { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Verse> Verses { get; set; }

        public BibleDataContext()
            : base("BibleDataContext")
        {
        }

        IQueryable<Translation> IBibleDataContext.Translations
        {
            get { return Translations; }
        }

        IQueryable<Book> IBibleDataContext.Books
        {
            get { return Books; }
        }

        IQueryable<Verse> IBibleDataContext.Verses
        {
            get { return Verses; }
        }
    }
}