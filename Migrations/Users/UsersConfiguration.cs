namespace WorkApplications.Migrations.Users
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class UsersConfiguration : DbMigrationsConfiguration<WorkApplications.Models.UsersDataContext>
    {
        public UsersConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WorkApplications.Models.UsersDataContext context)
        {
        }
    }
}
