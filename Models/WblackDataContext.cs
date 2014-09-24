using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Wblack;

namespace WorkApplications.Models
{
    public class WblackDataContext : DbContext, IWblackDataContext
    {
        private readonly DbInteractor _dbInteractor;
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<BankStatementAccount> BankStatementAccounts { get; set; }
        public DbSet<BankStatement> BankStatements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<GasReceipt> GasReceipts { get; set; }
        public DbSet<SleepTime> SleepTimes { get; set; }
        public DbSet<Tender> Tenders { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }

        public WblackDataContext()
            : base("WblackDataContext")
        {
            _dbInteractor = new DbInteractor(this);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<GasReceipt>().Property(x => x.Price).HasPrecision(5, 2);
            modelBuilder.Entity<GasReceipt>().Property(x => x.Gallons).HasPrecision(7, 3);
            modelBuilder.Entity<GasReceipt>().Property(x => x.Miles).HasPrecision(5, 1);
            modelBuilder.Entity<GasReceipt>().Property(x => x.TankStart).HasPrecision(3, 2);
            modelBuilder.Entity<GasReceipt>().Property(x => x.TankEnd).HasPrecision(3, 2);
            modelBuilder.Entity<Transaction>().Property(x => x.TransactionAmt).HasPrecision(10, 2);
            modelBuilder.Entity<Transaction>().Property(x => x.CashBack).HasPrecision(10, 2);
            modelBuilder.Entity<Transaction>().Property(x => x.BankRecordedAmt).HasPrecision(10, 2);
        }

        IQueryable<Account> IWblackDataContext.Accounts
        {
            get { return Accounts; }
        }

        IQueryable<AccountType> IWblackDataContext.AccountTypes
        {
            get { return AccountTypes; }
        }

        IQueryable<BankStatementAccount> IWblackDataContext.BankStatementAccounts
        {
            get { return BankStatementAccounts; }
        }

        IQueryable<BankStatement> IWblackDataContext.BankStatements
        {
            get { return BankStatements; }
        }

        IQueryable<Category> IWblackDataContext.Categories
        {
            get { return Categories; }
        }

        IQueryable<Entity> IWblackDataContext.Entities
        {
            get { return Entities; }
        }

        IQueryable<GasReceipt> IWblackDataContext.GasReceipts
        {
            get { return GasReceipts; }
        }

        IQueryable<SleepTime> IWblackDataContext.SleepTimes
        {
            get { return SleepTimes; }
        }

        IQueryable<Tender> IWblackDataContext.Tenders
        {
            get { return Tenders; }
        }

        IQueryable<Transaction> IWblackDataContext.Transactions
        {
            get { return Transactions; }
        }

        IQueryable<TransactionType> IWblackDataContext.TransactionTypes
        {
            get { return TransactionTypes; }
        }

        public int InsertTransaction(Transaction newTransaction)
        {
            return _dbInteractor.RunStoredProcedure("Transaction_Insert",newTransaction);
        }

        public int PostTransaction(Transaction transRec)
        {
            return _dbInteractor.RunStoredProcedure("Transaction_Post", transRec);
        }
    }
}