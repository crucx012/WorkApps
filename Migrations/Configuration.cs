using Bible;
using WorkApplications.Models;

namespace WorkApplications.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BibleDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(BibleDataContext context)
        {
            context.Translations.AddOrUpdate(
                p => p.Name,
                new Translation { Name = "ASV", FullName = "American Standard Version"},
                new Translation { Name = "ESV", FullName = "English Standard Version" },
                new Translation { Name = "KJV", FullName = "King James Version" },
                new Translation { Name = "NASB", FullName = "New American Standard Bible" },
                new Translation { Name = "NIV", FullName = "New International Version" },
                new Translation { Name = "NKJV", FullName = "New King James Version" },
                new Translation { Name = "NLT", FullName = "New Living Translation" }
                );
            context.Books.AddOrUpdate(
                p => p.Name,
                new Book { Name = "Genesis", Chapters = 50 },
                new Book { Name = "Exodus", Chapters = 40 },
                new Book { Name = "Leviticus", Chapters = 27 },
                new Book { Name = "Numbers", Chapters = 36 },
                new Book { Name = "Deuteronomy", Chapters = 34 },
                new Book { Name = "Joshua", Chapters = 24 },
                new Book { Name = "Judges", Chapters = 21 },
                new Book { Name = "Ruth", Chapters = 4 },
                new Book { Name = "1st Samuel", Chapters = 31 },
                new Book { Name = "2nd Samuel", Chapters = 24 },
                new Book { Name = "1st Kings", Chapters = 22 },
                new Book { Name = "2nd Kings", Chapters = 25 },
                new Book { Name = "1st Chronicles", Chapters = 29 },
                new Book { Name = "2nd Chronicles", Chapters = 36 },
                new Book { Name = "Ezra", Chapters = 10 },
                new Book { Name = "Nehemiah", Chapters = 13 },
                new Book { Name = "Esther", Chapters = 10 },
                new Book { Name = "Job", Chapters = 42 },
                new Book { Name = "Psalms", Chapters = 150 },
                new Book { Name = "Proverbs", Chapters = 31 },
                new Book { Name = "Ecclesiastes", Chapters = 12 },
                new Book { Name = "Song of Solomon", Chapters = 8 },
                new Book { Name = "Isaiah", Chapters = 66 },
                new Book { Name = "Jeremiah", Chapters = 52 },
                new Book { Name = "Lamentations", Chapters = 5 },
                new Book { Name = "Ezekiel", Chapters = 48 },
                new Book { Name = "Daniel", Chapters = 12 },
                new Book { Name = "Hosea", Chapters = 14 },
                new Book { Name = "Joel", Chapters = 3 },
                new Book { Name = "Amos", Chapters = 9 },
                new Book { Name = "Obadiah", Chapters = 1 },
                new Book { Name = "Jonah", Chapters = 4 },
                new Book { Name = "Micah", Chapters = 7 },
                new Book { Name = "Nahum", Chapters = 3 },
                new Book { Name = "Habakkuk", Chapters = 3 },
                new Book { Name = "Zephaniah", Chapters = 3 },
                new Book { Name = "Haggai", Chapters = 2 },
                new Book { Name = "Zechariah", Chapters = 14 },
                new Book { Name = "Malachi", Chapters = 4 },
                new Book { Name = "Matthew", Chapters = 28 },
                new Book { Name = "Mark", Chapters = 16 },
                new Book { Name = "Luke", Chapters = 24 },
                new Book { Name = "John", Chapters = 21 },
                new Book { Name = "Acts", Chapters = 28 },
                new Book { Name = "Romans", Chapters = 16 },
                new Book { Name = "1st Corinthians", Chapters = 16 },
                new Book { Name = "2nd Corinthians", Chapters = 13 },
                new Book { Name = "Galations", Chapters = 6 },
                new Book { Name = "Ephesians", Chapters = 6 },
                new Book { Name = "Philippians", Chapters = 4 },
                new Book { Name = "Colossians", Chapters = 4 },
                new Book { Name = "1st Thessalonians", Chapters = 5 },
                new Book { Name = "2nd Thessalonians", Chapters = 3 },
                new Book { Name = "1st Timothy", Chapters = 6 },
                new Book { Name = "2nd Timothy", Chapters = 4 },
                new Book { Name = "Titus", Chapters = 3 },
                new Book { Name = "Philemon", Chapters = 1 },
                new Book { Name = "Hebrews", Chapters = 13 },
                new Book { Name = "James", Chapters = 5 },
                new Book { Name = "1st Peter", Chapters = 5 },
                new Book { Name = "2nd Peter", Chapters = 3 },
                new Book { Name = "1st John", Chapters = 5 },
                new Book { Name = "2nd John", Chapters = 1 },
                new Book { Name = "3rd John", Chapters = 1 },
                new Book { Name = "Jude", Chapters = 1 },
                new Book { Name = "Revelation", Chapters = 22 }
            );
        }
    }
}
