using Wblack;
using WorkApplications.Models;

namespace WorkApplications.Migrations.Wblack
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class WblackConfiguration : DbMigrationsConfiguration<WblackDataContext>
    {
        public WblackConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WblackDataContext context)
        {
            context.AccountTypes.AddOrUpdate(
                p => p.Type,
                new AccountType {Type = "Checking", DateCreated = new DateTime(2014, 4, 9, 10, 19, 49, 827), DateModified = new DateTime(2014, 4, 9, 10, 19, 49, 827)},
                new AccountType {Type = "Savings", DateCreated = new DateTime(2014, 4, 9, 10, 19, 49, 827), DateModified = new DateTime(2014, 4, 9, 10, 19, 49, 827)});
            context.Categories.AddOrUpdate(
                p => p.Description,
                new Category {Description = "No Category", DateCreated = new DateTime(2014, 4, 11, 13, 11, 00, 000), DateModified = new DateTime(2014, 4, 11, 13, 11, 00, 000)});
            context.Entities.AddOrUpdate(
                p => p.Description,
                new Entity {Description = "N/A", DateCreated = new DateTime(2014, 4, 9, 11, 22, 00, 000), DateModified = new DateTime(2014, 4, 9, 11, 22, 00, 000)});
            context.Tenders.AddOrUpdate(
                p => p.Description,
                new Tender {Description = "Credit/Debit", DateCreated = new DateTime(2014, 6, 23, 21, 8, 00, 000), DateModified = new DateTime(2014, 6, 23, 21, 8, 00, 000)},
                new Tender {Description = "Cash", DateCreated = new DateTime(2014, 6, 23, 21, 8, 00, 000), DateModified = new DateTime(2014, 6, 23, 21, 8, 00, 000)},
                new Tender {Description = "Check", DateCreated = new DateTime(2014, 6, 23, 21, 8, 00, 000), DateModified = new DateTime(2014, 6, 23, 21, 8, 00, 000)});
            context.TransactionTypes.AddOrUpdate(
                p => p.Description,
                new TransactionType { Description = "Deposit", DateCreated = new DateTime(2014, 4, 11, 14, 23, 00, 000), DateModified = new DateTime(2014, 4, 11, 14, 23, 00, 000) },
                new TransactionType { Description = "Withdrawal", DateCreated = new DateTime(2014, 4, 11, 14, 23, 00, 000), DateModified = new DateTime(2014, 4, 11, 14, 23, 00, 000) },
                new TransactionType { Description = "Transfer", DateCreated = new DateTime(2014, 4, 11, 14, 23, 00, 000), DateModified = new DateTime(2014, 4, 11, 14, 23, 00, 000) });
        }
    }
}
